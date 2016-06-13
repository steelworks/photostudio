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
    public partial class NonSlideshowTitleForm : Form
    {
        string iTitle = string.Empty;

        public string Title
        {
            get
            {
                return iTitle;
            }
        }
        public NonSlideshowTitleForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            iTitle = titleTextBox.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
