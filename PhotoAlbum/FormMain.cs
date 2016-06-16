using PhotoStudio;
using Registry;
using System;
using System.Windows.Forms;
using System.IO;

namespace PhotoAlbum
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            SlideViewerParameters slideParameters = 
                new SlideViewerParameters(PhotoStudioRegistry.SlideSpeed, PhotoStudioRegistry.ShowCaptions);
            AlbumForm album = new AlbumForm(false, slideParameters);

            while (string.IsNullOrEmpty(PhotoStudioRegistry.AlbumDirectory) ||
                   !File.Exists(Path.Combine(PhotoStudioRegistry.AlbumDirectory, "Album.xml")))
            {
                OpenFileDialog albumSelection = new OpenFileDialog()
                {
                    CheckFileExists = true,
                    InitialDirectory = "C:\\",
                    Title = "Album not set: please select the Album.xml file",
                    CheckPathExists = true,
                    Filter = "XML files|*.xml"
                };


                if ((albumSelection.ShowDialog() == DialogResult.OK) &&
                    (string.Compare(Path.GetFileName(albumSelection.FileName), "Album.xml", true) == 0))
                {
                    PhotoStudioRegistry.AlbumDirectory = Path.GetDirectoryName(albumSelection.FileName);
                }
                else if (albumSelection.ShowDialog() == DialogResult.Cancel)
                {
                    Close();
                    return;
                }
            }

            album.LoadAlbum(PhotoStudioRegistry.AlbumDirectory);
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
