using DevExpress.DataProcessing;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Login : DevExpress.XtraEditors.XtraForm
    {
        private F_Main mainForm;

        public F_Login()
        {
            InitializeComponent();
            Bitmap bitmap0 = new Bitmap(pictureBox1.Image);
            var bt = MakeTransparent(bitmap0, Color.Transparent, 50);
            pictureBox1.Image = bt;

            spinNam.Value = Properties.Settings.Default.NamTS;
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
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DanhSach.CheckUsers();
            string username = txtName.Text;
            string password = txtPass.Text;
            var user = DanhSach.GetUser(username, password);
            if (user != null)
            {
                this.Hide();
                splashScreenManager1.ShowWaitForm();
                DanhSach.CurrUser = user;
                Properties.Settings.Default.NamTS = (int)spinNam.Value;
                Properties.Settings.Default.Save();
                Task.Run(() =>
                {
                    DanhSach.LoadStaticList();
                    //_Generator.RandomDsHSDT(Properties.Settings.Default.NamTS, "1", 250);
                    //DanhSach.LoadStaticList();
                });
                mainForm = new F_Main();
                mainForm.Closed += MainForm_Closed;
                mainForm.Show();

                splashScreenManager1.CloseWaitForm();
            }
            else
            {
                XtraMessageBox.Show(this, "Sai tên đăng nhập hoặc mật khẩu", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}