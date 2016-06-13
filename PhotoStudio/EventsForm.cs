using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoStudio
{
    public partial class EventsForm : Form
    {
        EventList iEventList = null;
        string iWorkingFolder = null;

        // Parameters for showing slide shows
        SlideViewerParameters iSlideParameters;

        public EventsForm(SlideViewerParameters aSlideParameters)
        {
            InitializeComponent();
            iSlideParameters = aSlideParameters;
        }

        public void LoadEvents(string aWorkingFolder, string aXmlFile)
        {
            // Load a new events list, discarding any previous one
            this.Text = aXmlFile;
            iWorkingFolder = aWorkingFolder;
            string fullPath = iWorkingFolder + "\\" + aXmlFile;
            iEventList = new EventList(fullPath);
            iEventList.Load();

            // Populate the listbox from the events list
            iEventList.Reset();
            while (iEventList.NextEvent())
            {
                listBoxEvents.Items.Add(iEventList.Name);
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            // Get the slide show for the current event
            SlideShow selectedShow = iEventList.GetSlideShow(listBoxEvents.SelectedIndex);
            if (selectedShow == null)
            {
                int choice = listBoxEvents.SelectedIndex;
                string choiceName = listBoxEvents.Items[choice].ToString();
                MessageBox.Show("Slide show for <" + choiceName + "> event cannot be played");
            }
            else
            {
                SlideViewerForm slideScreen = new SlideViewerForm(selectedShow, iSlideParameters);
                slideScreen.Show();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxEvents_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Set the DrawMode property to draw fixed sized items.
            // DO WE NEED THIS?
            listBoxEvents.DrawMode = DrawMode.OwnerDrawFixed;

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based on the index of the item to draw.
            Font requiredFont = e.Font;
            string padding = "";
            switch (iEventList.GetLevel(e.Index))
            {
                case 0:
                    myBrush = Brushes.Purple;
                    requiredFont = new Font(requiredFont, FontStyle.Bold);
                    break;
                case 1:
                    myBrush = Brushes.Red;
                    padding = "  ";
                    break;
                case 2:
                    myBrush = Brushes.Orange;
                    requiredFont = new Font(requiredFont, FontStyle.Italic);
                    padding = "    ";
                    break;
            }

            // Draw the current item text based on the current Font and the custom brush settings.
            string eventText = padding + listBoxEvents.Items[e.Index].ToString();
            e.Graphics.DrawString(eventText, requiredFont, myBrush, e.Bounds, StringFormat.GenericDefault);

            // Add a photo icon if the event has a slideshow
            if (iEventList.GetSlideShow(e.Index) != null)
            {
                imageList.Draw(e.Graphics, e.Bounds.Right - 16, e.Bounds.Top, 0);
            }

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Item selected in EventList listbox: enable/disable the Play button
            // depending upon whether it has a slide show
            buttonPlay.Enabled = (iEventList.GetSlideShow(listBoxEvents.SelectedIndex) != null);
        }
    }
}
