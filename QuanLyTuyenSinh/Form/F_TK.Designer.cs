namespace QuanLyTuyenSinh.Form
{
    partial class F_TK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_TK));
            directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            tabTK = new DevExpress.XtraTab.XtraTabControl();
            pageTK = new DevExpress.XtraTab.XtraTabPage();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            bar2 = new DevExpress.XtraBars.Bar();
            barSubItemTK = new DevExpress.XtraBars.BarSubItem();
            btnPrint = new DevExpress.XtraBars.BarButtonItem();
            chkXetTuyen = new DevExpress.XtraBars.BarCheckItem();
            chkTrungTuyen = new DevExpress.XtraBars.BarCheckItem();
            barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            barCbbDTS = new DevExpress.XtraBars.BarEditItem();
            CbbDTS = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            chkTrungCap = new DevExpress.XtraBars.BarCheckItem();
            chkGDTX = new DevExpress.XtraBars.BarCheckItem();
            bar3 = new DevExpress.XtraBars.Bar();
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            btnTK = new DevExpress.XtraBars.BarButtonItem();
            popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            pageChart = new DevExpress.XtraTab.XtraTabPage();
            panelchart = new DevExpress.XtraEditors.PanelControl();
            directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabTK).BeginInit();
            tabTK.SuspendLayout();
            pageTK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CbbDTS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelchart).BeginInit();
            SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            directXFormContainerControl1.Controls.Add(tabTK);
            directXFormContainerControl1.Controls.Add(barDockControlLeft);
            directXFormContainerControl1.Controls.Add(barDockControlRight);
            directXFormContainerControl1.Controls.Add(barDockControlBottom);
            directXFormContainerControl1.Controls.Add(barDockControlTop);
            directXFormContainerControl1.Location = new Point(1, 31);
            directXFormContainerControl1.Name = "directXFormContainerControl1";
            directXFormContainerControl1.Size = new Size(1210, 727);
            directXFormContainerControl1.TabIndex = 0;
            // 
            // tabTK
            // 
            tabTK.Dock = DockStyle.Fill;
            tabTK.Location = new Point(0, 24);
            tabTK.Name = "tabTK";
            tabTK.SelectedTabPage = pageTK;
            tabTK.Size = new Size(1210, 683);
            tabTK.TabIndex = 4;
            tabTK.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { pageTK, pageChart });
            // 
            // pageTK
            // 
            pageTK.Controls.Add(gridControl1);
            pageTK.Name = "pageTK";
            pageTK.Size = new Size(1208, 658);
            pageTK.Text = "Thống kê";
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(0, 0);
            gridControl1.MainView = gridView;
            gridControl1.MenuManager = barManager1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(1208, 658);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView, gridView1 });
            // 
            // gridView
            // 
            gridView.GridControl = gridControl1;
            gridView.Name = "gridView";
            gridView.OptionsView.ShowGroupPanel = false;
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] { bar2, bar3 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.DockWindowTabFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { btnTK, barSubItemTK, btnPrint, chkXetTuyen, chkTrungTuyen, barStaticItem1, barCbbDTS, chkTrungCap, chkGDTX });
            barManager1.MainMenu = bar2;
            barManager1.MaxItemId = 9;
            barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { CbbDTS });
            barManager1.StatusBar = bar3;
            // 
            // bar2
            // 
            bar2.BarName = "Main menu";
            bar2.DockCol = 0;
            bar2.DockRow = 0;
            bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, barSubItemTK, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, btnPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, chkXetTuyen, DevExpress.XtraBars.BarItemPaintStyle.Standard), new DevExpress.XtraBars.LinkPersistInfo(chkTrungTuyen), new DevExpress.XtraBars.LinkPersistInfo(barStaticItem1), new DevExpress.XtraBars.LinkPersistInfo(barCbbDTS), new DevExpress.XtraBars.LinkPersistInfo(chkTrungCap), new DevExpress.XtraBars.LinkPersistInfo(chkGDTX) });
            bar2.OptionsBar.MultiLine = true;
            bar2.OptionsBar.UseWholeRow = true;
            bar2.Text = "Main menu";
            // 
            // barSubItemTK
            // 
            barSubItemTK.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barSubItemTK.Caption = "Thống kê";
            barSubItemTK.Id = 1;
            barSubItemTK.ImageOptions.Image = (Image)resources.GetObject("barSubItemTK.ImageOptions.Image");
            barSubItemTK.ImageOptions.LargeImage = (Image)resources.GetObject("barSubItemTK.ImageOptions.LargeImage");
            barSubItemTK.Name = "barSubItemTK";
            // 
            // btnPrint
            // 
            btnPrint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            btnPrint.Caption = "In ấn";
            btnPrint.Id = 2;
            btnPrint.ImageOptions.Image = (Image)resources.GetObject("btnPrint.ImageOptions.Image");
            btnPrint.ImageOptions.LargeImage = (Image)resources.GetObject("btnPrint.ImageOptions.LargeImage");
            btnPrint.Name = "btnPrint";
            btnPrint.ItemClick += btnPrint_ItemClick;
            // 
            // chkXetTuyen
            // 
            chkXetTuyen.BindableChecked = true;
            chkXetTuyen.Caption = "Xét tuyển";
            chkXetTuyen.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            chkXetTuyen.Checked = true;
            chkXetTuyen.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            chkXetTuyen.GroupIndex = 2;
            chkXetTuyen.Id = 3;
            chkXetTuyen.Name = "chkXetTuyen";
            chkXetTuyen.CheckedChanged += chkXetTuyen_CheckedChanged;
            // 
            // chkTrungTuyen
            // 
            chkTrungTuyen.Caption = "Trúng tuyển";
            chkTrungTuyen.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            chkTrungTuyen.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            chkTrungTuyen.GroupIndex = 2;
            chkTrungTuyen.Id = 4;
            chkTrungTuyen.Name = "chkTrungTuyen";
            chkTrungTuyen.CheckedChanged += chkTrungTuyen_CheckedChanged;
            // 
            // barStaticItem1
            // 
            barStaticItem1.Caption = "Đợt TS";
            barStaticItem1.Id = 5;
            barStaticItem1.Name = "barStaticItem1";
            barStaticItem1.Size = new Size(50, 0);
            barStaticItem1.Width = 50;
            // 
            // barCbbDTS
            // 
            barCbbDTS.Caption = "barEditItem1";
            barCbbDTS.Edit = CbbDTS;
            barCbbDTS.Id = 6;
            barCbbDTS.Name = "barCbbDTS";
            barCbbDTS.Size = new Size(100, 0);
            barCbbDTS.EditValueChanged += barCbbDTS_EditValueChanged;
            // 
            // CbbDTS
            // 
            CbbDTS.AutoHeight = false;
            CbbDTS.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            CbbDTS.DropDownRows = 4;
            CbbDTS.Name = "CbbDTS";
            // 
            // chkTrungCap
            // 
            chkTrungCap.BindableChecked = true;
            chkTrungCap.Caption = "Trung cấp";
            chkTrungCap.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            chkTrungCap.Checked = true;
            chkTrungCap.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            chkTrungCap.GroupIndex = 1;
            chkTrungCap.Id = 7;
            chkTrungCap.Name = "chkTrungCap";
            chkTrungCap.CheckedChanged += chkTrungCap_CheckedChanged;
            // 
            // chkGDTX
            // 
            chkGDTX.Caption = "GDTX";
            chkGDTX.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            chkGDTX.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            chkGDTX.GroupIndex = 1;
            chkGDTX.Id = 8;
            chkGDTX.Name = "chkGDTX";
            chkGDTX.CheckedChanged += chkGDTX_CheckedChanged;
            // 
            // bar3
            // 
            bar3.BarName = "Status bar";
            bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            bar3.DockCol = 0;
            bar3.DockRow = 0;
            bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            bar3.OptionsBar.AllowQuickCustomization = false;
            bar3.OptionsBar.DrawDragBorder = false;
            bar3.OptionsBar.UseWholeRow = true;
            bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(1210, 24);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 707);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(1210, 20);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 24);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 683);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(1210, 24);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 683);
            // 
            // btnTK
            // 
            btnTK.ActAsDropDown = true;
            btnTK.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            btnTK.Caption = "Thống kê";
            btnTK.DropDownControl = popupMenu1;
            btnTK.Id = 0;
            btnTK.ImageOptions.Image = (Image)resources.GetObject("btnTK.ImageOptions.Image");
            btnTK.ImageOptions.LargeImage = (Image)resources.GetObject("btnTK.ImageOptions.LargeImage");
            btnTK.Name = "btnTK";
            // 
            // popupMenu1
            // 
            popupMenu1.Manager = barManager1;
            popupMenu1.Name = "popupMenu1";
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // pageChart
            // 
            pageChart.Controls.Add(panelchart);
            pageChart.Name = "pageChart";
            pageChart.Size = new Size(1194, 688);
            pageChart.Text = "Biểu đồ";
            // 
            // panelchart
            // 
            panelchart.Dock = DockStyle.Fill;
            panelchart.Location = new Point(0, 0);
            panelchart.Name = "panelchart";
            panelchart.Size = new Size(1194, 688);
            panelchart.TabIndex = 0;
            // 
            // F_TK
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ChildControls.Add(directXFormContainerControl1);
            ClientSize = new Size(1212, 759);
            Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "F_TK";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thống kê";
            Load += F_TK_Load;
            directXFormContainerControl1.ResumeLayout(false);
            directXFormContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tabTK).EndInit();
            tabTK.ResumeLayout(false);
            pageTK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)CbbDTS).EndInit();
            ((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            pageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelchart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem barSubItemTK;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.BarCheckItem chkXetTuyen;
        private DevExpress.XtraBars.BarCheckItem chkTrungTuyen;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnTK;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem barCbbDTS;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox CbbDTS;
        private DevExpress.XtraTab.XtraTabControl tabTK;
        private DevExpress.XtraTab.XtraTabPage pageTK;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraTab.XtraTabPage pageChart;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelchart;
        private DevExpress.XtraBars.BarCheckItem chkTrungCap;
        private DevExpress.XtraBars.BarCheckItem chkGDTX;
    }
}