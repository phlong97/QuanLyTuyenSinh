using DevExpress.XtraGrid.Views.Grid;
using QuanLyTuyenSinh.Properties;
using System.ComponentModel;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Main : DevExpress.XtraEditors.XtraForm
    {
        string TenDm { get; set; }
        BindingSource _bindingSource;
        public F_Main()
        {
            InitializeComponent();

        }
        private void F_Main_Load(object sender, EventArgs e)
        {
            pnImg.BackgroundImage = Resources.school_background2_2;
            pnImg.BringToFront();
            GridViewInit();
        }
        private void GridViewInit()
        {
            gridView1.CellValueChanged += GridView_CellValueChanged;
            gridView1.RowUpdated += GridView_RowUpdated;
            gridView1.ValidateRow += GridView_ValidateRow;
        }

        private void GridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (e.Valid)
            {
                var r = e.Row as DanhMuc;
                if (r != null)
                {
                    r.SaveToDb();
                    DanhSach.Refresh = true;
                }
            }
        }

        private void GridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var r = e.Row as DanhMuc;
            if (r != null)
            {
                r.SaveToDb();
                DanhSach.Refresh = true;
            }
        }

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.Equals("colMa"))
            {
                string? value = e.Value.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (CheckDuplicateCode(value))
                    {
                        MessageBox.Show(this, "Trùng mã!");
                        gridView1.DeleteSelectedRows();
                        gridView1.AddNewRow();
                    }
                }
            }
        }

        private bool CheckDuplicateCode(string code)
        {
            return DanhSach.categories.Where(x => x.Loai.Equals(TenDm)).Any(x => x.Ma.Equals(code));
        }

        private void LoadDanhMuc()
        {
            gridView1.Columns.Clear();
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
                case "QT":
                    _bindingSource.DataSource = DanhSach.DsQuocTich;
                    break;
                default: break;
            }
            gridControl.DataSource = _bindingSource;
            panelGrid.BringToFront();
        }

        private void btnTruong_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TruongHoc;
            LoadDanhMuc();
        }

        private void btnNghe_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.NganhNghe;
            LoadDanhMuc();
        }

        private void btnDTUT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DoiTuongUuTien;
            LoadDanhMuc();
        }

        private void btnKVUT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.KhuVucUuTien;
            LoadDanhMuc();
        }

        private void btnDotTS_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DotXetTuyen;
            LoadDanhMuc();
        }

        private void btnDanToc_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DanToc;
            LoadDanhMuc();
        }

        private void btnTonGiao_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TonGiao;
            LoadDanhMuc();
        }

        private void btnTDHV_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TrinhDo;
            LoadDanhMuc();
        }

        private void btnHTDT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.HinhThucDaoTao;
            LoadDanhMuc();
        }

        private void btnQuocTich_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.QuocTich;
            LoadDanhMuc();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelGrid.SendToBack();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = gridView1.GetFocusedRow() as DanhMuc;
            if (r is not null)
            {
                if (MessageBox.Show(this, "Xác nhận xóa?", "Xóa", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (_LiteDb.DeleteObj<ObjCategory>(r.Id))
                    {
                        MessageBox.Show(this, "Xóa thành công");
                        DanhSach.Refresh = true;
                    }
                }

            }
        }


    }
}