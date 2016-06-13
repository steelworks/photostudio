using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace PhotoStudio
{
    [Serializable]
    public class Year : IComparable
    {
        string iName = "A most peculiar year";
        string iFilePath;                       // Path to Events.xml
        EventList iEvents;


        // Accessors

        [XmlAttribute]
        public string Name
        {
            get
            {
                return iName;
            }
            set
            {
                iName = value;
            }
        }

        [XmlElement(ElementName = "Events")]
        public string Path
        {
            get
            {
                return iFilePath;
            }
            set
            {
                iFilePath = value;
            }
        }

        [XmlIgnore]
        public EventList Events
        {
            get
            {
                return iEvents;
            }
            set
            {
                iEvents = value;
            }
        }


        // Constructor: start with an empty year
        public Year()
        {
            iEvents = new EventList();
        }

        // Constructor: predefined year
        public Year(EventList aEvents)
        {
            iName = aEvents.Title;
            iFilePath = aEvents.Path;
            iEvents = aEvents;

            // Special case - it's new so does not need loading, but want to treat
            // it as loaded
            iEvents.Loaded = true;
        }

        // Method facilitates sorting as per IComparable interface
        public int CompareTo(object aNother)
        {
            Year otherYear = (Year)aNother;
            return this.Name.CompareTo(otherYear.Name);
        }
    }

    [Serializable]
    public class Album
    {
        string iTitle = "Everything that ever happened";
        string iFilePath;
        List<Year> iYear;       // Each event list is probably, though not necessarily, a year
        int iCurrent = -1;      // Index of current year

        // Accessor
        [XmlAttribute]
        public string Title
        {
            get { return iTitle; }
            set { iTitle = value; }
        }

        // Don't need this property - nothing uses it!
        [XmlElement(ElementName = "Year")]
        public List<Year> Events
        {
            get { return iYear; }
            set { iYear = value; }
        }

        [XmlIgnore]
        public List<string> Years
        {
            get
            {
                // Return a list of the names of the years
                List<string> names = new List<string>();
                foreach (Year year in iYear)
                {
                    names.Add(year.Name);
                }

                return names;
            }
        }


        // Constructor to build album from file
        public Album(string aPath)
        {
            // Path to XML document
            iFilePath = aPath;

            iYear = new List<Year>();
        }

        // Parameterless constructor required for serialisation
        Album()
        {
        }

        // Load the events from an XML document
        public bool Load()
        {
            if (!File.Exists(iFilePath))
            {
                MessageBox.Show("Cannot load " + iFilePath + " - no such file", "Album");
                return false;
            }

            XmlSerializer s = new XmlSerializer(typeof(Album));
            TextReader r = new StreamReader(iFilePath);

            // Wanted to say "this = s.Deserialize(r)", but it is illegal.
            // Instead load album into "loaded" and transcribe to "this".
            Album loaded = s.Deserialize(r) as Album;
            iTitle = loaded.Title;
            iYear = loaded.Events;
            r.Close();

            // Loaded the top level. Recurse to load each year.
            foreach (Year year in iYear)
            {
                year.Events.Title = year.Name;
                year.Events.Path = year.Path;

                // For quicker performance, defer loading until needed
                //year.Events.Load();
            }

            return true;
        }

        // Private method to save the album.
        // The aRecursive parameter determines whether to recurse and save
        // the events lists as well.
        private bool Save(bool aRecursive)
        {
            XmlSerializer s = new XmlSerializer(typeof(Album));
            TextWriter w = new StreamWriter(iFilePath);
            s.Serialize(w, this);
            w.Close();

            if (aRecursive)
            {
                foreach (Year year in iYear)
                {
                    if (year.Events.Loaded)
                    {
                        // Save the event list
                        year.Events.Save(aRecursive);
                    }
                }
            }

            return true;
        }

        // Save the album as an XML document.
        // This method recursively saves the whole album.
        public bool Save(string aPath)
        {
            iFilePath = aPath;
            return Save(true);
        }

        // This method should be called when the album already exists, and re-saves
        // just the album.xml file.
        public bool Save()
        {
            return Save(false);
        }

        // Add a new year to the album
        public void Add(EventList aEvents)
        {
            iYear.Add(new Year(aEvents));
            iYear.Sort();
        }

        // Reset the current EventList
        public void Reset()
        {
            iCurrent = -1;
        }

        // Select the specified "year" or EventList.
        public EventList GetYear(int aIndex)
        {
            if ((aIndex < 0) || (aIndex >= iYear.Count))
            {
                return null;
            }
            else
            {
                iCurrent = aIndex;

                if (!iYear[iCurrent].Events.Loaded)
                {
                    // First time the year has been accessed - load it in
                    iYear[iCurrent].Events.Load();
                }

                return iYear[iCurrent].Events;
            }
        }

        // Return the file path for the year's xml file, given the album path
        private string MakeYearPath(string aPath, string aYearName)
        {
            // Learn where the album name starts
            int endOfPath = aPath.LastIndexOf('\\');

            // Remove the albume name from the path
            string yearPath = aPath.Remove(endOfPath + 1);

            // Add the year folder and the filename
            yearPath += aYearName + "\\events.xml";
            return yearPath;
        }
    }
}
