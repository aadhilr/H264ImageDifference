using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class SetListClass
    {
        public Bitmap SetList(Bitmap bmp1, Bitmap bmp2)
        {
            Size s1 = bmp1.Size;
            Size s2 = bmp2.Size;
            if (s1 != s2) Console.WriteLine("The dimensions doesn't match");//return null;

            Bitmap bmp3 = new Bitmap(s1.Width, s1.Height);
            
            List<PixelsDetails> listPixels = new List<PixelsDetails>();
            ////////////////////
            int y , x;
            ////////////////////
            for (y = 0; y < s1.Height; y++)
            {
                for (x = 0; x < s1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);
                    if (c1 != c2)
                    {
                        /*PixelsDetails Pixeln = new PixelsDetails() { x = x, y = y, c = c2 };
                        listPixels.Add(Pixeln);*/
                        listPixels.Add(new PixelsDetails { hor = x, ver = y, c = c2 });
                    }
                }
                Console.WriteLine("x {0}", x);
            }

            
            Console.WriteLine("y {0}" ,y);
            /*foreach (PixelsDetails p in listPixels)
            {
                Console.WriteLine("X = {0}  Y = {1} Color = {2}", p.x, p.y, p.c);
            }*/
            Console.WriteLine("List Created");
    
            ///////////////////////////////////////////////////////////////////////

            //Bitmap bmpNew = new Bitmap(s1.Width, s1.Height);

            int i = 0;

            for (int y_ = 0; y_ < s1.Height; y_++)
            {
                for (int x_ = 0; x_ < s1.Width; x_++)
                {
                    
                    if ((y_ == listPixels[i].ver) &&( x_ == listPixels[i].hor))
                    {
                        bmp1.SetPixel(listPixels[i].hor, listPixels[i].ver, listPixels[i].c);
                        i++;
                        if (i == listPixels.Count) break;
                    }
                    else
                    {
                        Color c1 = bmp1.GetPixel(x_, y_);
                        bmp1.SetPixel(x_, y_, c1);
                        //Console.WriteLine("Bit took from Original");
                    }
                }

                if (i == listPixels.Count) break;
            }
            return bmp1;
            ///////////////////////////////////////////////////////////////////////
            //return listPixels;              
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

            Bitmap bmpNew = new Bitmap(s.Width, s.Height);

            int i = 0;

            for (int y_ = 0; y_ < s.Height; y_++)
            {
                for (int x_ = 0; x_ < s.Width; x_++)
                {
                    //Console.WriteLine(list[i].x);
                    //Console.WriteLine(list[i].y);
                    //Console.WriteLine(list[i].c);
                    //i++;

                    if (y_ == list[i].hor && x_ == list[i].ver)
                    {
                        bmpNew.SetPixel(list[i].ver, list[i].hor, list[i].c);
                        i++;
                    }
                    else
                    {
                        Color c1 = bmp1.GetPixel(x_, y_);
                        bmpNew.SetPixel(x_, y_, c1);
                        //Console.WriteLine("Bit took from Original");
                    }
                }
            }
            Console.WriteLine("New Image Recreated");           
            return bmpNew;
            //return bmp1;
        }
    }
}