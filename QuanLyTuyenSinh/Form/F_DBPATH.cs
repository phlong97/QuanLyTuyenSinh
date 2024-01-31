using DevExpress.XtraEditors;
using System.IO;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_DBPATH : DevExpress.XtraEditors.DirectXForm
    {
        public F_DBPATH()
        {
            InitializeComponent();
            txtDbPath.Text = Properties.Settings.Default.DBPATH;
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
            }
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
                if (!DataHelper.CheckDatabase(txtDbPath.Text))
                {
                    XtraMessageBox.Show("Thư mục chứa dữ liệu không hợp lệ, vui lòng chọn lại!");

                    return;
                }

                Properties.Settings.Default.DBPATH = txtDbPath.Text;
                Properties.Settings.Default.Save();
                if (MainWorkspace.FormMain is not null && !MainWorkspace.FormMain.IsDisposed)
                    MainWorkspace.FormMain.RefreshDatabase();
                Close();
            }
            else
                XtraMessageBox.Show(errors);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}