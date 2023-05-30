using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Login : DevExpress.XtraEditors.XtraForm
    {
        public F_Login()
        {
            InitializeComponent();
            spinNam.Value = Properties.Settings.Default.NamTS;
            txtName.Text = Properties.Settings.Default.UserName;
        }
        private void lblExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataHelper.CheckUsers();
            string username = txtName.Text;
            string password = txtPass.Text;
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