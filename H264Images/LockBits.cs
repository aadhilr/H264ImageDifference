using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class LockBits
    {
        public List<PixelsDetails> SetList(Bitmap bmp1, Bitmap bmp2)
        {
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            Size s1 = bmp1.Size;
            Size s2 = bmp2.Size;
            if (s1 != s2) Console.WriteLine("The dimensions doesn't match");// Comparing the sizes of the two bitmaps

            List<PixelsDetails> listPixels = new List<PixelsDetails>(); //creating a list to add pixel details

            int y, x;

            for (y = 0; y < s1.Height; y+=4)
            {
                for (x = 0; x < s1.Width; x+=4)
                {
                    
                    if (CompareImages(bmp1 , bmp2, x, x+4, y, y+4)) //comparing the pixels for any differences
                    {
                        int y_Now = y;
                        int x_Now = x;

                        for (y_Now = y; y_Now < y + 16; y_Now++)
                        {
                            if (y_Now == s1.Height) break;
                            for (x_Now = x; x_Now < x + 16; x_Now++)
                            {
                                if (x_Now == s1.Width) break;
                                if (bmp1.GetPixel(x_Now, y_Now) != bmp2.GetPixel(x_Now, y_Now))
                                    listPixels.Add(new PixelsDetails { hor = x_Now, ver = y_Now, c = bmp2.GetPixel(x_Now, y_Now) }); //adding that different pixels to a list
                            }

                        }        
                        //listPixels.Add(new PixelsDetails { hor = x, ver = y, c = c2 }); //adding that different pixels to a list
                    }
                }
            }

            watch1.Stop();
            var elapsedMs = watch1.ElapsedMilliseconds;

            Console.WriteLine("List created in {0}ms", elapsedMs);
            Console.WriteLine("List Size is {0} entries", listPixels.Count);
         
            return listPixels; //returning the created list 
        }

        private bool CompareImages(Bitmap FirstImage, Bitmap SecondImage, int Xbegin, int Xend, int Ybegin, int Yend)
        {
            BitmapData bmdFirstImage, bmdSecondImage;
            Int32 intPixelSize;
            
            /*-------------BS----------------------------------*/
            switch (FirstImage.PixelFormat)
            {
                // 8 bit - 1 byte
                case (PixelFormat.Format8bppIndexed):{intPixelSize = 1;break;}
                // 16 bit - 2 bytes
                case (PixelFormat.Format16bppArgb1555):{intPixelSize = 2;break;}
                case (PixelFormat.Format16bppGrayScale):{intPixelSize = 2;break;}
                case (PixelFormat.Format16bppRgb555):{intPixelSize = 2;break;}
                case (PixelFormat.Format16bppRgb565):{intPixelSize = 2; break;}
                // 24 bit - 3 bytes
                case (PixelFormat.Format24bppRgb):{intPixelSize = 3; break;}
                // 32 bit - 4 bytes
                case (PixelFormat.Format32bppArgb):{intPixelSize = 4;break;}
                case (PixelFormat.Format32bppPArgb):{intPixelSize = 4; break;}
                case (PixelFormat.Format32bppRgb):{intPixelSize = 4;break;}
                // 48 bit - 5 bytes
                case (PixelFormat.Format4bppIndexed):{ intPixelSize = 5; break;}
                // 64 bit - 6 bytes
                case (PixelFormat.Format64bppArgb):{ intPixelSize = 6; break;}
                case (PixelFormat.Format64bppPArgb):{ intPixelSize = 6; break;}
                // Unsupported size
                default:{return false;}
            }
            /*-------------BS----------------------------------*/

            // Lock both bitmap bits to initialize comparison of pixels
            bmdFirstImage = FirstImage.LockBits(new Rectangle(Xbegin, Ybegin, Xend, Yend),
                                                 ImageLockMode.ReadOnly,
                                                 FirstImage.PixelFormat);

            bmdSecondImage = SecondImage.LockBits(new Rectangle(Xbegin, Ybegin, Xend, Yend),
                                                   ImageLockMode.ReadOnly,
                                                   SecondImage.PixelFormat);

            // Compare each pixel in the images
            unsafe
            {
                for (Int32 y = 0; y < bmdFirstImage.Height; ++y)
                {
                    byte* rowFirstImage = (byte*)bmdFirstImage.Scan0 + (y * bmdFirstImage.Stride);
                    byte* rowSecondImage = (byte*)bmdSecondImage.Scan0 + (y * bmdSecondImage.Stride);

                    for (Int32 x = 0; x < bmdFirstImage.Width; ++x)
                    {
                        if (rowFirstImage[x * intPixelSize] != rowSecondImage[x * intPixelSize])
                        {
                            // Unlock bitmap bits
                            FirstImage.UnlockBits(bmdFirstImage);
                            SecondImage.UnlockBits(bmdSecondImage);

                            return false;
                        }
                    }
                }
            }
            // Unlock bitmap bits
            FirstImage.UnlockBits(bmdFirstImage);
            SecondImage.UnlockBits(bmdSecondImage);

            return true;
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
