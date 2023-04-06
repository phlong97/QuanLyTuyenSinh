namespace QuanLyTuyenSinh.Form
{
    partial class F_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Main));
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar1 = new DevExpress.XtraBars.Bar();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement5 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnDotTS = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            Nooi6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnTruong = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnNghe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnDTUT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnKVUT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnDanToc = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnTonGiao = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnTDHV = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnHTDT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            btnQuocTich = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            pnMain = new Panel();
            panelGrid = new DevExpress.Utils.Layout.TablePanel();
            panelControl = new Panel();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            panel1 = new Panel();
            btnDelete = new DevExpress.XtraEditors.SimpleButton();
            btnAdd = new DevExpress.XtraEditors.SimpleButton();
            gridControl = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            pnImg = new Panel();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelGrid).BeginInit();
            panelGrid.SuspendLayout();
            panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar1 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.MaxItemId = 6;
            // 
            // bar1
            // 
            bar1.BarName = "Tools";
            bar1.DockCol = 0;
            bar1.DockRow = 0;
            bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar1.Text = "Tools";
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Margin = new Padding(4);
            barDockControlTop.Size = new Size(1109, 20);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 556);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Margin = new Padding(4);
            barDockControlBottom.Size = new Size(1109, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 20);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Margin = new Padding(4);
            barDockControlLeft.Size = new Size(0, 536);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(1109, 20);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Margin = new Padding(4);
            barDockControlRight.Size = new Size(0, 536);
            // 
            // accordionControl1
            // 
            accordionControl1.Dock = DockStyle.Left;
            accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement3, accordionControlElement1, Nooi6, accordionControlElement2 });
            accordionControl1.Location = new Point(0, 20);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.True;
            accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            accordionControl1.Size = new Size(236, 536);
            accordionControl1.TabIndex = 4;
            accordionControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement3
            // 
            accordionControlElement3.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { accordionControlElement4, accordionControlElement5 });
            accordionControlElement3.Expanded = true;
            accordionControlElement3.Name = "accordionControlElement3";
            accordionControlElement3.Text = "Hồ sơ";
            // 
            // accordionControlElement4
            // 
            accordionControlElement4.ImageOptions.Image = (Image)resources.GetObject("accordionControlElement4.ImageOptions.Image");
            accordionControlElement4.Name = "accordionControlElement4";
            accordionControlElement4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            accordionControlElement4.Text = "Hồ sơ dự tuyển";
            // 
            // accordionControlElement5
            // 
            accordionControlElement5.ImageOptions.Image = (Image)resources.GetObject("accordionControlElement5.ImageOptions.Image");
            accordionControlElement5.Name = "accordionControlElement5";
            accordionControlElement5.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            accordionControlElement5.Text = "Hồ sơ trúng tuyển";
            // 
            // accordionControlElement1
            // 
            accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { btnDotTS });
            accordionControlElement1.Expanded = true;
            accordionControlElement1.Name = "accordionControlElement1";
            accordionControlElement1.Text = "Tuyển sinh";
            // 
            // btnDotTS
            // 
            btnDotTS.ImageOptions.Image = (Image)resources.GetObject("btnDotTS.ImageOptions.Image");
            btnDotTS.Name = "btnDotTS";
            btnDotTS.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnDotTS.Text = "Đọt tuyển sinh";
            btnDotTS.Click += btnDotTS_Click;
            // 
            // Nooi6
            // 
            Nooi6.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { btnTruong, btnNghe, btnDTUT, btnKVUT });
            Nooi6.Name = "Nooi6";
            Nooi6.Text = "Thông tin chung";
            // 
            // btnTruong
            // 
            btnTruong.ImageOptions.Image = (Image)resources.GetObject("btnTruong.ImageOptions.Image");
            btnTruong.Name = "btnTruong";
            btnTruong.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnTruong.Text = "Trường";
            btnTruong.Click += btnTruong_Click;
            // 
            // btnNghe
            // 
            btnNghe.ImageOptions.Image = (Image)resources.GetObject("btnNghe.ImageOptions.Image");
            btnNghe.Name = "btnNghe";
            btnNghe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnNghe.Text = "Nghành nghề";
            btnNghe.Click += btnNghe_Click;
            // 
            // btnDTUT
            // 
            btnDTUT.ImageOptions.Image = (Image)resources.GetObject("btnDTUT.ImageOptions.Image");
            btnDTUT.Name = "btnDTUT";
            btnDTUT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnDTUT.Text = "Đối tượng ưu tiên";
            btnDTUT.Click += btnDTUT_Click;
            // 
            // btnKVUT
            // 
            btnKVUT.ImageOptions.Image = (Image)resources.GetObject("btnKVUT.ImageOptions.Image");
            btnKVUT.Name = "btnKVUT";
            btnKVUT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnKVUT.Text = "Khu vực ưu tiên";
            btnKVUT.Click += btnKVUT_Click;
            // 
            // accordionControlElement2
            // 
            accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { btnDanToc, btnTonGiao, btnTDHV, btnHTDT, btnQuocTich });
            accordionControlElement2.Name = "accordionControlElement2";
            accordionControlElement2.Text = "Thông tin cơ bản";
            // 
            // btnDanToc
            // 
            btnDanToc.ImageOptions.Image = (Image)resources.GetObject("btnDanToc.ImageOptions.Image");
            btnDanToc.Name = "btnDanToc";
            btnDanToc.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnDanToc.Text = "Dân tộc";
            btnDanToc.Click += btnDanToc_Click;
            // 
            // btnTonGiao
            // 
            btnTonGiao.ImageOptions.Image = (Image)resources.GetObject("btnTonGiao.ImageOptions.Image");
            btnTonGiao.Name = "btnTonGiao";
            btnTonGiao.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnTonGiao.Text = "Tôn giáo";
            btnTonGiao.Click += btnTonGiao_Click;
            // 
            // btnTDHV
            // 
            btnTDHV.ImageOptions.Image = (Image)resources.GetObject("btnTDHV.ImageOptions.Image");
            btnTDHV.Name = "btnTDHV";
            btnTDHV.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnTDHV.Text = "Trình độ học vấn";
            btnTDHV.Click += btnTDHV_Click;
            // 
            // btnHTDT
            // 
            btnHTDT.ImageOptions.Image = (Image)resources.GetObject("btnHTDT.ImageOptions.Image");
            btnHTDT.Name = "btnHTDT";
            btnHTDT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnHTDT.Text = "Hình thức đào tạo";
            btnHTDT.Click += btnHTDT_Click;
            // 
            // btnQuocTich
            // 
            btnQuocTich.ImageOptions.Image = (Image)resources.GetObject("btnQuocTich.ImageOptions.Image");
            btnQuocTich.Name = "btnQuocTich";
            btnQuocTich.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            btnQuocTich.Text = "Quốc tịch";
            btnQuocTich.Click += btnQuocTich_Click;
            // 
            // pnMain
            // 
            pnMain.Controls.Add(panelGrid);
            pnMain.Controls.Add(pnImg);
            pnMain.Dock = DockStyle.Fill;
            pnMain.Location = new Point(236, 20);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(873, 536);
            pnMain.TabIndex = 10;
            // 
            // panelGrid
            // 
            panelGrid.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F) });
            panelGrid.Controls.Add(panelControl);
            panelGrid.Controls.Add(gridControl);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 0);
            panelGrid.Name = "panelGrid";
            panelGrid.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
            panelGrid.Size = new Size(873, 536);
            panelGrid.TabIndex = 8;
            panelGrid.UseSkinIndents = true;
            // 
            // panelControl
            // 
            panelGrid.SetColumn(panelControl, 0);
            panelControl.Controls.Add(btnClose);
            panelControl.Controls.Add(panel1);
            panelControl.Controls.Add(btnDelete);
            panelControl.Controls.Add(btnAdd);
            panelControl.Location = new Point(15, 14);
            panelControl.Name = "panelControl";
            panelGrid.SetRow(panelControl, 0);
            panelControl.Size = new Size(843, 36);
            panelControl.TabIndex = 11;
            // 
            // btnClose
            // 
            btnClose.Appearance.BackColor = Color.WhiteSmoke;
            btnClose.Appearance.BorderColor = Color.Transparent;
            btnClose.Appearance.Options.UseBackColor = true;
            btnClose.Appearance.Options.UseBorderColor = true;
            btnClose.Dock = DockStyle.Right;
            btnClose.ImageOptions.Image = (Image)resources.GetObject("btnClose.ImageOptions.Image");
            btnClose.Location = new Point(803, 0);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(40, 36);
            btnClose.TabIndex = 8;
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(164, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(679, 36);
            panel1.TabIndex = 9;
            // 
            // btnDelete
            // 
            btnDelete.Appearance.BackColor = Color.WhiteSmoke;
            btnDelete.Appearance.Options.UseBackColor = true;
            btnDelete.Dock = DockStyle.Left;
            btnDelete.ImageOptions.Image = (Image)resources.GetObject("btnDelete.ImageOptions.Image");
            btnDelete.Location = new Point(90, 0);
            btnDelete.Margin = new Padding(0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(74, 36);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Appearance.BackColor = Color.WhiteSmoke;
            btnAdd.Appearance.BorderColor = Color.WhiteSmoke;
            btnAdd.Appearance.Options.UseBackColor = true;
            btnAdd.Appearance.Options.UseBorderColor = true;
            btnAdd.Dock = DockStyle.Left;
            btnAdd.ImageOptions.Image = (Image)resources.GetObject("btnAdd.ImageOptions.Image");
            btnAdd.Location = new Point(0, 0);
            btnAdd.Margin = new Padding(0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 36);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Thêm";
            btnAdd.Click += btnAdd_Click;
            // 
            // gridControl
            // 
            panelGrid.SetColumn(gridControl, 0);
            gridControl.Dock = DockStyle.Fill;
            gridControl.Location = new Point(15, 54);
            gridControl.MainView = gridView1;
            gridControl.MenuManager = barManager1;
            gridControl.Name = "gridControl";
            panelGrid.SetRow(gridControl, 1);
            gridControl.Size = new Size(843, 467);
            gridControl.TabIndex = 10;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl;
            gridView1.Name = "gridView1";
            // 
            // pnImg
            // 
            pnImg.BackgroundImageLayout = ImageLayout.Stretch;
            pnImg.Dock = DockStyle.Fill;
            pnImg.Location = new Point(0, 0);
            pnImg.Name = "pnImg";
            pnImg.Size = new Size(873, 536);
            pnImg.TabIndex = 7;
            // 
            // F_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 556);
            Controls.Add(pnMain);
            Controls.Add(accordionControl1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Margin = new Padding(4);
            Name = "F_Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tuyển sinh";
            WindowState = FormWindowState.Maximized;
            Load += F_Main_Load;
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelGrid).EndInit();
            panelGrid.ResumeLayout(false);
            panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
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
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnQuocTich;
        private Panel pnMain;
        private Panel pnImg;
        private DevExpress.Utils.Layout.TablePanel panelGrid;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Panel panelControl;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement5;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private Panel panel1;
    }
}