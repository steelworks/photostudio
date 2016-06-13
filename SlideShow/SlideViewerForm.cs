using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PhotoStudio
{
    public partial class SlideViewerForm : Form
    {
        // Restrain the width of the caption text box to this many characters
        const int MaxCaptionLineLength = 80;

        // Max, min, and delta change slide show speeds in ms
        const int MaxSlideSpeedMs = 10000;
        const int MinSlideSpeedMs = 1000;
        const int DeltaSlideSpeedMs = 1000;

        SlideShow iSlideShow = null;
        string iWorkingFolder = null;
        bool iEndOfShow = false;
        SlideViewerParameters iSlideParameters = null;
        Point iMousePosition;
        bool iCursorVisible = true;

        // Main constructor
        public SlideViewerForm(SlideViewerParameters aSlideParameters)
        {
            InitializeComponent();
            iSlideParameters = aSlideParameters;
            this.WindowState = FormWindowState.Maximized;
            speedTrackBar.Minimum = MinSlideSpeedMs / 1000;
            speedTrackBar.Maximum = MaxSlideSpeedMs / 1000;
            iMousePosition = new Point(0, 0);
            iCursorVisible = false;
            Cursor.Hide();
        }

        // Constructor for primitive PhotoTest program
        public SlideViewerForm(bool aTest)
        {
            // Load one hard-coded slide show for now
            LoadShow("C:\\Users\\Public\\Pictures\\photoalbum\\2008\\xmas", "family.xml");
            iSlideParameters = new SlideViewerParameters(2000, true);
            iMousePosition = new Point(0, 0);
            iCursorVisible = false;
            Cursor.Hide();
        }

        // Constructor for preloaded slide show
        public SlideViewerForm(SlideShow aSlideShow, SlideViewerParameters aSlideParameters)
            : this(aSlideParameters)
        {
            iSlideShow = aSlideShow;
            iWorkingFolder = Path.GetDirectoryName(iSlideShow.GetFilePath());
            this.Text = iSlideShow.Title;

            // Transition slides on timer tick: make the first tick quick
            iEndOfShow = false;
            transitionTimer.Enabled = true;
            transitionTimer.Interval = 100;
        }

        public void LoadShow(string aWorkingFolder, string aXmlFile)
        {
            // Load a new slide show, discarding any previous one
            this.Text = aXmlFile;
            iSlideShow = new SlideShow();
            iWorkingFolder = aWorkingFolder;
            string fullPath = Path.Combine(iWorkingFolder, aXmlFile);
            iSlideShow.Load(fullPath);

            // Transition slides on timer tick: artificial tick to display the first slide
            iEndOfShow = false;
            transitionTimer.Enabled = true;
            timer1_Tick(null, null);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Bitmap canvas = new Bitmap(this.Width, this.Height, 
        //                               PixelFormat.Format16bppRgb565);
        //    Graphics canvasGraphics = Graphics.FromImage(canvas);
        //    Image photo = new Image();
        //    canvasGraphics.DrawImage(photo, new Point(0, 0));
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Ensure that we are set to any change in the required interval.
            transitionTimer.Interval = iSlideParameters.Speed;

            // Free the resources for any previous slide
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            if (iEndOfShow)
            {
                // End of slide show
                transitionTimer.Stop();
                controlTimer.Stop();
                this.Close();
            }
            else
            {
                // Load the current slide
                string slidePath = iSlideShow.GetPath();
                string pic1 = Path.Combine(iWorkingFolder, slidePath);
                if (File.Exists(pic1))      // Tolerate non-existence of image file
                {
                    pictureBox1.Image = System.Drawing.Bitmap.FromFile(pic1);
                    captionTextBox.Visible = false;         // Assume no caption
                    if (iSlideParameters.ShowCaptions)
                    {
                        List<String> caption = iSlideShow.Lines;
                        if ((caption != null) && (caption.Count > 0) && 
                            PopulateCaptionTextBox(caption))
                        {
                            captionTextBox.Visible = true;
                        }
                    }
                }
            }

            // Advance to next slide
            if (!iSlideShow.NextSlide())
            {
                // No more slides, so terminate on next timer tick
                iEndOfShow = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Size = Size;
        }

        // Transcribe the supplied list of strings into the caption text box.
        // Return true if transcribed, false if the caption text box will be empty.
        private bool PopulateCaptionTextBox(List<string> aCaption)
        {
            // First of all split long lines of the caption into multiple lines
            for (int i = 0; i < aCaption.Count; i++)
            {
                string thisLine = aCaption[i];
                if (thisLine.Length > MaxCaptionLineLength)
                {
                    // Ideal split position
                    string choppedLine = thisLine.Substring(0, MaxCaptionLineLength);

                    // But backtrack to most recent space.
                    int spacePos = choppedLine.LastIndexOf(' ');

                    if (spacePos < 0)
                    {
                        // No suitable spaces: make the most of a bad job
                        spacePos = MaxCaptionLineLength;
                    }
                    aCaption[i] = thisLine.Substring(0, spacePos);
                    aCaption.Insert(i + 1, thisLine.Substring(spacePos + 1));
                }
            }

            // Populate the caption text box
            captionTextBox.Lines = aCaption.ToArray();

            // Sometimes there is an empty caption which translates into no lines
            // of text box. We cannot meaningfully display an empty text box.
            if (captionTextBox.Lines.Length == 0)
            {
                return false;
            }

            // Resize the caption text box based on its text length.
            // Create a new SizeF object to return the size into.
            System.Drawing.SizeF thisSize = new System.Drawing.SizeF();
            System.Drawing.SizeF maxSize = new System.Drawing.SizeF();

            // Create a new font based on the font of the textbox we want to resize
            System.Drawing.Font captionFont = new System.Drawing.Font(captionTextBox.Font, 
                                                                      captionTextBox.Font.Style);

            // Determine the maximum line length
            maxSize = TextRenderer.MeasureText(captionTextBox.Lines[0], captionFont);
            foreach (string line in captionTextBox.Lines)
            {
                thisSize = TextRenderer.MeasureText(line, captionFont);
                maxSize.Width = Math.Max(maxSize.Width, thisSize.Width);
            }

            // Resize the textbox to accommodate the entire string
            int captionWidth = (int)Math.Round(maxSize.Width, 0) + captionTextBox.Margin.Horizontal;
            int captionHeight = (int)Math.Round(maxSize.Height * captionTextBox.Lines.Length, 0) + 
                                captionTextBox.Margin.Vertical;

            captionTextBox.SetBounds(pictureBox1.Location.X + 100,
                                     pictureBox1.Size.Height + pictureBox1.Location.Y - captionHeight - 100,
                                     captionWidth, captionHeight);
            return true;
        }

        private void SlideViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // Scroll
                case Keys.Add:
                case Keys.Oemplus:
                    if (iSlideParameters.Speed > MinSlideSpeedMs)
                    {
                        iSlideParameters.Speed -= DeltaSlideSpeedMs;
                        transitionTimer.Interval = iSlideParameters.Speed;
                        transitionTimer.Stop();
                        transitionTimer.Start();        // Restart transition timing
                    }
                    ShowSpeedControl();
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    if (iSlideParameters.Speed < MaxSlideSpeedMs)
                    {
                        iSlideParameters.Speed += DeltaSlideSpeedMs;
                        transitionTimer.Interval = iSlideParameters.Speed;
                        transitionTimer.Stop();
                        transitionTimer.Start();        // Restart transition timing
                    }
                    ShowSpeedControl();
                    break;
                case Keys.Right:
                    timer1_Tick(null, null);    // Transition to next slide now
                    break;
                case Keys.Left:
                    // Try to transition to previous slide
                    if (iSlideShow.PrevSlide() && iSlideShow.PrevSlide())
                    {
                        timer1_Tick(null, null);
                    }
                    break;
                case Keys.C:
                    iSlideParameters.ToggleCaptions();
                    ShowCaptionControl();
                    break;
                case Keys.Escape:
                    iEndOfShow = true;
                    timer1_Tick(null, null);
                    break;
                default:
                    // Ignore key stroke: uncomment the following to display the key pressed
                    //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
                    //messageBoxCS.AppendFormat("{0} = {1}", "Alt", e.Alt);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "Control", e.Control);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "Handled", e.Handled);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "KeyCode", e.KeyCode);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "KeyValue", e.KeyValue);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "KeyData", e.KeyData);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "Modifiers", e.Modifiers);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "Shift", e.Shift);
                    //messageBoxCS.AppendLine();
                    //messageBoxCS.AppendFormat("{0} = {1}", "SuppressKeyPress", e.SuppressKeyPress);
                    //messageBoxCS.AppendLine();
                    //MessageBox.Show(messageBoxCS.ToString(), "KeyDown Event");
                    break;
            }
        }

        // Temporarily show the controls for the slide show
        private void ShowSpeedControl()
        {
            // iSlideParameters.Speed represents the delay in ms between transitions.
            // A high value indicates low speed.
            // Set the track bar to high for min speed, low for max speed.
            speedTrackBar.Value = ((MaxSlideSpeedMs - iSlideParameters.Speed) / 1000) + 1;
            speedTrackBar.Visible = true;
            speedLabel.Visible = true;

            controlTimer.Stop();
            controlTimer.Start();
        }

        private void ShowCaptionControl()
        {
            // Show the caption label "struck out" if captions are invisible,
            // normal if visible.
            if (iSlideParameters.ShowCaptions)
            {
                captionLabel.Font = new Font(captionLabel.Font, FontStyle.Bold);
            }
            else
            {
                captionLabel.Font = new Font(captionLabel.Font, FontStyle.Strikeout|FontStyle.Bold);
            }
            captionLabel.Visible = true;

            controlTimer.Stop();
            controlTimer.Start();
        }

        private void controlTimer_Tick(object sender, EventArgs e)
        {
            // Turn the control display off
            speedLabel.Visible = false;
            speedTrackBar.Visible = false;
            captionLabel.Visible = false;
            if (iCursorVisible)
            {
                Cursor.Hide();
                iCursorVisible = false;
            }
            controlTimer.Stop();
        }

        private void SlideViewerForm_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point newPosition = new Point(e.X, e.Y);
            if (iMousePosition != newPosition)
            {
                iMousePosition = newPosition;
                if (!iCursorVisible)
                {
                    Cursor.Show();
                    iCursorVisible = true;
                }

                controlTimer.Stop();
                controlTimer.Start();
            }
        }

        private void SlideViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure that the cursor is visible and remains visible when the form is closed
            transitionTimer.Stop();
            controlTimer.Stop();
            if (!iCursorVisible)
            {
                Cursor.Show();
            }
        }
    }
}
