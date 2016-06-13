namespace PhotoAlbum
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splashPictureBox = new System.Windows.Forms.PictureBox();
            this.splashImageList = new System.Windows.Forms.ImageList(this.components);
            this.exitTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splashPictureBox
            // 
            this.splashPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashPictureBox.Image = global::PhotoAlbum.Properties.Resources.cover;
            this.splashPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("splashPictureBox.InitialImage")));
            this.splashPictureBox.Location = new System.Drawing.Point(0, 0);
            this.splashPictureBox.Name = "splashPictureBox";
            this.splashPictureBox.Size = new System.Drawing.Size(284, 264);
            this.splashPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.splashPictureBox.TabIndex = 0;
            this.splashPictureBox.TabStop = false;
            // 
            // splashImageList
            // 
            this.splashImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("splashImageList.ImageStream")));
            this.splashImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.splashImageList.Images.SetKeyName(0, "cover.jpg");
            // 
            // exitTimer
            // 
            this.exitTimer.Interval = 500;
            this.exitTimer.Tick += new System.EventHandler(this.exitTimer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.splashPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Steelworks PhotoAlbum";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.splashPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox splashPictureBox;
        private System.Windows.Forms.ImageList splashImageList;
        private System.Windows.Forms.Timer exitTimer;
    }
}

