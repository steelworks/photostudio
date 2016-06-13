using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PhotoStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlReader htmlReader = new HtmlReader();

            if (args.Count() < 2)
            {
                Usage();
            }
            else
            {
                string option = args[0];
                string masterFilename = args[1];
                if (option.Equals("page", StringComparison.CurrentCultureIgnoreCase))
                {
                    string diagnostic = null;
                    SlideShow slideShow = htmlReader.ReadSlideShow(masterFilename, out diagnostic);
                    if (slideShow == null)
                    {
                        Console.WriteLine("Converter: failed to read slide show in " + masterFilename);
                        Console.WriteLine(diagnostic);
                        Usage();
                    }

                    // Determine the name of the XML output file from the name of the HTML input file
                    string directory = GetDirectory(masterFilename);
                    string baseFilename = GetBaseFilename(masterFilename);
                    string xmlFilename = directory + "\\" + baseFilename + ".xml";
                    slideShow.Save(xmlFilename);
                }
                else if (option.Equals("album", StringComparison.CurrentCultureIgnoreCase))
                {
                    string diagnostic = null;
                    if (htmlReader.ReadAlbum(masterFilename, out diagnostic) == null)
                    {
                        Console.WriteLine("Converter: failed to read album in " + masterFilename);
                        Console.WriteLine(diagnostic);
                        Usage();
                    }
                }
                else
                {
                    Usage();
                }
            }
        }

        static void Usage()
        {
            Console.WriteLine("Usage: Converter album <path to yearframe.htm>");
            Console.WriteLine("or:    Converter page <path to singlepage.htm>");
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

        // Strip the directory path from the supplied path to determine the 
        // filename
        static string GetFilename(string aPath)
        {
            int filenamePos = aPath.LastIndexOf('\\') + 1;
            string filename =
                (filenamePos <= 0 ? aPath : aPath.Substring(filenamePos));
            return filename;
        }

        // Strip the directory path and the suffix from the full path to get
        // just the base filename
        static string GetBaseFilename(string aPath)
        {
            string filename = GetFilename(aPath);
            int dotPos = filename.LastIndexOf('.');
            string baseFilename = 
                (dotPos <= 0 ? filename : filename.Remove(dotPos));
            return baseFilename;
        }
    }
}
