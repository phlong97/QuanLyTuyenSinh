﻿using DevExpress.XtraEditors;
using System.Configuration;
using System.IO;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Setting : DevExpress.XtraEditors.DirectXForm
    {
        private CaiDat setting;
        private BindingSource source;
        public F_Setting(CaiDat setting)
        {
            InitializeComponent();
            this.setting = setting;
        }

        public void CreateBinding()
        {
            txtTenTruong.DataBindings.Clear();
            txtTenTruong.DataBindings.Add("Text", source, "TENTRUONG", true, DataSourceUpdateMode.OnPropertyChanged);
            spinCTVM.DataBindings.Clear();
            spinCTVM.DataBindings.Add("EditValue", source, "CHITIEUVUOTMUC", true, DataSourceUpdateMode.OnPropertyChanged);

            spinHKTHCS_TB.DataBindings.Clear();
            spinHKTHCS_TB.DataBindings.Add("EditValue", source, "HANH_KIEM_THCS.TRUNG_BINH", true, DataSourceUpdateMode.OnPropertyChanged);
            spinHKTHCS_Kha.DataBindings.Clear();
            spinHKTHCS_Kha.DataBindings.Add("EditValue", source, "HANH_KIEM_THCS.KHA", true, DataSourceUpdateMode.OnPropertyChanged);
            spinHKTHCS_Tot.DataBindings.Clear();
            spinHKTHCS_Tot.DataBindings.Add("EditValue", source, "HANH_KIEM_THCS.TOT", true, DataSourceUpdateMode.OnPropertyChanged);

            spinXLTNTHCS_TB.DataBindings.Clear();
            spinXLTNTHCS_TB.DataBindings.Add("EditValue", source, "XLTN_THCS.TRUNG_BINH", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLTNTHCS_Kha.DataBindings.Clear();
            spinXLTNTHCS_Kha.DataBindings.Add("EditValue", source, "XLTN_THCS.KHA", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLTNTHCS_Gioi.DataBindings.Clear();
            spinXLTNTHCS_Gioi.DataBindings.Add("EditValue", source, "XLTN_THCS.GIOI", true, DataSourceUpdateMode.OnPropertyChanged);

            spinHKTHPT_TB.DataBindings.Clear();
            spinHKTHPT_TB.DataBindings.Add("EditValue", source, "HANH_KIEM_THPT.TRUNG_BINH", true, DataSourceUpdateMode.OnPropertyChanged);
            spinHKTHPT_Kha.DataBindings.Clear();
            spinHKTHPT_Kha.DataBindings.Add("EditValue", source, "HANH_KIEM_THPT.KHA", true, DataSourceUpdateMode.OnPropertyChanged);
            spinHKTHPT_Tot.DataBindings.Clear();
            spinHKTHPT_Tot.DataBindings.Add("EditValue", source, "HANH_KIEM_THPT.TOT", true, DataSourceUpdateMode.OnPropertyChanged);

            spinXLTNTHPT_TB.DataBindings.Clear();
            spinXLTNTHPT_TB.DataBindings.Add("EditValue", source, "XLTN_THPT.TRUNG_BINH", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLTNTHPT_Kha.DataBindings.Clear();
            spinXLTNTHPT_Kha.DataBindings.Add("EditValue", source, "XLTN_THPT.KHA", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLTNTHPT_Gioi.DataBindings.Clear();
            spinXLTNTHPT_Gioi.DataBindings.Add("EditValue", source, "XLTN_THPT.GIOI", true, DataSourceUpdateMode.OnPropertyChanged);

            spinXLHTTHPT_TB.DataBindings.Clear();
            spinXLHTTHPT_TB.DataBindings.Add("EditValue", source, "XLHT_THPT.TRUNG_BINH", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLHTTHPT_Kha.DataBindings.Clear();
            spinXLHTTHPT_Kha.DataBindings.Add("EditValue", source, "XLHT_THPT.KHA", true, DataSourceUpdateMode.OnPropertyChanged);
            spinXLHTTHPT_Gioi.DataBindings.Clear();
            spinXLHTTHPT_Gioi.DataBindings.Add("EditValue", source, "XLHT_THPT.GIOI", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setting.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void F_Setting_Load(object sender, EventArgs e)
        {
            spinCTVM.Properties.MaskSettings.MaskExpression = "n2";

            source = new BindingSource();
            source.DataSource = setting;
            CreateBinding();
            Shown += F_Setting_Shown;
        }

        private void F_Setting_Shown(object? sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnDBPath_Click(object sender, EventArgs e)
        {
            MainWorkspace.FormDBPath.ShowDialog();
        }
    }
}