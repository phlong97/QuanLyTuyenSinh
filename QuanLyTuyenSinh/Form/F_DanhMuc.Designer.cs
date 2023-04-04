namespace QuanLyTuyenSinh.Form
{
    partial class F_DanhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_DanhMuc));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.Nooi6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnTruong = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnNghe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDTUT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnKVUT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDotTS = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnDanToc = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnTonGiao = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnTDHV = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnHTDT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete});
            this.barManager1.MaxItemId = 6;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "Thêm (F2)";
            this.btnAdd.Id = 3;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.LargeImage")));
            this.btnAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "Sửa (F3)";
            this.btnEdit.Id = 4;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.LargeImage")));
            this.btnEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Xóa (F4)";
            this.btnDelete.Id = 5;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.LargeImage")));
            this.btnDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(1109, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 536);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1109, 20);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 506);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1109, 30);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 506);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.Nooi6,
            this.accordionControlElement1,
            this.accordionControlElement2});
            this.accordionControl1.Location = new System.Drawing.Point(0, 30);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(189, 506);
            this.accordionControl1.TabIndex = 4;
            // 
            // Nooi6
            // 
            this.Nooi6.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnTruong,
            this.btnNghe,
            this.btnDTUT,
            this.btnKVUT});
            this.Nooi6.Expanded = true;
            this.Nooi6.Name = "Nooi6";
            this.Nooi6.Text = "Nội bộ";
            // 
            // btnTruong
            // 
            this.btnTruong.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTruong.ImageOptions.Image")));
            this.btnTruong.Name = "btnTruong";
            this.btnTruong.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnTruong.Text = "Trường";
            this.btnTruong.Click += new System.EventHandler(this.btnTruong_Click);
            // 
            // btnNghe
            // 
            this.btnNghe.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNghe.ImageOptions.Image")));
            this.btnNghe.Name = "btnNghe";
            this.btnNghe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnNghe.Text = "Nghành nghề";
            this.btnNghe.Click += new System.EventHandler(this.btnNghe_Click);
            // 
            // btnDTUT
            // 
            this.btnDTUT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDTUT.ImageOptions.Image")));
            this.btnDTUT.Name = "btnDTUT";
            this.btnDTUT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDTUT.Text = "Đối tượng ưu tiên";
            this.btnDTUT.Click += new System.EventHandler(this.btnDTUT_Click);
            // 
            // btnKVUT
            // 
            this.btnKVUT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKVUT.ImageOptions.Image")));
            this.btnKVUT.Name = "btnKVUT";
            this.btnKVUT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnKVUT.Text = "Khu vực ưu tiên";
            this.btnKVUT.Click += new System.EventHandler(this.btnKVUT_Click);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnDotTS});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Tuyển sinh";
            // 
            // btnDotTS
            // 
            this.btnDotTS.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDotTS.ImageOptions.Image")));
            this.btnDotTS.Name = "btnDotTS";
            this.btnDotTS.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDotTS.Text = "Đọt tuyển sinh";
            this.btnDotTS.Click += new System.EventHandler(this.btnDotTS_Click);
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnDanToc,
            this.btnTonGiao,
            this.btnTDHV,
            this.btnHTDT});
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Text = "Hồ sơ";
            // 
            // btnDanToc
            // 
            this.btnDanToc.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDanToc.ImageOptions.Image")));
            this.btnDanToc.Name = "btnDanToc";
            this.btnDanToc.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnDanToc.Text = "Dân tộc";
            this.btnDanToc.Click += new System.EventHandler(this.btnDanToc_Click);
            // 
            // btnTonGiao
            // 
            this.btnTonGiao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTonGiao.ImageOptions.Image")));
            this.btnTonGiao.Name = "btnTonGiao";
            this.btnTonGiao.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnTonGiao.Text = "Tôn giáo";
            this.btnTonGiao.Click += new System.EventHandler(this.btnTonGiao_Click);
            // 
            // btnTDHV
            // 
            this.btnTDHV.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTDHV.ImageOptions.Image")));
            this.btnTDHV.Name = "btnTDHV";
            this.btnTDHV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnTDHV.Text = "Trình độ học vấn";
            this.btnTDHV.Click += new System.EventHandler(this.btnTDHV_Click);
            // 
            // btnHTDT
            // 
            this.btnHTDT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHTDT.ImageOptions.Image")));
            this.btnHTDT.Name = "btnHTDT";
            this.btnHTDT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnHTDT.Text = "Hình thức đào tạo";
            this.btnHTDT.Click += new System.EventHandler(this.btnHTDT_Click);
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(189, 30);
            this.gridControl.MainView = this.gridView;
            this.gridControl.MenuManager = this.barManager1;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(920, 506);
            this.gridControl.TabIndex = 5;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsFilter.InHeaderSearchMode = DevExpress.XtraGrid.Views.Grid.GridInHeaderSearchMode.TextSearch;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.RowAutoHeight = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // F_DanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 556);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F_DanhMuc";
            this.Text = "Danh mục";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Nooi6;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTruong;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnNghe;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDTUT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnKVUT;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDotTS;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnDanToc;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTonGiao;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnTDHV;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnHTDT;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    }
}