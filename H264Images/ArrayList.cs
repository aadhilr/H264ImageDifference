using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H264Images
{
    class ArrayList
    {
        public List<short> GetDelta(Bitmap first, Bitmap second) //getting the 2 images
        {
            List<short> deltaList1 = new List<short>();
            List<short> deltaList2 = new List<short>();
            List<short> deltaList3 = new List<short>();
            List<short> deltaList4 = new List<short>();
            List<short> deltaList = new List<short>();//creating the list to insert image data

            Bitmap bmp1 = first;
            Bitmap bmp2 = second;

            short y, x; //x,y coordinates of the pixel grid in bitmap image
                        //Parallel.Invoke(() =>
                        //{

            for (y = 0; y < bmp1.Size.Height / 2; y++)
            {
                for (x = 0; x < bmp1.Size.Width / 2; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
                    Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
                    if (c1 != c2) //comparing the pixels for any differences
                    {
                        deltaList1.Add(y);
                        deltaList1.Add(x);
                        deltaList1.Add(c2.R);
                        deltaList1.Add(c2.G);
                        deltaList1.Add(c2.B);
                        deltaList1.Add(c2.A); //alpha value(0-transparent, 256-full appearence)
                    }
                }
            }
            //},
            //() =>
            //{
            for (y = 0; y < bmp1.Size.Height / 2; y++)
            {
                for (x = (short)(bmp1.Size.Width / 2); x < bmp1.Size.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
                    Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
                    if (c1 != c2) //comparing the pixels for any differences
                    {
                        deltaList2.Add(y);
                        deltaList2.Add(x);
                        deltaList2.Add(c2.R);
                        deltaList2.Add(c2.G);
                        deltaList2.Add(c2.B);
                        deltaList2.Add(c2.A); //alpha value(0-transparent, 256-full appearence)
                    }
                }
            }
            //},
            //() =>
            //{
            for (y = (short)(bmp1.Size.Height / 2); y < bmp1.Size.Height; y++)
            {
                for (x = 0; x < bmp1.Size.Width / 2; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
                    Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
                    if (c1 != c2) //comparing the pixels for any differences
                    {
                        deltaList3.Add(y);
                        deltaList3.Add(x);
                        deltaList3.Add(c2.R);
                        deltaList3.Add(c2.G);
                        deltaList3.Add(c2.B);
                        deltaList3.Add(c2.A); //alpha value(0-transparent, 256-full appearence)
                    }
                }
            }
            //},
            //   Task<List<int>> taskWithFactoryAndState =
            //Task.Factory.StartNew<List<int>>((stateObj) =>
            //{
            //    List<int> ints = new List<int>();
            //    for (int i = 0; i < (int)stateObj; i++)
            //    {
            //        ints.Add(i);
            //    }
            //    return ints;
            //}, 2000);
            //() =>
            //{
            for (y = (short)(bmp1.Size.Height / 2); y < bmp1.Size.Height; y++)
            {
                for (x = (short)(bmp1.Size.Width / 2); x < bmp1.Size.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
                    Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
                    if (c1 != c2) //comparing the pixels for any differences
                    {
                        deltaList4.Add(y);
                        deltaList4.Add(x);
                        deltaList4.Add(c2.R);
                        deltaList4.Add(c2.G);
                        deltaList4.Add(c2.B);
                        deltaList4.Add(c2.A); //alpha value(0-transparent, 256-full appearence)
                    }
                }
            }
            //}
            //);
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
            deltaList = deltaList1.Concat(deltaList2).Concat(deltaList3).Concat(deltaList4).ToList();
            return deltaList;
        }

        //public List<short> LocListReturn(Bitmap bmp1, Bitmap bmp2)
        //{
        //    List<short> LocList = new List<short>();
        //    short y, x;

        //    for (y = 0; y < bmp1.Size.Height; y++)
        //    {
        //        for (x = 0; x < bmp1.Size.Width; x++)
        //        {
        //            Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
        //            Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
        //            if (c1 != c2) //comparing the pixels for any differences
        //            {
        //                LocList.Add(y);
        //                LocList.Add(x);
        //                LocList.Add(c2.A);
        //            }
        //        }
        //    }
        //    return LocList;             
        //}
        //public List<short> DeltaListReturn(Bitmap bmp1, Bitmap bmp2)
        //{
        //    List<short> DeltaList = new List<short>();
        //    short y=0, x=0;
        //    int i = 0;
        //for(y = 0; y < bmp1.Size.Height; y++)
        //{
        //    for (x = 0; x < bmp1.Size.Width; x++)
        //    {
        //        Color c1 = bmp1.GetPixel(x, y); ///getting the pixel of the 1st image
        //        Color c2 = bmp2.GetPixel(x, y); //getting the pixel of the 2nd image
        //        if (c1 != c2) //comparing the pixels for any differences
        //        {
        //            i = (int)Math.Floor((double)i/(double)6)+6;
        //            DeltaList[i++] = y;
        //            DeltaList[i++] = x;
        //            DeltaList[i++] = c2.R;
        //            DeltaList[i++] = c2.G;
        //            DeltaList[i++] = c2.B;
        //            DeltaList[i++] = c2.A;

        //            //i += 6;
        //            //DeltaList.Add(y);
        //            //DeltaList.Add(x);
        //            //DeltaList.Add(c2.R);
        //            //DeltaList.Add(c2.G);
        //            //DeltaList.Add(c2.B);
        //            //DeltaList.Add(c2.A);
        //        }
        //    }
        //}



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
