namespace QuanLyTuyenSinh.Form
{
    partial class F_UploadHoSo
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_UploadHoSo));
            directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            groupControl2 = new DevExpress.XtraEditors.GroupControl();
            btnDeleteGDTX = new DevExpress.XtraEditors.SimpleButton();
            btnAddGDTX = new DevExpress.XtraEditors.SimpleButton();
            gridHSGDTX = new DevExpress.XtraGrid.GridControl();
            gridViewHSGDTX = new DevExpress.XtraGrid.Views.Grid.GridView();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar2 = new DevExpress.XtraBars.Bar();
            btnExportTemp = new DevExpress.XtraBars.BarButtonItem();
            btnImport = new DevExpress.XtraBars.BarButtonItem();
            btnUpload = new DevExpress.XtraBars.BarButtonItem();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            groupControl1 = new DevExpress.XtraEditors.GroupControl();
            btnDeleteTC = new DevExpress.XtraEditors.SimpleButton();
            gridHSTC = new DevExpress.XtraGrid.GridControl();
            gridViewHSTC = new DevExpress.XtraGrid.Views.Grid.GridView();
            directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
            groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridHSGDTX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHSGDTX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridHSTC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHSTC).BeginInit();
            SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            directXFormContainerControl1.Controls.Add(groupControl2);
            directXFormContainerControl1.Controls.Add(groupControl1);
            directXFormContainerControl1.Controls.Add(barDockControlLeft);
            directXFormContainerControl1.Controls.Add(barDockControlRight);
            directXFormContainerControl1.Controls.Add(barDockControlBottom);
            directXFormContainerControl1.Controls.Add(barDockControlTop);
            directXFormContainerControl1.Location = new Point(1, 31);
            directXFormContainerControl1.Name = "directXFormContainerControl1";
            directXFormContainerControl1.Size = new Size(1234, 658);
            directXFormContainerControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            groupControl2.Controls.Add(btnDeleteGDTX);
            groupControl2.Controls.Add(btnAddGDTX);
            groupControl2.Controls.Add(gridHSGDTX);
            groupControl2.Dock = DockStyle.Bottom;
            groupControl2.Enabled = false;
            groupControl2.Location = new Point(0, 585);
            groupControl2.Name = "groupControl2";
            groupControl2.Size = new Size(1234, 73);
            groupControl2.TabIndex = 5;
            groupControl2.Text = "Hồ sơ xét tuyển GDTX";
            // 
            // btnDeleteGDTX
            // 
            btnDeleteGDTX.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteGDTX.ImageOptions.Image = (Image)resources.GetObject("btnDeleteGDTX.ImageOptions.Image");
            btnDeleteGDTX.Location = new Point(1105, 0);
            btnDeleteGDTX.Name = "btnDeleteGDTX";
            btnDeleteGDTX.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnDeleteGDTX.Size = new Size(114, 23);
            btnDeleteGDTX.TabIndex = 0;
            btnDeleteGDTX.Text = "Xóa hồ sơ GDTX";
            // 
            // btnAddGDTX
            // 
            btnAddGDTX.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddGDTX.ImageOptions.Image = (Image)resources.GetObject("btnAddGDTX.ImageOptions.Image");
            btnAddGDTX.Location = new Point(985, 0);
            btnAddGDTX.Name = "btnAddGDTX";
            btnAddGDTX.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAddGDTX.Size = new Size(114, 23);
            btnAddGDTX.TabIndex = 0;
            btnAddGDTX.Text = "Tạo hồ sơ GDTX";
            // 
            // gridHSGDTX
            // 
            gridHSGDTX.Dock = DockStyle.Fill;
            gridHSGDTX.Location = new Point(2, 23);
            gridHSGDTX.MainView = gridViewHSGDTX;
            gridHSGDTX.MenuManager = barManager1;
            gridHSGDTX.Name = "gridHSGDTX";
            gridHSGDTX.Size = new Size(1230, 48);
            gridHSGDTX.TabIndex = 1;
            gridHSGDTX.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewHSGDTX });
            // 
            // gridViewHSGDTX
            // 
            gridViewHSGDTX.GridControl = gridHSGDTX;
            gridViewHSGDTX.Name = "gridViewHSGDTX";
            gridViewHSGDTX.OptionsView.ShowGroupPanel = false;
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar2 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.DockWindowTabFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { btnExportTemp, btnImport, btnUpload });
            barManager1.MainMenu = bar2;
            barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            bar2.BarName = "Main menu";
            bar2.DockCol = 0;
            bar2.DockRow = 0;
            bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(btnExportTemp), new DevExpress.XtraBars.LinkPersistInfo(btnImport), new DevExpress.XtraBars.LinkPersistInfo(btnUpload) });
            bar2.OptionsBar.MultiLine = true;
            bar2.OptionsBar.UseWholeRow = true;
            bar2.Text = "Main menu";
            // 
            // btnExportTemp
            // 
            btnExportTemp.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            btnExportTemp.Caption = "Xuất file Exel mẫu";
            btnExportTemp.Id = 0;
            btnExportTemp.ImageOptions.Image = (Image)resources.GetObject("btnExportTemp.ImageOptions.Image");
            btnExportTemp.ImageOptions.LargeImage = (Image)resources.GetObject("btnExportTemp.ImageOptions.LargeImage");
            btnExportTemp.Name = "btnExportTemp";
            btnExportTemp.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            btnExportTemp.ItemClick += btnExportTemp_ItemClick;
            // 
            // btnImport
            // 
            btnImport.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            btnImport.Caption = "Upload hồ sơ từ file Exel";
            btnImport.Id = 1;
            btnImport.ImageOptions.Image = (Image)resources.GetObject("btnImport.ImageOptions.Image");
            btnImport.ImageOptions.LargeImage = (Image)resources.GetObject("btnImport.ImageOptions.LargeImage");
            btnImport.Name = "btnImport";
            btnImport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            btnImport.ItemClick += btnImport_ItemClick;
            // 
            // btnUpload
            // 
            btnUpload.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            btnUpload.Caption = "Lưu lại hồ sơ";
            btnUpload.Id = 2;
            btnUpload.ImageOptions.Image = (Image)resources.GetObject("btnUpload.ImageOptions.Image");
            btnUpload.ImageOptions.LargeImage = (Image)resources.GetObject("btnUpload.ImageOptions.LargeImage");
            btnUpload.Name = "btnUpload";
            btnUpload.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            btnUpload.ItemClick += btnAccept_ItemClick;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(1234, 24);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 658);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(1234, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 24);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 634);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(1234, 24);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 634);
            // 
            // groupControl1
            // 
            groupControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupControl1.Controls.Add(btnDeleteTC);
            groupControl1.Controls.Add(gridHSTC);
            groupControl1.Location = new Point(0, 24);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(1234, 555);
            groupControl1.TabIndex = 4;
            groupControl1.Text = "Hồ sơ xét tuyển trung cấp";
            // 
            // btnDeleteTC
            // 
            btnDeleteTC.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteTC.ImageOptions.Image = (Image)resources.GetObject("btnDeleteTC.ImageOptions.Image");
            btnDeleteTC.Location = new Point(1116, 0);
            btnDeleteTC.Name = "btnDeleteTC";
            btnDeleteTC.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnDeleteTC.Size = new Size(103, 23);
            btnDeleteTC.TabIndex = 1;
            btnDeleteTC.Text = "Xóa hồ sơ TC";
            btnDeleteTC.Click += btnDeleteTC_Click;
            // 
            // gridHSTC
            // 
            gridHSTC.Dock = DockStyle.Fill;
            gridHSTC.Location = new Point(2, 23);
            gridHSTC.MainView = gridViewHSTC;
            gridHSTC.MenuManager = barManager1;
            gridHSTC.Name = "gridHSTC";
            gridHSTC.Size = new Size(1230, 530);
            gridHSTC.TabIndex = 0;
            gridHSTC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewHSTC });
            // 
            // gridViewHSTC
            // 
            gridViewHSTC.GridControl = gridHSTC;
            gridViewHSTC.Name = "gridViewHSTC";
            gridViewHSTC.OptionsView.ShowGroupPanel = false;
            // 
            // F_UploadHoSo
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ChildControls.Add(directXFormContainerControl1);
            ClientSize = new Size(1236, 690);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "F_UploadHoSo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Upload hồ sơ xét tuyển";
            directXFormContainerControl1.ResumeLayout(false);
            directXFormContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
            groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridHSGDTX).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHSGDTX).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridHSTC).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewHSTC).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnExportTemp;
        private DevExpress.XtraBars.BarButtonItem btnImport;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridHSTC;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHSTC;
        private DevExpress.XtraGrid.GridControl gridHSGDTX;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHSGDTX;
        private DevExpress.XtraBars.BarButtonItem btnUpload;
        private DevExpress.XtraEditors.SimpleButton btnDeleteGDTX;
        private DevExpress.XtraEditors.SimpleButton btnAddGDTX;
        private DevExpress.XtraEditors.SimpleButton btnDeleteTC;
    }
}