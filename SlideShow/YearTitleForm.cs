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
    public partial class YearTitleForm : Form
    {
        string iYearTitle = null;

        public YearTitleForm()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return iYearTitle;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            iYearTitle = titleTextBox.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
