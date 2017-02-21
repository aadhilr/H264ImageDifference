using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace H264Images
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "Image File(*.bmp,*.jpg)|*.bmp;*.jpg";
            if (DialogResult.OK == ofile.ShowDialog())
            {
                this.ImageFuture.SizeMode = PictureBoxSizeMode.Zoom;
                this.ImageFuture.Image = new Bitmap(ofile.FileName);
                //OpenI = new Open();
                //Open.OpenImage(ofile.FileName);
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Filter = "Image File(*.bmp,*.jpg)|*.bmp;*.jpg";
            if (DialogResult.OK == ofile.ShowDialog())
            {   this.ImagePresent.SizeMode = PictureBoxSizeMode.Zoom;
                this.ImagePresent.Image = new Bitmap(ofile.FileName);
                
                //OpenI = new Open();
                //Open.OpenImage(ofile.FileName);
            }
        }
    
        public void button2_Click(object sender, EventArgs e)
        {
            Bitmap copy1 = new Bitmap((Bitmap)this.ImageFuture.Image);
            Bitmap copy2 = new Bitmap((Bitmap)this.ImagePresent.Image);

            var instance = new Process();
            //instance.getDifferenctToArray(copy1, copy2);
          
            Bitmap bmp3 = instance.getDifferencBitmap(copy1,copy2,Color.DarkCyan);

            this.ImagePast.SizeMode = PictureBoxSizeMode.Zoom;
            this.ImagePast.Image = bmp3;

            //var fileName = "Ninja.jpg";
            //bmp3.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //bmp3.Save(Application.StartupPath + "\\img.jpg");
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromName("DarkCyan");
        }

        public void Recreate_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap((Bitmap)this.ImageFuture.Image);
            Bitmap image2 = new Bitmap((Bitmap)this.ImagePresent.Image);

            //var Set = new Random();
            //List<H264Images.Random.PixelsDetails> list = Set.SetList(image1, image2);
            
            var Set = new ArrayListNewCuda();
            Stopwatch stopWatch = Stopwatch.StartNew();

            //List<short> LocList = Set.LocListReturn(image1, image2);
            List<short> DeltaList = Set.GetDelta(image1, image2);

            Console.WriteLine("List created in : " + stopWatch.ElapsedMilliseconds + "ms");

            //Bitmap Newbmp = Set.SetList(image1, image2);

            Bitmap Newbmp = Set.BuildImage(image1, DeltaList);
            this.RecreatedImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.RecreatedImage.Image = Newbmp;
            //Newbmp.Save(Application.StartupPath + "\\NewNew2.jpg");
        }

        private void ImageFuture_Click(object sender, EventArgs e)
        {

        }

        private void ImagePresent_Click(object sender, EventArgs e)
        {

        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start("C:\\Users\\Kumar\\Downloads\\Charana\\H264Images\\H264Images\\Client.html");
        //}

    }
}