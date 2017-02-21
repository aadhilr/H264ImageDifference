using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class Random
    {
        public List<PixelsDetails> SetList(Bitmap bmp1, Bitmap bmp2)
        {
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            //Size s1 = bmp1.Size; 
            //Size s2 = bmp2.Size;
            //if (s1 != s2) Console.WriteLine("The dimensions doesn't match");// Comparing the sizes of the two bitmaps

            List<PixelsDetails> listPixels = new List<PixelsDetails>(); //creating a list to add pixel details

            int pixelSize = 64;
            int yborder = 0;
            int xborder = 0;
            int dif = 10;
            for (int y = yborder; y < bmp1.Size.Height-yborder; y+=pixelSize)
            {
                for (int x = xborder; x < bmp1.Size.Width-xborder; x+=pixelSize)
                {
                    if (bmp1.GetPixel(x, y).Equals(bmp2.GetPixel(x, y)))//comparing the pixels for any differences
                    {
                        for (int y_Now = y; y_Now < y + pixelSize; y_Now++)
                        {
                            if (y_Now == bmp1.Size.Height) break;
                            for (int x_Now = x; x_Now < x + pixelSize; x_Now++)
                            {
                                if (x_Now == bmp1.Size.Width) break;
                                Color c1 = bmp1.GetPixel(x_Now, y_Now); ///getting the pixel of the 1st image
                                Color c2 = bmp2.GetPixel(x_Now, y_Now); //getting the pixel of the 2nd image

                                if ((Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B))/3 > dif)
                                listPixels.Add(new PixelsDetails { hor = x_Now, ver = y_Now, c = bmp2.GetPixel(x_Now, y_Now) }); //adding that different pixels to a list
                            }
                            
                        }                                            
                    }
                }
            }
            
            watch1.Stop();
            var elapsedMs = watch1.ElapsedMilliseconds;

            Console.WriteLine("List created in {0}ms", elapsedMs);
            Console.WriteLine("List Size is {0} entries", listPixels.Count);

            /*foreach (PixelsDetails p in listPixels)
            {
                Console.WriteLine("X = {0}  Y = {1} Color = {2}", p.x, p.y, p.c);
            }*/

            return listPixels; //returning the created list 
        }

        public class PixelsDetails
        {
            public int hor { get; set; }
            public int ver { get; set; }
            public Color c { get; set; }
        }
        public Bitmap BuildImage(Bitmap bmp1, List<PixelsDetails> list)
        {
        //    Parallel.For(0, list.Count, listIndex =>
        //    {
        //        bmp1.SetPixel(list[listIndex].ver, list[listIndex].hor, list[listIndex].c);
        //    });
            for (int i=0 ; i < list.Count ; i++){
                bmp1.SetPixel(list[i].ver, list[i].hor, list[i].c);
            }
            Console.WriteLine("New Image Recreated");
            return bmp1; //returning the recreated image 1 bitmap
        }
    }
}
