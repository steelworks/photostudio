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
    public partial class ValidationForm : Form
    {
        public ValidationForm( List<string> aNotExist, List<string> aUncatalogued )
        {
            InitializeComponent();
            iTextBoxNotExist.Text = string.Join( Environment.NewLine, aNotExist );
            iTextBoxUncatalogued.Text = string.Join( Environment.NewLine, aUncatalogued );
            iLabelNotExist.Text += ( " (" + aNotExist.Count + ")" );
            iLabelUncatalogued.Text += ( " (" + aUncatalogued.Count + ")" );
        }

        void ButtonOK_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
