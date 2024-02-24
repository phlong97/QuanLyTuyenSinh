using ClosedXML.Excel;
using DevExpress.Export;
using DevExpress.Mvvm.Native;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using LiteDB;
using Microsoft.Office.Interop.Excel;
using QuanLyTuyenSinh.Models;
using QuanLyTuyenSinh.Properties;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using SummaryItemType = DevExpress.Data.SummaryItemType;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_Main : DevExpress.XtraEditors.XtraForm
    {
        private BindingSource _bindingSource;
        private bool _EditMode;

        public bool EditMode
        {
            get => _EditMode;
            set
            {
                _EditMode = value;
                btnEdit.Text = value ? "Lưu" : "Sửa";
            }
        }

        public void RefreshData()
        {
            DataHelper.RefreshDS(TenDm);
            ReLoadDS();
        }
        public void RefreshDatabase()
        {
            DataHelper.LoadStaticList();
            ReLoadDS();
        }

        private int _NamTS { get; set; }
        private string TenDm { get; set; }

        public F_Main()
        {
            InitializeComponent();
            _NamTS = Properties.Settings.Default.NamTS;
            Text = "Quản lý xét tuyển - " + DataHelper.CurrSettings.TENTRUONG;
            txtNamTS.Caption += $" <b>{_NamTS}</b>";
            txtUser.Caption += $" <b>{DataHelper.CurrUser.UserName}</b>";
        }
        private void F_Main_Shown(object? sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnDelete.Click += btnDelete_Click;
            btnLapChiTieu.Click += btnLapChiTieu_Click;
            btnClose.Click += btnClose_Click;
            btnExel.Click += BtnExel_Click;
            btnRefreshDTS.Click += BtnRefreshDTS_Click;
            btnExportGBTT.Click += BtnExportGBTT_Click;

            cbbDTS.EditValueChanged += cbbDTS_EditValueChanged;
            cbbTDHV.EditValueChanged += CbbHTDT_EditValueChanged;
            lookTinh.TextChanged += LookTinh_TextChanged;
            lookQuanHuyen.TextChanged += LookQuanHuyen_TextChanged;
            lookXa.TextChanged += LookXa_TextChanged;
            lookTruong.TextChanged += LookTruong_TextChanged;
            chkKhongTT.CheckedChanged += ChkKhongTT_CheckedChanged;
            rdTC.CheckedChanged += RdTC_CheckedChanged;
            rdGDTX.CheckedChanged += RdGDTX_CheckedChanged;
        }

        private void RdGDTX_CheckedChanged(object? sender, EventArgs e)
        {
            LoadForm();
        }

        private void RdTC_CheckedChanged(object? sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            EditMode = false;

            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC))
            {
                if (cbbDTS.SelectedIndex <= 0)
                {
                    XtraMessageBox.Show(this, "Chưa chọn đợt xét tuyển?", "Lỗi");
                    return;
                }
                int dts;
                if (!int.TryParse(cbbDTS.SelectedItem.ToString(), out dts))
                    return;
                int nts = _NamTS;
                var dxt = DataHelper.DsDotXetTuyen.FirstOrDefault(x => x.NamTS == _NamTS && x.DotTS == dts);
                if (dxt is null)
                    XtraMessageBox.Show(this, "Đợt xét tuyển không tồn tại", "Lỗi");
                if (dxt is not null && dxt.DenNgay < DateTime.Today)
                {
                    if (XtraMessageBox.Show(
                            this,
                            $"Đã hết thời hạn xét tuyển đợt {dts} năm {nts}, bạn vẫn muốn nhập hồ sơ?",
                            "Cảnh báo",
                            MessageBoxButtons.YesNo) ==
                        DialogResult.No)
                        return;
                }

                var dt = DataHelper.DsDanToc.FirstOrDefault();
                var qt = DataHelper.DsQuocTich.FirstOrDefault();
                var tg = DataHelper.DsTonGiao.FirstOrDefault();
                var tdvh = DataHelper.DsTrinhDo.FirstOrDefault();
                var kvut = DataHelper.DsKhuVucUT.FirstOrDefault(x => x.Ma == "KV2-NT");
                try
                {
                    if(rdTC.Checked)
                    {
                        F_HoSo f = new(
                        new HoSoDuTuyenTC
                        {
                            NamTS = nts,
                            DotTS = dts,
                            HTDT = "Chính quy",
                            MaTinh = "511",
                            MaHuyen = "51103",
                            NoiSinh = "Khánh Hòa",
                            IdQuocTich = qt is not null ? qt.Id : string.Empty,
                            IdDanToc = dt is not null ? dt.Id : string.Empty,
                            IdTrinhDoVH = tdvh is not null ? tdvh.Id : string.Empty,
                            IdTonGiao = tg is not null ? tg.Id : string.Empty,
                            IdKVUT = kvut is not null ? kvut.Id : string.Empty,
                        });
                            f.Show(this);
                    }
                    else
                    {
                        F_HoSo_TX f = new(
                        new HoSoDuTuyenGDTX
                        {
                            NamTS = nts,
                            DotTS = dts,
                            HTDT = "Chính quy",
                            MaTinh = "511",
                            MaHuyen = "51103",
                            NoiSinh = "Khánh Hòa",
                            IdQuocTich = qt is not null ? qt.Id : string.Empty,
                            IdDanToc = dt is not null ? dt.Id : string.Empty,
                            IdTrinhDoVH = tdvh is not null ? tdvh.Id : string.Empty,
                            IdTonGiao = tg is not null ? tg.Id : string.Empty,
                            IdKVUT = kvut is not null ? kvut.Id : string.Empty,
                        });
                        f.Show(this);
                    }
                }
                finally
                {
                }
            }
            else
                gridView.AddNewRow();
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            panelGrid.SendToBack();
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            var selectedRowHandles = gridView.GetSelectedRows();
            if (selectedRowHandles.Length == 0) return;
            if (selectedRowHandles[0] == -1) selectedRowHandles = selectedRowHandles.Skip(1).ToArray();
            if (selectedRowHandles.Length > 0)
            {
                if (XtraMessageBox.Show(this, "Xác nhận xóa?", "Xóa", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        for (int i = selectedRowHandles.Length - 1; i >= 0; i--)
                        {
                            int seletedRowHandle = selectedRowHandles[i];
                            var r = gridView.GetRow(seletedRowHandle) as DBClass;
                            if (r is not null)
                            {
                                r.Delete();
                            }
                        }
                    }
                    finally
                    {
                        RefreshData();
                    }
                }
            }
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC))
            {
                try
                {
                    var r = gridView.GetFocusedRow() as BaseClass;
                    if (r is not null)
                    {
                        if(rdTC.Checked)
                        {
                            var hs = DataHelper.DSHoSoXTTC.FirstOrDefault(x => x.Id.Equals(r.Id));
                            if (hs is not null)
                            {
                                F_HoSo f = new(hs.CloneJson());
                                f.Show(this);
                            }
                        }
                        else
                        {
                            var hs = DataHelper.DSHoSoXetTuyenTX.FirstOrDefault(x => x.Id.Equals(r.Id));
                            if (hs is not null)
                            {
                                F_HoSo_TX f = new(hs.CloneJson());
                                f.Show(this);
                            }
                        }
                    }
                }
                catch
                {
                    return;
                }
                return;
            }
            else
            {
                EditMode = !EditMode;
            }
        }

        private void BtnExel_Click(object? sender, EventArgs e)
        {
            if (_bindingSource is null)
                return;
            if (_bindingSource.Count <= 0)
            {
                XtraMessageBox.Show(this, "Chưa có dữ liệu!", "Lỗi");
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Exel file";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Exel file (*.xlsx)|*.xlsx";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Ensure that the data-aware export mode is enabled.
                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG;
                    //Customize export options
                    var view = (gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    view.OptionsPrint.PrintHeader = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    XlsxExportOptionsEx advOptions = new XlsxExportOptionsEx();
                    advOptions.ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.True;
                    advOptions.AllowGrouping = DefaultBoolean.False;
                    advOptions.LayoutMode = DevExpress.Export.LayoutMode.Table;
                    advOptions.ShowGroupSummaries = DefaultBoolean.False;
                    advOptions.TextExportMode = TextExportMode.Text;
                    advOptions.SheetName = TenDm;
                    gridView.VisibleColumns[0].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

                    gridView.ClearGrouping();
                    gridView.BestFitColumns(true);
                    gridControl.ExportToXlsx(saveFileDialog1.FileName, advOptions);
                    LoadForm();
                    XtraMessageBox.Show(this, "Xuất file thành công", "Thành công");
                }
                catch
                {
                    XtraMessageBox.Show(this, "Có lỗi xảy ra", "Lỗi");
                }
            }
        }

        private void btnLapChiTieu_Click(object? sender, EventArgs e)
        {
            if(rdTC.Checked)
            {
                if (DataHelper.DsChiTieuTC.Count() < DataHelper.DsNghe.Count())
                {
                    using (var db = _LiteDb.GetDatabase())
                    {
                        foreach (var nghe in DataHelper.DsNghe)
                        {
                            if (DataHelper.DsChiTieuTC.FirstOrDefault(x => x.IdNghe.Equals(nghe.Id)) is null)
                            {
                                ChiTieuTC ct = new() { IdNghe = nghe.Id, Nam = _NamTS, ChiTieu = 50 };
                                db.GetCollection<ChiTieuTC>().Upsert(ct);
                            }
                        }
                    }

                }
            }
            else
            {
                if (DataHelper.DsChiTieuTX.Count() < DataHelper.DsNghe.Count())
                {
                    using (var db = _LiteDb.GetDatabase())
                    {
                        foreach (var nghe in DataHelper.DsNghe)
                        {
                            if (DataHelper.DsChiTieuTX.FirstOrDefault(x => x.IdNghe.Equals(nghe.Id)) is null)
                            {
                                ChiTieuTX ct = new() { IdNghe = nghe.Id, Nam = _NamTS, ChiTieu = 50 };
                                db.GetCollection<ChiTieuTX>().Upsert(ct);
                            }
                        }
                    }

                }
            }
            RefreshData();

        }
        private void BtnExportGBTT_Click(object? sender, EventArgs e)
        {
            var selectedRowHandles = gridView.GetSelectedRows();
            if (selectedRowHandles.Length == 0)
            {
                XtraMessageBox.Show("Chưa chọn hồ sơ!");
                return;
            }
            if (selectedRowHandles[0] == -1) selectedRowHandles = selectedRowHandles.Skip(1).ToArray();
            if (selectedRowHandles.Length > 0)
            {
                List<HoSoTrungTuyenTC> dstt = new();
                for (int i = selectedRowHandles.Length - 1; i >= 0; i--)
                {
                    int seletedRowHandle = selectedRowHandles[i];
                    var r = gridView.GetRow(seletedRowHandle) as HoSoTrungTuyenTC;
                    if (r is not null)
                    {
                        dstt.Add(r);
                    }
                }
                F_ExportGBTT f = new(dstt);
                f.ShowDialog();
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            if (!TenDm.StartsWith("DM"))
            {
                LoadComboBoxHTDT();
            }
            RefreshData();
            LoadForm();
        }

        private void cbbDTS_EditValueChanged(object? sender, EventArgs e)
        {
            if (TenDm is null)
                return;
            if (_bindingSource is null)
                return;
            RefreshData();
        }

        private void CbbHTDT_EditValueChanged(object? sender, EventArgs e)
        {
            if (cbbDTS.EditValue is null)
                return;
            if (_bindingSource is null)
                return;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string td = cbbTDHV.EditValue.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (string.IsNullOrEmpty(td))
                return;
            DevForm.CreateSearchLookupEdit(lookTruong, "Ten", "Id", DataHelper.DsTruong.Where(x => x.LoaiTruong.Equals(cbbTDHV.EditValue.ToString())).ToList(), "(Tất cả)");
            RefreshData();
        }

        private void F_Main_Load(object? sender, EventArgs e)
        {

            WindowState = FormWindowState.Maximized;
            MinimumSize = this.Size;
            MaximumSize = this.Size;
            GridViewInit();
            LoadBackgound();
            LoadComboBoxDTS();
            LoadComboBoxHTDT();

            DevForm.CreateSearchLookupEdit(lookTinh, "AddressName", "AddressCode", DataHelper.lstTinh, "(Tất cả)");
            DevForm.CreateSearchLookupEdit(lookQuanHuyen, "AddressName", "AddressCode", null, "(Tất cả)");
            DevForm.CreateSearchLookupEdit(lookXa, "AddressName", "AddressCode", null, "(Tất cả)");
            DevForm.CreateSearchLookupEdit(lookTruong, "Ten", "Id", DataHelper.DsTruong.Where(x => x.LoaiTruong.Equals("THCS")).ToList(), "(Tất cả)");
            LoadLookupAddress();

            Shown += F_Main_Shown;
        }
        private void LoadLookupAddress()
        {
            lookTinh.EditValue = string.IsNullOrEmpty(Settings.Default.MaTinh) ? null : Settings.Default.MaTinh;
            lookQuanHuyen.EditValue = string.IsNullOrEmpty(Settings.Default.MaHuyen) ? null : Settings.Default.MaHuyen;
            lookQuanHuyen.Properties.DataSource = DataHelper.lstQuanHuyen.Where(x => x.AddressCode.StartsWith(Settings.Default.MaTinh)).ToList();
            lookXa.Properties.DataSource = DataHelper.lstPhuongXa.Where(x => x.AddressCode.StartsWith(Properties.Settings.Default.MaHuyen)).ToList();
        }

        private void LookTruong_TextChanged(object? sender, EventArgs e)
        {
            ReLoadDS();
        }

        private void ChkKhongTT_CheckedChanged(object? sender, EventArgs e)
        {
            ReLoadDS();
        }

        private void BtnRefreshDTS_Click(object? sender, EventArgs e)
        {
            LoadComboBoxDTS();
        }
        private void BtnLoadExel_Click(object? sender, EventArgs e)
        {
            if (cbbDTS.SelectedIndex <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đọt tuyển sinh");
                return;
            }
            List<HoSoTrungTuyenTC> lstHSTT = new List<HoSoTrungTuyenTC>();
            using (var fDlg = new OpenFileDialog
            {
                Title = "Danh sách trúng tuyển",
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = "*.xls",
                Filter = "Excel | *.xls | Excel 2010 | *.xlsx",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            })
            {
                if (fDlg.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        SplashScreenManager.ShowForm(typeof(F_Wait));
                        using (var wb = new XLWorkbook(fDlg.FileName))
                        {
                            var data = wb.Worksheet(TenDm);
                            var data1 = data.RangeUsed().AsTable().AsNativeDataTable();
                            /* Process data table as you wish */
                            foreach (DataRow row in data1.Rows)
                            {
                                var hsdt = DataHelper.DSHoSoXTTC.FirstOrDefault(x => x.DotTS == cbbDTS.SelectedIndex && x.TDHV.Equals(cbbTDHV.EditValue.ToString()) && x.MaHoSo.Equals(row[0].ToString()));
                                if (hsdt != null)
                                {
                                    lstHSTT.Add(hsdt.ToHSTT());
                                }
                            }
                        }
                        lstHSTTTemp = new(lstHSTT);
                        _bindingSource.DataSource = lstHSTTTemp;
                        SplashScreenManager.CloseForm();
                        gridView.ExpandAllGroups();
                        gridView.BestFitColumns();
                    }
                    catch
                    {
                        SplashScreenManager.CloseForm();
                    }
                }
            }
        }

        private void LoadBackgound()
        {
            pnImg.SuspendLayout();
            pnImg.BackgroundImage = Resources.school_background2_2;
            pnImg.Dock = DockStyle.Fill;
            pnImg.Visible = true;
            pnMain.Controls.Add(pnImg);
            pnImg.BringToFront();
            pnImg.ResumeLayout();
        }

        private void LoadComboBoxDTS()
        {
            var ds = DataHelper.DsDotXetTuyen.Where(x => x.NamTS == _NamTS).OrderBy(x => x.DotTS).ToList();
            if (ds.Count == 0)
            {
                cbbDTS.SelectedItem = null;
            }
            var lst = new List<string>() { "Cả năm" };
            lst.AddRange(ds.Select(x => x.DotTS.ToString()).ToList());

            DevForm.CreateComboboxEdit(cbbDTS, lst.ToArray());

            cbbDTS.SelectedIndex = 0;
        }

        private void LoadComboBoxHTDT()
        {
            string[] ds = { "THCS", "THPT" };
            DevForm.CreateComboboxEdit(cbbTDHV, ds);
            cbbTDHV.SelectedIndex = 0;
        }

        private PopupMenu popupMenu;
        private void LoadbtnDropDS()
        {
            popupMenu = new PopupMenu(barManager1);
            popupMenu.AutoFillEditorWidth = true;

            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC))
            {
                dropbtnHoSo.Text = "Hồ sơ dự tuyển";
                var btnExportXls1 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu DSDT-Trung cấp");
                var btnExportXls2 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu DSDT-GDTX");
                var btnExportXls5 = new BarButtonItem(barManager1, "Nhập danh sách dự tuyển từ Exel");


                btnExportXls1.ItemClick += XuatDSXTTheoMauDSDT_ItemClick;
                btnExportXls2.ItemClick += XuatDSXTTheoMauDSDT_GDTX_ItemClick;
                btnExportXls5.ItemClick += BtnExportXls5_ItemClick;


                popupMenu.AddItem(btnExportXls1);
                popupMenu.AddItem(btnExportXls2);
                popupMenu.AddItem(btnExportXls5);


            }
            else if (TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC))
            {
                dropbtnHoSo.Text = "Điểm dự tuyển";
                var btnExportXls1 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu KQXT-Trung cấp");
                var btnExportXls2 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu KQXT-GDTX");

                btnExportXls1.ItemClick += XuatDSXTTheoMauKQXT_ItemClick;
                btnExportXls2.ItemClick += XuatDSXTTheoMauKQXT_GDTX_ItemClick;
                popupMenu.AddItem(btnExportXls1);
                popupMenu.AddItem(btnExportXls2);

            }
            else if (TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC))
            {
                dropbtnHoSo.Text = "Hồ sơ trúng tuyển";

                var btnLapDSTT = new BarButtonItem(barManager1, "Lập danh sách trúng tuyển");
                var btnSaveDSTT = new BarButtonItem(barManager1, "Lưu lại ds trúng tuyển");
                var btnImportXls = new BarButtonItem(barManager1, "Nhập danh sách từ file Exel");
                var btnDanhLaiMa = new BarButtonItem(barManager1, "Lập lại mã hồ sơ");
                var btnExportXls = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu CSGDNN");
                var btnExportXls2 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu DSTT - Hệ trung cấp");
                var btnExportXls5 = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu DSTT - Hệ GDTX");
                var btnExportXls3 = new BarButtonItem(barManager1, "Xuất DS học sinh thuộc xã theo mẫu");
                var btnExportXls4 = new BarButtonItem(barManager1, "Xuất DS học sinh thuộc trường theo mẫu");

                btnLapDSTT.ItemClick += DropbtnDSTT_Click;
                btnSaveDSTT.ItemClick += BtnSaveDSTT_ItemClick;
                btnImportXls.ItemClick += BtnLoadExel_Click;
                btnExportXls2.ItemClick += XuatExelTheoMauDSTT_ItemClick;
                btnExportXls5.ItemClick += XuatExelDSTT_HeGDTX_ItemClick1;
                btnExportXls.ItemClick += XuatDsTTTheoMauCSGDNN_ItemClick;
                btnDanhLaiMa.ItemClick += BtnDanhLaiMa_ItemClick;
                btnExportXls3.ItemClick += XuatDSTheoXa_ItemClick;
                btnExportXls4.ItemClick += XuatDSTheoTruong_ItemClick;

                popupMenu.AddItem(btnLapDSTT);
                popupMenu.AddItem(btnSaveDSTT);
                popupMenu.AddItem(btnImportXls);
                popupMenu.AddItem(btnExportXls2);
                popupMenu.AddItem(btnExportXls5);
                popupMenu.AddItem(btnExportXls3);
                popupMenu.AddItem(btnExportXls4);
                popupMenu.AddItem(btnExportXls);
                popupMenu.AddItem(btnDanhLaiMa);
            }

            dropbtnHoSo.DropDownControl = popupMenu;
        }

        private void XuatExelDSTT_HeGDTX_ItemClick1(object sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
            if (!rdGDTX.Checked)
            {
                XtraMessageBox.Show($"Chưa chọn hệ GDTX");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSTT-TX.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Kết quả xét tuyển đợt {dts}-GDTX.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[5, 1] = $"NĂM HỌC: {DataHelper.NamTS} - {DataHelper.NamTS + 1}";

                    var lst = (List<HoSoTrungTuyenGDTX>)_bindingSource.DataSource;
                    if (lst != null && lst.Count > 0)
                    {
                        int width = 9;
                        int header = 9;
                        object[,] export = new object[lst.Count, width];
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].Ho;
                            export[i, 2] = lst[i].Ten;
                            export[i, 3] = lst[i].NgaySinh;
                            export[i, 4] = lst[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 5] = lst[i].NoiSinh;
                            export[i, 6] = lst[i].DiaChi; 
                            export[i, 7] = lst[i].TongDXT;
                            export[i, 8] = lst[i].GhiChu;
                        }

                        Excel.Range range = sheet.get_Range(sheet.Cells[header + 1, 1], sheet.Cells[lst.Count + header, width]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        Excel.Range rangeData = sheet.Range[$"C{header + 1}:C{1000}"];
                        rangeData.Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                        rangeData.Cells.Font.Size = 11;
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + header + 2, 1], sheet.Cells[lst.Count + header + 2, 4]);
                        sheet.Cells[lst.Count + header + 2, 1] = $"Tổng cộng: {lst.Count} học sinh.";
                        range2.Cells.Font.Italic = true;
                        range2.Merge();
                        sheet.Columns.AutoFit();
                    }

                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }

        private void XuatDSXTTheoMauKQXT_GDTX_ItemClick(object sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
            if (!rdGDTX.Checked)
            {
                XtraMessageBox.Show($"Chưa chọn hệ GDTX");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "KQXT-TX.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Kết quả xét tuyển đợt {dts}-GDTX.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[5, 1] = $"NĂM HỌC: {DataHelper.NamTS} - {DataHelper.NamTS + 1}";

                    var lst = (List<TongHopDiemXetTuyenGDTX>)_bindingSource.DataSource;
                    if (lst != null && lst.Count > 0)
                    {
                        int width = 22;
                        int header = 10;
                        object[,] export = new object[lst.Count, width];
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].Ho;
                            export[i, 2] = lst[i].Ten;
                            export[i, 3] = lst[i].NgaySinh;
                            export[i, 4] = lst[i].GT;
                            export[i, 5] = lst[i].NoiSinh;
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = lst[i].HocLuc6;
                            export[i, 8] = lst[i].HanhKiem6;
                            export[i, 9] = lst[i].DiemLop6;
                            export[i, 10] = lst[i].HocLuc7;
                            export[i, 11] = lst[i].HanhKiem7;
                            export[i, 12] = lst[i].DiemLop7;
                            export[i, 13] = lst[i].HocLuc8;
                            export[i, 14] = lst[i].HanhKiem8;
                            export[i, 15] = lst[i].DiemLop8;
                            export[i, 16] = lst[i].HocLuc9;
                            export[i, 17] = lst[i].HanhKiem9;
                            export[i, 18] = lst[i].DiemLop9;
                            export[i, 19] = string.IsNullOrEmpty(lst[i].IdDTUT) ? string.Empty : DataHelper.DsDoiTuongUT.First(x => x.Id.Equals(lst[i].IdDTUT)).Ma;
                            export[i, 20] = lst[i].Tong;
                            export[i, 21] = lst[i].GhiChu;
                        }
                        
                        Excel.Range range = sheet.get_Range(sheet.Cells[header + 1, 1], sheet.Cells[lst.Count + header, width]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        Excel.Range rangeData = sheet.Range[$"C{header + 1}:C{1000}"];
                        rangeData.Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                        rangeData.Cells.Font.Size = 11;
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + header + 2, 1], sheet.Cells[lst.Count + header + 2, 4]);
                        sheet.Cells[lst.Count + header + 2, 1] = $"Tổng cộng: {lst.Count} học sinh.";
                        range2.Cells.Font.Italic = true;
                        range2.Merge();
                        sheet.Columns.AutoFit();
                    }

                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }

        private void XuatDSXTTheoMauDSDT_GDTX_ItemClick(object sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
            if (!rdGDTX.Checked)
            {
                XtraMessageBox.Show($"Chưa chọn GDTX");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSDT-TX.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Danh sách dự tuyển đợt {dts}-GDTX.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[5, 1] = $"NĂM HỌC: {DataHelper.NamTS} - {DataHelper.NamTS + 1}";

                    var lst = (List<HoSoDuTuyenGDTXView>)_bindingSource.DataSource;
                    if (lst != null && lst.Count > 0)
                    {
                        int width = 17;
                        int header = 10;
                        object[,] export = new object[lst.Count, width];
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].Ho;
                            export[i, 2] = lst[i].Ten;
                            export[i, 3] = lst[i].NgaySinh;
                            export[i, 4] = lst[i].GT;
                            export[i, 5] = lst[i].NoiSinh;
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = lst[i].HocLuc6;
                            export[i, 8] = lst[i].HanhKiem6;
                            export[i, 9] = lst[i].HocLuc7;
                            export[i, 10] = lst[i].HanhKiem7;
                            export[i, 11] = lst[i].HocLuc8;
                            export[i, 12] = lst[i].HanhKiem8;
                            export[i, 13] = lst[i].HocLuc9;
                            export[i, 14] = lst[i].HanhKiem9;
                            export[i, 15] = string.IsNullOrEmpty(lst[i].IdDTUT) ? string.Empty : DataHelper.DsDoiTuongUT.First(x => x.Id.Equals(lst[i].IdDTUT)).Ma;
                            export[i, 16] = lst[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[11, 1], sheet.Cells[lst.Count + header + 1, width]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        Excel.Range rangeData = sheet.Range[$"C{header + 1}:C{200}"];
                        rangeData.Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                        rangeData.Cells.Font.Size = 11;
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + header + 2, 1], sheet.Cells[lst.Count + header + 2, 4]);
                        sheet.Cells[lst.Count + header + 2, 1] = $"Tổng cộng: {lst.Count} học sinh.";
                        range2.Cells.Font.Italic = true;
                        range2.Merge();
                        sheet.Columns.AutoFit();
                    }

                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }

        private void BtnExportXls5_ItemClick(object? sender, ItemClickEventArgs e)
        {
            if (cbbDTS.SelectedIndex <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đọt tuyển sinh");
                return;
            }

            MainWorkspace.FormUploadHS = new F_UploadHoSo(cbbDTS.SelectedIndex);
            MainWorkspace.FormUploadHS.ShowDialog();
        }

        private void XuatDSTheoTruong_ItemClick(object? sender, ItemClickEventArgs e)
        {
            if (lookTruong.EditValue == null)
            {
                XtraMessageBox.Show($"Chưa chọn trường!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSTRUONG.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Danh sách theo trường {lookTruong.Text}.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    string Truong = lookTruong.Text.ToUpper();
                    sheet.Cells[4, 1] = $"DANH SÁCH HỌC SINH TRƯỜNG {cbbTDHV.Text.ToUpper()} {lookTruong.Text.ToUpper()} ĐANG THEO HỌC TẠI TRƯỜNG";
                    string TrinhDo = rdTC.Checked ? "TRUNG CẤP" : "THƯỜNG XUYÊN";
                    sheet.Cells[5, 1] = $"TRÌNH ĐỘ: {TrinhDo} ; NĂM HỌC: {DataHelper.NamTS}-{DataHelper.NamTS + 1}.";

                    var lst = _bindingSource.DataSource as List<HoSoTrungTuyenTC>;
                    var lst2 = _bindingSource.DataSource as List<HoSoTrungTuyenGDTX>;
                    int count = (lst != null) ? lst.Count : (lst2 != null) ? lst2.Count : 0;
                    object[,] export = new object[count, 9];
                    if (lst != null && lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].MaHoSo;
                            export[i, 2] = lst[i].Ho;
                            export[i, 3] = lst[i].Ten;
                            export[i, 4] = lst[i].NgaySinh;
                            export[i, 5] = lst[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = DataHelper.DsNghe.First(x => x.Id.Equals(lst[i].IdNgheTrungTuyen)).Ten;
                            export[i, 8] = lst[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[8, 1], sheet.Cells[lst.Count + 7, 9]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{8}:C{200}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst.Count + 8, 1] = $"Tổng số: {lst.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + 8, 1], sheet.Cells[lst.Count + 8, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }
                    else if(lst2 != null && lst2.Count > 0)
                    {
                        for (int i = 0; i < lst2.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst2[i].MaHoSo;
                            export[i, 2] = lst2[i].Ho;
                            export[i, 3] = lst2[i].Ten;
                            export[i, 4] = lst2[i].NgaySinh;
                            export[i, 5] = lst2[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst2[i].DiaChi;
                            export[i, 7] = DataHelper.DsNghe.First(x => x.Id.Equals(lst2[i].IdNgheTrungTuyen)).Ten;
                            export[i, 8] = lst2[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[8, 1], sheet.Cells[lst2.Count + 7, 9]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{8}:C{200}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst2.Count + 8, 1] = $"Tổng số: {lst2.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst2.Count + 8, 1], sheet.Cells[lst2.Count + 8, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }
                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void XuatDSTheoXa_ItemClick(object? sender, ItemClickEventArgs e)
        {
            if (lookXa.EditValue == null)
            {
                XtraMessageBox.Show($"Chưa chọn xã!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSXA.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Danh sách theo {lookXa.Text}.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    string XA = lookXa.Text.ToUpper();
                    sheet.Cells[4, 1] = $"DANH SÁCH HỌC SINH {XA} ĐANG THEO HỌC TẠI TRƯỜNG";
                    string TringDo = rdTC.Checked ? "TRUNG CẤP" : "THƯỜNG XUYÊN";
                    sheet.Cells[5, 1] = $"TRÌNH ĐỘ: {TringDo}; NĂM HỌC: {DataHelper.NamTS}-{DataHelper.NamTS + 1}.";

                    var lst = _bindingSource.DataSource as List<HoSoTrungTuyenTC>;
                    var lst2 = _bindingSource.DataSource as List<HoSoTrungTuyenGDTX>;
                    int count = (lst != null) ? lst.Count : (lst2 != null) ? lst2.Count : 0;

                    object[,] export = new object[count, 9];

                    if (lst != null && lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].MaHoSo;
                            export[i, 2] = lst[i].Ho;
                            export[i, 3] = lst[i].Ten;
                            export[i, 4] = lst[i].NgaySinh;
                            export[i, 5] = lst[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = DataHelper.DsNghe.First(x => x.Id.Equals(lst[i].IdNgheTrungTuyen)).Ten;
                            export[i, 8] = lst[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[8, 1], sheet.Cells[lst.Count + 7, 9]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{8}:C{200}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst.Count + 8, 1] = $"Tổng số: {lst.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + 8, 1], sheet.Cells[lst.Count + 8, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }
                    else if(lst2 != null && lst2.Count > 0)
                    {
                        for (int i = 0; i < lst2.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst2[i].MaHoSo;
                            export[i, 2] = lst2[i].Ho;
                            export[i, 3] = lst2[i].Ten;
                            export[i, 4] = lst2[i].NgaySinh;
                            export[i, 5] = lst2[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst2[i].DiaChi;
                            export[i, 7] = DataHelper.DsNghe.First(x => x.Id.Equals(lst2[i].IdNgheTrungTuyen)).Ten;
                            export[i, 8] = lst2[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[8, 1], sheet.Cells[lst2.Count + 7, 9]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{8}:C{200}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst2.Count + 8, 1] = $"Tổng số: {lst2.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst2.Count + 8, 1], sheet.Cells[lst2.Count + 8, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }
                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void XuatExelTheoMauDSTT_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSTT.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Danh sách trúng tuyển đợt {dts}.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[4, 1] = $"DANH SÁCH HỌC SINH TRÚNG TUYỂN ĐỢT {_Helper.ToIIVX(dts)}";
                    sheet.Cells[5, 1] = $"TRÌNH ĐỘ: TRUNG CẤP ; NĂM HỌC: {DataHelper.NamTS}-{DataHelper.NamTS + 1}.";

                    var lst = (List<HoSoTrungTuyenTC>)_bindingSource.DataSource;
                    if (lst != null && lst.Count > 0)
                    {
                        object[,] export = new object[lst.Count, 10];
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].MaHoSo;
                            export[i, 2] = lst[i].Ho;
                            export[i, 3] = lst[i].Ten;
                            export[i, 4] = lst[i].NgaySinh;
                            export[i, 5] = lst[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = lst[i].TongDXT;
                            export[i, 8] = DataHelper.DsNghe.First(x => x.Id.Equals(lst[i].IdNgheTrungTuyen)).Ten;
                            export[i, 9] = lst[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[9, 1], sheet.Cells[lst.Count + 8, 10]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{9}:C{200}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst.Count + 9, 1] = $"Tổng số: {lst.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + 9, 1], sheet.Cells[lst.Count + 9, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }

                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void XuatDSXTTheoMauDSDT_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "DSDT.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Danh sách dự tuyển đợt {dts}.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[5, 1] = $"DANH SÁCH HỌC SINH ĐĂNG KÝ DỰ TUYỂN ĐỢT {_Helper.ToIIVX(dts)}";
                    sheet.Cells[6, 1] = $"TRÌNH ĐỘ: TRUNG CẤP ; NĂM HỌC: {DataHelper.NamTS}-{DataHelper.NamTS + 1}.";

                    var lst = (List<HoSoDuTuyenTCView>)_bindingSource.DataSource;
                    if (lst != null && lst.Count > 0)
                    {
                        object[,] export = new object[lst.Count, 13];
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].MaHoSo;
                            export[i, 2] = lst[i].Ho;
                            export[i, 3] = lst[i].Ten;
                            export[i, 4] = lst[i].NgaySinh;
                            export[i, 5] = lst[i].GT;
                            export[i, 6] = lst[i].DiaChi;
                            export[i, 7] = lst[i].HanhKiem == "Trung bình" ? "TB" : lst[i].HanhKiem;
                            export[i, 8] = lst[i].XLTN == "Trung bình" ? "TB" : lst[i].XLTN;
                            export[i, 9] = string.IsNullOrEmpty(lst[i].IdDTUT) ? string.Empty : DataHelper.DsDoiTuongUT.First(x => x.Id.Equals(lst[i].IdDTUT)).Ma;
                            export[i, 10] = string.IsNullOrEmpty(lst[i].IdKVUT) ? string.Empty : DataHelper.DsKhuVucUT.First(x => x.Id.Equals(lst[i].IdKVUT)).Ma;
                            export[i, 11] = DataHelper.DsNghe.First(x => x.Id.Equals(lst[i].IdNgheDT1)).Ten;
                            export[i, 12] = lst[i].GhiChu;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[9, 1], sheet.Cells[lst.Count + 8, 13]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{9}:C{1000}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                        sheet.Cells[lst.Count + 9, 1] = $"Tổng số: {lst.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + 9, 1], sheet.Cells[lst.Count + 9, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        sheet.Columns.AutoFit();
                    }

                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void XuatDSXTTheoMauKQXT_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
            if (!rdTC.Checked)
            {
                XtraMessageBox.Show($"Chưa chọn hệ Trung cấp!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "KQXT.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }

            try
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = $"Kết quả xét tuyển đợt {dts}.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    app.DisplayAlerts = false;
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.Cells[5, 1] = $"DANH SÁCH HỌC SINH ĐĂNG KÝ DỰ TUYỂN ĐỢT {_Helper.ToIIVX(dts)}";
                    sheet.Cells[6, 1] = $"TRÌNH ĐỘ: TRUNG CẤP ; NĂM HỌC: {DataHelper.NamTS}-{DataHelper.NamTS + 1}.";

                    var lst = ((List<TongHopDiemXetTuyenTC>)_bindingSource.DataSource).OrderBy(x => x.MaHoSo).ToList();
                    int slnghe = lst.DistinctBy(x => x.IdNgheNV1).Count();
                    List<int> RowIndexs = new();
                    if (lst != null && lst.Count() > 0)
                    {
                        object[,] export = new object[lst.Count() + slnghe, 13];
                        string Last = string.Empty, Current = string.Empty;
                        int sttnghe = 1, Index = 0, lstIndex = 0;
                        while (lstIndex < lst.Count())
                        {
                            Current = DataHelper.DsNghe.First(x => x.Id.Equals(lst[lstIndex].IdNgheNV1)).Ten;
                            if (!Current.Equals(Last))
                            {
                                export[Index, 0] = $"        {_Helper.ToIIVX(sttnghe)}.Nghề: {Current}";
                                RowIndexs.Add(Index + 1); sttnghe++; Index++;
                            }
                            else
                            {
                                export[Index, 0] = lstIndex + 1;
                                export[Index, 1] = lst[lstIndex].MaHoSo;
                                export[Index, 2] = lst[lstIndex].Ho;
                                export[Index, 3] = lst[lstIndex].Ten;
                                export[Index, 4] = lst[lstIndex].NgaySinh;
                                export[Index, 5] = lst[lstIndex].GT;
                                export[Index, 6] = lst[lstIndex].DiaChi;
                                export[Index, 7] = lst[lstIndex].HanhKiem;
                                export[Index, 8] = lst[lstIndex].XLTN;
                                export[Index, 9] = lst[lstIndex].UTDT;
                                export[Index, 10] = lst[lstIndex].UTKV;
                                export[Index, 11] = lst[lstIndex].Tong;
                                export[Index, 12] = lst[lstIndex].GhiChu;
                                lstIndex++;
                                Index++;
                            }
                            Last = Current;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[9, 1], sheet.Cells[lst.Count + slnghe + 8, 13]);
                        range.set_Value(Missing.Value, export);
                        range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                        Marshal.ReleaseComObject(range);
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        sheet.Range[$"C{9}:C{1000}"].Cells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

                        sheet.Cells[lst.Count + slnghe + 9, 1] = $"Tổng số: {lst.Count} thí sinh";
                        Excel.Range range2 = sheet.get_Range(sheet.Cells[lst.Count + slnghe + 9, 1], sheet.Cells[lst.Count + slnghe + 9, 5]);
                        range2.Merge();
                        range2.Cells.Font.Bold = true;
                        range2.Cells.Font.Italic = true;

                        foreach (var r in RowIndexs)
                        {
                            sheet.get_Range(sheet.Cells[r + 8, 1], sheet.Cells[r + 8, 13]).Merge();
                            sheet.get_Range(sheet.Cells[r + 8, 1], sheet.Cells[r + 8, 13]).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        }
                        sheet.Columns.AutoFit();
                    }
                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void BtnDanhLaiMa_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }

            var ds = DataHelper.DSHoSoTTTC.Where(x => x.DotTS == dts);
            if (ds.Count() > 0)
            {
                if (XtraMessageBox.Show($"Bạn xác nhận muốn lập lại mã đợt {dts}",
                    "Lập mã hồ sơ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var nghe in DataHelper.DsNghe)
                    {
                        var dstheonghe = ds.Where(x => x.IdNgheTrungTuyen == nghe.Id).OrderBy(x => x.Ten);
                        int i = 1;
                        foreach (var hs in dstheonghe)
                        {
                            hs.MaHoSo = $"{_NamTS}{nghe.Ma2}{i.ToString("D3")}";
                            _LiteDb.GetDatabase().GetCollection<HoSoTrungTuyenTC>(TuDien.CategoryName.HoSoTrungTuyenTC).Upsert(hs);
                            i++;
                        }
                    }
                    RefreshData();
                }
            }
        }
        private void XuatDsTTTheoMauCSGDNN_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show($"Chưa chọn đọt tuyển sinh!");
                return;
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            try
            {

                Directory.CreateDirectory(TuDien.EXEL_FOLDER);
                string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "SLTT.xlsx");
                if (!File.Exists(filePath))
                {
                    XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                    return;
                }
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = "Số_liệu_thí_sinh_trúng_tuyển.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);

                    var lst = _bindingSource.DataSource as List<HoSoTrungTuyenTC>;
                    var lst2 = _bindingSource.DataSource as List<HoSoTrungTuyenGDTX>;
                    int count = (lst != null) ? lst.Count : (lst2 != null) ? lst2.Count : 0;
                    object[,] export = new object[count, 27];

                    if (lst != null && lst.Count > 0)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst[i].MaHoSo;
                            export[i, 2] = $"{lst[i].Ho} {lst[i].Ten}";
                            export[i, 3] = lst[i].NgaySinh;
                            export[i, 4] = string.Empty;
                            export[i, 5] = lst[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst[i].NoiSinh;
                            export[i, 7] = string.Empty;
                            export[i, 8] = lst[i].ThonDuong;
                            export[i, 9] = DataHelper.lstTinh.First(x => x.AddressCode.Equals(lst[i].MaTinh)).AddressName;
                            export[i, 10] = string.Empty;
                            var lsthuyen = _Helper.getListDistrict(lst[i].MaTinh);
                            export[i, 11] = lsthuyen.First(x => x.AddressCode.Equals(lst[i].MaHuyen)).AddressName;
                            export[i, 12] = string.Empty;
                            var lstxa = _Helper.getListWards(lst[i].MaHuyen);
                            export[i, 13] = lstxa.First(x => x.AddressCode.Equals(lst[i].MaXa)).AddressName;
                            export[i, 14] = string.Empty;
                            export[i, 15] = lst[i].CCCD;
                            export[i, 16] = DataHelper.DsDanToc.First(x => x.Id.Equals(lst[i].IdDanToc)).Ten;
                            export[i, 17] = string.Empty;
                            export[i, 18] = DataHelper.DsTonGiao.First(x => x.Id.Equals(lst[i].IdTonGiao)).Ten;
                            export[i, 19] = string.Empty;
                            export[i, 20] = DataHelper.DsQuocTich.First(x => x.Id.Equals(lst[i].IdQuocTich)).Ten;
                            export[i, 21] = string.Empty;
                            export[i, 22] = lst[i].SDT;
                            export[i, 23] = lst[i].Email;
                            export[i, 24] = DataHelper.DsTrinhDo.First(x => x.Id == lst[i].IdTrinhDoVH).Ten;
                            export[i, 25] = string.Empty;
                            export[i, 26] = lst[i].HTDT;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[4, 1], sheet.Cells[lst.Count + 3, 27]);
                        range.set_Value(Missing.Value, export);
                        Marshal.ReleaseComObject(range);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                    }
                    else if (lst2 != null && lst2.Count > 0)
                    {
                        for (int i = 0; i < lst2.Count; i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lst2[i].MaHoSo;
                            export[i, 2] = $"{lst2[i].Ho} {lst2[i].Ten}";
                            export[i, 3] = lst2[i].NgaySinh;
                            export[i, 4] = string.Empty;
                            export[i, 5] = lst2[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lst2[i].NoiSinh;
                            export[i, 7] = string.Empty;
                            export[i, 8] = lst2[i].ThonDuong;
                            export[i, 9] = DataHelper.lstTinh.First(x => x.AddressCode.Equals(lst2[i].MaTinh)).AddressName;
                            export[i, 10] = string.Empty;
                            var lst2huyen = _Helper.getListDistrict(lst2[i].MaTinh);
                            export[i, 11] = lst2huyen.First(x => x.AddressCode.Equals(lst2[i].MaHuyen)).AddressName;
                            export[i, 12] = string.Empty;
                            var lst2xa = _Helper.getListWards(lst2[i].MaHuyen);
                            export[i, 13] = lst2xa.First(x => x.AddressCode.Equals(lst2[i].MaXa)).AddressName;
                            export[i, 14] = string.Empty;
                            export[i, 15] = lst2[i].CCCD;
                            export[i, 16] = DataHelper.DsDanToc.First(x => x.Id.Equals(lst2[i].IdDanToc)).Ten;
                            export[i, 17] = string.Empty;
                            export[i, 18] = DataHelper.DsTonGiao.First(x => x.Id.Equals(lst2[i].IdTonGiao)).Ten;
                            export[i, 19] = string.Empty;
                            export[i, 20] = DataHelper.DsQuocTich.First(x => x.Id.Equals(lst2[i].IdQuocTich)).Ten;
                            export[i, 21] = string.Empty;
                            export[i, 22] = lst2[i].SDT;
                            export[i, 23] = lst2[i].Email;
                            export[i, 24] = DataHelper.DsTrinhDo.First(x => x.Id == lst2[i].IdTrinhDoVH).Ten;
                            export[i, 25] = string.Empty;
                            export[i, 26] = lst2[i].HTDT;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[4, 1], sheet.Cells[lst2.Count + 3, 27]);
                        range.set_Value(Missing.Value, export);
                        Marshal.ReleaseComObject(range);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        range = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    }
                    book.Save();
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                if (book != null)
                {
                    book.Close();
                    Marshal.ReleaseComObject(book);
                }
                if (app != null)
                {
                    app.Quit();
                }
                XtraMessageBox.Show("Có lỗi xảy ra!");
            }
            finally
            {
                if (app != null)
                {
                    app.Visible = true;
                }
            }
        }
        private void BtnSaveDSTT_ItemClick(object? sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }

            if (DataHelper.DSHoSoTTTC.Where(x => x.DotTS == dts).Count() > 0)
            {
                if (lstHSTTTemp.Count() > 0)
                {
                    if (XtraMessageBox.Show($"Đã tồn tại danh sách trúng tuyển đợt {dts}, bạn xác nhận muốn lưu lại?",
                    "Lập danh sách trúng tuyển", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (var db = _LiteDb.GetDatabase())
                        {
                            db.GetCollection<HoSoTrungTuyenTC>(TuDien.CategoryName.HoSoTrungTuyenTC).DeleteMany(Query.And(Query.EQ("DotTS", dts), Query.EQ("NamTS", _NamTS)));
                            db.GetCollection<HoSoTrungTuyenTC>(TuDien.CategoryName.HoSoTrungTuyenTC).InsertBulk(lstHSTTTemp);
                        }

                        lstHSTTTemp = new();
                        RefreshData();
                    }
                }
            }
            else if (lstHSTTTemp.Count() > 0)
            {
                _LiteDb.InsertMany(lstHSTTTemp, TuDien.CategoryName.HoSoTrungTuyenTC);
                lstHSTTTemp = new();
                XtraMessageBox.Show("Đã lưu lại danh sách!");
                RefreshData();
            }
        }

        private List<HoSoTrungTuyenTC> lstHSTTTemp = new();

        private void DropbtnDSTT_Click(object? sender, EventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }
            if (rdTC.Checked)
            {
                if (DataHelper.DSHoSoTTTC.Count(x => x.DotTS.Equals(dts)) > 0)
                {
                    if (XtraMessageBox.Show($"Đã tồn tại danh sách trúng tuyển đợt {dts}, bạn xác nhận muốn lập lại danh sách?",
                        "Lập danh sách trúng tuyển - Hệ trung cấp", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataHelper.LapDSTrungTuyenTC(cbbDTS.SelectedIndex);
                    }
                }
                else
                {
                    DataHelper.LapDSTrungTuyenTC(cbbDTS.SelectedIndex);
                }
            }
            else
            {
                if (DataHelper.DSHoSoTrungTuyenTX.Count(x => x.DotTS.Equals(dts)) > 0)
                {
                    if (XtraMessageBox.Show($"Đã tồn tại danh sách trúng tuyển đợt {dts}, bạn xác nhận muốn lập lại danh sách?",
                        "Lập danh sách trúng tuyển - Hệ GDTX", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataHelper.LapDSTrungTuyenTX(cbbDTS.SelectedIndex);
                    }
                }
                else
                {
                    DataHelper.LapDSTrungTuyenTX(cbbDTS.SelectedIndex);
                }
            }
            
            RefreshData();
            LoadForm();
        }

        private void ShowTotalFooter()
        {
            if (gridView.Columns["MaHoSo"] is not null)
            {
                GridColumnSummaryItem siTotal = new GridColumnSummaryItem();
                siTotal.SummaryType = SummaryItemType.Count;
                siTotal.DisplayFormat = "Tổng số: {0}";
                gridView.Columns["MaHoSo"].Summary.Add(siTotal);
            }
        }

        private void ReLoadDS()
        {
            switch (TenDm)
            {
                case TuDien.CategoryName.TruongHoc:
                    _bindingSource.DataSource = DataHelper.DsTruong;
                    break;

                case TuDien.CategoryName.NganhNghe:
                    _bindingSource.DataSource = DataHelper.DsNghe;
                    break;

                case TuDien.CategoryName.DoiTuongUuTien:
                    _bindingSource.DataSource = DataHelper.DsDoiTuongUT;
                    break;

                case TuDien.CategoryName.KhuVucUuTien:
                    _bindingSource.DataSource = DataHelper.DsKhuVucUT;
                    break;

                case TuDien.CategoryName.DanToc:
                    _bindingSource.DataSource = DataHelper.DsDanToc;
                    break;

                case TuDien.CategoryName.TonGiao:
                    _bindingSource.DataSource = DataHelper.DsTonGiao;
                    break;

                case TuDien.CategoryName.TrinhDo:
                    _bindingSource.DataSource = DataHelper.DsTrinhDo;
                    break;

                case TuDien.CategoryName.QuocTich:
                    _bindingSource.DataSource = DataHelper.DsQuocTich;
                    break;

                case TuDien.CategoryName.DotXetTuyen:
                    _bindingSource.DataSource = DataHelper.DsDotXetTuyen;
                    break;

                case TuDien.CategoryName.ChiTieuTC:
                    _bindingSource.DataSource = rdTC.Checked ? DataHelper.DsChiTieuTC.Select(x => x.ToCTXT()).ToList() : 
                        DataHelper.DsChiTieuTX.Select(x => x.ToCTXT()).ToList();
                    break;

                case TuDien.CategoryName.HoSoDuTuyenTC:
#pragma warning disable CS8604 // Possible null reference argument.
                    _bindingSource.DataSource = rdTC.Checked ? DataHelper.GetDSDuTuyenTC(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS") : DataHelper.GetDSDuTuyenTX(cbbDTS.SelectedIndex);
#pragma warning restore CS8604 // Possible null reference argument.
                    break;

                case TuDien.CategoryName.HoSoTrungTuyenTC:
#pragma warning disable CS8604 // Possible null reference argument.
                    _bindingSource.DataSource = rdTC.Checked ? DataHelper.GetDSTrungTuyenTC(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS", (string)lookTinh.EditValue, (string)lookQuanHuyen.EditValue, (string)lookXa.EditValue, (string)lookTruong.EditValue, chkKhongTT.Checked) :
                    DataHelper.GetDSTrungTuyenTX(cbbDTS.SelectedIndex, (string)lookTinh.EditValue, (string)lookQuanHuyen.EditValue, (string)lookXa.EditValue, (string)lookTruong.EditValue, chkKhongTT.Checked);
#pragma warning restore CS8604 // Possible null reference argument.
                    break;

                case TuDien.CategoryName.DiemXetTuyenTC:
#pragma warning disable CS8604 // Possible null reference argument.
                    _bindingSource.DataSource = rdTC.Checked ? DataHelper.THDiemXetTuyen(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS") : DataHelper.THDiemXetTuyenTX(cbbDTS.SelectedIndex);
#pragma warning restore CS8604 // Possible null reference argument.
                    break;

                default: break;
            }
            cbbTDHV.Enabled = rdTC.Checked;
            gridView.ExpandAllGroups();
            gridView.BestFitColumns(true);
        }
        public void LoadForm()
        {
            EditMode = false;
            gridView.Columns.Clear();
            _bindingSource = new BindingSource();
            gridControl.DataSource = _bindingSource;
            ReLoadDS();

            LoadbtnDropDS();
            if (TenDm.Equals(TuDien.CategoryName.TruongHoc))
            {
                var colLoaiTruong = gridView.Columns.ColumnByFieldName("LoaiTruong");
                if (colLoaiTruong != null)
                {
                    RepositoryItemComboBox riComboBox = new RepositoryItemComboBox();
                    riComboBox.Items.AddRange(new string[] { "THCS", "THPT" });
                    colLoaiTruong.ColumnEdit = riComboBox;
                }
            }
            if (TenDm.Equals(TuDien.CategoryName.DoiTuongUuTien))
            {
                var colApdung = gridView.Columns.ColumnByFieldName("ApDung");
                if (colApdung != null)
                {
                    RepositoryItemComboBox riComboBox = new RepositoryItemComboBox();
                    riComboBox.Items.AddRange(new string[] { "Trung cấp", "Thường xuyên" });
                    colApdung.ColumnEdit = riComboBox;
                }
            }
            if (TenDm.Equals(TuDien.CategoryName.ChiTieuTC))
            {
                GridColumnSummaryItem gsTong = new();
                gsTong.SummaryType = SummaryItemType.Sum;
                gsTong.DisplayFormat = "Tổng: {0}";
                var colct = gridView.Columns.ColumnByFieldName("ChiTieu");
                if (colct != null) colct.Summary.Add(gsTong);
            }
            if (TenDm.Equals(TuDien.CategoryName.DotXetTuyen))
            {
                var colDotXT = gridView.Columns.ColumnByFieldName("NamTS");
                if (colDotXT != null) colDotXT.Group();
            }
            if (TenDm.StartsWith("HS"))
            {
                var colGT = gridView.Columns.ColumnByFieldName("GioiTinh");
                if (colGT != null)
                {
                    RepositoryItemTextEdit riComboBox = new RepositoryItemTextEdit();
                    colGT.ColumnEdit = riComboBox;
                }
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.lstTinh, "MaTinh", "AddressName", "AddressCode");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.lstQuanHuyen, "MaHuyen", "AddressName", "AddressCode");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.lstPhuongXa, "MaXa", "AddressName", "AddressCode");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsTruong, "IdTruong", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsDoiTuongUT, "IdDTUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsKhuVucUT, "IdKVUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsNghe, "IdNgheDT1", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsNghe, "IdNgheDT2", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsNghe, "IdNgheTrungTuyen", "Ten", "Id");
                var colXLHT = gridView.Columns.ColumnByFieldName("XLHT");
                if (colXLHT != null) { colXLHT.Visible = ((string)cbbTDHV.EditValue != "THCS"); }
                if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC))
                {
                    if(rdTC.Checked)
                        for (int i = 27; i <= 37; i++)
                        {
                            gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        }
                    else
                        for (int i = 33; i <= 43; i++)
                        {
                            gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        }
                }
                var colDotTS = gridView.Columns.ColumnByFieldName("DotTS");
                if (colDotTS != null) colDotTS.Group();
                var colNghe = TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC) ? gridView.Columns.ColumnByFieldName("IdNgheDT1") : gridView.Columns.ColumnByFieldName("IdNgheTrungTuyen");
                if (colNghe != null) colNghe.Group();                
            }
            if (TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC))
            {
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsNghe, "IdNgheNV1", "Ten", "Id");

                gridView.Columns.ColumnByFieldName("IdNgheNV1").Group();
                DevForm.CreateRepositoryItemLookUpEdit(gridView, DataHelper.DsNghe, "IdDTUT", "Ten", "Id");
                
            }

            panelGrid.RowStyles[1].Height = TenDm.StartsWith("HS") || TenDm.StartsWith("TK") || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? 40 : 0;
            btnAdd.Enabled = (TenDm.Equals(TuDien.CategoryName.ChiTieuTC) || TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC)) || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? false : true;
            btnEdit.Enabled = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC) || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? false : true;
            btnDelete.Enabled = TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? false : true;
            btnLapChiTieu.Width = TenDm.Equals(TuDien.CategoryName.ChiTieuTC) ? 110 : 0;
            btnExportGBTT.Width = TenDm.StartsWith("HSTT") ? 185 : 0;
            _panelButton.Width = TenDm.StartsWith("TK") ? 0 : 220;
            panelTS.Width = (TenDm.StartsWith("TK") || TenDm.StartsWith("HS")) || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? 180 : 0;
            panelTDHV.Width = TenDm.StartsWith("HS") || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? 155 : 0;
            chkKhongTT.Visible = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC) ? true : false;
            panelFilter.Visible = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC) ? true : false;
            dropbtnHoSo.Width = TenDm.StartsWith("HS") || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) ? 145 : 0;
            panelHeDaoTao.Height = TenDm.StartsWith("HS") || TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC) || TenDm.StartsWith("ChiTieu") ? 20 : 0;
            gridView.OptionsBehavior.KeepFocusedRowOnUpdate = TenDm.StartsWith("HS");

            ShowTotalFooter();
            gridView.ExpandAllGroups();
            panelGrid.BringToFront();
            gridView.BestFitColumns(true);

        }

        private void LookQuanHuyen_TextChanged(object? sender, EventArgs e)
        {
            if (lookQuanHuyen.EditValue != null)
#pragma warning disable CS8604 // Possible null reference argument.
                lookXa.Properties.DataSource = DataHelper.lstPhuongXa.Where(x => x.AddressCode.StartsWith(lookQuanHuyen.EditValue.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
            Settings.Default.MaHuyen = lookQuanHuyen.EditValue == null ? string.Empty : lookQuanHuyen.EditValue.ToString();
            Settings.Default.Save();
            lookXa.EditValue = null;
            RefreshData();

        }

        private void LookTinh_TextChanged(object? sender, EventArgs e)
        {
            if (lookTinh.EditValue != null)
#pragma warning disable CS8604 // Possible null reference argument.
                lookQuanHuyen.Properties.DataSource = DataHelper.lstQuanHuyen.Where(x => x.AddressCode.StartsWith(lookTinh.EditValue.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
            Settings.Default.MaTinh = lookTinh.EditValue == null ? string.Empty : lookTinh.EditValue.ToString();
            Settings.Default.Save();
            lookXa.EditValue = null;
            lookQuanHuyen.EditValue = null;
            RefreshData();

        }

        private void LookXa_TextChanged(object? sender, EventArgs e)
        {
            RefreshData();

        }

        #region Xử lý gridControl1

        private void GridView_CellValueChanged(object? sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string? value = e.Value.ToString();

            gridView.CellValueChanged -= GridView_CellValueChanged;
            if (e.Column.FieldName.Equals("Ma"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (DataHelper.CheckDupCode(value, TenDm))
                    {
                        XtraMessageBox.Show(this, "Trùng mã!");
                        gridView.SetFocusedRowCellValue("Ma", null);
                    }
                }
            }
            if (e.Column.FieldName.Equals("Ma2"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (DataHelper.CheckDupCode(value, TenDm))
                    {
                        XtraMessageBox.Show(this, "Trùng mã!");
                        gridView.SetFocusedRowCellValue("Ma", null);
                    }
                    else
                        gridView.SetFocusedRowCellValue("Ma2", value.ToUpper());
                }
            }
            gridView.CellValueChanged += GridView_CellValueChanged;
        }

        private void GridView_CustomDrawRowIndicator(object? sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString("D3");
        }

        private void GridView_DoubleClick(object? sender, EventArgs e)
        {
            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyenTC))
            {
                try
                {
                    var r = gridView.GetFocusedRow() as BaseClass;
                    if (r is not null)
                    {
                        if (rdTC.Checked)
                        {
                            var hs = DataHelper.DSHoSoXTTC.FirstOrDefault(x => x.Id.Equals(r.Id));
                            if (hs is not null)
                            {
                                F_HoSo f = new(hs.CloneJson());
                                f.Show();
                            }
                        }
                        else
                        {
                            var hs = DataHelper.DSHoSoXetTuyenTX.FirstOrDefault(x => x.Id.Equals(r.Id));
                            if (hs is not null)
                            {
                                F_HoSo_TX f = new(hs.CloneJson());
                                f.Show();
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else if (TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyenTC))
            {
                try
                {
                    if (rdTC.Checked)
                    {
                        var r = gridView.GetFocusedRow() as HoSoTrungTuyenTC;
                        if (r is not null)
                        {
                            var hs = DataHelper.DSHoSoXTTC.FirstOrDefault(x => x.Id.Equals(r.IdHSDT));
                            if (hs is not null)
                            {
                                F_HoSo f = new(hs.CloneJson());
                                f.Show();
                            }
                            else
                            {
                                XtraMessageBox.Show("Hồ sơ xét tuyển không tồn tại trong hệ thống");
                            }
                        }
                    }
                    else
                    {
                        var r = gridView.GetFocusedRow() as HoSoTrungTuyenGDTX;
                        if (r is not null)
                        {
                            var hs = DataHelper.DSHoSoXetTuyenTX.FirstOrDefault(x => x.Id.Equals(r.IdHSDT));
                            if (hs is not null)
                            {
                                F_HoSo_TX f = new(hs.CloneJson());
                                f.Show();
                            }
                            else
                            {
                                XtraMessageBox.Show("Hồ sơ xét tuyển không tồn tại trong hệ thống");
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else if (TenDm.Equals(TuDien.CategoryName.DiemXetTuyenTC))
            {
                try
                {
                    var r = gridView.GetFocusedRow() as TongHopDiemXetTuyenTC;
                    if (r is not null)
                    {
                        var hs = DataHelper.DSHoSoXTTC.FirstOrDefault(x => x.Id.Equals(r.IdHoSo));
                        if (hs is not null)
                        {
                            F_HoSo f = new(hs.CloneJson());
                            f.Show();
                        }
                        else
                        {
                            XtraMessageBox.Show("Hồ sơ xét tuyển không tồn tại trong hệ thống");
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void GridView_RowUpdated(object? sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var r = e.Row as DBClass;
            if (r != null)
            {
                r.Save();
                DataHelper.RefreshDS(TenDm);
            }
        }

        private void GridView_ShowingEditor(object? sender, CancelEventArgs e)
        {
            e.Cancel = EditMode ^ (gridView.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
        }

        private void GridView_CustomColumnDisplayText(object? sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "GioiTinh")
            {
                if (e.Value is null)
                    return;
                if ((bool)e.Value)
                    e.DisplayText = "Nam";
                else
                    e.DisplayText = "Nữ";
            }
        }

        private void GridViewInit()
        {
            gridView.IndicatorWidth = 50;
            gridView.OptionsCustomization.AllowColumnMoving = false;
            gridView.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridView.OptionsView.ShowFooter = true;
            gridView.OptionsPrint.AutoWidth = false;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsView.ShowAutoFilterRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.BestFitMode = GridBestFitMode.Fast;

            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridView.OptionsBehavior.KeepGroupExpandedOnSorting = true;

            GridGroupSummaryItem gscCount = new GridGroupSummaryItem();
            gscCount.SummaryType = SummaryItemType.Count;
            gscCount.DisplayFormat = "(Số lượng: {0})";
            gridView.GroupSummary.Add(gscCount);

            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.RowUpdated += GridView_RowUpdated;
            gridView.ShowingEditor += GridView_ShowingEditor;
            gridView.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
            gridView.DoubleClick += GridView_DoubleClick;
            gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText;
        }

        #endregion Xử lý gridControl1

        #region MenuSelect

        private void btnChiTieu_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.ChiTieuTC;
            LoadForm();
        }

        private void btnDanToc_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DanToc;
            LoadForm();
        }

        private void btnDotTS_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DotXetTuyen;
            LoadForm();
        }

        private void btnDTUT_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DoiTuongUuTien;
            LoadForm();
        }

        private void btnHoSoDuTuyenTC_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.HoSoDuTuyenTC;
            LoadForm();
        }

        private void btnKVUT_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.KhuVucUuTien;
            LoadForm();
        }

        private void btnNghe_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.NganhNghe;
            LoadForm();
        }

        private void btnQuocTich_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.QuocTich;
            LoadForm();
        }

        private void btnTDHV_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TrinhDo;
            LoadForm();
        }

        private void btnTonGiao_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TonGiao;
            LoadForm();
        }

        private void btnTruong_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TruongHoc;
            LoadForm();
        }

        private void btnHSTT_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.HoSoTrungTuyenTC;
            LoadForm();
        }

        private void btnSetting_Click(object? sender, EventArgs e)
        {
            F_Setting f = new(DataHelper.CurrSettings.CloneJson());
            f.Show();
        }

        private void btnAccount_Click(object? sender, EventArgs e)
        {
            F_Account f = new(DataHelper.CurrUser.CloneJson());
            f.Show();
        }

        private void btnTKTT_Click(object? sender, EventArgs e)
        {
            if (!F_TK.GetForm.Visible) F_TK.GetForm.Show(this);
            F_TK.GetForm.BringToFront();
        }

        private void btnDiemXetTuyen_Click(object? sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DiemXetTuyenTC;
            LoadForm();
        }

        #endregion MenuSelect
    }
}