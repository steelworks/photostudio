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
    public partial class AlbumForm : Form
    {
        Album iAlbum = null;            // The whole album
        string iWorkingFolder = null;

        EventList iEventList;           // The events for the selected year

        bool iEditMode = false;         // Whether the user can edit stuff

        bool iEventListChanged = false; // Remember when the user has edited the events list
        bool iAlbumChanged = false;     // Remember when the user has added tabs to the album

        List<Control> iTabControls; // All the controls that appear on a tab page

        // Parameters for showing slide shows
        SlideViewerParameters iSlideParameters;

        public AlbumForm(bool aEditable, SlideViewerParameters aSlideParameters)
        {
            InitializeComponent();

            // Append CVS version to the main form title
            string version = "$Name: Rev_1_001 $";
            string[] components = version.Split(' ');
            if ((components.Length >= 3) && !version.Contains("1_001"))
            {
                this.Text += (" " + components[1]);
            }
            else
            {
                this.Text += " beta";
            }

            iEditMode = aEditable;
            editButton.Visible = iEditMode;
            contextMenuEvents.Enabled = iEditMode;
            iSlideParameters = aSlideParameters;
        }

        public void LoadAlbum(string aWorkingFolder)
        {
            // Load the album - only expect this to be called once
            iWorkingFolder = aWorkingFolder;
            string fullPath = Path.Combine(iWorkingFolder, "album.xml");
            iAlbum = new Album(fullPath);
            iAlbum.Load();

            // Add the tab pages to the tab control
            iAlbum.Reset();
            foreach (string yearName in iAlbum.Years)
            {
                TabPage yearTab = new TabPage(yearName);
                yearTabControl.TabPages.Add(yearTab);
            }

            // Get the original list of controls on the default tab page
            iTabControls = new List<Control>();
            TabPage originalTab = yearTabControl.TabPages[0];
            foreach (Control control in originalTab.Controls)
            {
                iTabControls.Add(control);
            }

            // If in Edit mode, there is an additional special tab
            if (iEditMode)
            {
                TabPage newYear = new TabPage("New");
                yearTabControl.TabPages.Add(newYear);
            }

            // Remove the default tab pages
            yearTabControl.TabPages.RemoveAt(0);
            yearTabControl.TabPages.RemoveAt(0);
            yearTabControl.SelectTab(0);

            // Set up callback for when a tab page is selected
            yearTabControl.Selected += new TabControlEventHandler(TabControlSelected);

            // Artificial invocation for first year
            TabControlEventArgs tabControlEventArgs = 
                new TabControlEventArgs(yearTabControl.SelectedTab, 0, TabControlAction.Selected);
            TabControlSelected(null, tabControlEventArgs);
        }

        void TabControlSelected(object sender, TabControlEventArgs e)
        {
            // Before populating the newly selected tab, check whether the event list
            // for the previously selected tab was changed
            CheckEventListEdits();

            // Populate the selected tab with the controls common to all tabs
            TabPage yearTab = yearTabControl.TabPages[e.TabPageIndex];
            foreach (Control control in iTabControls)
            {
                yearTab.Controls.Add(control);
            }

            // Populate the listbox from the chosen events list
            eventsListBox.Items.Clear();

            if (yearTab.Text == "New")
            {
                // It's a new events list
                string newYearName = CreateNewYear();
                if (newYearName == null)
                {
                    // User declined to create a new year: switch tabs
                    yearTabControl.SelectedIndex--;
                    return;
                }
                else
                {
                    // Create the events list for the new year
                    string newYearFolder = Path.Combine(iWorkingFolder, newYearName);
                    string newYearFile = Path.Combine(newYearFolder, "Events.xml");
                    iEventList = new EventList(newYearFile);
                    iEventList.Title = newYearName;

                    // Problem: the Add method inserts the iEventList into order,
                    // but the tab remains in its same position
                    iAlbum.Add(iEventList);
                    yearTab.Text = newYearName;

                    iAlbumChanged = true;               // Remember that a new tab has been added

                    eventLabel.Text = "No events";      // Clear details from previous tab
                    albumPictureBox.Image = null;
                    editButton.Enabled = false;         // No events - can't edit
                    playButton.Enabled = false;
                }
            }
            else
            {
                iEventList = iAlbum.GetYear(e.TabPageIndex);
            }

            if (iEventList != null)
            {
                // Also populate the label above the picture box
                yearLabel.Text = iEventList.Title;

                PopulateEventsListBox(false);
            }
        }

        // Time to query the user if changes to the Events List are to be saved
        void CheckEventListEdits()
        {
            if (iEventListChanged)
            {
                DialogResult response = MessageBox.Show("Events list has been modified. Save changes?",
                                                        "PhotoStudio", MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);
                if (response == DialogResult.Yes)
                {
                    iEventList.Save(false);
                }

                iEventListChanged = false;
            }
        }

        // Handler for use when the user wants to create a new year in the album.
        // Return the name of the new year.
        string CreateNewYear()
        {
            YearTitleForm yearTitleForm = new YearTitleForm();
            DialogResult result = yearTitleForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                return yearTitleForm.Title;
            }
            else
            {
                return null;
            }
        }

        // (Re)populate the Events Listbox.
        // The aKeepSelected parameter specifies whether to retain the currently
        // selected event, or to select the first playable event.
        private void PopulateEventsListBox(bool aKeepSelected)
        {
            int currentEvent = eventsListBox.SelectedIndex;

            iEventList.Reset();
            eventsListBox.Items.Clear();
            bool initialEventDetermined = aKeepSelected;
            while (iEventList.NextEvent())
            {
                eventsListBox.Items.Add(iEventList.Name);
                if (!initialEventDetermined && (iEventList.SlideShow != null))
                {
                    // Make the first playable item in the EventsList the selected item
                    currentEvent = eventsListBox.Items.Count - 1;
                    initialEventDetermined = true;
                }
            }

            // Synchronise the EventsList with the selection in the ListBox
            eventsListBox.SelectedIndex = currentEvent;
            iEventList.SetEvent(currentEvent);

            // Prepare context menu in accordance with the initially selected event
            PrepareContextMenu();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Get the slide show for the current event
            SlideShow selectedShow = iEventList.GetSlideShow(eventsListBox.SelectedIndex);
            if (selectedShow == null)
            {
                int choice = eventsListBox.SelectedIndex;
                string choiceName = eventsListBox.Items[choice].ToString();
                MessageBox.Show("Slide show for <" + choiceName + "> event cannot be played");
            }
            else
            {
                selectedShow.Reset();
                SlideViewerForm slideScreen = new SlideViewerForm(selectedShow, iSlideParameters);
                slideScreen.Show();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eventsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();

            // This method is evidently called with a bad index when the list box is cleared
            // and repopulated: just exit if so.
            if (e.Index < 0)
            {
                return;
            }

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
            string eventText = padding + eventsListBox.Items[e.Index].ToString();
            e.Graphics.DrawString(eventText, requiredFont, myBrush, e.Bounds, StringFormat.GenericDefault);

            // Add a photo icon if the event has a slideshow
            if (iEventList.GetSlideShow(e.Index) != null)
            {
                imageList.Draw(e.Graphics, e.Bounds.Right - 16, e.Bounds.Top, 0);
            }

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();

        }

        private void eventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Item selected in EventList listbox: enable/disable the Play button
            // depending upon whether it has a slide show
            if (iEventList.SetEvent(eventsListBox.SelectedIndex))
            {
                SlideShow show = iEventList.SlideShow;
                if (show == null)
                {
                    // Not a slide show
                    eventLabel.Text = iEventList.Name;

                    // Free up resources for thumbnail
                    if (albumPictureBox.Image != null)
                    {
                        albumPictureBox.Image.Dispose();
                        albumPictureBox.Image = null;
                    }

                    playButton.Enabled = false;
                    editButton.Enabled = false;
                }
                else
                {
                    eventLabel.Text = show.Title;
                    string eventFavourite = show.GetFavourite();
                    if (File.Exists(eventFavourite))
                    {
                        // Can't use the following statement, as it locks the file and
                        // causes problems if the user wants to delete it within the editor
                        //albumPictureBox.Image = System.Drawing.Bitmap.FromFile(show.GetFavourite());

                        Stream s = File.Open(show.GetFavourite(), FileMode.Open);
                        albumPictureBox.Image = Image.FromStream(s);
                        s.Close();
                    }
                    else
                    {
                        albumPictureBox.Image = null;
                    }
                    playButton.Enabled = true;
                    editButton.Enabled = true;
                }
            }

            PrepareContextMenu();
        }

        // Each context menu item must be selectable only if meaningful for the current event
        // selected
        void PrepareContextMenu()
        {
            if (contextMenuEvents.Enabled)
            {
                // Enable/disable the context menu items as appropriate:
                //  - can move an event up if it is not currently at position 0
                //  - can move an event down if it is not currently at the bottom
                //  - can promote an event if it is not currently at level 0
                //  - can demote an event if this is consistent with the event immediately above
                int currentItem = eventsListBox.SelectedIndex;
                moveUpToolStripMenuItem.Enabled = (currentItem > 0);
                moveDownToolStripMenuItem.Enabled = (currentItem < (eventsListBox.Items.Count - 1));
                promoteToolStripMenuItem.Enabled = (iEventList.GetLevel(currentItem) > 0);
                int maxLevel = ((currentItem == 0) ? 0 : iEventList.GetLevel(currentItem - 1) + 1);
                demoteToolStripMenuItem.Enabled = (iEventList.GetLevel(currentItem) < maxLevel);

                // Toolstrip buttons have one-to-one correspondence with context menu
                toolStripButtonMoveUp.Enabled = moveUpToolStripMenuItem.Enabled;
                toolStripButtonMoveDown.Enabled = moveDownToolStripMenuItem.Enabled;
                toolStripButtonPromote.Enabled = promoteToolStripMenuItem.Enabled;
                toolStripButtonDemote.Enabled = demoteToolStripMenuItem.Enabled;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Get the slide show for the current event
            SlideShow selectedShow = iEventList.GetSlideShow(eventsListBox.SelectedIndex);
            if (selectedShow == null)
            {
                int choice = eventsListBox.SelectedIndex;
                string choiceName = eventsListBox.Items[choice].ToString();
                MessageBox.Show("Slide show for <" + choiceName + "> event cannot be edited");
            }
            else
            {
                SlideShowEditor slideEditor = new SlideShowEditor(selectedShow);
                slideEditor.ShowDialog();
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iEventList.MoveUp();
            eventsListBox.SelectedIndex--;  // Keep the moved event selected
            iEventListChanged = true;       // Remember that an edit has taken place

            // Repopulate the listbox with the revised order of events
            PopulateEventsListBox(true);
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iEventList.MoveDown();
            eventsListBox.SelectedIndex++;  // Keep the moved event selected
            iEventListChanged = true;       // Remember that an edit has taken place

            // Repopulate the listbox with the revised order of events
            PopulateEventsListBox(true);
        }

        private void demoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Demote by increasing the level: the PrepareContextMenu method should
            // ensure that the user can only select this menu item when it is valid
            // to do so.
            iEventList.Demote();
            iEventListChanged = true;       // Remember that an edit has taken place

            // Repopulate the listbox with the revised levelling
            PopulateEventsListBox(true);
        }

        private void promoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Promote by decreasing the level: the PrepareContextMenu method should
            // ensure that the user can only select this menu item when it is valid
            // to do so.
            iEventList.Promote();
            iEventListChanged = true;       // Remember that an edit has taken place

            // Repopulate the listbox with the revised levelling
            PopulateEventsListBox(true);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentItem = eventsListBox.SelectedIndex;
            if ((currentItem >= 0) && (currentItem < eventsListBox.Items.Count))
            {
                string eventName = eventsListBox.Items[currentItem].ToString();
                DialogResult answer = MessageBox.Show("Delete \"" + eventName + "\"?", 
                                                      "PhotoStudio", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question,
                                                      MessageBoxDefaultButton.Button2);
                if (answer == DialogResult.Yes)
                {
                    iEventList.Delete();
                    iEventListChanged = true;       // Remember that an edit has taken place

                    // If we have just deleted the final item in the list box, we have
                    // to change the selection
                    if (currentItem == iEventList.Count)
                    {
                        eventsListBox.SelectedIndex--;
                    }

                    // Repopulate the listbox with the new shorter event list
                    PopulateEventsListBox(true);
                }
            }
            // else no event selected
        }

        // Want a right-click on the Listbox to cause a change in selection
        private void eventsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                eventsListBox.SelectedIndex = eventsListBox.IndexFromPoint(p);
            }
        }

        // End of session
        private void AlbumForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if current page has changed and might be saved
            CheckEventListEdits();


            // Check if pages have been added to the album, and if the top level album.xml
            // should be saved
            if (iAlbumChanged)
            {
                DialogResult response = MessageBox.Show("Album has been modified. Save changes?",
                                                        "PhotoStudio", MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);
                if (response == DialogResult.Yes)
                {
                    iAlbum.Save();
                }
            }
        }

        private void addExistingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentItem = eventsListBox.SelectedIndex;

            if ((currentItem < 0) || (currentItem >= eventsListBox.Items.Count))
            {
                // Not a valid selection, but cater for the special case in which there
                // is an empty EventList, hence nothing to select, but we do want to allow
                // an event to be added.
                if (iEventList.Count == 0)
                {
                    currentItem = 0;
                }
                else
                {
                    // Not a valid selection - don't allow event to be added
                    return;
                }
            }

            openSlideShowDialog.InitialDirectory = Path.GetDirectoryName(iEventList.Path);
            DialogResult res = openSlideShowDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                string slideShowPath = openSlideShowDialog.FileName;
                SlideShow newShow = new SlideShow();
                newShow.Load(slideShowPath);

                // Let the user title the slide show: but recall the existing full title
                SlideShowTitleForm titleForm = new SlideShowTitleForm(newShow.Title);
                DialogResult result = titleForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Save titled slide show to XML file
                    newShow.Title = titleForm.FullTitle;

                    iEventList.Insert(currentItem, titleForm.BriefTitle, newShow);
                    iEventListChanged = true;       // Remember that an edit has taken place

                    // Repopulate the listbox with the new longer event list
                    PopulateEventsListBox(true);
                }
            }
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentItem = eventsListBox.SelectedIndex;  // Get current selected item
            if ((currentItem < 0) || (currentItem >= eventsListBox.Items.Count))
            {
                // Not a valid selection, but cater for the special case in which there
                // is an empty EventList, hence nothing to select, but we do want to allow
                // an event to be added.
                if (iEventList.Count == 0)
                {
                    currentItem = 0;
                }
                else
                {
                    // Not a valid selection - don't allow event to be added
                    return;
                }
            }

            createSlideShowDialog.SelectedPath = Path.GetDirectoryName(iEventList.Path);
            DialogResult res = createSlideShowDialog.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                string slideShowFolder = createSlideShowDialog.SelectedPath;
                string currentDirectory = Path.GetDirectoryName(slideShowFolder);
                string subDirectory = Path.GetFileName(slideShowFolder);
                SlideShow newShow = SlideShow.Create(currentDirectory, subDirectory);
                if (newShow != null)
                {
                    // Let the user title the slide show
                    SlideShowTitleForm titleForm = new SlideShowTitleForm();
                    DialogResult result = titleForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        // Save titled slide show to XML file
                        newShow.Title = titleForm.FullTitle;
                        if (newShow.Save())
                        {
                            // Add the saved show to the Events List
                            iEventList.Insert(currentItem, titleForm.BriefTitle, newShow);
                            iEventListChanged = true;       // Remember that an edit has taken place

                            // Repopulate the listbox with the new longer event list
                            PopulateEventsListBox(true);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create slideshow", "PhotoStudio");
                        }
                    }

                    titleForm.Close();
                }
            }
        }

        private void addNonslideEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentItem = eventsListBox.SelectedIndex;

            if ((currentItem < 0) || (currentItem >= eventsListBox.Items.Count))
            {
                // Not a valid selection, but cater for the special case in which there
                // is an empty EventList, hence nothing to select, but we do want to allow
                // an event to be added.
                if (iEventList.Count == 0)
                {
                    currentItem = 0;
                }
                else
                {
                    // Not a valid selection - don't allow event to be added
                    return;
                }
            }

            NonSlideshowTitleForm titleForm = new NonSlideshowTitleForm();
            DialogResult result = titleForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                iEventList.Insert(currentItem, titleForm.Title, null);
                iEventListChanged = true;       // Remember that an edit has taken place

                // Repopulate the listbox with the new longer event list
                PopulateEventsListBox(true);
            }
        }

        private void toolStripButtonMoveUp_Click(object sender, EventArgs e)
        {
            moveUpToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonMoveDown_Click(object sender, EventArgs e)
        {
            moveDownToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonPromote_Click(object sender, EventArgs e)
        {
            promoteToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonDemote_Click(object sender, EventArgs e)
        {
            demoteToolStripMenuItem_Click(sender, e);
        }
    }
}
