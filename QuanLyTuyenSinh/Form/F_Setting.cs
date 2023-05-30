using DevExpress.XtraEditors;
using System.Configuration;
using System.IO;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Setting : DevExpress.XtraEditors.DirectXForm
    {
        private CaiDat _setting;
        private BindingSource source;
        bool dbChanged = false;
        public F_Setting(CaiDat setting)
        {
            InitializeComponent();
            _setting = setting;
            txtDbPath.Text = Properties.Settings.Default.DBPATH;
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
            string errors = string.Empty;
            if (string.IsNullOrEmpty(txtDbPath.Text))
                errors += "Chưa chọn thư mục lưu dữ liệu\n";
            else if (!Path.Exists(txtDbPath.Text))
                errors += "Thư mục lưu dữ liệu không tồm tại\n";

            if (string.IsNullOrEmpty(errors))
            {
                if (dbChanged)
                {
                    if (!DataHelper.CheckDatabase(txtDbPath.Text))
                    {
                        XtraMessageBox.Show("Thư mục chứa dữ liệu không hợp lệ, vui lòng chọn lại!");
                        _Helper.ResetDbPath();

                        return;
                    }
                    Properties.Settings.Default.DBPATH = txtDbPath.Text;
                    Properties.Settings.Default.Save();
                    if (!MainWorkspace.FormMain.IsDisposed)
                        MainWorkspace.FormMain.RefreshDatabase();
                }

                DataHelper.CurrSettings = _setting;
                DataHelper.CurrSettings.Save();

                Close();
            }
            else
                XtraMessageBox.Show(errors);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void F_Setting_Load(object sender, EventArgs e)
        {
            spinCTVM.Properties.MaskSettings.MaskExpression = "n2";

            source = new BindingSource();
            source.DataSource = _setting;
            CreateBinding();
            Shown += F_Setting_Shown;
        }

        private void F_Setting_Shown(object? sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnOpenPath.Click += btnOpenPath_Click;
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (string.IsNullOrEmpty(txtDbPath.Text))
                fbd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            else
                fbd.InitialDirectory = txtDbPath.Text;

            if ((fbd.ShowDialog() == DialogResult.OK) && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                txtDbPath.Text = fbd.SelectedPath;
                if (!fbd.SelectedPath.Equals(Properties.Settings.Default.DBPATH))
                    dbChanged = true;
            }
        }
    }
}