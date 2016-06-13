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
    // Private class defines a single event
    // Level: 0 for overall title, 1 for main event, 2 for sub-event etc
    // Name: to appear in Photo album selection lsit
    // SlideShow: will be null for a title
    // Did not really want this class to be public, but cannot serialise it otherwise
    [Serializable]
    public class Event
    {
        private int iLevel;
        private string iName;
        private SlideShow iShow;

        // Accessor
        [XmlAttribute]
        public int Level
        {
            get
            {
                return iLevel;
            }
            set
            {
                iLevel = value;
            }
        }

        // Accessor
        [XmlElement]
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

        // Accessor
        [XmlIgnore]
        public SlideShow SlideShow
        {
            get
            {
                return iShow;
            }
            set
            {
                iShow = value;
            }
        }

        // Would rather this not be public - only needed for load and save
        [XmlElement(ElementName = "SlideShow")]
        public string Path
        {
            get 
            { 
                return (iShow == null) ? null : iShow.GetFilePath(); 
            }
            set
            {
                iShow = new SlideShow();
                if (!iShow.Load(value))
                {
                    MessageBox.Show("Cannot load " + value + " - slide show", "Event Path");
                }
            }
        }

        // Constructor
        public Event(int aLevel, string aName, SlideShow aShow)
        {
            iLevel = aLevel;
            iName = aName;
            iShow = aShow;
        }

        // Parameterless constructor required for serialisation
        public Event()
        {
            iLevel = 0;
            iName = "Anon";
            iShow = null;
        }
    }

    [Serializable]
    public class EventList
    {
        string iTitle = "A most peculiar year";
        string iFilePath;
        List<Event> iEventList;
        int iCurrent = -1;          // Index of current event
        bool iLoaded = false;

        [XmlIgnore]
        public bool Loaded
        {
            get { return iLoaded; }
            set { iLoaded = value; }
        }

        // Accessor
        [XmlAttribute]
        public string Title
        {
            get { return iTitle; }
            set { iTitle = value; }
        }

        //[XmlElement(ElementName = "Events")]
        [XmlIgnore]
        public string Path
        {
            get { return iFilePath; }
            set { iFilePath = value; }
        }

        [XmlIgnore]
        public int Count
        {
            get { return iEventList.Count; }
        }

        // Would rather this not be public - only needed for load and save
        [XmlElement(ElementName = "Event")]
        public List<Event> Events
        {
            get { return iEventList; }
            set { iEventList = value; }
        }

        // Constructor to build events
        public EventList(string aPath)
        {
            // Path to XML document
            iFilePath = aPath;

            // Start with empty EventsList
            iEventList = new List<Event>();

            iLoaded = false;
        }

        // Parameterless constructor required for serialisation
        public EventList()
        {
            iLoaded = false;
        }

        // Load the events from an XML document
        public bool Load()
        {
            // Start with empty EventsList
            iEventList = new List<Event>();

            if (!File.Exists(iFilePath))
            {
                MessageBox.Show("Cannot load " + iFilePath + " - no such file", "EventList");
                return false;
            }

            XmlSerializer s = new XmlSerializer(typeof(EventList));
            TextReader r = new StreamReader(iFilePath);

            // Wanted to say "this = s.Deserialize(r)", but it is illegal.
            // Instead load EventList into "loaded" and transcribe to "this".
            EventList loaded = s.Deserialize(r) as EventList;
            iTitle = loaded.Title;
            iEventList = loaded.Events;
            iCurrent = -1;
            r.Close();

            // Loaded the event list. Recurse to load each slide show.
            foreach (Event happening in iEventList)
            {
                if (happening.Path != null)
                {
                    SlideShow slideShow = new SlideShow();
                    slideShow.Load(happening.Path);
                }
            }

            iLoaded = true;
            return true;
        }

        // Save the event list as an XML document
        public bool Save(bool aRecursive)
        {
            XmlSerializer s = new XmlSerializer(typeof(EventList));
            TextWriter w = new StreamWriter(iFilePath);
            s.Serialize(w, this);
            w.Close();

            if (aRecursive)
            {
                // Also save each slideshow in the event list
                foreach (Event happening in iEventList)
                {
                    // Event may have no slide show if it is more of a heading for a subset of events
                    if (happening.SlideShow != null)
                    {
                        // Save the slide show
                        happening.SlideShow.Save();
                    }
                }
            }

            return true;
        }

        // Add a new event to the list
        public void Add(int aLevel, string aName, SlideShow aShow)
        {
            iEventList.Add(new Event(aLevel, aName, aShow));
        }

        // Insert a new event to the list at the specified positiion
        public bool Insert(int aPosition, string aName, SlideShow aShow)
        {
            if ((aPosition < 0) || (aPosition > iEventList.Count))
            {
                return false;                   // Cannot do
            }
            else
            {
                iEventList.Insert(aPosition, new Event(0, aName, aShow));
                return true;
            }
        }

        // Determine the active level numbers of the events in the list, and re-number sequentially
        public void Relevel()
        {
            IDictionary<int, int> levelMap = new Dictionary<int, int>();

            // First build up a dictionary of old levels, each mapped to zero
            int maxOldLevel = 0;
            foreach (Event oneEvent in iEventList)
            {
                int oldLevel = oneEvent.Level;
                if (!levelMap.ContainsKey(oldLevel))
                {
                    levelMap.Add(oldLevel, 0);
                    if (oldLevel > maxOldLevel)
                    {
                        maxOldLevel = oldLevel;
                    }
                }
            }

            // Now determine the mapping to new level numbers
            int newLevel = 0;
            for (int oldLevel = 0; oldLevel <= maxOldLevel; oldLevel++)
            {
                if (levelMap.ContainsKey(oldLevel))
                {
                    levelMap[oldLevel] = newLevel++;
                }
            }

            // Now ready to do the re-levelling
            foreach (Event oneEvent in iEventList)
            {
                oneEvent.Level = levelMap[oneEvent.Level];
            }
        }

        // Reset the current event
        public void Reset()
        {
            iCurrent = -1;
        }

        // Advance the current event.
        // Returns true if there is a new current event, false if list is exhausted.
        public bool NextEvent()
        {
            return (++iCurrent < iEventList.Count);
        }

        // Make the specified event current
        public bool SetEvent(int aItem)
        {
            if ((aItem >= 0) && (aItem < iEventList.Count))
            {
                iCurrent = aItem;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Exchange the current event with the one above
        public bool MoveUp()
        {
            if (iCurrent < 1)
            {
                return false;           // Cannot do
            }
            else
            {
                Event temp = iEventList[iCurrent];
                iEventList.RemoveAt(iCurrent);
                iEventList.Insert(--iCurrent, temp);
                return true;
            }
        }

        // Exchange the current event with the one below
        public bool MoveDown()
        {
            if (iCurrent >= (iEventList.Count - 1))
            {
                return false;           // Cannot do
            }
            else
            {
                Event temp = iEventList[iCurrent];
                iEventList.RemoveAt(iCurrent);
                iEventList.Insert(++iCurrent, temp);
                return true;
            }
        }

        // Level the current event upwards
        public bool Promote()
        {
            if ((iCurrent < 0) || (iCurrent >= iEventList.Count))
            {
                return false;           // Cannot do
            }
            else if (iEventList[iCurrent].Level <= 0)
            {
                return false;           // Already at highest level
            }
            else
            {
                iEventList[iCurrent].Level--;
                return true;
            }
        }

        // Level the current event upwards
        public bool Demote()
        {
            if ((iCurrent < 0) || (iCurrent >= iEventList.Count))
            {
                return false;           // Cannot do
            }
            else
            {
                iEventList[iCurrent].Level++;
                return true;
            }
        }

        // Delete the current event from the EventList
        public bool Delete()
        {
            if ((iCurrent < 0) || (iCurrent >= iEventList.Count))
            {
                return false;           // Cannot do
            }
            else
            {
                iEventList.RemoveAt(iCurrent);
                if ((iCurrent >= iEventList.Count) && (iCurrent > 0))
                {
                    // Removed the final event: reposition iCurrent
                    iCurrent--;
                }
                return true;
            }
        }

        // Method to retrieve the level of the specified event in the list
        public int GetLevel(int aItem)
        {
            if ((aItem < 0) || (aItem >= iEventList.Count))
            {
                return -1;                  // Invalid
            }
            else
            {
                return iEventList[aItem].Level;
            }
        }

        // Method to retrieve the SlideShow for the specified event in the list
        public SlideShow GetSlideShow(int aItem)
        {
            if ((aItem < 0) || (aItem >= iEventList.Count))
            {
                return null;                // Invalid
            }
            else
            {
                return iEventList[aItem].SlideShow;
            }
        }


        // Accessors
        public int Level
        {
            get
            {
                if (iCurrent < iEventList.Count)
                {
                    return iEventList[iCurrent].Level;
                }
                else
                {
                    throw new System.Exception("EventList: attempt to access level of non-existent event");
                }
            }
        }

        // Accessor - get the name of the current event
        public string Name
        {
            get
            {
                if (iCurrent < iEventList.Count)
                {
                    return iEventList[iCurrent].Name;
                }
                else
                {
                    throw new System.Exception("EventList: attempt to access name of non-existent event");
                }
            }
        }

        // Accessor
        public SlideShow SlideShow
        {
            get
            {
                if ((iCurrent >= 0) && (iCurrent < iEventList.Count))
                {
                    return iEventList[iCurrent].SlideShow;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
