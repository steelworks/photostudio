namespace PhotoStudio
{
    partial class AlbumForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumForm));
            this.yearTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.eventsListBox = new System.Windows.Forms.ListBox();
            this.contextMenuEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNonslideEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.albumPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.albumToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPromote = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDemote = new System.Windows.Forms.ToolStripButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.openSlideShowDialog = new System.Windows.Forms.OpenFileDialog();
            this.createSlideShowDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.yearTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.contextMenuEvents.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.albumPictureBox)).BeginInit();
            this.albumToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // yearTabControl
            // 
            this.yearTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.yearTabControl.Controls.Add(this.tabPage1);
            this.yearTabControl.Controls.Add(this.tabPage2);
            this.yearTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yearTabControl.Location = new System.Drawing.Point(0, 25);
            this.yearTabControl.Name = "yearTabControl";
            this.yearTabControl.SelectedIndex = 0;
            this.yearTabControl.Size = new System.Drawing.Size(605, 565);
            this.yearTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mainTableLayoutPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(597, 539);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayoutPanel.Controls.Add(this.eventsListBox, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.eventLabel, 1, 1);
            this.mainTableLayoutPanel.Controls.Add(this.yearLabel, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.buttonTableLayoutPanel, 1, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 4;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(591, 533);
            this.mainTableLayoutPanel.TabIndex = 8;
            // 
            // eventsListBox
            // 
            this.eventsListBox.ContextMenuStrip = this.contextMenuEvents;
            this.eventsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventsListBox.FormattingEnabled = true;
            this.eventsListBox.Location = new System.Drawing.Point(3, 3);
            this.eventsListBox.Name = "eventsListBox";
            this.mainTableLayoutPanel.SetRowSpan(this.eventsListBox, 4);
            this.eventsListBox.Size = new System.Drawing.Size(289, 527);
            this.eventsListBox.TabIndex = 0;
            this.eventsListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.eventsListBox_DrawItem);
            this.eventsListBox.SelectedIndexChanged += new System.EventHandler(this.eventsListBox_SelectedIndexChanged);
            this.eventsListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eventsListBox_MouseDown);
            // 
            // contextMenuEvents
            // 
            this.contextMenuEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.promoteToolStripMenuItem,
            this.demoteToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuEvents.Name = "contextMenuEvents";
            this.contextMenuEvents.Size = new System.Drawing.Size(140, 136);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // promoteToolStripMenuItem
            // 
            this.promoteToolStripMenuItem.Name = "promoteToolStripMenuItem";
            this.promoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.promoteToolStripMenuItem.Text = "<< Promote";
            this.promoteToolStripMenuItem.Click += new System.EventHandler(this.promoteToolStripMenuItem_Click);
            // 
            // demoteToolStripMenuItem
            // 
            this.demoteToolStripMenuItem.Name = "demoteToolStripMenuItem";
            this.demoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.demoteToolStripMenuItem.Text = "Demote >>";
            this.demoteToolStripMenuItem.Click += new System.EventHandler(this.demoteToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExistingToolStripMenuItem,
            this.addNewToolStripMenuItem,
            this.addNonslideEventToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // addExistingToolStripMenuItem
            // 
            this.addExistingToolStripMenuItem.Name = "addExistingToolStripMenuItem";
            this.addExistingToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addExistingToolStripMenuItem.Text = "existing ...";
            this.addExistingToolStripMenuItem.Click += new System.EventHandler(this.addExistingToolStripMenuItem_Click);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addNewToolStripMenuItem.Text = "new ...";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // addNonslideEventToolStripMenuItem
            // 
            this.addNonslideEventToolStripMenuItem.Name = "addNonslideEventToolStripMenuItem";
            this.addNonslideEventToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addNonslideEventToolStripMenuItem.Text = "non-slide event";
            this.addNonslideEventToolStripMenuItem.Click += new System.EventHandler(this.addNonslideEventToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // eventLabel
            // 
            this.eventLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventLabel.AutoSize = true;
            this.eventLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventLabel.Location = new System.Drawing.Point(298, 50);
            this.eventLabel.MaximumSize = new System.Drawing.Size(205, 40);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(205, 40);
            this.eventLabel.TabIndex = 5;
            this.eventLabel.Text = "Event";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearLabel.Location = new System.Drawing.Point(298, 0);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(49, 24);
            this.yearLabel.TabIndex = 4;
            this.yearLabel.Text = "Year";
            this.yearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonTableLayoutPanel
            // 
            this.buttonTableLayoutPanel.ColumnCount = 3;
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonTableLayoutPanel.Controls.Add(this.closeButton, 1, 3);
            this.buttonTableLayoutPanel.Controls.Add(this.editButton, 1, 1);
            this.buttonTableLayoutPanel.Controls.Add(this.playButton, 1, 2);
            this.buttonTableLayoutPanel.Controls.Add(this.albumPictureBox, 0, 0);
            this.buttonTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTableLayoutPanel.Location = new System.Drawing.Point(298, 103);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 4;
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttonTableLayoutPanel.Size = new System.Drawing.Size(290, 253);
            this.buttonTableLayoutPanel.TabIndex = 7;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(84, 227);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(122, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Location = new System.Drawing.Point(84, 169);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(122, 23);
            this.editButton.TabIndex = 6;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // playButton
            // 
            this.playButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playButton.Location = new System.Drawing.Point(84, 198);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(122, 23);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // albumPictureBox
            // 
            this.buttonTableLayoutPanel.SetColumnSpan(this.albumPictureBox, 3);
            this.albumPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.albumPictureBox.Location = new System.Drawing.Point(3, 3);
            this.albumPictureBox.Name = "albumPictureBox";
            this.albumPictureBox.Size = new System.Drawing.Size(284, 160);
            this.albumPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.albumPictureBox.TabIndex = 1;
            this.albumPictureBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(597, 539);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // albumToolStrip
            // 
            this.albumToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPromote,
            this.toolStripButtonMoveUp,
            this.toolStripButtonMoveDown,
            this.toolStripButtonDemote});
            this.albumToolStrip.Location = new System.Drawing.Point(0, 0);
            this.albumToolStrip.Name = "albumToolStrip";
            this.albumToolStrip.Size = new System.Drawing.Size(605, 25);
            this.albumToolStrip.TabIndex = 1;
            this.albumToolStrip.Text = "Tools";
            // 
            // toolStripButtonPromote
            // 
            this.toolStripButtonPromote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPromote.Image = global::PhotoStudio.Properties.Resources.Promote;
            this.toolStripButtonPromote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPromote.Name = "toolStripButtonPromote";
            this.toolStripButtonPromote.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPromote.ToolTipText = "Promote event";
            this.toolStripButtonPromote.Click += new System.EventHandler(this.toolStripButtonPromote_Click);
            // 
            // toolStripButtonMoveUp
            // 
            this.toolStripButtonMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMoveUp.Image = global::PhotoStudio.Properties.Resources.MoveUp;
            this.toolStripButtonMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveUp.Name = "toolStripButtonMoveUp";
            this.toolStripButtonMoveUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMoveUp.ToolTipText = "Move event up the list";
            this.toolStripButtonMoveUp.Click += new System.EventHandler(this.toolStripButtonMoveUp_Click);
            // 
            // toolStripButtonMoveDown
            // 
            this.toolStripButtonMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMoveDown.Image = global::PhotoStudio.Properties.Resources.MoveDown;
            this.toolStripButtonMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveDown.Name = "toolStripButtonMoveDown";
            this.toolStripButtonMoveDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMoveDown.ToolTipText = "Move event down the list";
            this.toolStripButtonMoveDown.Click += new System.EventHandler(this.toolStripButtonMoveDown_Click);
            // 
            // toolStripButtonDemote
            // 
            this.toolStripButtonDemote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDemote.Image = global::PhotoStudio.Properties.Resources.Demote;
            this.toolStripButtonDemote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDemote.Name = "toolStripButtonDemote";
            this.toolStripButtonDemote.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDemote.ToolTipText = "Demote event";
            this.toolStripButtonDemote.Click += new System.EventHandler(this.toolStripButtonDemote_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Photo.png");
            // 
            // openSlideShowDialog
            // 
            this.openSlideShowDialog.Filter = "Slide show files|*.xml";
            this.openSlideShowDialog.Title = "Select slide show to add";
            // 
            // AlbumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 590);
            this.Controls.Add(this.yearTabControl);
            this.Controls.Add(this.albumToolStrip);
            this.MinimumSize = new System.Drawing.Size(621, 626);
            this.Name = "AlbumForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Steelworks Photo Album";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlbumForm_FormClosing);
            this.yearTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.contextMenuEvents.ResumeLayout(false);
            this.buttonTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.albumPictureBox)).EndInit();
            this.albumToolStrip.ResumeLayout(false);
            this.albumToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl yearTabControl;
        private System.Windows.Forms.ToolStrip albumToolStrip;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.PictureBox albumPictureBox;
        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuEvents;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem demoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openSlideShowDialog;
        private System.Windows.Forms.FolderBrowserDialog createSlideShowDialog;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNonslideEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveDown;
        private System.Windows.Forms.ToolStripButton toolStripButtonPromote;
        private System.Windows.Forms.ToolStripButton toolStripButtonDemote;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;
    }
}