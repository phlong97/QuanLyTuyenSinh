using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.IO;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Login : DevExpress.XtraEditors.XtraForm
    {
        public F_Login()
        {
            InitializeComponent();
            spinNam.Value = Properties.Settings.Default.NamTS;
            txtName.Text = Properties.Settings.Default.UserName;
            //Properties.Settings.Default.DBPATH = string.Empty;
            //Properties.Settings.Default.Save();
        }
        private void lblExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.DBPATH) || !DataHelper.CheckDatabase(Properties.Settings.Default.DBPATH))
            {
                XtraMessageBox.Show(this, "Đường dẫn lưu dữ liệu không hợp lệ, vui lòng chọn lại", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Hide();
                Form.F_DBPATH f = new();
                f.FormClosed += (sender, e) =>
                {
                    Show();
                };
                f.Show();
                return;
            }

            DataHelper.CheckUsers();
            string username = txtName.Text.Trim();
            string password = txtPass.Text.Trim();
            var user = DataHelper.GetUser(username, password);
            if (user != null)
            {
                Hide();
                SplashScreenManager.ShowForm(this, typeof(F_Wait));
                DataHelper.CurrUser = user;
                Properties.Settings.Default.NamTS = (int)spinNam.Value;
                Properties.Settings.Default.UserName = txtName.Text;
                Properties.Settings.Default.Save();
                Task.Run(DataHelper.LoadStaticList);
                MainWorkspace.FormMain = new F_Main();
                MainWorkspace.FormMain.FormClosed += FormMain_FormClosed;
                MainWorkspace.FormMain.Show();
                SplashScreenManager.CloseForm();
            }
            else
            {
                XtraMessageBox.Show(this, "Sai tên đăng nhập hoặc mật khẩu", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void F_Login_Load(object sender, EventArgs e)
        {

        }
    }
}