using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoStudio
{
    public partial class SlideShowTitleForm : Form
    {
        string iBriefTitle = string.Empty;
        string iFullTitle = string.Empty;

        public string BriefTitle
        {
            get { return iBriefTitle; }
        }

        public string FullTitle
        {
            get { return iFullTitle; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SlideShowTitleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with preset title
        /// </summary>
        public SlideShowTitleForm(string aTitle)
        {
            InitializeComponent();
            iFullTitle = aTitle;
            textBoxFull.Text = iFullTitle;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            iBriefTitle = textBoxBrief.Text;
            iFullTitle = textBoxFull.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
