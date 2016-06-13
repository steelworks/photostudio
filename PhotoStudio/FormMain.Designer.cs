using Registry;

namespace PhotoStudio
{
    partial class formMain
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
            PhotoStudioRegistry.CurrentSize = formMain.ActiveForm.Size;
            PhotoStudioRegistry.CurrentPosition = formMain.ActiveForm.Location;
            PhotoStudioRegistry.SlideSpeed = iSlideParameters.Speed;
            PhotoStudioRegistry.ShowCaptions = iSlideParameters.ShowCaptions;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.iLabelYear = new System.Windows.Forms.Label();
            this.iProgressBar = new System.Windows.Forms.ProgressBar();
            this.photopageToolStrip = new System.Windows.Forms.ToolStrip();
            this.dateNameButton = new System.Windows.Forms.ToolStripButton();
            this.hourPlusButton = new System.Windows.Forms.ToolStripButton();
            this.hourMinusButton = new System.Windows.Forms.ToolStripButton();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuPhoto = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePhotoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuHtml = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuXml = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.applyPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteXmlMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createSlideshowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.photopageToolStrip.SuspendLayout();
            this.contextMenuPhoto.SuspendLayout();
            this.contextMenuHtml.SuspendLayout();
            this.contextMenuXml.SuspendLayout();
            this.contextMenuFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.iLabelYear);
            this.splitContainer.Panel2.Controls.Add(this.iProgressBar);
            this.splitContainer.Panel2.Controls.Add(this.photopageToolStrip);
            this.splitContainer.Panel2.Controls.Add(this.listView);
            this.splitContainer.Size = new System.Drawing.Size(567, 564);
            this.splitContainer.SplitterDistance = 189;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(189, 564);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.ico");
            this.imageList.Images.SetKeyName(1, "document.ico");
            this.imageList.Images.SetKeyName(2, "steelworks.ico");
            // 
            // iLabelYear
            // 
            this.iLabelYear.AutoSize = true;
            this.iLabelYear.Location = new System.Drawing.Point(123, 3);
            this.iLabelYear.Name = "iLabelYear";
            this.iLabelYear.Size = new System.Drawing.Size(35, 13);
            this.iLabelYear.TabIndex = 3;
            this.iLabelYear.Text = "label1";
            this.iLabelYear.Visible = false;
            // 
            // iProgressBar
            // 
            this.iProgressBar.Location = new System.Drawing.Point(192, 3);
            this.iProgressBar.Name = "iProgressBar";
            this.iProgressBar.Size = new System.Drawing.Size(179, 13);
            this.iProgressBar.TabIndex = 2;
            this.iProgressBar.Visible = false;
            // 
            // photopageToolStrip
            // 
            this.photopageToolStrip.AutoSize = false;
            this.photopageToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateNameButton,
            this.hourPlusButton,
            this.hourMinusButton});
            this.photopageToolStrip.Location = new System.Drawing.Point(0, 0);
            this.photopageToolStrip.Name = "photopageToolStrip";
            this.photopageToolStrip.Size = new System.Drawing.Size(374, 25);
            this.photopageToolStrip.TabIndex = 1;
            this.photopageToolStrip.Text = "toolStrip1";
            // 
            // dateNameButton
            // 
            this.dateNameButton.AutoToolTip = false;
            this.dateNameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dateNameButton.Image = ((System.Drawing.Image)(resources.GetObject("dateNameButton.Image")));
            this.dateNameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dateNameButton.Name = "dateNameButton";
            this.dateNameButton.Size = new System.Drawing.Size(23, 22);
            this.dateNameButton.Text = "DateName";
            this.dateNameButton.ToolTipText = "DateName - rename photos to match the timestamps";
            this.dateNameButton.Click += new System.EventHandler(this.dateNameButton_Click);
            // 
            // hourPlusButton
            // 
            this.hourPlusButton.AutoToolTip = false;
            this.hourPlusButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hourPlusButton.Image = ((System.Drawing.Image)(resources.GetObject("hourPlusButton.Image")));
            this.hourPlusButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hourPlusButton.Name = "hourPlusButton";
            this.hourPlusButton.Size = new System.Drawing.Size(23, 22);
            this.hourPlusButton.Text = "Hour plus";
            this.hourPlusButton.ToolTipText = "Add one hour to date names";
            this.hourPlusButton.Click += new System.EventHandler(this.hourPlusButton_Click);
            // 
            // hourMinusButton
            // 
            this.hourMinusButton.AutoToolTip = false;
            this.hourMinusButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hourMinusButton.Image = ((System.Drawing.Image)(resources.GetObject("hourMinusButton.Image")));
            this.hourMinusButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hourMinusButton.Name = "hourMinusButton";
            this.hourMinusButton.Size = new System.Drawing.Size(23, 22);
            this.hourMinusButton.Text = "Hour minus";
            this.hourMinusButton.ToolTipText = "Subtract one hour from date names";
            this.hourMinusButton.Click += new System.EventHandler(this.hourMinusButton_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderModified});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(374, 564);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            // 
            // columnHeaderModified
            // 
            this.columnHeaderModified.Text = "DateName";
            // 
            // contextMenuPhoto
            // 
            this.contextMenuPhoto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMenuItem,
            this.deletePhotoMenuItem});
            this.contextMenuPhoto.Name = "contextMenuStrip";
            this.contextMenuPhoto.Size = new System.Drawing.Size(108, 48);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(107, 22);
            this.viewMenuItem.Text = "View";
            // 
            // deletePhotoMenuItem
            // 
            this.deletePhotoMenuItem.Name = "deletePhotoMenuItem";
            this.deletePhotoMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deletePhotoMenuItem.Text = "Delete";
            // 
            // contextMenuHtml
            // 
            this.contextMenuHtml.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertMenuItem,
            this.deleteMenuItem});
            this.contextMenuHtml.Name = "contextMenuHtml";
            this.contextMenuHtml.Size = new System.Drawing.Size(117, 48);
            // 
            // convertMenuItem
            // 
            this.convertMenuItem.Name = "convertMenuItem";
            this.convertMenuItem.Size = new System.Drawing.Size(116, 22);
            this.convertMenuItem.Text = "Convert";
            this.convertMenuItem.Click += new System.EventHandler(this.convertMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deleteMenuItem.Text = "Delete";
            // 
            // contextMenuXml
            // 
            this.contextMenuXml.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playMenuItem,
            this.editToolStripMenuItem,
            this.validateToolStripMenuItem1,
            this.applyPropertiesToolStripMenuItem,
            this.deleteXmlMenuItem2});
            this.contextMenuXml.Name = "contextMenuXml";
            this.contextMenuXml.Size = new System.Drawing.Size(162, 136);
            // 
            // playMenuItem
            // 
            this.playMenuItem.Name = "playMenuItem";
            this.playMenuItem.Size = new System.Drawing.Size(161, 22);
            this.playMenuItem.Text = "Play";
            this.playMenuItem.Click += new System.EventHandler(this.playMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem1
            // 
            this.validateToolStripMenuItem1.Name = "validateToolStripMenuItem1";
            this.validateToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.validateToolStripMenuItem1.Text = "Validate";
            this.validateToolStripMenuItem1.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // applyPropertiesToolStripMenuItem
            // 
            this.applyPropertiesToolStripMenuItem.Name = "applyPropertiesToolStripMenuItem";
            this.applyPropertiesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.applyPropertiesToolStripMenuItem.Text = "Apply Properties";
            this.applyPropertiesToolStripMenuItem.Click += new System.EventHandler(this.ApplyPropertiesMenuItem_Click);
            // 
            // deleteXmlMenuItem2
            // 
            this.deleteXmlMenuItem2.Name = "deleteXmlMenuItem2";
            this.deleteXmlMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.deleteXmlMenuItem2.Text = "Delete";
            // 
            // contextMenuFolder
            // 
            this.contextMenuFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSlideshowToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuFolder.Name = "contextMenuFolder";
            this.contextMenuFolder.Size = new System.Drawing.Size(165, 48);
            // 
            // createSlideshowToolStripMenuItem
            // 
            this.createSlideshowToolStripMenuItem.Name = "createSlideshowToolStripMenuItem";
            this.createSlideshowToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.createSlideshowToolStripMenuItem.Text = "Create Slideshow";
            this.createSlideshowToolStripMenuItem.Click += new System.EventHandler(this.createSlideshowToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 564);
            this.Controls.Add(this.splitContainer);
            this.Name = "formMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Steelworks Photo Studio";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.photopageToolStrip.ResumeLayout(false);
            this.photopageToolStrip.PerformLayout();
            this.contextMenuPhoto.ResumeLayout(false);
            this.contextMenuHtml.ResumeLayout(false);
            this.contextMenuXml.ResumeLayout(false);
            this.contextMenuFolder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderModified;
        private System.Windows.Forms.ToolStrip photopageToolStrip;
        private System.Windows.Forms.ToolStripButton dateNameButton;
        private System.Windows.Forms.ToolStripButton hourPlusButton;
        private System.Windows.Forms.ToolStripButton hourMinusButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuPhoto;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePhotoMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuHtml;
        private System.Windows.Forms.ToolStripMenuItem convertMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuXml;
        private System.Windows.Forms.ToolStripMenuItem playMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteXmlMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuFolder;
        private System.Windows.Forms.ToolStripMenuItem createSlideshowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyPropertiesToolStripMenuItem;
        private System.Windows.Forms.ProgressBar iProgressBar;
        private System.Windows.Forms.Label iLabelYear;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem1;
    }
}

