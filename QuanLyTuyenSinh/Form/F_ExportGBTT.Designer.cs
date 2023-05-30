namespace QuanLyTuyenSinh.Form
{
    partial class F_ExportGBTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_ExportGBTT));
            directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            groupControl1 = new DevExpress.XtraEditors.GroupControl();
            progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            NgayHetHan = new DevExpress.XtraEditors.DateEdit();
            btnExport = new DevExpress.XtraEditors.SimpleButton();
            TuNgay = new DevExpress.XtraEditors.DateEdit();
            NgayXuat = new DevExpress.XtraEditors.DateEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            Phut = new DevExpress.XtraEditors.SpinEdit();
            Gio = new DevExpress.XtraEditors.SpinEdit();
            DenNgay = new DevExpress.XtraEditors.DateEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl6 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NgayHetHan.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NgayHetHan.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TuNgay.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TuNgay.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NgayXuat.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NgayXuat.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Phut.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gio.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DenNgay.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DenNgay.Properties.CalendarTimeProperties).BeginInit();
            SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            directXFormContainerControl1.Controls.Add(panelControl1);
            directXFormContainerControl1.Location = new Point(1, 31);
            directXFormContainerControl1.Name = "directXFormContainerControl1";
            directXFormContainerControl1.Size = new Size(443, 233);
            directXFormContainerControl1.TabIndex = 0;
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(groupControl1);
            panelControl1.Dock = DockStyle.Fill;
            panelControl1.Location = new Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new Size(443, 233);
            panelControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(progressBarControl1);
            groupControl1.Controls.Add(NgayHetHan);
            groupControl1.Controls.Add(btnExport);
            groupControl1.Controls.Add(TuNgay);
            groupControl1.Controls.Add(NgayXuat);
            groupControl1.Controls.Add(labelControl1);
            groupControl1.Controls.Add(Phut);
            groupControl1.Controls.Add(Gio);
            groupControl1.Controls.Add(DenNgay);
            groupControl1.Controls.Add(labelControl4);
            groupControl1.Controls.Add(labelControl7);
            groupControl1.Controls.Add(labelControl5);
            groupControl1.Controls.Add(labelControl3);
            groupControl1.Controls.Add(labelControl6);
            groupControl1.Controls.Add(labelControl2);
            groupControl1.Dock = DockStyle.Fill;
            groupControl1.Location = new Point(2, 2);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(439, 229);
            groupControl1.TabIndex = 4;
            groupControl1.Text = "Thời gian";
            // 
            // progressBarControl1
            // 
            progressBarControl1.Location = new Point(5, 203);
            progressBarControl1.Name = "progressBarControl1";
            progressBarControl1.Size = new Size(429, 21);
            progressBarControl1.TabIndex = 4;
            // 
            // NgayHetHan
            // 
            NgayHetHan.EditValue = null;
            NgayHetHan.Location = new Point(279, 97);
            NgayHetHan.Name = "NgayHetHan";
            NgayHetHan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            NgayHetHan.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            NgayHetHan.Size = new Size(90, 20);
            NgayHetHan.TabIndex = 1;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Right;
            btnExport.ImageOptions.Image = (Image)resources.GetObject("btnExport.ImageOptions.Image");
            btnExport.Location = new Point(122, 160);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(163, 23);
            btnExport.TabIndex = 3;
            btnExport.Text = "Xuất giấy báo trúng tuyển";
            btnExport.Click += btnExport_Click;
            // 
            // TuNgay
            // 
            TuNgay.EditValue = null;
            TuNgay.Location = new Point(102, 68);
            TuNgay.Name = "TuNgay";
            TuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            TuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            TuNgay.Size = new Size(90, 20);
            TuNgay.TabIndex = 1;
            // 
            // NgayXuat
            // 
            NgayXuat.EditValue = null;
            NgayXuat.Location = new Point(279, 71);
            NgayXuat.Name = "NgayXuat";
            NgayXuat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            NgayXuat.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            NgayXuat.Size = new Size(90, 20);
            NgayXuat.TabIndex = 1;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(225, 74);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(48, 13);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Ngày xuất";
            // 
            // Phut
            // 
            Phut.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            Phut.Location = new Point(375, 32);
            Phut.Name = "Phut";
            Phut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            Phut.Properties.IsFloatValue = false;
            Phut.Properties.MaskSettings.Set("mask", "N00");
            Phut.Size = new Size(35, 20);
            Phut.TabIndex = 2;
            // 
            // Gio
            // 
            Gio.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            Gio.Location = new Point(307, 32);
            Gio.Name = "Gio";
            Gio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            Gio.Properties.IsFloatValue = false;
            Gio.Properties.MaskSettings.Set("mask", "N00");
            Gio.Size = new Size(40, 20);
            Gio.TabIndex = 2;
            // 
            // DenNgay
            // 
            DenNgay.EditValue = null;
            DenNgay.Location = new Point(102, 94);
            DenNgay.Name = "DenNgay";
            DenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            DenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            DenNgay.Size = new Size(90, 20);
            DenNgay.TabIndex = 1;
            // 
            // labelControl4
            // 
            labelControl4.Location = new Point(416, 35);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new Size(21, 13);
            labelControl4.TabIndex = 0;
            labelControl4.Text = "phút";
            // 
            // labelControl7
            // 
            labelControl7.Location = new Point(209, 100);
            labelControl7.Name = "labelControl7";
            labelControl7.Size = new Size(64, 13);
            labelControl7.TabIndex = 0;
            labelControl7.Text = "Ngày hêt hạn";
            // 
            // labelControl5
            // 
            labelControl5.Location = new Point(55, 71);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new Size(39, 13);
            labelControl5.TabIndex = 0;
            labelControl5.Text = "Từ ngày";
            // 
            // labelControl3
            // 
            labelControl3.Location = new Point(353, 35);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new Size(16, 13);
            labelControl3.TabIndex = 0;
            labelControl3.Text = "Giờ";
            // 
            // labelControl6
            // 
            labelControl6.Location = new Point(48, 97);
            labelControl6.Name = "labelControl6";
            labelControl6.Size = new Size(46, 13);
            labelControl6.TabIndex = 0;
            labelControl6.Text = "Đến ngày";
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(194, 35);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(105, 13);
            labelControl2.TabIndex = 0;
            labelControl2.Text = "Thời  gian làm thủ tục:";
            // 
            // F_ExportGBTT
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ChildControls.Add(directXFormContainerControl1);
            ClientSize = new Size(445, 265);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_ExportGBTT";
            Text = "Thiết lập";
            Load += F_ExportGBTT_Load;
            directXFormContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NgayHetHan.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NgayHetHan.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TuNgay.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)TuNgay.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NgayXuat.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NgayXuat.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Phut.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gio.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)DenNgay.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)DenNgay.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit Phut;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit Gio;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit DenNgay;
        private DevExpress.XtraEditors.DateEdit TuNgay;
        private DevExpress.XtraEditors.DateEdit NgayXuat;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit NgayHetHan;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
    }
}