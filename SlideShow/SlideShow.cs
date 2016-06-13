using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PhotoStudio
{
    // A caption is a list of lines of text, where each line should
    // be shown on a separate line
    [Serializable]
    public class Caption
    {
        // Accessor
        [XmlElement(ElementName = "Line")]
        public List<string> Lines { get; set; }

        // Constructor: create new empty caption
        public Caption()
        {
            Lines = new List<string>();
        }

        // Constructor: create caption with single line
        public Caption(string aText)
            : this()
        {
            AddLine(aText);
        }

        // Constructor: create caption with multiple lines
        public Caption(List<string> aLines)
        {
            Lines = aLines;
        }

        // Add a complete line to the caption
        public void AddLine(string aLine)
        {
            Lines.Add(aLine);
        }

        // Add a character to the current line.
        // Successive white space characters are collapsed into a single space:
        // this includes newlines.
        public void AddChar(char aChar)
        {
            // Determine current line
            if (Lines.Count == 0)
            {
                // Caption is empty: start with new blank line
                Lines.Add("");
            }

            if (char.IsWhiteSpace(aChar))
            {
                if ((Lines[Lines.Count - 1].Length == 0) || (Lines[Lines.Count - 1].EndsWith(" ")))
                {
                    // Do not add space to start of line, or immediately following a space
                }
                else
                {
                    Lines[Lines.Count - 1] += " ";
                }
            }
            else
            {
                Lines[Lines.Count - 1] += aChar;
            }
        }

        // Start new line in caption
        public void NewLine()
        {
            // Do not create a new line if the current line is empty
            if ((Lines.Count == 0) || (Lines[Lines.Count - 1].Length > 0))
            {
                Lines.Add("");
            }
        }
    }

    // Did not really want this class public, but need it for serialisation
    [Serializable]
    public class Slide : IComparable
    {
        Caption iCaption;

        [XmlElement(ElementName = "Photo")]
        public string Path { get; set; }

        [XmlArray(ElementName = "Caption")]
        [XmlArrayItem(ElementName = "Line")]
        public List<string> Line
        {
            get { return ( iCaption == null ? null : iCaption.Lines ); }
            set { iCaption = new Caption(value); }
        }

        public Slide(string aPath, Caption aCaption)
        {
            Path = aPath;
            iCaption = aCaption;
        }

        // Parameterless constructor needed for serialisation
        public Slide()
        {
            Path = null;
            iCaption = null;
        }

        /// <summary>
        /// Return the Caption (Lines) without any initial date/time
        /// </summary>
        /// <returns>Will always return a list rather than null. List may be empty.</returns>
        public List<string> Comments()
        {
            Regex longDateTime = new Regex( @"\w+ \d+ \w+ \d+, \d+:\d+" );
            Regex shortDateTime = new Regex( @"\w+, \d+:\d+" );
            if( ( iCaption == null ) || ( iCaption.Lines.Count == 0 ) )
            {
                return new List<string>();
            }

            if ( longDateTime.Match( iCaption.Lines[0] ).Success || shortDateTime.Match( iCaption.Lines[0] ).Success )
            {
                // Caption includes date/time
                if ( iCaption.Lines.Count == 1 )
                {
                    // Caption is only date/time
                    return new List<string>();
                }
                else
                {
                    return iCaption.Lines.GetRange( 1, iCaption.Lines.Count - 1 );
                }
            }
            else
            {
                // Caption does not include date/time
                return iCaption.Lines;
            }
        }

        // Method facilitates sorting as per IComparable interface.
        // Sort by path: effectively by filename.
        public int CompareTo(object aNother)
        {
            Slide otherSlide = (Slide)aNother;
            return Path.CompareTo(otherSlide.Path);
        }
    }

    [Serializable]
    public class SlideShow
    {
        string iFilePath;
        List<Slide> iShow;
        int iCurrent = 0;

        // Accessors
        [XmlAttribute]
        public string Title { get; set; }

        [XmlIgnore]
        public int Count
        {
            get { return ( iShow == null ? 0 : iShow.Count ); }
        }

        // Accessor for caption to current slide
        [XmlIgnore]
        public List<string> Lines
        {
            get { return ( (iCurrent < iShow.Count) ? iShow[iCurrent].Line : null ); }
            set
            {
                if (iCurrent < iShow.Count)
                {
                    iShow[iCurrent].Line = value;
                }
            }
        }

        // Did not want a public property here, but we need it for XML serialisation
        [XmlElement(ElementName = "Slide")]
        public List<Slide> Slides
        {
            get { return iShow; }
            set { iShow = value; }
        }


        // Constructor to build anonymous slide show
        public SlideShow()
        {
            Title = "A most peculiar day";
            iFilePath = null;
            iShow = new List<Slide>();
        }

        // Constructor to build slide show from named file
        public SlideShow(string aPath)
        {
            Title = "A most peculiar day";
            iFilePath = aPath;
            iShow = new List<Slide>();
        }

        // Load the slide show from an XML document
        public bool Load(string aPath)
        {
            iFilePath = aPath;
            if (!File.Exists(iFilePath))
            {
                MessageBox.Show("Cannot load " + iFilePath + " - no such file", "SlideShow");
                return false;
            }

            XmlSerializer s = new XmlSerializer(typeof(SlideShow));
            TextReader r = new StreamReader(iFilePath);

            // Wanted to say "this = s.Deserialize(r)", but it is illegal.
            // Instead load album into "loaded" and transcribe to "this".
            SlideShow loaded = s.Deserialize(r) as SlideShow;
            Title = loaded.Title;
            iShow = loaded.Slides;
            iCurrent = 0;
            r.Close();
            Reset();
            return true;
        }

        // Save the slide show as a new XML document
        public bool Save(string aPath)
        {
            iFilePath = aPath;

            XmlSerializer s = new XmlSerializer(typeof(SlideShow));
            TextWriter w = new StreamWriter(iFilePath);
            s.Serialize(w, this);
            w.Close();

            return true;
        }

        // Save the slide show to the previously determined location
        public bool Save()
        {
            return Save(iFilePath);
        }

        public string GetFilePath()
        {
            return iFilePath;
        }

        // Add a new slide to the sequence
        public void Add(string aPhotoPath, Caption aCaption)
        {
            iShow.Add(new Slide(aPhotoPath, aCaption));
        }

        // Reset the slide sequence
        public void Reset()
        {
            iCurrent = 0;
        }

        // Advance to the next slide
        public bool NextSlide()
        {
            if (++iCurrent < iShow.Count)
            {
                return true;
            }
            else
            {
                // Avoid letting iCurrent run off the end of the show
                iCurrent = iShow.Count - 1;
                return false;
            }
        }

        // Rewind to the previous slide
        public bool PrevSlide()
        {
            if (--iCurrent >= 0)
            {
                return true;
            }
            else
            {
                // Avoid letting iCurrent go negative
                iCurrent = 0;
                return false;
            }
        }

        // Return path to the current slide image
        public string GetPath()
        {
            if ((iCurrent >= 0) && (iCurrent < iShow.Count))
            {
                return iShow[iCurrent].Path;
            }
            else
            {
                return null;
            }
        }

        // Return path to the slide image relative to the current one
        public string GetPath(int aOffset)
        {
            int desired = iCurrent + aOffset;
            if ( (desired >= 0) && (desired < iShow.Count) )
            {
                return iShow[desired].Path;
            }
            else
            {
                return null;
            }
        }

        // Return path to the favourite slide image: set to the first image for now
        public string GetFavourite()
        {
            if (iShow.Count > 0)
            {
                return Path.Combine(Path.GetDirectoryName(iFilePath), iShow[0].Path);
            }
            else
            {
                return null;
            }
        }

        // Remove the current slide from the slide show.
        // This normally makes the next slide the current slide.
        // If we are deleting the final slide, the previous slide becomes the current slide.
        // The aRetracting flag is set if the final slide is deleted.
        public bool DeleteCurrent(bool aDeleteFile, out bool aRetracting)
        {
            aRetracting = false;

            if ((iCurrent >= 0) && (iCurrent < iShow.Count))
            {
                // Delete the image file, if required
                if (aDeleteFile)
                {
                    string unwantedFile = 
                        Path.Combine(Path.GetDirectoryName(iFilePath), iShow[iCurrent].Path);

                    // Apparently no exception is thrown if the file does not exist, which
                    // might be the reason why the user has chosen to delete the slide
                    File.Delete(unwantedFile);
                }

                // Remove the slide from the show
                iShow.RemoveAt(iCurrent);

                // If we have just deleted the final slide, we need to retract the current slide
                if (iCurrent >= iShow.Count)
                {
                    iCurrent--;
                    aRetracting = true;
                }

                return true;
            }
            else
            {
                // No such slide, cannot delete
                return false;
            }
        }

        // Return a list of additional pictures in the file path for this slide show
        // which are not in the slide show
        public List<string> GetUnincludedPictures()
        {
            List<string> fileList = new List<string>();

            // Deduce the picture folder from the slideshow file path
            // NOT NECESSARILY VALID
            string mainPath = Path.GetDirectoryName(iFilePath);
            string pictureFolder = Path.GetFileNameWithoutExtension(iFilePath);
            string picturePath = Path.Combine(mainPath, pictureFolder);

            foreach (FileInfo file in new DirectoryInfo(picturePath).GetFiles())
            {
                if (IsPhoto(file) && !IsInShow(file.ToString()))
                {
                    fileList.Add(file.ToString());
                }
            }

            return fileList;
        }

        // aSubDirectory is the name of a folder in the aCurrentDirectory which contains
        // the photos to make up a new slide show
        static public SlideShow Create(string aCurrentDirectory, string aSubDirectory)
        {
            string slideShowFolder = Path.Combine(aCurrentDirectory, aSubDirectory);
            string slideShowFilename = slideShowFolder + ".xml";
            if (File.Exists(slideShowFilename))
            {
                DialogResult response = MessageBox.Show("Slide show already exists for this folder. Overwrite?",
                                                        "PhotoStudio", MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button2);
                if (response == DialogResult.No)
                {
                    // User declined to let existing file be overwritten - cancel operation
                    return null;
                }
            }
            SlideShow newShow = new SlideShow(slideShowFilename);
            int numPictures = newShow.PopulateFromFolder();

            if (numPictures > 0)
            {
                // SlideShow created in memory - not saved yet - this is the responsibility
                // of the caller, which is probably going to modify the SlideShow first.
                return newShow;
            }
            else
            {
                MessageBox.Show("No pictures found: slide show not created", "PhotoStudio");
                return null;
            }
        }

        // Add the images in the folder to the slide show.
        // Return the number of images found.
        public int PopulateFromFolder()
        {
            string mainPath = Path.GetDirectoryName(iFilePath);
            string subDirectory = Path.GetFileNameWithoutExtension(iFilePath);
            string slideShowFolder = Path.Combine(mainPath, subDirectory);

            int dayOfPrevSlide = -1;
            foreach (FileInfo file in new DirectoryInfo(slideShowFolder).GetFiles())
            {
                if (IsPhoto(file))
                {
                    string relativePath = Path.Combine(subDirectory, file.ToString());
                    Caption caption = null;
                    DateTime dateTime;
                    if (ParseDateName(file.ToString(), out dateTime))
                    {
                        string day = dateTime.DayOfWeek.ToString();
                        string date = dateTime.ToLongDateString();
                        caption = new Caption();

                        int thisDay = dateTime.DayOfYear;
                        if (thisDay == dayOfPrevSlide)
                        {
                            // Same day: abbreviated date in caption
                            caption.AddLine(string.Format("{0}, {1:HH}:{1:mm}", day, dateTime));
                        }
                        else
                        {
                            // New day: include full date in caption
                            caption.AddLine(string.Format("{0} {1}", day, date));
                            caption.AddLine(string.Format("{0:HH}:{0:mm}", dateTime));
                            dayOfPrevSlide = thisDay;
                        }
                    }

                    // Add images provided they are not already in the slideshow.
                    // This enables new images to be added to an existing slideshow.
                    if (!IsInShow(file.ToString()))
                    {
                        Add(relativePath, caption);
                    }
                }
            }

            // In case we are adding new slides to an existing show, sort the final set
            // to ensure that the new slides appear in the correct order
            iShow.Sort();

            return iShow.Count;
        }

        // Determine whether this SlideShow includes the supplied filename
        private bool IsInShow(string aFilename)
        {
            foreach (Slide slide in iShow)
            {
                if (Path.GetFileName(slide.Path).Equals(aFilename, 
                                                        StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;            // Found the slide
                }
            }

            return false;                   // No slides match this filename
        }

        private static bool IsPhoto(FileInfo file)
        {
            return file.Name.EndsWith(".jpg", true, null);
        }

        // Determine whether the filename is a datename: return true if so, and populate the DateTime
        private static bool ParseDateName(string aFileName, out DateTime aDateTime)
        {
            aDateTime = new DateTime();
            int year = 0;
            int month = 0;
            int day = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            // 23 characters caters for a regular DateName, 24 characters caters for a suffix
            // which can be ignored. Any other length of filename implies not a DateName and
            // therefore no date/time information can be derived.
            if ( ( (aFileName.Length == 23) || (aFileName.Length == 24) ) &&
                 int.TryParse(aFileName.Substring(0, 4), out year) &&
                 int.TryParse(aFileName.Substring(5, 2), out month) &&
                 int.TryParse(aFileName.Substring(8, 2), out day) &&
                 int.TryParse(aFileName.Substring(11, 2), out hours) &&
                 int.TryParse(aFileName.Substring(14, 2), out minutes) &&
                 int.TryParse(aFileName.Substring(17, 2), out seconds))
            {
                aDateTime = new DateTime(year, month, day, hours, minutes, seconds);
                return true;
            }
            else
            {
                aDateTime = new DateTime();
                return false;
            }
        }
    }
}
