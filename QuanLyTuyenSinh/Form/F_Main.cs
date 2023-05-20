﻿using DevExpress.Export;
using DevExpress.Mvvm.Native;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet.Model;
using LiteDB;
using QuanLyTuyenSinh.Properties;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using Font = System.Drawing.Font;
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
        public void RefreshForm()
        {
            if (!TenDm.StartsWith("DM"))
            {
                LoadComboBoxHTDT();
            }

            Data.RefreshDS(TenDm);
            LoadData();
        }
        private int _NamTS { get; set; }
        private string TenDm { get; set; }
        private List<_Helper.Adress> lstTinh = _Helper.getListProvince();
        private List<_Helper.Adress> lstQuanHuyen;
        private List<_Helper.Adress> lstPhuongXa;
        public F_Main()
        {
            InitializeComponent();
            _NamTS = Properties.Settings.Default.NamTS;
            txtNamTS.Caption = $"Năm tuyển sinh : <b>{_NamTS}</b>";
            txtUser.Caption = $"Xin chào : <b>{Data.CurrUser.UserName}</b>";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditMode = false;

            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyen))
            {
                if (cbbDTS.SelectedIndex != -1)
                {
                    int dts;
                    if (!int.TryParse(cbbDTS.SelectedItem.ToString(), out dts))
                        return;
                    int nts = _NamTS;
                    var dxt = Data.DsDotXetTuyen.FirstOrDefault(x => x.NamTS == _NamTS && x.DotTS == dts);
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

                    var dt = Data.DsDanToc.FirstOrDefault();
                    var qt = Data.DsQuocTich.FirstOrDefault();
                    var tg = Data.DsTonGiao.FirstOrDefault();
                    var tdvh = Data.DsTrinhDo.FirstOrDefault();
                    var kvut = Data.DsKhuVucUT.FirstOrDefault(x => x.Ma == "KV2-NT");
                    try
                    {
                        F_HoSo f = new F_HoSo(
                        new HoSoDuTuyen
                        {
                            NamTS = nts,
                            DotTS = dts,
                            HTDT = "Chính quy",
                            MaTinh = "511",
                            MaHuyen = "51103",
                            IdQuocTich = qt is not null ? qt.Id : string.Empty,
                            IdDanToc = dt is not null ? dt.Id : string.Empty,
                            IdTrinhDoVH = tdvh is not null ? tdvh.Id : string.Empty,
                            IdTonGiao = tg is not null ? tg.Id : string.Empty,
                            IdKVUT = kvut is not null ? kvut.Id : string.Empty,
                        });
                        f.Show(this);
                    }
                    finally
                    {
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "Chưa chọn đợt xét tuyển?", "Lỗi");
                }
            }
            else
                gridView.AddNewRow();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelGrid.SendToBack();
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                            var r = gridView.GetRow(seletedRowHandle) as BaseClass;
                            if (r is not null)
                            {
                                if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyen))
                                {
                                    var hs = Data.DSHoSoDT.FirstOrDefault(x => x.Id == r.Id);
                                    if (hs != null)
                                    {
                                        Data.DSHoSoDT.Remove(hs);
                                    }
                                }
                                else if (TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen))
                                {
                                    var hs = Data.DSHoSoTT.FirstOrDefault(x => x.Id == r.Id);
                                    if (hs != null)
                                    {
                                        Data.DSHoSoTT.Remove(hs);
                                    }
                                }
                                else
                                {
                                    _bindingSource.RemoveAt(seletedRowHandle);
                                }
                            }
                        }
                    }
                    finally
                    {
                        Data.SaveDS(TenDm);
                        LoadData();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyen))
            {
                try
                {
                    var r = gridView.GetFocusedRow() as BaseClass;
                    if (r is not null)
                    {
                        var hs = Data.DSHoSoDT.FirstOrDefault(x => x.Id.Equals(r.Id));
                        if (hs is not null)
                        {
                            F_HoSo f = new(hs.CloneJson());
                            f.Show(this);
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
                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware;
                    //Customize export options
                    var view = (gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
                    view.OptionsPrint.PrintHeader = true;

                    XlsxExportOptionsEx advOptions = new XlsxExportOptionsEx();
                    advOptions.ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.True;
                    advOptions.AllowGrouping = DefaultBoolean.False;
                    advOptions.LayoutMode = DevExpress.Export.LayoutMode.Table;
                    advOptions.ShowGroupSummaries = DefaultBoolean.False;
                    advOptions.TextExportMode = TextExportMode.Text;
                    advOptions.SheetName = TenDm;
                    if (!TenDm.StartsWith("TK"))
                        gridView.VisibleColumns[0].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

                    gridView.BestFitColumns(true);
                    gridControl.ExportToXlsx(saveFileDialog1.FileName, advOptions);
                    XtraMessageBox.Show(this, "Xuất file thành công", "Thành công");
                }
                catch
                {
                    XtraMessageBox.Show(this, "Có lỗi khi mở tệp Exel. Nếu bạn đang mở tệp này trên Exel hãy đóng nó", "Lỗi");
                }
            }
        }

        private void btnLapChiTieu_Click(object sender, EventArgs e)
        {
            if (Data.DsChiTieu.Where(x => x.Nam == _NamTS).Count() < Data.DsNghe.Count())
            {
                foreach (var nghe in Data.DsNghe)
                {
                    if (Data.DsChiTieu.FirstOrDefault(x => x.IdNghe.Equals(nghe.Id)) is null)
                    {
                        ChiTieuXetTuyen ct = new() { IdNghe = nghe.Id, Nam = _NamTS, ChiTieu = Data.CurrSettings.CHITIEUMACDINH };
                        _bindingSource.Add(ct);
                    }
                }
                Data.SaveDS(TenDm);
                gridView.ExpandAllGroups();
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            if (!TenDm.StartsWith("DM"))
            {
                LoadComboBoxHTDT();
            }

            Data.RefreshDS(TenDm);
            LoadData();
        }

        private void cbbDTS_EditValueChanged(object sender, EventArgs e)
        {
            if (TenDm is null)
                return;
            if (_bindingSource is null)
                return;

            LoadData();
            gridView.ExpandAllGroups();
        }

        private void CbbHTDT_EditValueChanged(object? sender, EventArgs e)
        {
            if (cbbDTS.EditValue is null)
                return;
            if (_bindingSource is null)
                return;
            string td = cbbTDHV.EditValue.ToString();
            if (string.IsNullOrEmpty(td))
                return;

            LoadData();
        }

        private void F_Main_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            WindowState = FormWindowState.Maximized;
            MinimumSize = this.Size;
            MaximumSize = this.Size;
            GridViewInit();
            LoadBackgound();
            LoadbtnDSTT();
            LoadComboBoxDTS();
            LoadComboBoxHTDT();
            lookTinh.EditValue = Data.CurrSettings.MaTinh;
            lookQuanHuyen.EditValue = Data.CurrSettings.MaHuyen;
            lstQuanHuyen = _Helper.getListDistrict(Data.CurrSettings.MaTinh);
            lstPhuongXa = _Helper.getListWards(Data.CurrSettings.MaHuyen);

            DevForm.CreateSearchLookupEdit(lookTinh, "AdressName", "AdressCode", lstTinh);
            DevForm.CreateSearchLookupEdit(lookQuanHuyen, "AdressName", "AdressCode", lstQuanHuyen);
            DevForm.CreateSearchLookupEdit(lookXa, "AdressName", "AdressCode", lstPhuongXa);

            Shown += F_Main_Shown;
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
            btnLoadExel.Click += BtnLoadExel_Click;
            btnRefreshDTS.Click += BtnRefreshDTS_Click;
            btnThongKe.Click += (sender, e) =>
            {
                F_TK f = new();
                f.Show();
            };

            cbbDTS.EditValueChanged += cbbDTS_EditValueChanged;
            cbbTDHV.EditValueChanged += CbbHTDT_EditValueChanged;
            lookTinh.TextChanged += LookTinh_TextChanged;
            lookQuanHuyen.TextChanged += LookQuanHuyen_TextChanged;
            lookXa.TextChanged += LookXa_TextChanged;
            chkKhongTT.CheckedChanged += ChkKhongTT_CheckedChanged;
            ResumeLayout(true);
        }

        private void ChkKhongTT_CheckedChanged(object? sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnRefreshDTS_Click(object? sender, EventArgs e)
        {
            LoadComboBoxDTS();
        }

        private string connString = "";
        private OleDbConnection conn;
        private List<HoSoTrungTuyen> lstHSTT;

        private void BtnLoadExel_Click(object? sender, EventArgs e)
        {
            lstHSTT = new();
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
                    string strFileType = Path.GetExtension(fDlg.FileName).ToLower();
                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fDlg.FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fDlg.FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    try
                    {
                        splashScreenManager1.ShowWaitForm();

                        conn = new OleDbConnection(connString);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        using (OleDbCommand comm = new OleDbCommand())
                        {
                            DataTable dt = new DataTable();
                            comm.CommandText = $@"SELECT [Mã HS] FROM [{TenDm}$]";
                            comm.Connection = conn;
                            using (OleDbDataAdapter da = new OleDbDataAdapter())
                            {
                                da.SelectCommand = comm;
                                da.Fill(dt);
                                foreach (DataRow row in dt.Rows)
                                {
                                    var hsdt = Data.DSHoSoDT.FirstOrDefault(x => x.MaHoSo == row[0].ToString());
                                    if (hsdt != null)
                                    {
                                        lstHSTT.Add(hsdt.ToHSTT());
                                    }
                                }
                            }
                        }
                        splashScreenManager1.CloseWaitForm();
                    }
                    catch
                    {
                        conn.Close();
                        conn.Dispose();
                        splashScreenManager1.CloseWaitForm();
                        XtraMessageBox.Show("Có lỗi khi mở tệp Exel. Nếu bạn đang mở tệp này trên Exel hãy đóng nó");
                    }

                    this.Cursor = Cursors.Default;
                    conn.Close();
                    conn.Dispose();

                    lstHDTTTemp = new(lstHSTT);
                    _bindingSource.DataSource = lstHDTTTemp;
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
            var ds = Data.DsDotXetTuyen.Where(x => x.NamTS == _NamTS).OrderBy(x => x.DotTS).ToList();
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



        private PopupMenu popupMenu1;
        private BarButtonItem btnSaveDSTT;
        private BarButtonItem btnExportXls;
        private BarButtonItem btnDanhLaiMa;

        private void LoadbtnDSTT()
        {
            dropbtnDSTT.Click += DropbtnDSTT_Click;

            popupMenu1 = new PopupMenu(barManager1);
            popupMenu1.AutoFillEditorWidth = true;
            btnSaveDSTT = new BarButtonItem(barManager1, "Lưu lại ds trúng tuyển");
            btnExportXls = new BarButtonItem(barManager1, "Xuất file Exel theo mẫu");
            btnDanhLaiMa = new BarButtonItem(barManager1, "Lập lại mã hồ sơ");

            btnSaveDSTT.ItemClick += BtnSaveDSTT_ItemClick;
            btnExportXls.ItemClick += BtnExportXls_ItemClick;
            btnDanhLaiMa.ItemClick += BtnDanhLaiMa_ItemClick;

            popupMenu1.AddItem(btnSaveDSTT);
            popupMenu1.AddItem(btnExportXls);
            popupMenu1.AddItem(btnDanhLaiMa);

            dropbtnDSTT.DropDownControl = popupMenu1;
        }

        private void BtnDanhLaiMa_ItemClick(object sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }

            var ds = Data.DSHoSoTT.Where(x => x.DotTS == dts);
            if (ds.Count() > 0)
            {
                if (XtraMessageBox.Show($"Bạn xác nhận muốn lập lại mã đợt {dts}",
                    "Lập mã hồ sơ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var nghe in Data.DsNghe)
                    {
                        var dstheonghe = ds.Where(x => x.IdNgheTrungTuyen == nghe.Id).OrderBy(x => x.Ten);
                        int i = 1;
                        foreach (var hs in dstheonghe)
                        {
                            hs.MaHoSo = $"{_NamTS}{nghe.Ma2}{i.ToString("D3")}";
                            i++;
                        }
                    }
                    Data.SaveDS(TenDm);
                    BtnRefresh_Click(this, null);
                }
            }

        }
        List<HoSoTrungTuyen> lstHsKhongTT = new();
        private void btnDsKhongTT_ItemClick(object sender, ItemClickEventArgs e)
        {

            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }

            if (lstHsKhongTT.Count == 0)
            {
                var lstHSDT = Data.DSHoSoDT.Where(x => x.DotTS == dts).Select(x => x.Id).ToList();
                var lstHSTT = Data.DSHoSoTT.Where(x => x.DotTS == dts).Select(x => x.IdHSDT).ToList();
                var lstHSKhongTT = lstHSDT.Except(lstHSTT).ToList();
                foreach (var id in lstHSKhongTT)
                {
                    var hs = Data.DSHoSoDT.FirstOrDefault(x => x.DotTS == dts && x.Id == id);
                    if (hs != null)
                        lstHsKhongTT.Add(hs.ToHSTT());
                }
                _bindingSource.DataSource = lstHsKhongTT;
                gridView.RefreshData();
                gridView.ExpandAllGroups();
            }
            else if (lstHDTTTemp.Count(x => x.DotTS == dts) > 0)
            {
                var lstHSDT = Data.DSHoSoDT.Where(x => x.DotTS == dts).Select(x => x.Id).ToList();
                var lstHSTT = lstHDTTTemp.Where(x => x.DotTS == dts).Select(x => x.Id).ToList(); ;
                var lstHSKhongTT = lstHSDT.Except(lstHSTT).ToList();
                foreach (var id in lstHSKhongTT)
                {
                    var hs = Data.DSHoSoDT.FirstOrDefault(x => x.DotTS == dts && x.Id == id);
                    if (hs != null)
                        lstHsKhongTT.Add(hs.ToHSTT());
                }
            }

        }

        private void BtnExportXls_ItemClick(object sender, ItemClickEventArgs e)
        {
            Excel.Application app = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            try
            {
                string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "SLTT.xlsx");
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = "Số_liệu_thí_sinh_trúng_tuyển.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Exel file (*.xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == true)
                {
                    splashScreenManager1.ShowWaitForm();
                    app = new Excel.Application();
                    book = app.Workbooks.Open(filePath);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    sheet.SaveAs(sfd.FileName);
                    book = app.Workbooks.Open(sfd.FileName);
                    sheet = (Excel.Worksheet)book.Worksheets.get_Item(1);
                    var lstHSTT = (List<HoSoTrungTuyen>)_bindingSource.DataSource;
                    var lstTinh = _Helper.getListProvince();
                    if (lstHSTT != null && lstHSTT.Count > 0)
                    {
                        object[,] export = new object[lstHSTT.Count, 27];
                        for (int i = 0; i < lstHSTT.Count; i++)
                        {
                            export[i, 0] = i + 1;
                            export[i, 1] = lstHSTT[i].MaHoSo;
                            export[i, 2] = $"{lstHSTT[i].Ho} {lstHSTT[i].Ten}";
                            export[i, 3] = lstHSTT[i].NgaySinh;
                            export[i, 4] = string.Empty;
                            export[i, 5] = lstHSTT[i].GioiTinh ? "Nam" : "Nữ";
                            export[i, 6] = lstHSTT[i].NoiSinh;
                            export[i, 7] = string.Empty;
                            export[i, 8] = lstHSTT[i].DiaChi;
                            export[i, 9] = lstTinh.First(x => x.AdressCode.Equals(lstHSTT[i].MaTinh)).AdressName;
                            export[i, 10] = string.Empty;
                            var lsthuyen = _Helper.getListDistrict(lstHSTT[i].MaTinh);
                            export[i, 11] = lsthuyen.First(x => x.AdressCode.Equals(lstHSTT[i].MaHuyen)).AdressName;
                            export[i, 12] = string.Empty;
                            var lstxa = _Helper.getListWards(lstHSTT[i].MaHuyen);
                            export[i, 13] = lstxa.First(x => x.AdressCode.Equals(lstHSTT[i].MaXa)).AdressName;
                            export[i, 14] = string.Empty;
                            export[i, 15] = lstHSTT[i].CCCD;
                            export[i, 16] = Data.DsDanToc.First(x => x.Id.Equals(lstHSTT[i].IdDanToc)).Ten;
                            export[i, 17] = string.Empty;
                            export[i, 18] = Data.DsTonGiao.First(x => x.Id.Equals(lstHSTT[i].IdTonGiao)).Ten;
                            export[i, 19] = string.Empty;
                            export[i, 20] = Data.DsQuocTich.First(x => x.Id.Equals(lstHSTT[i].IdQuocTich)).Ten;
                            export[i, 21] = string.Empty;
                            export[i, 22] = lstHSTT[i].SDT;
                            export[i, 23] = lstHSTT[i].Email;
                            export[i, 24] = Data.DsTrinhDo.First(x => x.Id == lstHSTT[i].IdTrinhDoVH).Ten;
                            export[i, 25] = string.Empty;
                            export[i, 26] = lstHSTT[i].HTDT;
                        }
                        Excel.Range range = sheet.get_Range(sheet.Cells[4, 1], sheet.Cells[lstHSTT.Count + 4, 27]);
                        range.set_Value(Missing.Value, export);
                        Marshal.ReleaseComObject(range);
                        range = null;
                    }
                    book.Save();
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("Xuất file thành công!");
                }
            }
            catch
            {
                splashScreenManager1.CloseWaitForm();
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

        private void BtnSaveDSTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }

            if (Data.DSHoSoTT.Where(x => x.DotTS == dts).Count() > 0)
            {
                if (lstHDTTTemp.Count() > 0)
                {
                    if (XtraMessageBox.Show($"Đã tồn tại danh sách trúng tuyển đợt {dts}, bạn xác nhận muốn lưu lại?",
                    "Lập danh sách trúng tuyển", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Data.DSHoSoTT.RemoveAll(x => x.DotTS == dts);
                        Data.DSHoSoTT.AddRange(lstHDTTTemp);
                        lstHDTTTemp = new();
                        Data.SaveDS(TenDm);
                        XtraMessageBox.Show("Đã lưu lại danh sách!");
                        LoadData();
                    }
                }
            }
            else if (lstHDTTTemp.Count() > 0)
            {
                Data.DSHoSoTT.AddRange(lstHDTTTemp);
                lstHDTTTemp = new();
                Data.SaveDS(TenDm);
                XtraMessageBox.Show("Đã lưu lại danh sách!");
                LoadData();
            }
        }

        private List<HoSoTrungTuyen> lstHDTTTemp = new();
        private List<HoSoTrungTuyen> lstHSKhongTT = new();
        private void DropbtnDSTT_Click(object? sender, EventArgs e)
        {
            int dts = cbbDTS.SelectedIndex;
            if (dts <= 0)
            {
                XtraMessageBox.Show("Chưa chọn đợt tuyển sinh!");
                return;
            }
            if (Data.DSHoSoTT.Count(x => x.DotTS.Equals(dts)) > 0)
            {
                if (XtraMessageBox.Show($"Đã tồn tại danh sách trúng tuyển đợt {dts}, bạn xác nhận muốn lập lại danh sách?",
                    "Lập danh sách trúng tuyển", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Data.LapDSTrungTuyen(cbbDTS.SelectedIndex, lstHDTTTemp, lstHSKhongTT);
                    _bindingSource.DataSource = lstHDTTTemp.Where(x => x.TDHV.Equals(cbbTDHV.SelectedItem));
                    gridView.RefreshData();
                    gridView.ExpandAllGroups();
                }
            }
            else
            {
                Data.LapDSTrungTuyen(cbbDTS.SelectedIndex, lstHDTTTemp, lstHSKhongTT);
                _bindingSource.DataSource = lstHDTTTemp.Where(x => x.TDHV.Equals(cbbTDHV.SelectedItem));
                gridView.RefreshData();
                gridView.ExpandAllGroups();
            }
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

        private void LoadData()
        {
            EditMode = false;
            gridView.Columns.Clear();
            _bindingSource = new BindingSource();
            switch (TenDm)
            {
                case TuDien.CategoryName.TruongHoc:
                    _bindingSource.DataSource = Data.DsTruong;
                    break;
                case TuDien.CategoryName.NganhNghe:
                    _bindingSource.DataSource = Data.DsNghe;
                    break;
                case TuDien.CategoryName.DoiTuongUuTien:
                    _bindingSource.DataSource = Data.DsDoiTuongUT;
                    break;
                case TuDien.CategoryName.KhuVucUuTien:
                    _bindingSource.DataSource = Data.DsKhuVucUT;
                    break;
                case TuDien.CategoryName.DanToc:
                    _bindingSource.DataSource = Data.DsDanToc;
                    break;
                case TuDien.CategoryName.TonGiao:
                    _bindingSource.DataSource = Data.DsTonGiao;
                    break;
                case TuDien.CategoryName.TrinhDo:
                    _bindingSource.DataSource = Data.DsTrinhDo;
                    break;
                case TuDien.CategoryName.QuocTich:
                    _bindingSource.DataSource = Data.DsQuocTich;
                    break;
                case TuDien.CategoryName.DotXetTuyen:
                    _bindingSource.DataSource = Data.DsDotXetTuyen;
                    break;
                case TuDien.CategoryName.ChiTieu:
                    _bindingSource.DataSource = Data.DsChiTieu;
                    break;
                case TuDien.CategoryName.HoSoDuTuyen:
                    _bindingSource.DataSource = Data.GetDSDuTuyen(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS");
                    break;
                case TuDien.CategoryName.HoSoTrungTuyen:
                    _bindingSource.DataSource = Data.GetDSTrungTuyen(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS", (string)lookTinh.EditValue, (string)lookQuanHuyen.EditValue, (string)lookXa.EditValue, chkKhongTT.Checked);
                    break;
                case TuDien.CategoryName.ThongKeDiemDT:
                    _bindingSource.DataSource = Data.THDiemXetTuyen(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS");
                    break;
                case TuDien.CategoryName.ThongKeTT:
                    _bindingSource.DataSource = Data.GetDSTrungTuyen(cbbDTS.SelectedIndex, cbbTDHV.SelectedIndex >= 0 ?
                    cbbTDHV.EditValue.ToString() : "THCS");
                    break;
                default: break;
            }
            gridControl.DataSource = _bindingSource;
            gridView.OptionsSelection.MultiSelect = !TenDm.StartsWith("TK");
            gridView.OptionsSelection.MultiSelectMode = !TenDm.StartsWith("TK") ? GridMultiSelectMode.CheckBoxRowSelect : GridMultiSelectMode.RowSelect;
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
            if (TenDm.Equals(TuDien.CategoryName.ChiTieu))
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
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsTruong, "IdTruong", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsDoiTuongUT, "IdDTUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsKhuVucUT, "IdKVUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdNgheDT1", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdNgheDT2", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdNgheTrungTuyen", "Ten", "Id");
                var colXLHT = gridView.Columns.ColumnByFieldName("XLHT");
                if (colXLHT != null) { colXLHT.Visible = ((string)cbbTDHV.EditValue != "THCS"); }
                if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyen))
                {
                    for (int i = 23; i <= 32; i++)
                    {
                        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }
                }
                var colDotTS = gridView.Columns.ColumnByFieldName("DotTS");
                if (colDotTS != null) colDotTS.Group();
            }
            if (TenDm.Equals(TuDien.CategoryName.ThongKeDiemDT))
            {
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdNgheNV1", "Ten", "Id");
                gridView.Columns.ColumnByFieldName("IdNgheNV1").Group();
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdDTUT", "Ten", "Id");
            }
            if (TenDm.Equals(TuDien.CategoryName.ThongKeTT))
            {
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsTruong, "IdTruong", "Ten", "Id");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsDoiTuongUT, "IdDTUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsKhuVucUT, "IdKVUT", "Ma", "Id", "");
                DevForm.CreateRepositoryItemLookUpEdit(gridView, Data.DsNghe, "IdNgheTrungTuyen", "Ten", "Id");
                gridView.Columns.ColumnByFieldName("IdNgheTrungTuyen").Group();
                var colGT = gridView.Columns.ColumnByFieldName("GioiTinh");
                if (colGT != null)
                {
                    RepositoryItemTextEdit riComboBox = new RepositoryItemTextEdit();
                    colGT.ColumnEdit = riComboBox;
                }
                var colDTS = gridView.Columns.ColumnByFieldName("DotTS");
                if (colDTS != null)
                {
                    colDTS.Visible = false;
                }
            }

            panelGrid.RowStyles[1].Height = TenDm.StartsWith("HS") || TenDm.StartsWith("TK") ? 40 : 0;
            btnAdd.Enabled = (TenDm.Equals(TuDien.CategoryName.ChiTieu) || TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen)) ? false : true;
            btnEdit.Enabled = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen) ? false : true;
            btnLapChiTieu.Width = TenDm.Equals(TuDien.CategoryName.ChiTieu) ? 110 : 0;
            _panelButton.Width = TenDm.StartsWith("TK") ? 0 : 220;
            panelTS.Width = (TenDm.StartsWith("TK") || TenDm.StartsWith("HS")) ? 180 : 0;
            panelTDHV.Width = TenDm.StartsWith("HS") || TenDm.Equals(TuDien.CategoryName.ThongKeDiemDT) ? 155 : 0;
            chkKhongTT.Visible = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen) ? true : false;
            panelFilter.Visible = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen) ? true : false;
            btnThongKe.Visible = TenDm.StartsWith("TK") ? true : false;
            dropbtnDSTT.Width = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen) ? 185 : 0;
            btnLoadExel.Width = TenDm.Equals(TuDien.CategoryName.HoSoTrungTuyen) ? 236 : 0;

            FocusRowGrid();
            ShowTotalFooter();
            panelGrid.BringToFront();
            gridView.ExpandAllGroups();
            gridView.BestFitColumns(true);

        }

        private void FocusRowGrid()
        {
            // Prevent the focused cell from being highlighted.
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            // Draw a dotted focus rectangle around the entire row.
            gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        }

        private void LookQuanHuyen_TextChanged(object? sender, EventArgs e)
        {
            if (lookQuanHuyen.EditValue != null)
            {
                lstPhuongXa = _Helper.getListWards(lookQuanHuyen.EditValue.ToString());
                lookXa.Properties.DataSource = lstPhuongXa;
                lookXa.EditValue = null;
                LoadData();
            }
        }

        private void LookTinh_TextChanged(object? sender, EventArgs e)
        {
            if (lookTinh.EditValue != null)
            {
                lstQuanHuyen = _Helper.getListDistrict(lookTinh.EditValue.ToString());
                lookQuanHuyen.Properties.DataSource = lstQuanHuyen;
                lookXa.EditValue = null;
                lookQuanHuyen.EditValue = null;
                LoadData();
            }
        }

        private void LookXa_TextChanged(object? sender, EventArgs e)
        {
            LoadData();
        }



        #region Xử lý GridControl

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string? value = e.Value.ToString();

            gridView.CellValueChanged -= GridView_CellValueChanged;
            if (e.Column.FieldName.Equals("Ma"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Data.CheckDupCode(value, TenDm))
                    {
                        MessageBox.Show(this, "Trùng mã!");
                        gridView.SetFocusedRowCellValue("Ma", string.Empty);
                    }
                }
            }
            if (e.Column.FieldName.Equals("Ma2"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Data.CheckDupCode(value, TenDm))
                    {
                        MessageBox.Show(this, "Trùng mã!");
                        gridView.SetFocusedRowCellValue("Ma", string.Empty);
                    }
                    else
                        gridView.SetFocusedRowCellValue("Ma2", value.ToUpper());
                }
            }
            gridView.CellValueChanged += GridView_CellValueChanged;
        }

        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString("D3");
        }

        private void GridView_DoubleClick(object? sender, EventArgs e)
        {
            if (TenDm.Equals(TuDien.CategoryName.HoSoDuTuyen))
            {
                try
                {
                    var r = gridView.GetFocusedRow() as BaseClass;
                    if (r is not null)
                    {
                        var hs = Data.DSHoSoDT.FirstOrDefault(x => x.Id.Equals(r.Id));
                        if (hs is not null)
                        {
                            F_HoSo f = new(hs.CloneJson());
                            f.Show(this);
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void GridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var r = e.Row;
            if (r != null && !TenDm.StartsWith("HS"))
            {
                Data.SaveDS(TenDm);
            }
        }

        private void GridView_ShowingEditor(object? sender, CancelEventArgs e)
        {
            e.Cancel = EditMode ^ (gridView.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
        }

        private void GridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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
            gridView.IndicatorWidth = 55;
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

        #endregion Xử lý GridControl

        #region MenuSelect

        private void btnChiTieu_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.ChiTieu;
            LoadData();
        }

        private void btnDanToc_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DanToc;
            LoadData();
        }

        private void btnDotTS_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DotXetTuyen;
            LoadData();
        }

        private void btnDTUT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.DoiTuongUuTien;
            LoadData();
        }

        private void btnHoSoDuTuyen_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.HoSoDuTuyen;
            LoadData();
        }

        private void btnKVUT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.KhuVucUuTien;
            LoadData();
        }

        private void btnNghe_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.NganhNghe;
            LoadData();
        }

        private void btnQuocTich_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.QuocTich;
            LoadData();
        }

        private void btnTDHV_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TrinhDo;
            LoadData();
        }

        private void btnTHDTD_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.ThongKeDiemDT;
            LoadData();
        }

        private void btnTonGiao_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TonGiao;
            LoadData();
        }

        private void btnTruong_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.TruongHoc;
            LoadData();
        }

        private void btnHSTT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.HoSoTrungTuyen;
            LoadData();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Form.F_Setting f = new(Data.CurrSettings.CloneJson());
            f.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            F_Account f = new(Data.CurrUser.CloneJson());
            f.Show();
        }

        private void btnTKTT_Click(object sender, EventArgs e)
        {
            TenDm = TuDien.CategoryName.ThongKeTT;
            LoadData();
        }

        #endregion MenuSelect
    }
}