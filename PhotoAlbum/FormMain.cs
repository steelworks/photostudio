using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PhotoStudio;
using Registry;

namespace PhotoAlbum
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            SlideViewerParameters slideParameters = 
                new SlideViewerParameters(PhotoStudioRegistry.SlideSpeed, 
                                          PhotoStudioRegistry.ShowCaptions);
            AlbumForm album = new AlbumForm(false, slideParameters);
            album.LoadAlbum(PhotoStudioRegistry.CurrentDirectory);
            album.ShowDialog();

            // End of program run: save any changes to parameters
            PhotoStudioRegistry.SlideSpeed = slideParameters.Speed;
            PhotoStudioRegistry.ShowCaptions = slideParameters.ShowCaptions;

            // Trigger closure of main form
            exitTimer.Enabled = true;
        }

        private void exitTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("FormMain captured keydown");
        }
    }
}
