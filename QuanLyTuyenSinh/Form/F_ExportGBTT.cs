using DevExpress.XtraEditors;
using Microsoft.Office.Interop.Word;
using System.IO;
using MsWord = Microsoft.Office.Interop.Word;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_ExportGBTT : DevExpress.XtraEditors.DirectXForm
    {
        List<HoSoTrungTuyenTC> _dstt;
        public F_ExportGBTT(List<HoSoTrungTuyenTC> dstt)
        {
            InitializeComponent();
            MinimumSize = Size;
            StartPosition = FormStartPosition.CenterScreen;
            _dstt = dstt;
        }
        void LoadDateTime()
        {
            Gio.Properties.MinValue = 1;
            Gio.Properties.MaxValue = 24;
            Gio.Properties.MaxLength = 2;

            Phut.Properties.MinValue = 0;
            Phut.Properties.MaxValue = 59;
            Phut.Properties.MaxLength = 2;

            NgayXuat.EditValue = DateTime.Now;
            TuNgay.EditValue = DateTime.Now;
            DenNgay.EditValue = DateTime.Now.AddDays(14);
            NgayHetHan.EditValue = DateTime.Now.AddDays(14);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            var wordApp = new MsWord.Application();
            MsWord.Document documentFrom = null;
            try
            {
                string fileNameFrom = System.IO.Path.Combine(TuDien.EXEL_FOLDER, "GBTT.doc");
                if (!File.Exists(fileNameFrom))
                {
                    XtraMessageBox.Show($"Chưa có file mẫu giấy báo trúng tuyển!\n {fileNameFrom}");
                    return;
                }
                var fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    documentFrom = wordApp.Documents.Open(fileNameFrom, Type.Missing, true);
                    documentFrom.ActiveWindow.Selection.WholeStory();
                    documentFrom.ActiveWindow.Selection.Copy();
                    Directory.CreateDirectory(fbd.SelectedPath);
                    // Initializing progress bar properties
                    progressBarControl1.Properties.Step = 1;
                    progressBarControl1.Properties.PercentView = true;
                    progressBarControl1.Properties.Maximum = _dstt.Count;
                    progressBarControl1.Properties.Minimum = 0;
                    progressBarControl1.Properties.ShowTitle = true;
                    foreach (var tt in _dstt)
                    {
                        var newDocument = wordApp.Documents.Add();
                        newDocument.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdPasteDefault);
                        object fileName = System.IO.Path.Combine(fbd.SelectedPath, $"{tt.NamTS}_{tt.DotTS}_{tt.MaHoSo}.docx");
                        newDocument.SaveAs2(fileName);
                        newDocument.Close();

                        Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(ref fileName, ReadOnly: false, Visible: false);
                        aDoc.Activate();
                        Nghe nghe = DataHelper.DsNghe.First(x => x.Id == tt.IdNgheTrungTuyen);
                        _Helper.FindAndReplace(wordApp, "{0}", NgayXuat.DateTime.Day.ToString());
                        _Helper.FindAndReplace(wordApp, "{1}", NgayXuat.DateTime.Month.ToString());
                        _Helper.FindAndReplace(wordApp, "{2}", NgayXuat.DateTime.Year.ToString());
                        _Helper.FindAndReplace(wordApp, "{3}", tt.NamTS.ToString());
                        _Helper.FindAndReplace(wordApp, "{4}", $"{tt.Ho} {tt.Ten}");
                        _Helper.FindAndReplace(wordApp, "{0000000000}", tt.NgaySinh.ToShortDateString());
                        _Helper.FindAndReplace(wordApp, "{6}", tt.GioiTinh ? "Nam" : "Nữ");
                        _Helper.FindAndReplace(wordApp, "{7}", tt.DiaChi);
                        string NgheTT = string.Empty;
                        if (nghe.Ten.Length <= 5)
                            NgheTT = $"{nghe.Ten}\t\t\t\tMã nghề: {nghe.Ma}";
                        else if (nghe.Ten.Length <= 12)
                            NgheTT = $"{nghe.Ten}\t\t\tMã nghề: {nghe.Ma}";
                        else if (nghe.Ten.Length <= 20)
                            NgheTT = $"{nghe.Ten}\t\tMã nghề: {nghe.Ma}";
                        else
                            NgheTT = $"{nghe.Ten}\tMã nghề: {nghe.Ma}";
                        _Helper.FindAndReplace(wordApp, "{8}", NgheTT);
                        _Helper.FindAndReplace(wordApp, "{10}", Gio.Value.ToString("00"));
                        _Helper.FindAndReplace(wordApp, "{11}", Phut.Value.ToString("00"));
                        _Helper.FindAndReplace(wordApp, "{12}", TuNgay.DateTime.ToString("dd/MM/yyyy"));
                        _Helper.FindAndReplace(wordApp, "{13}", DenNgay.DateTime.ToString("dd/MM/yyyy"));
                        _Helper.FindAndReplace(wordApp, "{14}", NgayHetHan.DateTime.ToString("dd/MM/yyyy"));
                        aDoc.Save();
                        aDoc.Close();
                        progressBarControl1.PerformStep();
                        progressBarControl1.Update();
                    }

                    XtraMessageBox.Show($"Xuất giấy báo thành công!\n {fbd.SelectedPath}");
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Có lỗi xảy ra!\n {ex.Message}");
            }
            finally
            {
                if (documentFrom != null)
                    documentFrom.Close(false);

                wordApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void F_ExportGBTT_Load(object sender, EventArgs e)
        {
            LoadDateTime();
        }
    }
}