using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using ClosedXML.Excel;
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using QuanLyTuyenSinh.Models;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_UploadHoSo : DevExpress.XtraEditors.DirectXForm
    {
        static int _dts;
        List<HoSoDuTuyenTC> lstHSTC = new List<HoSoDuTuyenTC>();
        BindingSource binding = new BindingSource();
        public F_UploadHoSo(int dts)
        {
            InitializeComponent();
            _dts = dts;
            GridViewInit();
            FormClosed += (sender, e) => MainWorkspace.FormMain.RefreshData();
        }

        public void RefreshData()
        {
            binding.DataSource = lstHSTC.Select(x => x.ToView());
            gridHSTC.DataSource = binding;
            gridViewHSTC.BestFitColumns();
        }

        private void btnExportTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Directory.CreateDirectory(TuDien.EXEL_FOLDER);
            string filePath = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "ImportExel.xlsx");
            if (!File.Exists(filePath))
            {
                XtraMessageBox.Show($"Chưa có file mẫu!\n {filePath}");
                return;
            }
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = $"Mẫu Upload Exel.xlsx";
            sfd.DefaultExt = "xlsx";
            sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
            sfd.AddExtension = true;
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.CheckPathExists = true;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Application app = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Workbook book = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Excel.Worksheet sheet = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            try
            {
                if (sfd.ShowDialog() == true)
                {
                    SplashScreenManager.ShowForm(typeof(F_Wait));
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
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

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstHSTC.Count() > 0)
            {
                if (XtraMessageBox.Show(this, "Danh sách đã có dữ liệu, xác nhận load lại?", "Upload", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                using (var fDlg = new OpenFileDialog
                {
                    Title = "Hồ sơ xét tuyển",
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
                        SplashScreenManager.ShowForm(typeof(F_Wait));
                        using (var wb = new XLWorkbook(fDlg.FileName))
                        {
                            var ws = wb.Worksheet("Data");
                            if (ws == null)
                            {
                                XtraMessageBox.Show($"File Exel không đúng định dạng");
                                return;
                            }
                            var fisrtCell = ws.FirstCellUsed();
                            var lastCell = ws.Rows().Last(r => !r.Cell(ws.FirstColumnUsed().ColumnLetter()).IsEmpty()).Cell(ws.LastColumnUsed().ColumnLetter());
                            var Data = ws.Range(fisrtCell, lastCell).AsTable().AsNativeDataTable();
                            var dsxt = DataHelper.DSHoSoXTTC.Where(x => x.DotTS == _dts);

                            lstHSTC.Clear();
                            foreach (DataRow r in Data.Rows)
                            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
                                var hs = new HoSoDuTuyenTC()
                                {
                                    DotTS = _dts,
                                    NamTS = DataHelper.NamTS,
                                    MaHoSo = r[1].ToString(),
                                    Ho = r[2].ToString(),
                                    Ten = r[3].ToString(),
                                    NgaySinh = Convert.ToDateTime(r[4]),
                                    GioiTinh = r[5].ToString().Equals("Nam") ? true : false,
                                    NoiSinh = r[6].ToString(),
                                    ThonDuong = r[7].ToString(),
                                    MaTinh = r[9].ToString(),
                                    MaHuyen = r[11].ToString(),
                                    MaXa = r[13].ToString(),
                                    DiaChi = r[14].ToString(),
                                    CCCD = r[15].ToString(),
                                    SDT = r[22].ToString(),
                                    Email = r[23].ToString(),
                                    HTDT = r[26].ToString(),
                                    HoTenCha = r[27].ToString(),
                                    NgheNghiepCha = r[28].ToString(),
                                    NamSinhCha = r[29].ToString(),
                                    HoTenMe = r[30].ToString(),
                                    NgheNghiepMe = r[31].ToString(),
                                    NamSinhMe = r[32].ToString(),
                                    Lop = r[35].ToString(),
                                    NamTN = r[36].ToString(),
                                    GhiChu = r[37].ToString(),
                                    KiemTraHS = new KiemTraHoSo
                                    {
                                        PhieuDKDT = !string.IsNullOrEmpty(r[38].ToString()) ? true : false,
                                        GCNTT = !string.IsNullOrEmpty(r[39].ToString()) ? true : false,
                                        GKSK = !string.IsNullOrEmpty(r[40].ToString()) ? true : false,
                                        GiayCNUT = !string.IsNullOrEmpty(r[41].ToString()) ? true : false,
                                        GiayKhaiSinh = !string.IsNullOrEmpty(r[42].ToString()) ? true : false,
                                        SYLL = !string.IsNullOrEmpty(r[43].ToString()) ? true : false,
                                        CCCD = !string.IsNullOrEmpty(r[44].ToString()) ? true : false,
                                        HocBa = !string.IsNullOrEmpty(r[45].ToString()) ? true : false,
                                        BangTN = !string.IsNullOrEmpty(r[46].ToString()) ? true : false,
                                        HinhThe = !string.IsNullOrEmpty(r[47].ToString()) ? true : false,
                                        GhiChu = r[48].ToString()
                                    },
                                    HanhKiem = r[49].ToString(),
                                    XLHocTap = r[50].ToString(),
                                    XLTN = r[51].ToString(),

                                    DsNguyenVong = new List<NguyenVong>()
                                };
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                                if (!string.IsNullOrEmpty(r[17].ToString()))
                                {
                                    var dt = DataHelper.DsDanToc.FirstOrDefault(x => x.Ma == r[17].ToString());
                                    if (dt != null) hs.IdDanToc = dt.Id;
                                }
                                if (!string.IsNullOrEmpty(r[19].ToString()))
                                {
                                    var tg = DataHelper.DsTonGiao.FirstOrDefault(x => x.Ma == r[19].ToString());
                                    if (tg != null) hs.IdTonGiao = tg.Id;
                                }
                                if (!string.IsNullOrEmpty(r[21].ToString()))
                                {
                                    var qt = DataHelper.DsQuocTich.FirstOrDefault(x => x.Ma == r[21].ToString());
                                    if (qt != null) hs.IdQuocTich = qt.Id;
                                }
                                if (!string.IsNullOrEmpty(r[25].ToString()))
                                {
                                    var tdvh = DataHelper.DsTrinhDo.FirstOrDefault(x => x.Ma == r[25].ToString());
                                    if (tdvh != null) hs.IdTrinhDoVH = tdvh.Id;
                                }
                                if (!string.IsNullOrEmpty(r[34].ToString()))
                                {
                                    var truong = DataHelper.DsTruong.FirstOrDefault(x => x.Ma == r[34].ToString());
                                    if (truong != null)
                                    {
                                        hs.IdTruong = truong.Id;
                                        hs.TDHV = truong.LoaiTruong;
                                    }
                                }
                                if (!string.IsNullOrEmpty(r[52].ToString()))
                                {
                                    var dtut = DataHelper.DsDoiTuongUT.FirstOrDefault(x => x.Ma == r[52].ToString());
                                    hs.IdDTUT = dtut is null ? string.Empty : dtut.Id;
                                }
                                if (!string.IsNullOrEmpty(r[53].ToString()))
                                {
                                    var kvut = DataHelper.DsKhuVucUT.FirstOrDefault(x => x.Ma == r[53].ToString());
                                    hs.IdKVUT = kvut is null ? string.Empty : kvut.Id;
                                }
                                if (!string.IsNullOrEmpty(r[55].ToString()))
                                {
                                    var nghe = DataHelper.DsNghe.FirstOrDefault(x => x.Ma2 == r[55].ToString());
                                    if (nghe != null)
                                        hs.DsNguyenVong.Add(new NguyenVong
                                        {
                                            NV = 1,
                                            IdNghe = nghe.Id
                                        });
                                }
                                if (!string.IsNullOrEmpty(r[57].ToString()))
                                {
                                    var nghe = DataHelper.DsNghe.FirstOrDefault(x => x.Ma2 == r[57].ToString());
                                    if (nghe != null)
                                        hs.DsNguyenVong.Add(new NguyenVong
                                        {
                                            NV = 2,
                                            IdNghe = nghe.Id
                                        });
                                }

                                lstHSTC.Add(hs);
                            }
                        }
                        binding.DataSource = lstHSTC.Select(x => x.ToView());
                        gridHSTC.DataSource = binding;
                        gridViewHSTC.BestFitColumns();
                        LoadLookup();
                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show($"Upload thành công!");
                    }
                }
            }
            catch
            {
                SplashScreenManager.CloseForm();
                XtraMessageBox.Show($"Có lỗi khi mở file Exel. Nếu bạn đang mở file Exel xin hãy đóng lại");
            }
        }

        private void btnAccept_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string errs = string.Empty;
            var selectedRowHandles = gridViewHSTC.GetSelectedRows();
            if (selectedRowHandles.Length == 0)
            {
                XtraMessageBox.Show($"Chưa chọn hồ sơ để lưu");

                return;
            }
            if (selectedRowHandles[0] == -1) selectedRowHandles = selectedRowHandles.Skip(1).ToArray();
            if (selectedRowHandles.Length > 0)
            {
                for (int i = selectedRowHandles.Length - 1; i >= 0; i--)
                {
                    int seletedRowHandle = selectedRowHandles[i];
                    var r = gridViewHSTC.GetRow(seletedRowHandle) as HoSoDuTuyenTCView;
                    if (r is not null)
                    {
                        var hstc = lstHSTC.FirstOrDefault(x => x.Id == r.Id);
                        if (hstc is not null)
                            errs += CheckErrors(hstc);
                    }
                }
                if (!string.IsNullOrEmpty(errs))
                {
                    XtraMessageBox.Show(errs, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    for (int i = selectedRowHandles.Length - 1; i >= 0; i--)
                    {
                        int seletedRowHandle = selectedRowHandles[i];
                        var r = gridViewHSTC.GetRow(seletedRowHandle) as HoSoDuTuyenTCView;
                        if (r is not null)
                        {
                            var hstc = lstHSTC.FirstOrDefault(x => x.Id == r.Id);
                            if (hstc is not null)
                            {
                                hstc.Save();
                                lstHSTC.RemoveAt(seletedRowHandle);
                            }
                        }
                    }
                    RefreshData();
                    XtraMessageBox.Show("Lưu hồ sơ thành công");
                }
            }
        }
        private string CheckErrors(HoSoDuTuyenTC hs)
        {
            string errs = hs.CheckError();
            if (!string.IsNullOrEmpty(errs))
            {
                errs = errs.Insert(0, $"Mã hồ sơ ({hs.MaHoSo}):\n");
            }
            return errs;
        }

        private void LoadLookup()
        {
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.lstTinh, "MaTinh", "AddressName", "AddressCode");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.lstQuanHuyen, "MaHuyen", "AddressName", "AddressCode");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.lstPhuongXa, "MaXa", "AddressName", "AddressCode");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.DsTruong, "IdTruong", "Ten", "Id");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.DsDoiTuongUT, "IdDTUT", "Ma", "Id", "");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.DsKhuVucUT, "IdKVUT", "Ma", "Id", "");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.DsNghe, "IdNgheDT1", "Ten", "Id");
            DevForm.CreateRepositoryItemLookUpEdit(gridViewHSTC, DataHelper.DsNghe, "IdNgheDT2", "Ten", "Id");
        }
        private void GridViewInit()
        {
            gridViewHSTC.IndicatorWidth = 50;
            gridViewHSTC.OptionsCustomization.AllowColumnMoving = false;
            gridViewHSTC.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.True;
            gridViewHSTC.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;

            gridViewHSTC.OptionsPrint.AutoWidth = false;
            gridViewHSTC.OptionsView.ColumnAutoWidth = false;
            gridViewHSTC.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewHSTC.OptionsView.ShowAutoFilterRow = true;
            gridViewHSTC.OptionsView.ShowGroupPanel = false;
            gridViewHSTC.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Fast;

            gridViewHSTC.OptionsSelection.MultiSelect = true;
            gridViewHSTC.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridViewHSTC.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            gridViewHSTC.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewHSTC.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridViewHSTC.OptionsBehavior.KeepGroupExpandedOnSorting = true;

            gridViewHSTC.CustomDrawRowIndicator += (sender, e) =>
            {
                if (e.RowHandle >= 0)
                    e.Info.DisplayText = (e.RowHandle + 1).ToString("D3");
            };
            gridViewHSTC.DoubleClick += (sender, e) =>
            {
                var r = gridViewHSTC.GetFocusedRow() as HoSoDuTuyenTCView;
                if (r is not null)
                {
                    var hs = lstHSTC.FirstOrDefault(x => x.Id.Equals(r.Id));
                    if (hs is not null)
                    {
                        F_HoSo f = new(hs, true);
                        f.Show();
                    }
                }
            };
            gridViewHSTC.ShowingEditor += (sender, e) => e.Cancel = true;
        }
        private void btnDeleteTC_Click(object sender, EventArgs e)
        {
            var selectedRowHandles = gridViewHSTC.GetSelectedRows();
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
                            var r = gridViewHSTC.GetRow(seletedRowHandle) as HoSoDuTuyenTCView;
                            if (r is not null)
                            {
                                lstHSTC.RemoveAt(seletedRowHandle);
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
    }
}