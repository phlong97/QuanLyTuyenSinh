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
    public partial class F_HoSo : DevExpress.XtraEditors.XtraForm
    {
        HoSoDuTuyen _hoSo;
        BindingSource _source;
        public F_HoSo(HoSoDuTuyen hoSo)
        {
            InitializeComponent();
            _hoSo = hoSo;

        }

        private void F_HoSo_Load(object sender, EventArgs e)
        {
            _source = new BindingSource();
            _source.DataSource = _hoSo;
        }
        void CreateBinding()
        {

        }
    }
}