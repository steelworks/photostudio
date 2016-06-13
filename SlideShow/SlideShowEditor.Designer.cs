namespace PhotoStudio
{
    partial class SlideShowEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideShowEditor));
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.centrePictureBox = new System.Windows.Forms.PictureBox();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.leftLeftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.rightRightPictureBox = new System.Windows.Forms.PictureBox();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.captionTextBox = new System.Windows.Forms.TextBox();
            this.progressStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addThemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filmStripPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centrePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftLeftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRightPictureBox)).BeginInit();
            this.progressStatusStrip.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.filmStripPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPictureBox.Location = new System.Drawing.Point(3, 3);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(711, 347);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            // 
            // centrePictureBox
            // 
            this.centrePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.centrePictureBox.Location = new System.Drawing.Point(289, 3);
            this.centrePictureBox.Name = "centrePictureBox";
            this.centrePictureBox.Size = new System.Drawing.Size(119, 81);
            this.centrePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.centrePictureBox.TabIndex = 1;
            this.centrePictureBox.TabStop = false;
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Location = new System.Drawing.Point(164, 3);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(119, 81);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 1;
            this.leftPictureBox.TabStop = false;
            // 
            // leftLeftPictureBox
            // 
            this.leftLeftPictureBox.Location = new System.Drawing.Point(39, 3);
            this.leftLeftPictureBox.Name = "leftLeftPictureBox";
            this.leftLeftPictureBox.Size = new System.Drawing.Size(119, 81);
            this.leftLeftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftLeftPictureBox.TabIndex = 1;
            this.leftLeftPictureBox.TabStop = false;
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Location = new System.Drawing.Point(414, 3);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(119, 81);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            // 
            // rightRightPictureBox
            // 
            this.rightRightPictureBox.Location = new System.Drawing.Point(539, 3);
            this.rightRightPictureBox.Name = "rightRightPictureBox";
            this.rightRightPictureBox.Size = new System.Drawing.Size(119, 81);
            this.rightRightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightRightPictureBox.TabIndex = 1;
            this.rightRightPictureBox.TabStop = false;
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(-1, 3);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(34, 81);
            this.leftButton.TabIndex = 2;
            this.leftButton.Text = "<<";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(664, 3);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(34, 81);
            this.rightButton.TabIndex = 2;
            this.rightButton.Text = ">>";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.closeButton.Location = new System.Drawing.Point(720, 523);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // captionTextBox
            // 
            this.captionTextBox.AcceptsReturn = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.captionTextBox, 2);
            this.captionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captionTextBox.Location = new System.Drawing.Point(10, 363);
            this.captionTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.captionTextBox.Multiline = true;
            this.captionTextBox.Name = "captionTextBox";
            this.captionTextBox.Size = new System.Drawing.Size(778, 80);
            this.captionTextBox.TabIndex = 4;
            this.captionTextBox.TextChanged += new System.EventHandler(this.captionTextBox_TextChanged);
            // 
            // progressStatusStrip
            // 
            this.progressStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripCountLabel,
            this.toolStripDropDownButton});
            this.progressStatusStrip.Location = new System.Drawing.Point(0, 549);
            this.progressStatusStrip.Name = "progressStatusStrip";
            this.progressStatusStrip.Size = new System.Drawing.Size(798, 22);
            this.progressStatusStrip.TabIndex = 5;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripCountLabel
            // 
            this.toolStripCountLabel.Name = "toolStripCountLabel";
            this.toolStripCountLabel.Size = new System.Drawing.Size(59, 17);
            this.toolStripCountLabel.Text = "n pictures";
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.addThemToolStripMenuItem});
            this.toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton.Image")));
            this.toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(282, 20);
            this.toolStripDropDownButton.Text = "There are additional pictures not in the slide show";
            this.toolStripDropDownButton.ToolTipText = "Additional pictures";
            this.toolStripDropDownButton.Visible = false;
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.showDetailsToolStripMenuItem.Text = "Show details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // addThemToolStripMenuItem
            // 
            this.addThemToolStripMenuItem.Name = "addThemToolStripMenuItem";
            this.addThemToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.addThemToolStripMenuItem.Text = "Add them";
            this.addThemToolStripMenuItem.Click += new System.EventHandler(this.addThemToolStripMenuItem_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deleteButton.Location = new System.Drawing.Point(720, 327);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.deleteButton, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.mainPictureBox, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.closeButton, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.captionTextBox, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.filmStripPanel, 0, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(798, 549);
            this.mainTableLayoutPanel.TabIndex = 7;
            // 
            // filmStripPanel
            // 
            this.filmStripPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.filmStripPanel.Controls.Add(this.rightRightPictureBox);
            this.filmStripPanel.Controls.Add(this.centrePictureBox);
            this.filmStripPanel.Controls.Add(this.rightButton);
            this.filmStripPanel.Controls.Add(this.leftPictureBox);
            this.filmStripPanel.Controls.Add(this.leftButton);
            this.filmStripPanel.Controls.Add(this.leftLeftPictureBox);
            this.filmStripPanel.Controls.Add(this.rightPictureBox);
            this.filmStripPanel.Location = new System.Drawing.Point(8, 456);
            this.filmStripPanel.Name = "filmStripPanel";
            this.filmStripPanel.Size = new System.Drawing.Size(700, 90);
            this.filmStripPanel.TabIndex = 7;
            // 
            // SlideShowEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 571);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.progressStatusStrip);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(804, 597);
            this.Name = "SlideShowEditor";
            this.ShowInTaskbar = false;
            this.Text = "SlideShowEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SlideShowEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centrePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftLeftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightRightPictureBox)).EndInit();
            this.progressStatusStrip.ResumeLayout(false);
            this.progressStatusStrip.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.filmStripPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.PictureBox centrePictureBox;
        private System.Windows.Forms.PictureBox leftPictureBox;
        private System.Windows.Forms.PictureBox leftLeftPictureBox;
        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.PictureBox rightRightPictureBox;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox captionTextBox;
        private System.Windows.Forms.StatusStrip progressStatusStrip;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addThemToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCountLabel;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Panel filmStripPanel;
    }
}