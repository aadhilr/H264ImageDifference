using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class RandPtr
    {
        public List<PixelsDetails> SetList(Bitmap bmp1, Bitmap bmp2)
        {
            var watch1 = System.Diagnostics.Stopwatch.StartNew();

            bool equals = true;
            Rectangle rect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
            BitmapData bmpData1 = bmp1.LockBits(rect, ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bmpData2 = bmp2.LockBits(rect, ImageLockMode.ReadOnly, bmp2.PixelFormat);

            List<PixelsDetails> listPixels = new List<PixelsDetails>(); //creating a list to add pixel details
            unsafe
            {
                byte* ptr1 = (byte*)bmpData1.Scan0.ToPointer();
                byte* ptr2 = (byte*)bmpData2.Scan0.ToPointer();
                int width = rect.Width * 3; // for 24bpp pixel data
                for (int y = 0; equals && y < rect.Height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (*ptr1 != *ptr2)
                        {
                            listPixels.Add(new PixelsDetails { hor = x, ver = y, c = Color.AliceBlue }); //adding that different pixels to a list
                            //equals = false;
                            //break;
                            //Console.WriteLine("{0} {1}", x, y);
                        }
                        ptr1++;
                        ptr2++;
                    }
                    ptr1 += bmpData1.Stride - width;
                    ptr2 += bmpData2.Stride - width;
                }
            }
            bmp1.UnlockBits(bmpData1);
            bmp2.UnlockBits(bmpData2);

            watch1.Stop();
            var elapsedMs = watch1.ElapsedMilliseconds;

            Console.WriteLine("List created in {0}ms", elapsedMs);
            Console.WriteLine("List Size is {0} entries", listPixels.Count);

            return listPixels;
        }

        public class PixelsDetails
        {
            public int hor { get; set; }
            public int ver { get; set; }
            public Color c { get; set; }
        }
        public Bitmap BuildImage(Bitmap bmp1, List<PixelsDetails> list)
        {
            Size s = bmp1.Size;
            int i = 0;

            for (int y_ = 0; y_ < s.Height; y_++)
            {
                for (int x_ = 0; x_ < s.Width; x_++)
                {

                    if (y_ == list[i].hor && x_ == list[i].ver) //checking whether the list has an entry to that particular x,y coordinates
                    {
                        bmp1.SetPixel(list[i].ver, list[i].hor, list[i].c); //setting the pixels of the 1st image to the pixel taken from the list
                        i++; //incrementing list index
                        if (i == list.Count) break; //incase the list ends the loop breaks because the 1st image has the rest
                    }
                }
                if (i == list.Count) break; //incase the list ends the loop breaks because the 1st image has the rest
            }
            Console.WriteLine("New Image Recreated");
            return bmp1; //returning the recreated image 1 bitmap
        }
    }
}
