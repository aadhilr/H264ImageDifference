using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace H264Images
{
    class SetListClass2
    {
        
        //Stopwatch sw = Stopwatch.StartNew();
        public List<PixelsDetails> SetList(Bitmap bmp1, Bitmap bmp2)
        {
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            Size s1 = bmp1.Size; 
            Size s2 = bmp2.Size;
            if (s1 != s2) Console.WriteLine("The dimensions doesn't match");// Comparing the sizes of the two bitmaps

            List<PixelsDetails> listPixels = new List<PixelsDetails>(); //creating a list to add pixel details
          
            int y, x;

            for (y = 0; y < s1.Height; y++)
            {
                for (x = 0; x < s1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
                    Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image

                    int dif = 10;

                    if (Math.Abs(c1.R-c2.R) > dif && Math.Abs(c1.G-c2.G) > dif && Math.Abs(c1.B-c2.B) > dif) //comparing the pixels for any differences
                    {
                        listPixels.Add(new PixelsDetails { hor = x, ver = y, c = c2 }); //adding that different pixels to a list
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

        //Console.WriteLine(sw.ElapsedMilliseconds);
        //sw.Stop();
        //watch1.Stop();
        //var elapsedMs = watch1.ElapsedMilliseconds;

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