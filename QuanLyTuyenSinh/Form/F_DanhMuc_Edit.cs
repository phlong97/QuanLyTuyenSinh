using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509.Qualified;
using DevExpress.XtraEditors;
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
    public partial class F_DanhMuc_Edit : DevExpress.XtraEditors.XtraForm
    {
        BaseClass _obj;
        public F_DanhMuc_Edit(BaseClass? obj)
        {
            InitializeComponent();
            _obj = obj;
        }
    }
}