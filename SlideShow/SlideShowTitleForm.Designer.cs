namespace PhotoStudio
{
    partial class SlideShowTitleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBrief = new System.Windows.Forms.Label();
            this.labelFull = new System.Windows.Forms.Label();
            this.textBoxBrief = new System.Windows.Forms.TextBox();
            this.textBoxFull = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBrief
            // 
            this.labelBrief.AutoSize = true;
            this.labelBrief.Location = new System.Drawing.Point(16, 13);
            this.labelBrief.Name = "labelBrief";
            this.labelBrief.Size = new System.Drawing.Size(118, 13);
            this.labelBrief.TabIndex = 0;
            this.labelBrief.Text = "Brief title (for events list)";
            // 
            // labelFull
            // 
            this.labelFull.AutoSize = true;
            this.labelFull.Location = new System.Drawing.Point(16, 41);
            this.labelFull.Name = "labelFull";
            this.labelFull.Size = new System.Drawing.Size(115, 13);
            this.labelFull.TabIndex = 1;
            this.labelFull.Text = "Full title (for slide show)";
            // 
            // textBoxBrief
            // 
            this.textBoxBrief.Location = new System.Drawing.Point(152, 12);
            this.textBoxBrief.MaxLength = 25;
            this.textBoxBrief.Name = "textBoxBrief";
            this.textBoxBrief.Size = new System.Drawing.Size(119, 20);
            this.textBoxBrief.TabIndex = 2;
            // 
            // textBoxFull
            // 
            this.textBoxFull.Location = new System.Drawing.Point(152, 40);
            this.textBoxFull.MaxLength = 80;
            this.textBoxFull.Name = "textBoxFull";
            this.textBoxFull.Size = new System.Drawing.Size(318, 20);
            this.textBoxFull.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(314, 89);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(395, 89);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SlideShowTitleForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(482, 130);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxFull);
            this.Controls.Add(this.textBoxBrief);
            this.Controls.Add(this.labelFull);
            this.Controls.Add(this.labelBrief);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SlideShowTitleForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter slide show details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBrief;
        private System.Windows.Forms.Label labelFull;
        private System.Windows.Forms.TextBox textBoxBrief;
        private System.Windows.Forms.TextBox textBoxFull;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}