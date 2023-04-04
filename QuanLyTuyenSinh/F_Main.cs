using DevExpress.CodeParser;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTuyenSinh
{
    public partial class F_Main : DevExpress.XtraEditors.XtraForm
    {
        public F_Main()
        {
            InitializeComponent();
        }
        XtraTabbedMdiManager mdiManager;
        private void F_Main_Load(object sender, EventArgs e)
        {            
            mdiManager = new XtraTabbedMdiManager(components);
            mdiManager.MdiParent = this;
            mdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
        }

        private void btnDmTruong_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDmNganh_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void btnDanhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form.F_DanhMuc f = new();
            f.MdiParent = this;
            f.Show();
        }
    }
}