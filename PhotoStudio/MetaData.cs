using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace PhotoStudio
{
    /// <summary>
    /// Manages Exif Meta Data in an image
    /// </summary>
    class MetaData
    {
        /// <summary>
        /// Constructor: takes path to an image file
        /// </summary>
        /// <param name="aImagePath"></param>
        public MetaData( string aImagePath )
        {
            iImagePath = aImagePath.Replace( "%20", " " );
            if ( File.Exists( iImagePath ) )
            {
                iImage = new Bitmap( iImagePath );
                iPropertyItems = iImage.PropertyItems;
            }
            else
            {
                throw new Exception( "Image " + iImagePath + " not found" );
            }
        }

        /// <summary>
        /// Apply a new string value to the specified PropertyItem
        /// </summary>
        /// <remarks>PropertyTagType values are defined in http://msdn.microsoft.com/en-us/library/system.drawing.imaging.propertyitem.type(v=vs.110).aspx </remarks>
        /// <param name="aItem">Name of the PropertyItem</param>
        /// <param name="aValue">Value to be applied</param>
        public void ApplyPropertyItem( string aItem, string aValue )
        {
            // X marks the spot where the zero terminator goes
            var encodedValue = System.Text.Encoding.UTF8.GetBytes( aValue + "X" );
            encodedValue[encodedValue.Length - 1] = 0;
            PropertyItem title = GetPropertyItem( aItem );
            if ( title != null )
            {
                title.Id = iTagNames[aItem];
                title.Type = 2;             // ascii string
                title.Len = encodedValue.Length;
                title.Value = encodedValue;
                iImage.SetPropertyItem( title );
            }

        }

        /// <summary>
        /// Saves PropertyItems applied with ApplyPropertyItem to the image file
        /// </summary>
        public void Save()
        {
            // Get errors if trying to save to the original file
            string temp = Path.ChangeExtension( iImagePath, "temp" );
            string backup = Path.ChangeExtension( iImagePath, "bak" );
            iImage.Save( temp );
            iImage.Dispose();
            File.Replace( temp, iImagePath, backup );
            File.Delete( backup );
        }

        /// <summary>
        /// Retrieve PropertyItem if it exists, or return blank one if not
        /// </summary>
        /// <param name="aExistingItems"></param>
        /// <param name="aName"></param>
        /// <returns></returns>
        PropertyItem GetPropertyItem( string aName )
        {
            int id;
            if ( iTagNames.TryGetValue( aName, out id ) )
            {
                var existingItem = iPropertyItems.FirstOrDefault( x => x.Id == id );
                if ( existingItem == null )
                {
                    // Property does not exist
                    return iPropertyItems[0];
                }
                else
                {
                    // Found existing property
                    return existingItem;
                }
            }
            else
            {
                // Property name not recognised
                return null;
            }
        }

        static TagId iTagNames = new TagId();
        string iImagePath;
        Image iImage;
        PropertyItem[] iPropertyItems;
    }
}
