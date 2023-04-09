using DevExpress.XtraCharts.Native;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using LiteDB;
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
            //pnImg.BackgroundImage = Resources.school_background2_2;
            pnImg.BringToFront();

            GridViewInit();

            _spinNam.Value = DateTime.Now.Year;
        }
        #region Xử lý GridControl
        private void GridViewInit()
        {
            gridView1.IndicatorWidth = 55;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            gridView1.CellValueChanged += GridView_CellValueChanged;
            gridView1.RowUpdated += GridView_RowUpdated;
            gridView1.ShowingEditor += GridView_ShowingEditor;
            gridView1.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

        }

        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString("D3");
        }

        private void GridView_ShowingEditor(object? sender, CancelEventArgs e)
        {
            //e.Cancel = (gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
        }       

        private void GridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var r = e.Row as DBClass;
            if (r != null)
            {
                if (r.SaveToDB())
                {
                    DanhSach.RefreshDS(TenDm);
                }
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
        #endregion

        private void LoadDanhMuc()
        {
            gridView1.Columns.Clear();
            _bindingSource = new BindingSource();
            switch (TenDm)
            {
                case TuDien.CategoryName.TruongHoc:
                    _bindingSource.DataSource = DanhSach.DsTruong;
                    break;
                case TuDien.CategoryName.NganhNghe:
                    _bindingSource.DataSource = DanhSach.DsNghe;
                    break;
                case TuDien.CategoryName.DoiTuongUuTien:
                    _bindingSource.DataSource = DanhSach.DsDoiTuongUT;
                    break;
                case TuDien.CategoryName.KhuVucUuTien:
                    _bindingSource.DataSource = DanhSach.DsKhuVucUT;
                    break;
                case TuDien.CategoryName.DanToc:
                    _bindingSource.DataSource = DanhSach.DsDanToc;
                    break;
                case TuDien.CategoryName.TonGiao:
                    _bindingSource.DataSource = DanhSach.DsTonGiao;
                    break;
                case TuDien.CategoryName.TrinhDo:
                    _bindingSource.DataSource = DanhSach.DsTrinhDo;
                    break;
                case TuDien.CategoryName.HinhThucDaoTao:
                    _bindingSource.DataSource = DanhSach.DsHinhThucDT;
                    break;
                case TuDien.CategoryName.QuocTich:
                    _bindingSource.DataSource = DanhSach.DsQuocTich;
                    break;
                case TuDien.CategoryName.DotXetTuyen:
                    _bindingSource.DataSource = DanhSach.DsDotXetTuyen;
                    break;
                case TuDien.CategoryName.ChiTieu:
                    _bindingSource.DataSource = DanhSach.DsChiTieu;
                    break;
                default: break;
            }
            gridControl.DataSource = _bindingSource;
            if (TenDm.Equals(TuDien.CategoryName.ChiTieu))
            {
                gridView1.Columns.ColumnByName("colNam").Group();
            }
            btnAdd.Visible = !TenDm.Equals(TuDien.CategoryName.ChiTieu);
            _panelNam.Visible = TenDm.Equals(TuDien.CategoryName.ChiTieu);
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
        private void btnChiTieu_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.ChiTieu;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = gridView1.GetFocusedRow() as DBClass;
            if (r is not null)
            {
                if (MessageBox.Show(this, "Xác nhận xóa?", "Xóa", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (r.DeleteFromDB())
                    {
                        DanhSach.RefreshDS(TenDm);
                        LoadDanhMuc();
                    }
                }

            }
        }

        private void btnLapChiTieu_Click(object sender, EventArgs e)
        {
            int Nam = Convert.ToInt32(_spinNam.Value);
            if (DanhSach.DsChiTieu.FirstOrDefault(x => x.Nam == Nam) is not null)
            {
                MessageBox.Show(this, $"Đã tồn tại chỉ tiêu năm {Nam}");
                return;
            }

            foreach (var nghe in DanhSach.DsNghe)
            {
                ChiTieuXetTuyen ct = new() { IdNghe = nghe.Id, Nam = Nam, ChiTieu = TuDien.Settings.CHITIEUMACDINH };
                ct.SaveToDB();
            }
            DanhSach.RefreshDS(TenDm);
            LoadDanhMuc();
            gridView1.ExpandAllGroups();
        }
    }
}