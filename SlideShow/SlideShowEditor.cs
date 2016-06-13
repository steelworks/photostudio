using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PhotoStudio
{
    public partial class SlideShowEditor : Form
    {
        SlideShow iSlideShow = null;
        string iWorkingFolder = null;
        int iCurrentPosition = 0;
        List<PictureBox> iPictureStrip = new List<PictureBox>();
        bool iCaptionChanged = false;       // Remember if caption for current slide has been modified
        bool iSlideShowChanged = false;     // Remember if there has been any modification to the slideshow

        // Constructor with no slide show
        public SlideShowEditor()
        {
            InitializeComponent();

            // Manage strip of picture boxes as an indexed list
            iPictureStrip.Add(leftLeftPictureBox);
            iPictureStrip.Add(leftPictureBox);
            iPictureStrip.Add(centrePictureBox);
            iPictureStrip.Add(rightPictureBox);
            iPictureStrip.Add(rightRightPictureBox);
        }

        // Constructor with slide show
        public SlideShowEditor(SlideShow aSlideShow) : this()
        {
            iSlideShow = aSlideShow;
            iWorkingFolder = Path.GetDirectoryName(iSlideShow.GetFilePath());

            // Load picture boxes for start of show
            iCurrentPosition = 0;
            iSlideShow.Reset();
            toolStripProgressBar.Value = 0;
            InitialiseShow();
        }

        public void LoadShow(string aWorkingFolder, string aXmlFile)
        {
            // Load a new slide show, discarding any previous one
            this.Text = aXmlFile;
            iSlideShow = new SlideShow();
            iWorkingFolder = aWorkingFolder;
            string fullPath = Path.Combine(iWorkingFolder, aXmlFile);
            iSlideShow.Load(fullPath);
            InitialiseShow();
        }

        void InitialiseShow()
        {
            // Load picture boxes for start of show
            iCurrentPosition = 0;
            iSlideShow.Reset();
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = iSlideShow.Count - 1;
            toolStripProgressBar.Value = 0;
            LoadPictures();

            // Show how many pictures are currently in the slide show
            toolStripCountLabel.Text = string.Format("{0} pictures", iSlideShow.Count);

            // We have loaded just the pictures defined in the slideshow.
            // Check if there are any additional pictures in the folder.
            toolStripDropDownButton.Visible = (iSlideShow.GetUnincludedPictures().Count > 0);
        }

        void LoadPictures()
        {
            // Free the resources for any previous slide show: sets all images to null
            UnloadPictures();

            // Load the main picture
            LoadMain();

            // Load the picture strip
            for (int i = 0; i < 5; i++)
            {
                string slidePath = iSlideShow.GetPath(i - 2);
                if (slidePath != null)
                {
                    string picStrip = Path.Combine(iWorkingFolder, slidePath);
                    if (File.Exists(picStrip))
                    {
                        iPictureStrip[i].Image = System.Drawing.Bitmap.FromFile(picStrip);
                    }
                }
            }

            iCaptionChanged = false;
            toolStripProgressBar.Value = iCurrentPosition;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            UnloadPictures();
            CheckCaptionChanged();
            this.Close();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            CheckCaptionChanged();
            if (iSlideShow.PrevSlide())
            {
                iCurrentPosition--;

                // Right hand picture disappears
                if (iPictureStrip[4].Image != null)
                {
                    iPictureStrip[4].Image.Dispose();
                }

                // Other pictures shunt along
                for (int i = 4; i > 0; i--)
                {
                    iPictureStrip[i].Image = iPictureStrip[i - 1].Image;
                }

                // Replace first picture
                iPictureStrip[0].Image = null;
                string slidePath = iSlideShow.GetPath(-2);
                if (slidePath != null)
                {
                    string picStrip = Path.Combine(iWorkingFolder, slidePath);
                    if (File.Exists(picStrip))
                    {
                        iPictureStrip[0].Image = System.Drawing.Bitmap.FromFile(picStrip);
                    }
                }

                // Replace main picture
                LoadMain();
            }

            iCaptionChanged = false;
            toolStripProgressBar.Value = iCurrentPosition;
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            CheckCaptionChanged();
            if (iSlideShow.NextSlide())
            {
                iCurrentPosition++;

                // Left hand picture disappears
                if (iPictureStrip[0].Image != null)
                {
                    iPictureStrip[0].Image.Dispose();
                }

                // Other pictures shunt along
                for (int i = 0; i < 4; i++)
                {
                    iPictureStrip[i].Image = iPictureStrip[i + 1].Image;
                }

                // Replace final picture
                iPictureStrip[4].Image = null;
                string slidePath = iSlideShow.GetPath(2);
                if (slidePath != null)
                {
                    string picStrip = Path.Combine(iWorkingFolder, slidePath);
                    if (File.Exists(picStrip))
                    {
                        iPictureStrip[4].Image = System.Drawing.Bitmap.FromFile(picStrip);
                    }
                }

                // Replace main picture
                LoadMain();
            }

            iCaptionChanged = false;
            toolStripProgressBar.Value = iCurrentPosition;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> additionalPictures = iSlideShow.GetUnincludedPictures();
            string messageList = string.Empty;
            foreach (string onePicture in additionalPictures)
            {
                messageList += (onePicture + Environment.NewLine);
            }

            MessageBox.Show(messageList, "Unincluded pictures");
        }

        private void addThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add all images in the SlideShow folder that are not already in the SlideShow
            iSlideShow.PopulateFromFolder();
            InitialiseShow();                       // Resynchronise the editor
            iSlideShowChanged = true;               // Ensure new slides are saved to file
        }

        private void CheckCaptionChanged()
        {
            // If the user has edited the caption for the current slide, then update
            // the slideshow and remember that the slideshow has changed
            if (iCaptionChanged)
            {
                iSlideShow.Lines = new List<String>(captionTextBox.Lines);
                iSlideShowChanged = true;
            }
        }

        private void captionTextBox_TextChanged(object sender, EventArgs e)
        {
            iCaptionChanged = true;
        }

        private void SlideShowEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iSlideShowChanged)
            {
                DialogResult response = MessageBox.Show("Slide show has been modified. Save changes?",
                                                        "PhotoStudio", MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);
                if (response == DialogResult.Yes)
                {
                    iSlideShow.Save();
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("Delete image file as well as slide show position?",
                                                    "PhotoStudio", MessageBoxButtons.YesNoCancel,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button3);
            if ((response == DialogResult.Yes) || (response == DialogResult.No))
            {
                // Must release resources for the current picture before trying to delete it
                if (mainPictureBox.Image != null)
                {
                    mainPictureBox.Image.Dispose();
                    mainPictureBox.Image = null;
                }

                if (centrePictureBox.Image != null)
                {
                    centrePictureBox.Image.Dispose();
                    centrePictureBox.Image = null;
                }

                bool isFinalSlide = false;
                iSlideShow.DeleteCurrent(response == DialogResult.Yes, out isFinalSlide);

                if (isFinalSlide)
                {
                    // Retract to the previous slide
                    iPictureStrip[2].Image = iPictureStrip[1].Image;
                    iPictureStrip[1].Image = iPictureStrip[0].Image;

                    // Replace first picture
                    iPictureStrip[0].Image = null;
                    string slidePath = iSlideShow.GetPath(-2);
                    if (slidePath != null)
                    {
                        string picStrip = Path.Combine(iWorkingFolder, slidePath);
                        if (File.Exists(picStrip))
                        {
                            iPictureStrip[0].Image = System.Drawing.Bitmap.FromFile(picStrip);
                        }
                    }
                }
                else
                {
                    // Shunt pictures in from the right
                    iPictureStrip[2].Image = iPictureStrip[3].Image;
                    iPictureStrip[3].Image = iPictureStrip[4].Image;

                    // Replace final picture
                    iPictureStrip[4].Image = null;
                    string slidePath = iSlideShow.GetPath(2);
                    if (slidePath != null)
                    {
                        string picStrip = Path.Combine(iWorkingFolder, slidePath);
                        if (File.Exists(picStrip))
                        {
                            iPictureStrip[4].Image = System.Drawing.Bitmap.FromFile(picStrip);
                        }
                    }
                }

                // Replace main picture
                LoadMain();

                iSlideShowChanged = true;
            }
        }

        // Load main picture
        private void LoadMain()
        {
            if (mainPictureBox.Image != null)
            {
                mainPictureBox.Image.Dispose();
            }

            string slidePath = iSlideShow.GetPath();
            if (slidePath != null)
            {
                string picMain = Path.Combine(iWorkingFolder, slidePath);
                if (File.Exists(picMain))
                {
                    mainPictureBox.Image = System.Drawing.Bitmap.FromFile(picMain);
                }

                List<String> caption = iSlideShow.Lines;
                if ((caption == null) || (caption.Count == 0))
                {
                    // No caption
                    captionTextBox.Lines = null;
                }
                else
                {
                    captionTextBox.Lines = caption.ToArray();
                }
            }
        }

        // Free all image resources
        private void UnloadPictures()
        {
            if (mainPictureBox.Image != null)
            {
                mainPictureBox.Image.Dispose();
                mainPictureBox.Image = null;
            }

            for (int i = 0; i < 5; i++)
            {
                if (iPictureStrip[i].Image != null)
                {
                    iPictureStrip[i].Image.Dispose();
                    iPictureStrip[i].Image = null;
                }
            }
        }
    }
}
