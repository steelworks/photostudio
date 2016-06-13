using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoStudio
{
    // Parameters for running slide shows in SlideViewerForm
    public class SlideViewerParameters
    {
        int iSpeed;
        bool iShowCaptions;

        public SlideViewerParameters(int aSpeed, bool aShowCaptions)
        {
            iSpeed = aSpeed;
            iShowCaptions = aShowCaptions;
        }

        public int Speed
        {
            get
            {
                return iSpeed;
            }

            set
            {
                iSpeed = value;
            }
        }

        public bool ShowCaptions
        {
            get
            {
                return iShowCaptions;
            }

            set
            {
                iShowCaptions = value;
            }
        }

        public void ToggleCaptions()
        {
            iShowCaptions = !iShowCaptions;
        }
    }
}
