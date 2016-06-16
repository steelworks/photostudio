using Registry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PhotoStudio
{
    public partial class formMain : Form
    {
        const int folderIcon = 0;
        const int fileIcon = 1;
        const int photoIcon = 2;
        SlideViewerParameters iSlideParameters = null;

        public formMain()
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

            PopulateTreeView();
            iSlideParameters = new SlideViewerParameters(PhotoStudioRegistry.SlideSpeed,
                                                         PhotoStudioRegistry.ShowCaptions);
        }

        private void PopulateTreeView()
        {
            TreeNode computer;
            computer = new TreeNode("Computer");
            computer.Tag = "Computer";
            treeView.Nodes.Add(computer);

            // preferredPosition corresponds to the selected directory when the program was last run
            string preferredPosition = PhotoStudioRegistry.BrowseDirectory;
            this.Size = PhotoStudioRegistry.CurrentSize;
            this.Location = PhotoStudioRegistry.CurrentPosition;

            for (char drive = 'C'; drive <= 'Z'; drive++)
            {
                DirectoryInfo driveRoot = new DirectoryInfo(drive + @":\");
                if (driveRoot.Exists)
                {
                    TreeNode driveNode = new TreeNode(driveRoot.Name);
                    driveNode.Tag = driveRoot;

                    // No recursion: all we do is add the subnodes of the drive root: this allows
                    // the root to be expanded, and when it is expanded, then we will add the
                    // next level of subnodes.
                    TreeNode dest = GetDirectories(driveRoot.GetDirectories(), driveNode, preferredPosition);
                    computer.Nodes.Add(driveNode);
                    if (dest != null)
                    {
                        // The preferredPosition path, or at least part of it, is available, signified
                        // by dest. Select the dest node and populate the listbox.
                        treeView.SelectedNode = dest;
                        ListFolderContents((DirectoryInfo)dest.Tag);
                    }
                }
            }
        }

        // Get the subdirectories of the supplied list of subdirectories.
        // navigateTo is the node that we would like to select: return its
        // closest match.
        private TreeNode GetDirectories(DirectoryInfo[] aSubDirs, TreeNode aNodeToAddTo, string aNavigateTo)
        {
            TreeNode bestMatch = null;
            TreeNode aNode;
            string diagnostic = "";
            foreach (DirectoryInfo subDir in aSubDirs)
            {
                diagnostic += subDir.Name + ", ";
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Name = subDir.Name;
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                if ((subDir.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == 0)
                {
                    // Setting treeView.SelectedNode automatically causes expansion and calling
                    // of treeView_BeforeExpand.
                    // Need to ensure node is added only once: alternatively remove and re-add
                    // sub-nodes.
                    // Expand causes treeView_BeforeExpand to be invoked, which adds grandchildren.
                    aNodeToAddTo.Nodes.Add(aNode);
                    if (aNavigateTo.StartsWith(subDir.FullName, true, null))
                    {
                        DirectoryInfo[] subSubDirs = subDir.GetDirectories();
                        if (subSubDirs.Length != 0)
                        {
                            TreeNode deeperMatch = GetDirectories(subSubDirs, aNode, aNavigateTo);
                            if (deeperMatch != null)
                            {
                                // Deeper match is best
                                bestMatch = deeperMatch;
                            }
                        }

                        if (bestMatch == null)
                        {
                            // Partial match is better than none
                            bestMatch = aNode;
                        }
                    }
                }
            }

            //MessageBox.Show("GetDirectories adding " + diagnostic);

            return bestMatch;
        }

        bool IsImageOrVideo(FileInfo file)
        {
            string[] fileTypes = new string[] { ".jpg", ".gif", ".png", ".avi", ".wmv" };
            foreach ( string imageType in fileTypes )
            {
                if ( file.Name.ToLower().EndsWith( imageType, true, null ) )
                {
                    return true;
                }
            }

            return false;
        }

        bool IsHtml(FileInfo file)
        {
            return (file.Name.EndsWith(".htm", true, null) || file.Name.EndsWith(".html", true, null));
        }

        bool IsXml(FileInfo file)
        {
            return file.Name.EndsWith(".xml", true, null);
        }

        bool IsDateNamed(FileInfo file)
        {
            char [] chars = file.Name.ToCharArray();

            // Date named files normally have 23 characters, 24 if a suffix is required
            if( (chars.Length < 23) ||  (chars.Length > 24 ) )
            {
                return false;       // Wrong length: can't be date-named
            }

            // Assume that if the hyphens and underscore match, the intervening
            // characters are digits
            //return ( chars[4] == '-' ) && ( chars[7] == '-' ) && ( chars[10] == '_' ) &&
            //       ( chars[13] == '-' ) && ( chars[16] == '-' );
            for( int i = 0; i < chars.Length; i++ )
            {
                //                           yyyy   -        mm     -      dd      _      hh     -       mm     -  ss       suffix
                if ( !Char.IsDigit( chars[i] ) && ( i != 4 ) && ( i != 7 ) && ( i != 10 ) && ( i != 13 ) && ( i != 16 ) && ( i != 19 ) )
                {
                    return false;
                }
            }

            return true;
        }

        string DateName(FileInfo aThisFile)
        {
            // CreationTime property does not return the expected value
            //DateTime creationTime = thisFile.CreationTime;
            DateTime creationTime = aThisFile.LastWriteTime;
            int year = creationTime.Year;
            int month = creationTime.Month;
            int day = creationTime.Day;
            int hours = creationTime.Hour;
            int minutes = creationTime.Minute;
            int seconds = creationTime.Second;
            return string.Format("{0:D4}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}{6}",
                                 year, month, day, hours, minutes, seconds, aThisFile.Extension);
        }

        string DateName(FileInfo aThisFile, char aSuffix)
        {
            // CreationTime property does not return the expected value
            //DateTime creationTime = thisFile.CreationTime;
            DateTime creationTime = aThisFile.LastWriteTime;
            int year = creationTime.Year;
            int month = creationTime.Month;
            int day = creationTime.Day;
            int hours = creationTime.Hour;
            int minutes = creationTime.Minute;
            int seconds = creationTime.Second;
            return string.Format("{0:D4}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}{6}.jpg",
                                 year, month, day, hours, minutes, seconds, aSuffix);
        }

        void DateFromName( string aFilename, out int aYear, out int aMonth, out int aDay,
                           out int aHours, out int aMinutes, out int aSeconds )
        {
            aYear = int.Parse( aFilename.Substring( 0, 4 ) );
            aMonth = int.Parse( aFilename.Substring( 5, 2 ) );
            aDay = int.Parse( aFilename.Substring( 8, 2 ) );
            aHours = int.Parse( aFilename.Substring( 11, 2 ) );
            aMinutes = int.Parse( aFilename.Substring( 14, 2 ) );
            aSeconds = int.Parse( aFilename.Substring( 17, 2 ) );
        }

        DateTime MakeDateTimeFromName( string aFilename )
        {
            int year;
            int month;
            int day;
            int hours;
            int minutes;
            int seconds;
            DateFromName( aFilename, out year, out month, out day, out hours, out minutes, out seconds );
            return new DateTime( year, month, day, hours, minutes, seconds );
        }

        void ListFolderContents(DirectoryInfo nodeDirInfo)
        {
            listView.Items.Clear();
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, folderIcon);
                item.ToolTipText = "Directory " + dir.Name;
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"), 
                     new ListViewItem.ListViewSubItem(item, "")};
                item.SubItems.AddRange(subItems);
                listView.Items.Add(item);
            }

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                if (IsImageOrVideo(file))
                {
                    item = new ListViewItem(file.Name, photoIcon);

                    // Avoid re-datenaming an already datenamed file - the date/time stamp on
                    // the file may have changed since the original datenaming.
                    string dateNamed = IsDateNamed( file ) ? file.Name : DateName( file );

                    item.ToolTipText = "Photo " + dateNamed;
                    subItems = new ListViewItem.ListViewSubItem[]
                        { new ListViewItem.ListViewSubItem(item, "Photo"), 
                         new ListViewItem.ListViewSubItem(item, dateNamed)};
                }
                else if (IsHtml(file))
                {
                    item = new ListViewItem(file.Name, fileIcon);
                    item.ToolTipText = "HTML " + DateName(file);
                    subItems = new ListViewItem.ListViewSubItem[]
                        { new ListViewItem.ListViewSubItem(item, "HTML"), 
                         new ListViewItem.ListViewSubItem(item, "Convertible?")};
                }
                else if (IsXml(file))
                {
                    item = new ListViewItem(file.Name, fileIcon);
                    item.ToolTipText = "XML " + DateName(file);
                    subItems = new ListViewItem.ListViewSubItem[]
                        { new ListViewItem.ListViewSubItem(item, "XML"), 
                         new ListViewItem.ListViewSubItem(item, "Slide show?")};
                }
                else
                {
                    item = new ListViewItem(file.Name, fileIcon);
                    item.ToolTipText = "File " + DateName(file);
                    subItems = new ListViewItem.ListViewSubItem[]
                        { new ListViewItem.ListViewSubItem(item, "File"), 
                         new ListViewItem.ListViewSubItem(item, "")};
                }

                item.SubItems.AddRange(subItems);
                listView.Items.Add(item);
            }

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            PhotoStudioRegistry.BrowseDirectory = nodeDirInfo.FullName;
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            if (newSelected.Tag.Equals("Computer"))
            {
                listView.Items.Clear();
            }
            else
            {
                ListFolderContents((DirectoryInfo)newSelected.Tag);
            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // The tree already contains the children of this node, otherwise the branch
            // cannot be expanded. At this point we want to learn and add the grandchildren
            // of this node, so that the subnodes can be displayed and expanded
            TreeNode nodeToExpand = e.Node;
            string diagnostic = "";
            foreach (TreeNode subNode in nodeToExpand.Nodes)
            {
                DirectoryInfo nodeDirInfo = (DirectoryInfo)subNode.Tag;
                try
                {
                    foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
                    {
                        TreeNode subSubNode = new TreeNode(dir.Name, 0, 0);
                        subSubNode.Name = dir.Name;
                        subSubNode.Tag = dir;
                        subSubNode.ImageKey = "folder";
                        if ((dir.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == 0)
                        {
                            // Avoid dupllicates: only add if grandchild not already in tree
                            if (!subNode.Nodes.ContainsKey(dir.Name))
                            {
                                subNode.Nodes.Add(subSubNode);
                            }
                            diagnostic += dir + ", ";
                        }
                    }
                }
                catch (UnauthorizedAccessException fred)
                {
                    // Ignore this directory
                    fred.Message.ToString();
                }
            }
        }

        private void dateNameButton_Click(object sender, EventArgs e)
        {
            DirectoryInfo currentDirectory = (DirectoryInfo)treeView.SelectedNode.Tag;

            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                // Avoid re-datenaming an already datenamed file - the date/time stamp on
                // the file may have changed since the original datenaming.
                if ( IsImageOrVideo( file ) && !IsDateNamed( file ) )
                {
                    string originalName = file.FullName;
                    string newName = originalName.Replace(file.Name, DateName(file));

                    // If using a simple dateName YYYY-MM-DD_HH-MM-SS, duplicate names
                    // can arise, particularly if the camera was used in burst mode.
                    if (File.Exists(newName))
                    {
                        char suffix = 'a';
                        do
                        {
                            newName = originalName.Replace(file.Name, DateName(file, suffix));
                            suffix++;
                        }
                        while (File.Exists(newName));
                    }

                    file.MoveTo(newName);
                }
            }

            ListFolderContents(currentDirectory);
        }

        private void hourPlusButton_Click(object sender, EventArgs e)
        {
            // Crude - not satisfactory on day boundary
            DirectoryInfo currentDirectory = (DirectoryInfo)treeView.SelectedNode.Tag;

            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                if (IsImageOrVideo(file) && IsDateNamed(file))
                {
                    int year, month, day, hours, minutes, seconds;
                    DateFromName( file.Name, out year, out month, out day,
                                  out hours, out minutes, out seconds );
                    hours++;
                    string dateName = 
                            string.Format("{0:D4}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}.jpg",
                                          year, month, day, hours, minutes, seconds);
                    string newName = file.FullName.Replace(file.Name, dateName);
                    file.MoveTo(newName);
                }
            }

            ListFolderContents(currentDirectory);
        }

        private void hourMinusButton_Click(object sender, EventArgs e)
        {
            // Crude - not satisfactory on day boundary
            DirectoryInfo currentDirectory = (DirectoryInfo)treeView.SelectedNode.Tag;

            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                if (IsImageOrVideo(file) && IsDateNamed(file))
                {
                    int year, month, day, hours, minutes, seconds;
                    DateFromName( file.Name, out year, out month, out day,
                                  out hours, out minutes, out seconds );
                    hours--;
                    string dateName =
                            string.Format("{0:D4}-{1:D2}-{2:D2}_{3:D2}-{4:D2}-{5:D2}.jpg",
                                          year, month, day, hours, minutes, seconds);
                    string newName = file.FullName.Replace(file.Name, dateName);
                    file.MoveTo(newName);
                }
            }

            ListFolderContents(currentDirectory);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems =
                        this.listView.SelectedItems;
            if (selectedItems.Count == 1)
            {
                string filename = selectedItems[0].Text;
                if (selectedItems[0].SubItems[1].Text.Equals("Directory"))
                {
                    listView.ContextMenuStrip = contextMenuFolder;
                }
                else if (IsImageOrVideo(new FileInfo(filename)))
                {
                    listView.ContextMenuStrip = contextMenuPhoto;
                }
                else if (IsHtml(new FileInfo(filename)))
                {
                    listView.ContextMenuStrip = contextMenuHtml;
                }
                else if (IsXml(new FileInfo(filename)))
                {
                    listView.ContextMenuStrip = contextMenuXml;
                }
                else
                {
                    listView.ContextMenuStrip = null;
                }

            }
        }

        // Trace the path from the leaf to the root and return the full path
        private string TreePath(TreeNode aLeaf)
        {
            if ((aLeaf.Parent == null) || 
                (aLeaf.Parent.Text.Equals("Computer", StringComparison.CurrentCultureIgnoreCase) ) )
            {
                // We are at the root
                return aLeaf.Text;
            }
            else
            {
                // Too complex: want just a single slash wherever there should be a single slash
                string path = TreePath(aLeaf.Parent) + "\\" + aLeaf.Text;
                path = path.Replace("\\\\", "\\");
                return path;
            }
        }

        void playMenuItem_Click(object sender, EventArgs e)
        {
            // It should only be possible to invoke the Play menu item for an XML
            // file, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems =
                        this.listView.SelectedItems;
            if (selectedItems.Count == 1)
            {
                string filename = selectedItems[0].Text;
                if (IsXml(new FileInfo(filename)))
                {
                    string currentDirectory = TreePath(treeView.SelectedNode);

                    string filetype = Path.GetFileNameWithoutExtension(filename);
                    if (filetype.Equals("album", StringComparison.CurrentCultureIgnoreCase))
                    {
                        AlbumForm albumForm = new AlbumForm(true, iSlideParameters);
                        albumForm.LoadAlbum(currentDirectory);
                        albumForm.ShowDialog();
                    }
                    else if (filetype.Equals("events", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Display the events for the selected year so that the user can choose a slide show
                        EventsForm eventsForm = new EventsForm(iSlideParameters);
                        eventsForm.LoadEvents(currentDirectory, filename);
                        eventsForm.ShowDialog();
                    }
                    else
                    {
                        // Not an album nor an events page: must be a slide show
                        SlideViewerForm slideScreen = new SlideViewerForm(iSlideParameters);
                        slideScreen.LoadShow(currentDirectory, filename);
                        slideScreen.ShowDialog();
                    }
                }
            }
        }

        void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // It should only be possible to invoke the Edit menu item for an XML
            // file, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems =
                        this.listView.SelectedItems;
            if (selectedItems.Count == 1)
            {
                string filename = selectedItems[0].Text;
                if (IsXml(new FileInfo(filename)))
                {
                    string currentDirectory = TreePath(treeView.SelectedNode);

                    string filetype = Path.GetFileNameWithoutExtension(filename);
                    if (filetype.Equals("album", StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show("Edit album: not implemented");
                        //AlbumForm albumForm = new AlbumForm();
                        //albumForm.LoadAlbum(currentDirectory);
                        //albumForm.ShowDialog();
                    }
                    else if (filetype.Equals("events", StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show("Edit event list: not implemented");
                        //EventsForm eventsForm = new EventsForm();
                        //eventsForm.LoadEvents(currentDirectory, filename);
                        //eventsForm.ShowDialog();
                    }
                    else
                    {
                        // Not an album nor an events page: must be a slide show
                        SlideShowEditor slideEditor = new SlideShowEditor();
                        slideEditor.LoadShow(currentDirectory, filename);
                        slideEditor.ShowDialog();
                    }
                }
            }

        }

        /// <summary>
        /// Verify that all catalogued images exist, and report on any non-catalogued images
        /// </summary>
        void validateToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // All images recorded in the album
            List<string> cataloguedImages = new List<string>();

            // Images recorded in the album but not physically present
            List<string> unpresentImages = new List<string>();

            // Images physically present but not recorded in the album
            List<string> uncataloguedImages = new List<string>();

            // It should only be possible to invoke the Apply Properties menu item for an XML
            // file, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
            if ( selectedItems.Count == 1 )
            {
                string filename = selectedItems[0].Text;
                if ( IsXml( new FileInfo( filename ) ) )
                {
                    string currentDirectory = TreePath( treeView.SelectedNode );

                    string filetype = Path.GetFileNameWithoutExtension( filename );
                    if ( filetype.Equals( "album", StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        Album album = new Album( Path.Combine( currentDirectory, filename ) );
                        album.Load();
                        foreach ( Year year in album.Events )
                        {
                            EventList catalogue = new EventList( year.Path );
                            if ( catalogue.Load() )
                            {
                                string yearFolder = Path.GetDirectoryName( year.Path );
                                foreach ( Event happening in catalogue.Events )
                                {
                                    // Title events have no slideshow
                                    if ( happening.SlideShow != null )
                                    {
                                        string eventFolder = Path.GetDirectoryName( happening.Path );
                                        foreach ( var slide in happening.SlideShow.Slides )
                                        {
                                            string fullPath = Path.Combine( eventFolder, slide.Path );
                                            cataloguedImages.Add( fullPath.ToLower() );
                                            if ( !File.Exists( fullPath ) )
                                            {
                                                unpresentImages.Add( fullPath );
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show( "Failed to load events for " + year.Name, "Year" );
                            }
                        }

                        foreach ( string presentImage in
                                  Directory.GetFiles( currentDirectory, "*.jpg", SearchOption.AllDirectories ) )
                        {
                            if( !cataloguedImages.Contains( presentImage.ToLower() ) )
                            {
                                uncataloguedImages.Add( presentImage );
                            }
                        }

                        if ( ( unpresentImages.Count == 0 ) && ( uncataloguedImages.Count == 0 ) )
                        {
                            MessageBox.Show( "Validated" );
                        }
                        else
                        {
                            ValidationForm problems = new ValidationForm( unpresentImages, uncataloguedImages );
                            problems.Show();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Need pre-validation - for any image files which are not catalogued, and any catalogued images that do not exist
        /// </summary>
        void ApplyPropertiesMenuItem_Click( object sender, EventArgs e )
        {
            // It should only be possible to invoke the Apply Properties menu item for an XML
            // file, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
            if ( selectedItems.Count == 1 )
            {
                string filename = selectedItems[0].Text;
                if ( IsXml( new FileInfo( filename ) ) )
                {
                    string currentDirectory = TreePath( treeView.SelectedNode );

                    string filetype = Path.GetFileNameWithoutExtension( filename );
                    if ( filetype.Equals( "album", StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        Album album = new Album( Path.Combine( currentDirectory, filename ) );
                        album.Load();
                        Application.DoEvents();
                        foreach ( Year year in album.Events )
                        {
                            iLabelYear.Text = year.Name;
                            iLabelYear.Visible = true;

                            EventList catalogue = new EventList( year.Path );
                            if ( catalogue.Load() )
                            {
                                iProgressBar.Maximum = catalogue.Events.Count;
                                iProgressBar.Value = 0;
                                iProgressBar.Visible = true;

                                foreach ( Event happening in catalogue.Events )
                                {
                                    // Title events have no slideshow
                                    if ( happening.SlideShow != null )
                                    {
                                        ApplyPropertiesToSlideshow( happening.Path );
                                    }

                                    iProgressBar.Value++;
                                    Application.DoEvents();
                                }
                            }
                            else
                            {
                                MessageBox.Show( "Failed to load events for " + year.Name, "Year" );
                            }
                        }
                    }
                    else if ( filetype.Equals( "events", StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        EventList catalogue = new EventList( Path.Combine( currentDirectory, filename ) );
                        if( catalogue.Load() )
                        {
                            iProgressBar.Maximum = catalogue.Events.Count;
                            iProgressBar.Value = 0;
                            iProgressBar.Visible = true;

                            iLabelYear.Text = catalogue.Title;
                            iLabelYear.Visible = true;
                            foreach ( Event happening in catalogue.Events )
                            {
                                // Title events have no slideshow
                                if ( happening.SlideShow != null )
                                {
                                    ApplyPropertiesToSlideshow( happening.Path );
                                }

                                iProgressBar.Value++;
                                Application.DoEvents();
                            }
                        }
                        else
                        {
                            MessageBox.Show( "Failed to load events for " + catalogue.Name, "Events" );
                        }
                    }
                    else
                    {
                        ApplyPropertiesToSlideshow( Path.Combine( filename, currentDirectory ) );
                    }
                }

                iLabelYear.Visible = false;
                iProgressBar.Visible = false;
            }
        }

        /// <summary>
        /// Apply properties to a single slide show in the specified folder
        /// </summary>
        void ApplyPropertiesToSlideshow( string aSlideShowPath )
        {
            //MessageBox.Show( "Applying album data to image properties for a slide show: " + filename );
            SlideShow slideShow = new SlideShow();
            slideShow.Load( aSlideShowPath );
            foreach ( Slide slide in slideShow.Slides )
            {
                string directory = Path.GetDirectoryName( aSlideShowPath );
                string fullPathToSlide = Path.Combine( directory, slide.Path );
                string baseFileName = Path.GetFileNameWithoutExtension( slide.Path );
                //string operation = "Set Date/Time to: " + baseFileName;
                //operation += Environment.NewLine;
                //operation += "Set title/caption to: " + slideShow.Title;
                //operation += Environment.NewLine;
                //operation += "Set comments to: " + string.Join( Environment.NewLine, slide.Comments() );

                //// USE THE METADATA CLASS TO APPLY THESE CHANGES TO THE IMAGE
                //MessageBox.Show( operation, "Apply properties to: " + slide.Path );

                // Some files are not date-named. Ideally we would like at least a year.
                DateTime actualDateTime = new DateTime();
                bool isDateNamed = IsDateNamed( new FileInfo( baseFileName ) );
                MetaData metaData = new MetaData( fullPathToSlide );
                if ( isDateNamed )
                {
                    actualDateTime = MakeDateTimeFromName( baseFileName );
                    metaData.ApplyPropertyItem( "DateTime", actualDateTime.ToString() );
                }
                metaData.ApplyPropertyItem( "ImageTitle", slideShow.Title );
                metaData.ApplyPropertyItem( "ImageDescription", string.Join( Environment.NewLine, slide.Comments() ) );
                metaData.ApplyPropertyItem( "ExifUserComment", string.Join( Environment.NewLine, slide.Comments() ) );

                metaData.Save();

                if ( isDateNamed )
                {
                    // Want one or both of these
                    File.SetCreationTime( fullPathToSlide, actualDateTime );
                    File.SetLastWriteTime( fullPathToSlide, actualDateTime );
                }
            }
        }

        void convertMenuItem_Click(object sender, EventArgs e)
        {
            // It should only be possible to invoke the Convert menu item for an HTML
            // file, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems =
                        this.listView.SelectedItems;
            if (selectedItems.Count == 1)
            {
                string filename = selectedItems[0].Text;
                if (IsHtml(new FileInfo(filename)))
                {
                    // Parse HTML file
                    string currentDirectory = TreePath(treeView.SelectedNode);
                    string htmlPath = currentDirectory + "\\" + filename;
                    HtmlReader htmlReader = new HtmlReader();
                    string diagnostic = null;
                    string baseFilename = Path.GetFileNameWithoutExtension(filename);
                    string xmlFilename = Path.Combine(currentDirectory, baseFilename + ".xml");

                    if (baseFilename.Equals("yearframe", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Create album from parsing
                        Album album = htmlReader.ReadAlbum(htmlPath, out diagnostic);
                        if (album == null)
                        {
                            MessageBox.Show("Failed to read HTML file\n" + diagnostic, "PhotoStudio");
                        }
                        else
                        {
                            // Save album to XML file
                            xmlFilename = currentDirectory + "\\album.xml";
                            album.Save(xmlFilename);
                            MessageBox.Show("Album converted", "PhotoStudio");
                        }
                    }
                    else if (baseFilename.Equals("events", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Create event list from parsing
                        EventList events = htmlReader.ReadEvents(htmlPath, out diagnostic);
                        if (events == null)
                        {
                            MessageBox.Show("Failed to read HTML file\n" + diagnostic, "PhotoStudio");
                        }
                        else
                        {
                            // Save event list to XML file
                            events.Save(true);
                            MessageBox.Show("Events converted", "PhotoStudio");
                        }
                    }
                    else
                    {
                        // Create slide show from parsing
                        SlideShow slideShow = htmlReader.ReadSlideShow(htmlPath, out diagnostic);
                        if (slideShow == null)
                        {
                            MessageBox.Show("Failed to read HTML file\n" + diagnostic, "PhotoStudio");
                        }
                        else
                        {
                            // Save slide show to XML file
                            slideShow.Save(xmlFilename);
                            MessageBox.Show("SlideShow converted", "PhotoStudio");
                        }
                    }

                    // Refresh display (should be a new XML file)
                    ListFolderContents(new DirectoryInfo(currentDirectory));
                }
            }
        }

        private void createSlideshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // It should only be possible to invoke the Create Slideshow menu item for a
            // folder, but we will check to make sure
            ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
            if (selectedItems.Count == 1)
            {
                if (selectedItems[0].SubItems[1].Text.Equals("Directory"))
                {
                    string currentDirectory = TreePath(treeView.SelectedNode);
                    string subDirectory = selectedItems[0].SubItems[0].Text;
                    SlideShow newShow = SlideShow.Create(currentDirectory, subDirectory);
                    if (newShow != null)
                    {
                        // Note: slide show has no title - could copy code from AlbumForm
                        // addNewToolStripMenuItem_Click method
                        if (newShow.Save())
                        {
                            // Refresh display (should be a new XML file)
                            MessageBox.Show("SlideShow created", "PhotoStudio");
                            ListFolderContents(new DirectoryInfo(currentDirectory));
                        }
                        else
                        {
                            MessageBox.Show("Failed to create slideshow", "PhotoStudio");
                        }
                    }
                }
            }
        }
    }
}
