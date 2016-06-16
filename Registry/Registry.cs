using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using Microsoft.Win32;

namespace Registry
{
	/// <summary>
	/// Provides access to registry settings under PhotoStudio
	/// </summary>
	public class PhotoStudioRegistry
	{
        #region Public properties

        /// <summary>
        /// The directory containing the album.xml file
        /// </summary>
        public static string AlbumDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(iAlbumDirectory))
                {
                    iAlbumDirectory = GetRegistryStringValue(AlbumDirectoryKey);
                }

                return iAlbumDirectory;
            }

            set
            {
                SetRegistryFolderValue(AlbumDirectoryKey, value);
            }
        }

        /// <summary>
        /// The most recent directory browsed in PhotoStudio
        /// </summary>
        public static string BrowseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(iBrowseDirectory))
                {
                    iBrowseDirectory = GetRegistryStringValue(BrowseDirectoryKey);

                    if (string.IsNullOrEmpty(iBrowseDirectory))
                    {
                        // No last browse directory: go for the album directory if we have one
                        iBrowseDirectory = AlbumDirectory;
                    }

                    if (string.IsNullOrEmpty(iBrowseDirectory))
                    {
                        // No album directory either
                        iBrowseDirectory = "C:\\";
                    }
                }

                return iBrowseDirectory;
            }

            set
            {
                SetRegistryFolderValue(BrowseDirectoryKey, value);
            }
        }

        /// <summary>
        /// Size of form in pixels
        /// </summary>
        public static Size CurrentSize
        {
            get
            {
                if (iSizeX < 0)
                {
                    iSizeX = GetRegistryIntValue(SizeXKey, 800);
                }

                if (iSizeY < 0)
                {
                    iSizeY = GetRegistryIntValue(SizeYKey, 600);
                }

                return new Size(iSizeX, iSizeY);
            }

            set
            {
                iSizeX = value.Width;
                iSizeY = value.Height;
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, SizeXKey, iSizeX);
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, SizeYKey, iSizeY);
            }
        }


        /// <summary>
        /// Position of form in pixels
        /// </summary>
        public static Point CurrentPosition
        {
            get
            {
                if (iPositionX < 0)
                {
                    iPositionX = GetRegistryIntValue(PositionXKey);
                }

                if (iPositionY < 0)
                {
                    iPositionY = GetRegistryIntValue(PositionYKey);
                }

                return new Point(iPositionX, iPositionY);
            }

            set
            {
                iPositionX = value.X;
                iPositionY = value.Y;
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, PositionXKey, iPositionX);
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, PositionYKey, iPositionY);
            }
        }

        /// <summary>
		/// Slide show speed in milliseconds per frame
		/// </summary>
		public static int SlideSpeed
		{
			get
			{
				if ( iSlideSpeedMs < 0 )
				{
                    // Retrieve value from registry, or default to 2000 ms
					iSlideSpeedMs = GetRegistryIntValue( SlideSpeedKey, 2000 );
				}

				return iSlideSpeedMs;
			}

			set
			{
				iSlideSpeedMs = value;
				Microsoft.Win32.Registry.SetValue( PhotoStudioRegistryPath, SlideSpeedKey, value );
			}
		}

		/// <summary>
		/// Whether to show captions
		/// </summary>
		public static bool ShowCaptions
		{
			get
			{
				if ( iShowCaptions < 0 )
				{
					iShowCaptions = GetRegistryIntValue( ShowCaptionsKey );
				}

				return (iShowCaptions != 0);
			}

			set
			{
				iShowCaptions = ( value ? 1 : 0 );
				Microsoft.Win32.Registry.SetValue( PhotoStudioRegistryPath, ShowCaptionsKey, iShowCaptions );
			}
		}

		#endregion Public properties

		#region Private methods

		/// <summary>
		/// Query the supplied registry key and return a value from the registry,
		/// creating an empty registry entry if none exists.
		/// </summary>
		/// <param name="aRegistryKey">Registry key name</param>
		/// <returns></returns>
		private static string GetRegistryStringValue( string aRegistryKey )
		{
			object result = Microsoft.Win32.Registry.GetValue( PhotoStudioRegistryPath, aRegistryKey, null );
			string resultString = ( result != null ? result.ToString() : null );

			// Bad or non-existent registry entry: set it to empty
			if ( string.IsNullOrEmpty( resultString ) || !Directory.Exists( resultString ) )
			{
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, aRegistryKey, string.Empty);
				return string.Empty;
			}

			return resultString;
		}

        /// <summary>
        /// Query the supplied registry key and return a value from the registry,
        /// creating an empty registry entry if none exists.
        /// </summary>
        /// <param name="aRegistryKey">Registry key name</param>
        /// <returns>Registry setting or default value as an int</returns>
        private static int GetRegistryIntValue(string aRegistryKey)
        {
            return GetRegistryIntValue(aRegistryKey, 0);
        }

        /// <summary>
        /// Query the supplied registry key and return a value from the registry,
        /// creating a registry entry with the default value if none exists.
        /// </summary>
        /// <param name="aRegistryKey">Registry key name</param>
        /// <param name="aDefaultValue">Value to return if no registry setting exists</param>
        /// <returns>Registry setting or default value as an int</returns>
        private static int GetRegistryIntValue(string aRegistryKey, int aDefaultValue)
        {
            object result = Microsoft.Win32.Registry.GetValue(PhotoStudioRegistryPath, aRegistryKey, null);
            string resultString = (result != null ? result.ToString() : null);

            // Bad or non-existent registry entry: set it to zero
            if (string.IsNullOrEmpty(resultString))
            {
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, aRegistryKey, aDefaultValue.ToString());
                return aDefaultValue;
            }

            int resultValue = aDefaultValue;
            int.TryParse(resultString, out resultValue);
            return resultValue;
        }

		/// <summary>
		/// Store the supplied value in the specified registry entry.
		/// The value is interpreted as a folder path: if the path does not exist, the registry entry
		/// will be cleared.
		/// </summary>
		/// <param name="aRegistryKey">Registry key</param>
		/// <param name="aValue">Folder path to be stored</param>
		private static void SetRegistryFolderValue( string aRegistryKey, string aValue )
		{
			if ( !string.IsNullOrEmpty( aValue ) && Directory.Exists( aValue ) )
			{
				// Valid path (though it may not hold a valid map)
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, aRegistryKey, aValue);
			}
			else
			{
				// Invalid path: clear the registry entry
                Microsoft.Win32.Registry.SetValue(PhotoStudioRegistryPath, aRegistryKey, string.Empty);
			}
		}

		#endregion Private methods


		#region Private constants and variables

		const string PhotoStudioRegistryPath = @"HKEY_CURRENT_USER\SOFTWARE\Steelworks\PhotoStudio";
        const string AlbumDirectoryKey = "Album Directory";
        const string BrowseDirectoryKey = "Browse Directory";
        const string SizeXKey = "Size X";
        const string SizeYKey = "Size Y";
        const string PositionXKey = "Position X";
        const string PositionYKey = "Position Y";
        const string SlideSpeedKey = "Slide Speed";
		const string ShowCaptionsKey = "Show Captions";

        /// <summary>
        /// Album directory path.
        /// </summary>
        static string iAlbumDirectory;

        /// <summary>
        /// Browse directory path (PhotoStudio).
        /// </summary>
        static string iBrowseDirectory;

        /// <summary>
        /// Current size of form
        /// </summary>
        static int iSizeX = -1;
        static int iSizeY = -1;

        /// <summary>
        /// Current position of form
        /// </summary>
        static int iPositionX = -1;
        static int iPositionY = -1;

		/// <summary>
		/// Speed of slide show: milliseconds per frame
		/// </summary>
		static int iSlideSpeedMs = -1;

		/// <summary>
		/// Whether to show captions
		/// </summary>
		static int iShowCaptions = -1;

		#endregion
	}
}
