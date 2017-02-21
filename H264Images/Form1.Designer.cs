namespace H264Images
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageFuture = new System.Windows.Forms.PictureBox();
            this.ImagePresent = new System.Windows.Forms.PictureBox();
            this.ImagePast = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.RecreatedImage = new System.Windows.Forms.PictureBox();
            this.Recreate = new System.Windows.Forms.Button();
            this.BrowserBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageFuture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePresent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecreatedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageFuture
            // 
            this.ImageFuture.Location = new System.Drawing.Point(21, 13);
            this.ImageFuture.Name = "ImageFuture";
            this.ImageFuture.Size = new System.Drawing.Size(215, 156);
            this.ImageFuture.TabIndex = 0;
            this.ImageFuture.TabStop = false;
            this.ImageFuture.Click += new System.EventHandler(this.ImageFuture_Click);
            // 
            // ImagePresent
            // 
            this.ImagePresent.Location = new System.Drawing.Point(242, 13);
            this.ImagePresent.Name = "ImagePresent";
            this.ImagePresent.Size = new System.Drawing.Size(215, 156);
            this.ImagePresent.TabIndex = 1;
            this.ImagePresent.TabStop = false;
            this.ImagePresent.Click += new System.EventHandler(this.ImagePresent_Click);
            // 
            // ImagePast
            // 
            this.ImagePast.Location = new System.Drawing.Point(21, 252);
            this.ImagePast.Name = "ImagePast";
            this.ImagePast.Size = new System.Drawing.Size(215, 156);
            this.ImagePast.TabIndex = 2;
            this.ImagePast.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "Open 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(63, 424);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 39);
            this.button2.TabIndex = 5;
            this.button2.Text = "Process";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(289, 187);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 39);
            this.button3.TabIndex = 6;
            this.button3.Text = "Open 2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RecreatedImage
            // 
            this.RecreatedImage.Location = new System.Drawing.Point(242, 252);
            this.RecreatedImage.Name = "RecreatedImage";
            this.RecreatedImage.Size = new System.Drawing.Size(215, 156);
            this.RecreatedImage.TabIndex = 7;
            this.RecreatedImage.TabStop = false;
            // 
            // Recreate
            // 
            this.Recreate.Location = new System.Drawing.Point(289, 424);
            this.Recreate.Name = "Recreate";
            this.Recreate.Size = new System.Drawing.Size(122, 39);
            this.Recreate.TabIndex = 8;
            this.Recreate.Text = "Recreate";
            this.Recreate.UseVisualStyleBackColor = true;
            this.Recreate.Click += new System.EventHandler(this.Recreate_Click);
            // 
            // BrowserBtn
            // 
            this.BrowserBtn.Location = new System.Drawing.Point(0, 0);
            this.BrowserBtn.Margin = new System.Windows.Forms.Padding(2);
            this.BrowserBtn.Name = "BrowserBtn";
            this.BrowserBtn.Size = new System.Drawing.Size(50, 15);
            this.BrowserBtn.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 462);
            this.Controls.Add(this.BrowserBtn);
            this.Controls.Add(this.Recreate);
            this.Controls.Add(this.RecreatedImage);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ImagePast);
            this.Controls.Add(this.ImagePresent);
            this.Controls.Add(this.ImageFuture);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageFuture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePresent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecreatedImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageFuture;
        private System.Windows.Forms.PictureBox ImagePresent;
        private System.Windows.Forms.PictureBox ImagePast;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox RecreatedImage;
        private System.Windows.Forms.Button Recreate;
        private System.Windows.Forms.Button BrowserBtn;
    }
}