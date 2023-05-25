using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Windows;
using Color = System.Drawing.Color;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Login : DevExpress.XtraEditors.XtraForm
    {
        public F_Login()
        {
            InitializeComponent();
            Bitmap bitmap0 = new Bitmap(pictureBox1.Image);
            var bt = MakeTransparent(bitmap0, Color.Transparent, 50);
            pictureBox1.Image = bt;
            spinNam.Value = Properties.Settings.Default.NamTS;
            txtName.Text = Properties.Settings.Default.UserName;
        }

        private Bitmap MakeTransparent(Bitmap bitmap, Color color, int tolerance)
        {
            Bitmap transparentImage = new Bitmap(bitmap);

            for (int i = transparentImage.Size.Width - 1; i >= 0; i--)
            {
                for (int j = transparentImage.Size.Height - 1; j >= 0; j--)
                {
                    var currentColor = transparentImage.GetPixel(i, j);
                    if (Math.Abs(color.R - currentColor.R) < tolerance &&
                      Math.Abs(color.G - currentColor.G) < tolerance &&
                      Math.Abs(color.B - currentColor.B) < tolerance)
                        transparentImage.SetPixel(i, j, color);
                }
            }

            transparentImage.MakeTransparent(color);
            return transparentImage;
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Data.CheckUsers();
            string username = txtName.Text;
            string password = txtPass.Text;
            var user = Data.GetUser(username, password);
            if (user != null)
            {
                Hide();
                SplashScreenManager.ShowForm(this, typeof(F_Wait));
                Data.CurrUser = user;
                Properties.Settings.Default.NamTS = (int)spinNam.Value;
                Properties.Settings.Default.UserName = txtName.Text;
                Properties.Settings.Default.Save();
                Task.Run(() =>
                {
                    Data.LoadStaticList2();
                });
                MainWorkspace.FormMain = new F_Main();
                MainWorkspace.FormMain.FormClosed += FormMain_FormClosed;
                MainWorkspace.FormMain.Show();
                SplashScreenManager.CloseForm();
                Hide();
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
    }
}