using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HTML;

namespace PhotoStudio
{
    public class HtmlReader
    {
        public HtmlReader()
        {
        }

        // Read one HTML page and return a slide show
        public SlideShow ReadSlideShow(string aSlideFile, out string aDiagnostic)
        {
            aDiagnostic = null;

            // Determine the name of the future XML slide show file
            string xmlFilePath = aSlideFile.Replace(".htm", ".xml");
            SlideShow slideShow = new SlideShow(xmlFilePath);

            //Console.WriteLine("     HtmlReader ReadSlideShow: parsing " + aSlideFile);
            string html = ReadFile(aSlideFile);

            if (html == null)
            {
                aDiagnostic = "ReadSlideShow: bad HTML slideshow file " + aSlideFile;
                return null;
            }
            else
            {
                ParseHTML parse = new ParseHTML();
                parse.Source = html;

                HtmlPreprocess htmlPreprocess = new HtmlPreprocess();

                // Default overall title for the slide show, hopefully replaced with something better
                string title = "A most peculiar day";
                bool collectingTitle = false;

                bool collectingCaption = false;
                Caption caption = new Caption();
                string link = "";
                while (!parse.Eof())
                {
                    char ch = parse.Parse();
                    if (ch == 0)
                    {
                        AttributeList tag = parse.GetTag();
                        if (tag.Name.Equals("title", StringComparison.CurrentCultureIgnoreCase))
                        {
                            collectingTitle = true;       // Start collecting title
                            title = string.Empty;
                        }
                        else if (tag.Name.Equals("/title", StringComparison.CurrentCultureIgnoreCase))
                        {
                            collectingTitle = false;      // Title now complete
                            slideShow.Title = title.Trim();
                        }
                        if (tag.Name.Equals("td", StringComparison.CurrentCultureIgnoreCase))
                        {
                            collectingCaption = true;       // Start collecting new caption
                            caption = new Caption();
                        }
                        else if (tag.Name.Equals("/td", StringComparison.CurrentCultureIgnoreCase))
                        {
                            collectingCaption = false;      // Any caption is now complete
                            if (!link.Equals(""))
                            {
                                // Got a link to go with the caption
                                slideShow.Add(link, caption);
                                link = "";
                            }
                        }
                        else if (collectingCaption &&
                                  tag.Name.Equals("p", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // HTML paragraph tag within caption
                            caption.NewLine();
                        }
                        else if (tag["href"] != null)
                        {
                            string href = tag["href"].Value.Replace('/', '\\');
                            if (IsPhoto(href))
                            {
                                //Console.WriteLine("     + HtmlReader ReadSlideShow: add " + href + 
                                //                  " from tag " + tag.Name);
                                link = href;
                            }
                        }

                        // Preprocessing of regular character stream starts with clean sheet after tag
                        htmlPreprocess.Reset();
                    }
                    else
                    {
                        // Got a character
                        ch = htmlPreprocess.Add(ch);
                        if (ch != HtmlPreprocess.NullChar)
                        {
                            if (collectingTitle)
                            {
                                title += ch;
                            }
                            else if (collectingCaption)
                            {
                                caption.AddChar(ch);
                            }
                        }
                    }
                }

                return slideShow;
            }
        }

        // Read one HTML page and return an Event list
        public EventList ReadEvents(string aEventsFile, out string aDiagnostic)
        {
            aDiagnostic = null;
            string xmlFilePath = aEventsFile.Replace(".htm", ".xml");
            EventList events = new EventList(xmlFilePath);

            string html = ReadFile(aEventsFile);

            if (html == null)
            {
                aDiagnostic = "ReadEvents: bad HTML Events file " + aEventsFile;
                return null;
            }
            else
            {
                // Determine the events directory
                string eventsDirectory = GetDirectory(aEventsFile);

                //Console.WriteLine("  HtmlReader ReadEvents: parsing " + aEventsFile);
                ParseHTML parse = new ParseHTML();
                parse.Source = html;

                HtmlPreprocess htmlPreprocess = new HtmlPreprocess();
                string name = "";               // Collect stream of characters in HTML source
                int indent = 0;

                SlideShow slideShow = null;     // Collect slide show from href

                while (!parse.Eof())
                {
                    char ch = parse.Parse();
                    if (ch == 0)
                    {
                        AttributeList tag = parse.GetTag();
                        if (tag.Name.Equals("h2", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Start collecting title
                            name = "";
                            indent = 0;
                        }
                        else if (tag.Name.Equals("/h2", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Title is now complete
                            events.Title = name.Trim();
                            name = "";
                            indent = 0;
                        }
                        else if ((tag.Name.Equals("p", StringComparison.CurrentCultureIgnoreCase)) ||
                                  (tag.Name.Equals("br", StringComparison.CurrentCultureIgnoreCase)) ||
                                  (tag.Name.Equals("hr", StringComparison.CurrentCultureIgnoreCase)))
                        {
                            // End of line, check whether we have an event
                            if (name.Length > 0)
                            {
                                // Use indent as the level for now
                                events.Add(indent, name.Trim(), slideShow);
                            }

                            // Reset for next event
                            name = "";
                            indent = 0;
                            slideShow = null;
                        }
                        else if (tag["href"] != null)
                        {
                            string href = tag["href"].Value.Replace('/', '\\');
                            //Console.WriteLine("   + HtmlReader ReadEvents: add event " + href + " to events XML file");

                            // Strip any anchor: we cannot handle it
                            if (href.Contains('#'))
                            {
                                href = href.Remove(href.IndexOf('#'));
                            }

                            // Process child events file
                            slideShow = ReadSlideShow(eventsDirectory + href, out aDiagnostic);
                            if (slideShow == null)
                            {
                                return null;
                            }
                        }

                        // Preprocessing of regular character stream starts with clean sheet after tag
                        htmlPreprocess.Reset();
                    }
                    else
                    {
                        // Preprocess
                        ch = htmlPreprocess.Add(ch);
                        if (ch == HtmlPreprocess.NullChar)
                        {
                            // Nothing to do
                            continue;
                        }
                        else if (ch.Equals(' '))
                        {
                            if (name.Length == 0)
                            {
                                // Leading space: count the indent
                                indent++;
                            }
                            else
                            {
                                // Count all non-leading spaces returned by the preprocessor
                                name += ch;
                            }
                        }
                        else if (ch.Equals('+'))
                        {
                            if (name.Length == 0)
                            {
                                // Initial plus marks a subevent - count it in the indent
                                indent++;
                            }
                            else
                            {
                                // Transcribe other plus symbols into the event name
                                name += ch;
                            }
                        }
                        else
                        {
                            // Add regular character
                            name += ch;
                        }
                    }
                }

                // End of event list, check for any outstanding event
                if (name.Length > 0)
                {
                    events.Add(indent, name.Trim(), slideShow);
                }

                // The event levels were arbitrarily set as a measure of the indentation
                // of each event name in the HTML. Reassign sequential levels.
                events.Relevel();

                return events;
            }
        }

        // Very simple: just parse the EventLists out of the album front page HTML.
        // No need to get the event list names (usually years) from the HTML, as these
        // names should be embedded in the EventLists.
        public Album ReadAlbum(string masterFilename, out string aDiagnostic)
        {
            aDiagnostic = null;
            string xmlFilename = GetDirectory(masterFilename) + "Album.xml";
            Album album = new Album(xmlFilename);

            string html = ReadFile(masterFilename);

            if (html == null)
            {
                aDiagnostic = "ReadAlbum: bad HTML Album file " + masterFilename;
                return null;
            }
            else
            {
                // Determine the master directory
                string masterDirectory = GetDirectory(masterFilename);

                //Console.WriteLine("HtmlReader: parsing " + masterFilename);
                ParseHTML parse = new ParseHTML();
                parse.Source = html;

                while (!parse.Eof())
                {
                    char ch = parse.Parse();
                    if (ch == 0)
                    {
                        AttributeList tag = parse.GetTag();
                        if (tag["href"] != null)
                        {
                            string href = tag["href"].Value.Replace('/', '\\');
                            //Console.WriteLine("HtmlReader: add year " + href + " to master XML file");

                            // Process child events file
                            EventList events = ReadEvents(masterDirectory + href, out aDiagnostic);
                            if (events == null)
                            {
                                return null;
                            }
                            else
                            {
                                album.Add(events);
                            }
                        }
                    }
                }

                return album;
            }
        }

        // Strip the filename from the supplied path to determine the 
        // holding directory
        static string GetDirectory(string aPath)
        {
            int filenamePos = aPath.LastIndexOf('\\') + 1;
            string holdingDirectory =
                (filenamePos <= 0 ? "" : aPath.Remove(filenamePos));
            return holdingDirectory;
        }

        // Read file containing HTML source and return as a string
        static string ReadFile(string aPath)
        {
            if (File.Exists(aPath))
            {
                string fileAsString = "";

                FileStream fileStream = new FileStream(aPath, FileMode.Open, FileAccess.Read);
                using (TextReader textReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        fileAsString += line + "\r\n";
                    }

                    textReader.Close();
                    fileStream.Close();
                }

                return fileAsString;
            }
            else
            {
                return null;
            }
        }

        static bool IsPhoto(string iFile)
        {
            return iFile.EndsWith(".jpg", true, null);
        }
    }
}
