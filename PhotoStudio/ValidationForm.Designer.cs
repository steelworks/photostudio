namespace PhotoStudio
{
    partial class ValidationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.iButtonOK = new System.Windows.Forms.Button();
            this.iTextBoxNotExist = new System.Windows.Forms.TextBox();
            this.iLabelNotExist = new System.Windows.Forms.Label();
            this.iTextBoxUncatalogued = new System.Windows.Forms.TextBox();
            this.iLabelUncatalogued = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iButtonOK
            // 
            this.iButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.iButtonOK.Location = new System.Drawing.Point(320, 416);
            this.iButtonOK.Name = "iButtonOK";
            this.iButtonOK.Size = new System.Drawing.Size(75, 23);
            this.iButtonOK.TabIndex = 0;
            this.iButtonOK.Text = "OK";
            this.iButtonOK.UseVisualStyleBackColor = true;
            this.iButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // iTextBoxNotExist
            // 
            this.iTextBoxNotExist.BackColor = System.Drawing.SystemColors.Window;
            this.iTextBoxNotExist.Location = new System.Drawing.Point(13, 23);
            this.iTextBoxNotExist.Multiline = true;
            this.iTextBoxNotExist.Name = "iTextBoxNotExist";
            this.iTextBoxNotExist.ReadOnly = true;
            this.iTextBoxNotExist.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.iTextBoxNotExist.Size = new System.Drawing.Size(678, 180);
            this.iTextBoxNotExist.TabIndex = 1;
            // 
            // iLabelNotExist
            // 
            this.iLabelNotExist.AutoSize = true;
            this.iLabelNotExist.Location = new System.Drawing.Point(13, 4);
            this.iLabelNotExist.Name = "iLabelNotExist";
            this.iLabelNotExist.Size = new System.Drawing.Size(185, 13);
            this.iLabelNotExist.TabIndex = 2;
            this.iLabelNotExist.Text = "Catalogued images which do not exist";
            // 
            // iTextBoxUncatalogued
            // 
            this.iTextBoxUncatalogued.BackColor = System.Drawing.SystemColors.Window;
            this.iTextBoxUncatalogued.Location = new System.Drawing.Point(12, 222);
            this.iTextBoxUncatalogued.Multiline = true;
            this.iTextBoxUncatalogued.Name = "iTextBoxUncatalogued";
            this.iTextBoxUncatalogued.ReadOnly = true;
            this.iTextBoxUncatalogued.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.iTextBoxUncatalogued.Size = new System.Drawing.Size(678, 180);
            this.iTextBoxUncatalogued.TabIndex = 1;
            // 
            // iLabelUncatalogued
            // 
            this.iLabelUncatalogued.AutoSize = true;
            this.iLabelUncatalogued.Location = new System.Drawing.Point(13, 206);
            this.iLabelUncatalogued.Name = "iLabelUncatalogued";
            this.iLabelUncatalogued.Size = new System.Drawing.Size(110, 13);
            this.iLabelUncatalogued.TabIndex = 2;
            this.iLabelUncatalogued.Text = "Uncatalogued images";
            // 
            // ValidationForm
            // 
            this.AcceptButton = this.iButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 451);
            this.ControlBox = false;
            this.Controls.Add(this.iLabelUncatalogued);
            this.Controls.Add(this.iTextBoxUncatalogued);
            this.Controls.Add(this.iLabelNotExist);
            this.Controls.Add(this.iTextBoxNotExist);
            this.Controls.Add(this.iButtonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValidationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PhotoAlbum Validation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button iButtonOK;
        private System.Windows.Forms.TextBox iTextBoxNotExist;
        private System.Windows.Forms.Label iLabelNotExist;
        private System.Windows.Forms.TextBox iTextBoxUncatalogued;
        private System.Windows.Forms.Label iLabelUncatalogued;
    }
}