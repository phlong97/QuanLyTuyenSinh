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
    public partial class F_DanhMuc : DevExpress.XtraEditors.XtraForm
    {
        string TenDm { get; set; }
        BindingSource _bindingSource;
        public F_DanhMuc()
        {
            InitializeComponent();            
        }

        private void LoadDanhMuc()
        {
            _bindingSource = new BindingSource();
            switch (TenDm)
            {
                case "TH":
                    _bindingSource.DataSource = DanhSach.DsTruong;
                    break;
                case "NN":
                    _bindingSource.DataSource = DanhSach.DsNghe;
                    break;
                case "DTUT":
                    _bindingSource.DataSource = DanhSach.DsDoiTuongUT;
                    break;
                case "KVUT":
                    _bindingSource.DataSource = DanhSach.DsKhuVucUT;
                    break;
                case "DT":
                    _bindingSource.DataSource = DanhSach.DsDanToc;
                    break;
                case "TG":
                    _bindingSource.DataSource = DanhSach.DsTonGiao;
                    break;
                case "TDHV":
                    _bindingSource.DataSource = DanhSach.DsTrinhDo;
                    break;
                case "HTDT":
                    _bindingSource.DataSource = DanhSach.DsHinhThucDT;
                    break;
                case "DTS":
                    _bindingSource.DataSource = DanhSach.DsDotXetTuyen;
                    break;
                default: break;
            }
            gridControl.DataSource = _bindingSource;
        }

        private void btnTruong_Click(object sender, EventArgs e)
        {
            TenDm = "T";
            LoadDanhMuc();
        }

        private void btnNghe_Click(object sender, EventArgs e)
        {
            TenDm = "NN";
            LoadDanhMuc();
        }

        private void btnDTUT_Click(object sender, EventArgs e)
        {
            TenDm = "DTUT";
            LoadDanhMuc();
        }

        private void btnKVUT_Click(object sender, EventArgs e)
        {
            TenDm = "KVUT";
            LoadDanhMuc();
        }

        private void btnDotTS_Click(object sender, EventArgs e)
        {
            TenDm = "DTS";
            LoadDanhMuc();
        }

        private void btnDanToc_Click(object sender, EventArgs e)
        {
            TenDm = "DT";
            LoadDanhMuc();
        }

        private void btnTonGiao_Click(object sender, EventArgs e)
        {
            TenDm = "TG";
            LoadDanhMuc();
        }

        private void btnTDHV_Click(object sender, EventArgs e)
        {
            TenDm = "TDHV";
            LoadDanhMuc();
        }

        private void btnHTDT_Click(object sender, EventArgs e)
        {
            TenDm = "HTDT";
            LoadDanhMuc();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseClass obj = null;
            switch (TenDm)
            {
                case "TH":
                    obj = new Truong();
                    break;
                case "NN":
                    obj = new Nghe();
                    break;
                case "DTUT":
                    obj = new DoiTuongUT();
                    break;
                case "KVUT":
                    obj = new KhuVucUT();
                    break;
                case "DT":
                    obj = new DanToc();
                    break;
                case "TG":
                    obj = new TonGiao();
                    break;
                case "TDHV":
                    obj = new TrinhDo();
                    break;
                case "HTDT":
                    obj = new HinhThucDaoTao();
                    break;
                case "DTS":
                    obj = new DotXetTuyen();
                    break;
                default: break;
            }
            using (F_DanhMuc_Edit f = new(obj))
            {
                f.ShowDialog();
            }
               
            
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseClass curr = null;
            switch (TenDm)
            {
                case "T":
                    obj = new Truong();
                    break;
                case "NN":
                    obj = new Nghe();
                    break;
                case "DTUT":
                    obj = new DoiTuongUT();
                    break;
                case "KVUT":
                    obj = new KhuVucUT();
                    break;
                case "DT":
                    obj = new DanToc();
                    break;
                case "TG":
                    obj = new TonGiao();
                    break;
                case "TDHV":
                    obj = new TrinhDo();
                    break;
                case "HTDT":
                    obj = new HinhThucDaoTao();
                    break;
                case "DTS":
                    obj = new DotXetTuyen();
                    break;
                default: break;
            }
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}