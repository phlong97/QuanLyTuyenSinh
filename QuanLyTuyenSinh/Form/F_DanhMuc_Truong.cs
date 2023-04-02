using DevExpress.CodeParser;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_DanhMuc_Truong : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        BindingList<Truong> src;
        public F_DanhMuc_Truong()
        {            
            InitializeComponent();
        }

        private void F_DanhMuc_Load(object sender, EventArgs e)
        {
            src = new BindingList<Truong>(DanhSach.DsTruong);
            gridControl.DataSource = src;
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}