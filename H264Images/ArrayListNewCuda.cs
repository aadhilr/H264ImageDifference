using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class ArrayListNewCuda
    {

        public List<short> GetDelta(Bitmap first, Bitmap second) //getting the 2 images
        {
            List<short> deltaList1 = new List<short>();
            List<short> deltaList2 = new List<short>();
            List<short> deltaList3 = new List<short>();
            List<short> deltaList4 = new List<short>();
            List<short> deltaList = new List<short>();//creating the list to insert image data

            Stopwatch stopWatch = Stopwatch.StartNew();
            CudafyModule km = CudafyTranslator.Cudafy();
            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
            gpu.LoadModule(km);

            Console.WriteLine("load Module : " + stopWatch.ElapsedMilliseconds);

            Bitmap bmp1 = first;
            Bitmap bmp2 = second;

            stopWatch.Restart();
            Rectangle area1 = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
            BitmapData bitmapData1 = bmp1.LockBits(area1, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            int stride = bitmapData1.Stride;
            IntPtr ptr1 = bitmapData1.Scan0;
            int numBytes = Math.Abs(bitmapData1.Stride) * bmp1.Height;
            byte[] rgbValues1 = new byte[numBytes];
            Marshal.Copy(ptr1, rgbValues1, 0, numBytes);

            Rectangle area2 = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
            BitmapData bitmapData2 = bmp2.LockBits(area2, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            int stride2 = bitmapData2.Stride;
            IntPtr ptr2 = bitmapData2.Scan0;
            int numBytes2 = bitmapData2.Stride * bmp2.Height;
            byte[] rgbValues2 = new byte[numBytes2];
            Marshal.Copy(ptr2, rgbValues2, 0, numBytes2);

            stopWatch.Stop();

            Console.WriteLine("copy images to byte array : " + stopWatch.ElapsedMilliseconds);

            int[] count = new int[2];
            count[0] = 0;
            count[1] = 0;

            int[] possition = new int[bmp1.Width * bmp1.Height * 2];
            byte[] results = new byte[bmp1.Width * bmp1.Height * 4];

            int[] width = new int[2];
            width[0] = bmp1.Width;
            width[1] = bmp1.Height;

            dim3 dimBlock = new dim3(16, 16);
            int yBlocks = width[0] * 3 / dimBlock.y + ((width[0] * 3 % dimBlock.y) == 0 ? 0 : 1);
            int xBlocks = width[1] / dimBlock.x + ((width[1] % dimBlock.x) == 0 ? 0 : 1);
            dim3 dimGrid = new dim3(xBlocks, yBlocks);

            stopWatch.Restart();

            int[] imageWidth = gpu.CopyToDevice<int>(width);
            int[] dev_count = gpu.CopyToDevice<int>(count);
            byte[] dev_bitmap1 = gpu.CopyToDevice<byte>(rgbValues1);
            byte[] dev_bitmap2 = gpu.CopyToDevice<byte>(rgbValues2);

            byte[] dev_result = gpu.Allocate<byte>(results);
            int[] dev_possition = gpu.CopyToDevice<int>(possition);

            stopWatch.Stop();

            Console.WriteLine("Copy to GPU : " + stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            gpu.Launch(128, 1).calGPU(dev_bitmap1, dev_bitmap2, dev_result, imageWidth, dev_count, dev_possition);

            stopWatch.Stop();
            Console.WriteLine("func : " + stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
            for (int cnt = 0; cnt < possition.Length; cnt++)
            {
                    deltaList1[6 * cnt + 0] = (short)possition[2 * cnt + 0];
                    deltaList1[6 * cnt + 1] = (short)possition[2 * cnt + 1];
                    deltaList1[6 * cnt + 2] = (short)results[4 * cnt + 2];
                    deltaList1[6 * cnt + 3] = (short)results[4 * cnt + 1];
                    deltaList1[6 * cnt + 4] = (short)results[4 * cnt + 0];
                    deltaList1[6 * cnt + 5] = (short)results[4 * cnt + 3];
                }

            //for (y = 0; y < bmp1.Size.Height; y++)
            //{
            //    for (x = 0; x < bmp1.Size.Width; x++)
            //    {
            //        Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
            //        Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
            //        if (c1 != c2) //comparing the pixels for any differences
            //        {
            //            deltaList.Add(y);
            //            deltaList.Add(x);
            //            deltaList.Add(c2.R);
            //            deltaList.Add(c2.G);
            //            deltaList.Add(c2.B);
            //            deltaList.Add(c2.A); //alpha value(0-transparent, 256-full appearence)
            //        }
            //    }
            //}
            //deltaList = deltaList1.Concat(deltaList2).Concat(deltaList3).Concat(deltaList4).ToList();
            return deltaList;
        }

        [Cudafy]
        public struct PixelData
        {
            public byte blue;
            public byte green;
            public byte red;
            public byte alpha;
        }


        [Cudafy]
        //CPU launches kernels on GPU to process the data
        public static void calGPU(GThread thread, byte[] dev_bitmap1, byte[] dev_bitmap2, byte[] dev_result, int[] imageWidth, int[] count, int[] possition)
        {

            int i = (thread.blockIdx.x * thread.blockDim.x) + thread.threadIdx.x;
            int j = (thread.blockIdx.y * thread.blockDim.y) + thread.threadIdx.y;

            int[] sharedCount = thread.AllocateShared<int>("count1", 2);
            sharedCount[0] = 0;
            sharedCount[1] = 0;
            int tid = 0;

            //if (j < imageWidth[1] && i < imageWidth[0] * 3)
            //{
            int alpha_delta, red_delta, green_delta, blue_delta;
            //for (int tid = thread.blockIdx.x * thread.blockDim.x + thread.threadIdx.x; tid < 24;tid += thread.blockDim.x * thread.gridDim.x)
            //    {
            for (i = 0; i < imageWidth[1]; i += 1)
            {
                for (j = 0; j < imageWidth[0]; j += 1)
                {
                    tid = (i * imageWidth[0] + j) * 4;
                    //while (tid < 326)
                    //{
                    PixelData pixelColor1 = new PixelData();
                    PixelData pixelColor2 = new PixelData();

                    pixelColor1.red = dev_bitmap1[tid + 2];
                    pixelColor1.green = dev_bitmap1[tid + 1];
                    pixelColor1.blue = dev_bitmap1[tid];
                    pixelColor1.alpha = dev_bitmap1[tid + 3];

                    pixelColor2.green = dev_bitmap2[tid + 1];
                    pixelColor2.red = dev_bitmap2[tid + 2];
                    pixelColor2.blue = dev_bitmap2[tid];
                    pixelColor2.alpha = dev_bitmap2[tid + 3];

                    //if ((pixelColor1.red != pixelColor2.red) ||
                    //     (pixelColor1.green != pixelColor2.green) ||
                    //     (pixelColor1.blue != pixelColor2.blue) ||
                    //     (pixelColor1.alpha != pixelColor2.alpha))
                    if (pixelColor1.red > pixelColor2.red)
                        red_delta = pixelColor1.red - pixelColor2.red;
                    else
                        red_delta = pixelColor2.red - pixelColor1.red;

                    if (pixelColor1.alpha > pixelColor2.alpha)
                        alpha_delta = pixelColor1.alpha - pixelColor2.alpha;
                    else
                        alpha_delta = pixelColor2.alpha - pixelColor1.alpha;

                    if (pixelColor1.green > pixelColor2.green)
                        green_delta = pixelColor1.green - pixelColor2.green;
                    else
                        green_delta = pixelColor2.green - pixelColor1.green;

                    if (pixelColor1.blue > pixelColor2.blue)
                        blue_delta = pixelColor1.blue - pixelColor2.blue;
                    else
                        blue_delta = pixelColor2.blue - pixelColor1.blue;
                    if ((red_delta > 8) || (alpha_delta > 8) || (green_delta > 8) || (blue_delta > 8))
                    {

                        //thread.SyncThreads();
                        possition[sharedCount[1]++] = i;//(thread.blockIdx.x * thread.blockDim.x) + thread.threadIdx.x;
                        possition[sharedCount[1]++] = j;//(thread.blockIdx.y * thread.blockDim.y) + thread.threadIdx.y;

                        dev_result[sharedCount[0]++] = pixelColor2.blue;
                        dev_result[sharedCount[0]++] = pixelColor2.green;
                        dev_result[sharedCount[0]++] = pixelColor2.red;
                        dev_result[sharedCount[0]++] = pixelColor2.alpha;

                        //sharedCount[0] += 4;

                        //sharedCount[1] += 2;
                        count[1] = sharedCount[1];
                        count[0] = sharedCount[0];

                    }
                    // tid += thread.gridDim.x;
                }
            }
        }



        public Bitmap BuildImage(Bitmap bmp1, List<short> DeltaList)
        {
            Size s = bmp1.Size;

            int i;

            for (i = 0; i < DeltaList.Count; i += 6)
            {
                int y = DeltaList[i + 0];
                int x = DeltaList[i + 1];

                Color C1 = bmp1.GetPixel(x, y);
                Color C = Color.FromArgb(DeltaList[i + 5], DeltaList[i + 2], DeltaList[i + 3], DeltaList[i + 4]);
                bmp1.SetPixel(x, y, C);
            }

            Console.WriteLine("New Image Recreated");
            return bmp1;
        }
    }
}


   