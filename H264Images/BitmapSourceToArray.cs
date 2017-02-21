using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media.Imaging;

namespace H264Images
{
    class BitmapSourceToArray
    {
        /*public byte[] BitmapSourceToArrayCon(BitmapSource bitmapSource)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];

            bitmapSource.CopyPixels(pixels, stride, 0);

            return pixels;
        }*/

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
