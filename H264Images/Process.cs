using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace H264Images
{
    public class Process
    {
        public Bitmap getDifferencBitmap(Bitmap bmp1, Bitmap bmp2, Color diffColor)
        {
            Size s1 = bmp1.Size;
            Size s2 = bmp2.Size;
            if (s1 != s2) return null;

            int k = 0;
            int l = 0;


            Bitmap bmp3 = new Bitmap(s1.Width, s1.Height);

            for (int y = 35; y < s1.Height-200; y++)
                for (int x = 40; x < s1.Width-40; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);

                    //int f = 240;
                    int dif = 10;

                    if (/*c1 != c2 && c2.R < f && c2.G < f && c2.B < f &&*/ Math.Abs(c1.R-c2.R) > dif && Math.Abs(c1.G-c2.G) > dif && Math.Abs(c1.B-c2.B) > dif)
                    {
                        bmp3.SetPixel(x, y, c2);
                        k++; 
                    }
                    else
                    {
                        bmp3.SetPixel(x, y, diffColor);
                        //k++;
                    }
                }

            for (int y = 0; y < s1.Height; y++)
                for (int x = 0; x < s1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);

                    if (c1 != c2)
                    {
                        //bmp3.SetPixel(x, y, c2);
                        l++;
                    }
                    else
                    {
                        //bmp3.SetPixel(x, y, diffColor);
                    }
                }

           Console.WriteLine("Normal {0} different pixels",l);
           Console.WriteLine("Big changes {0} different pixels",k);
            return bmp3;
        }
    }
}
