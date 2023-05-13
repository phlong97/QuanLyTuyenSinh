using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Account : DevExpress.XtraEditors.XtraForm
    {
        private User _user;

        private BindingSource source;

        public F_Account(User user)
        {
            InitializeComponent();
            _user = user;
            source = new BindingSource();
            source.DataSource = _user;
            CreateBinding();
        }

        private void CreateBinding()
        {
            txtAccountName.DataBindings.Clear();
            txtAccountName.DataBindings.Add("Text", source, "UserName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHoVaTen.DataBindings.Clear();
            txtHoVaTen.DataBindings.Add("Text", source, "FullName", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewPass.Text) && !string.IsNullOrEmpty(txtNewPassRe.Text))
            {
                if (txtNewPass.Text == txtNewPassRe.Text)
                {
                    if (PasswordHasher.VerifyPassword(txtNewPass.Text, _user.PasswordHash, _user.Salt))
                    {
                        XtraMessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ");
                        return;
                    }
                    else
                    {
                        if (DanhSach.UpdateUser(_user.Id, txtHoVaTen.Text, txtNewPass.Text))
                        {
                            XtraMessageBox.Show("Đổi mật khẩu thành công!");
                            Close();
                        }
                        else
                            XtraMessageBox.Show("Có lỗi xảy ra!");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Mật khẩu nhập lại không trùng với mật khẩu mới");
                    return;
                }
            }
            else
            {
                DanhSach.UpdateUser(_user.Id, txtHoVaTen.Text);
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}