using DevExpress.Charts.Native;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using System.Data;
using System.Dynamic;
using System.Windows.Media;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_TK : DevExpress.XtraEditors.DirectXForm
    {
        int dts = -1;
        public F_TK()
        {
            InitializeComponent();
        }

        private void LoadComboBoxDTS()
        {
            var ds = Data.DsDotXetTuyen.Where(x => x.NamTS == Data._NamTS).OrderBy(x => x.DotTS).ToList();

            var lst = new List<string>() { "Cả năm" };
            lst.AddRange(ds.Select(x => x.DotTS.ToString()).ToList());

            CbbDTS.Items.Clear();
            CbbDTS.NullText = "(Trống)";
            CbbDTS.TextEditStyle = TextEditStyles.DisableTextEditor;
            CbbDTS.AutoComplete = false;

            CbbDTS.Items.AddRange(lst);
            CbbDTS.EditValueChanged += (sender, e) =>
            {
                var cbb = sender as ComboBoxEdit;
                if (cbb != null)
                {
                    dts = cbb.SelectedIndex;
                }
            };
        }
        private void LoadMenuTK()
        {
            barSubItemTK.ClearLinks();

            BarButtonItem buttonTKTheoTruong = new BarButtonItem(barManager1, "Số lượng theo từng trường");
            buttonTKTheoTruong.ItemClick += ButtonTKTheoTruong_ItemClick;
            BarButtonItem buttonTKTheoNghe = new BarButtonItem(barManager1, "Số lượng theo từng nghề");
            buttonTKTheoNghe.ItemClick += ButtonTKTheoNghe_ItemClick;
            barSubItemTK.AddItems(new BarItem[] { buttonTKTheoTruong, buttonTKTheoNghe });
            if (!chkXetTuyen.Checked)
            {
                BarButtonItem buttonTKTheoXa = new BarButtonItem(barManager1, "Số lượng theo từng Xã (Vạn Ninh)");
                buttonTKTheoXa.ItemClick += (sender, e) =>
                {
                    gridView.Columns.Clear();
                    LoadThongKeTheoXa();
                };
                barSubItemTK.AddItem(buttonTKTheoXa);
            }
        }

        private void LoadThongKeTheoXa(string mahuyen = "51103")
        {
            if (dts < 0)
            {
                XtraMessageBox.Show("Chưa chọn đơt tuyển sinh");
                return;
            }
            gridView.Columns.Clear();
            gridView.CustomDrawCell += HighlightTotal;
            gridControl1.DataSource = Data.THSLTTTheoXa(dts);
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.BeginInit();

            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("tenxa", typeof(string)), new DataColumn("tennghe", typeof(string)), new DataColumn("sl", typeof(int)) });
            var lstTT = Data.DSHoSoTT.Where(x => (dts == 0 ? true : x.DotTS == dts));
            var lstXa = _Helper.getListWards(mahuyen);

            for (int i = 0; i < Data.DsNghe.Count; i++)
            {
                for (int j = 0; j < lstXa.Count; j++)
                {
                    int sl = lstTT.Where(x => x.MaXa == lstXa[j].AdressCode && x.IdNgheTrungTuyen.Equals(Data.DsNghe[i].Id)).Count();
                    table.Rows.Add(lstXa[j].AdressName, Data.DsNghe[i].Ten, sl);
                }

            }
            chart.DataSource = table;
            chart.SeriesTemplate.ChangeView(ViewType.StackedBar);
            chart.SeriesTemplate.SeriesDataMember = "tenxa";
            chart.SeriesTemplate.SetDataMembers("tennghe", "sl");
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng trúng tuyển theo từng xã huyện Vạn Ninh";
            chart.Titles.Add(chartTitle1);
            chart.EndInit();
        }

        private void ButtonTKTheoNghe_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dts < 0)
            {
                XtraMessageBox.Show("Chưa chọn đơt tuyển sinh");
                return;
            }
            gridView.Columns.Clear();
            gridView.CustomDrawCell += HighlightTotal;
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.BeginInit();
            if (chkXetTuyen.Checked)
            {
                List<THSLTheoNghe> lstReport = new();

                List<NguyenVong> lstNV1 = new();
                List<NguyenVong> lstNV2 = new();
                var hsdt = Data.DSHoSoDT.Where(x => (dts == 0 ? true : x.DotTS == dts));
                foreach (var hs in hsdt)
                {
                    var nv1 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 1);
                    if (nv1 != null) lstNV1.Add(nv1);
                    var nv2 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 2);
                    if (nv2 != null) lstNV2.Add(nv2);
                }

                Series series1 = new Series("Nguyện vọng 1", ViewType.Bar);
                Series series2 = new Series("Nguyện vọng 2", ViewType.Bar);

                for (int i = 0; i < Data.DsNghe.Count; i++)
                {
                    lstReport.Add(new THSLTheoNghe
                    {
                        STT = (i + 1).ToString(),
                        MaNghe = Data.DsNghe[i].Ma,
                        TenNghe = Data.DsNghe[i].Ten,
                        SLNV1 = lstNV1.Where(x => x.IdNghe.Equals(Data.DsNghe[i].Id)).Count(),
                        SLNV2 = lstNV2.Where(x => x.IdNghe.Equals(Data.DsNghe[i].Id)).Count(),
                    });
                    series1.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstNV1.Where(x => x.IdNghe.Equals(Data.DsNghe[i].Id)).Count()));
                    series2.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstNV2.Where(x => x.IdNghe.Equals(Data.DsNghe[i].Id)).Count()));
                }
                lstReport.Add(new THSLTheoNghe
                {
                    TenNghe = "Tổng cộng",
                    SLNV1 = lstNV1.Count(),
                    SLNV2 = lstNV2.Count(),
                });
                gridControl1.DataSource = lstReport;
                chart.Series.AddRange(new Series[] { series1, series2 });

                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "Số lượng dự tuyển theo nghề";
                chart.Titles.Add(chartTitle1);
            }
            else
            {
                List<THSLTTTheoNghe> lstReport = new();

                Series series1 = new Series("Số lượng học sinh", ViewType.Bar);
                Series series2 = new Series("Học sinh nữ", ViewType.Bar);
                Series series3 = new Series("Đối tượng ưu tiên", ViewType.Bar);
                Series series4 = new Series("Tốt nghiệp THCS", ViewType.Bar);
                Series series5 = new Series("Tốt nghiệp THPT", ViewType.Bar);
                for (int i = 0; i < Data.DsNghe.Count; i++)
                {
                    var lstHDTTNghe = Data.DSHoSoTT.Where(x => x.IdNgheTrungTuyen.Equals(Data.DsNghe[i].Id)
                    && (dts == 0 ? true : x.DotTS == dts));
                    series1.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstHDTTNghe.Count()));
                    series2.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstHDTTNghe.Where(x => !x.GioiTinh).Count()));
                    series3.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstHDTTNghe.Where(x => !string.IsNullOrEmpty(x.IdDTUT)).Count()));
                    series4.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THCS")).Count()));
                    series5.Points.Add(new SeriesPoint(Data.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THPT")).Count()));
                    lstReport.Add(new THSLTTTheoNghe
                    {
                        STT = (i + 1).ToString(),
                        MaNghe = Data.DsNghe[i].Ma,
                        TenNghe = Data.DsNghe[i].Ten,
                        SLHS = lstHDTTNghe.Count(),
                        SLHSNu = lstHDTTNghe.Where(x => !x.GioiTinh).Count(),
                        SLDTUUT = lstHDTTNghe.Where(x => !string.IsNullOrEmpty(x.IdDTUT)).Count(),
                        SLTNTHCS = lstHDTTNghe.Where(x => x.TDHV.Equals("THCS")).Count(),
                        SLTNTHPTT = lstHDTTNghe.Where(x => x.TDHV.Equals("THPT")).Count(),
                    });
                }
                lstReport.Add(new THSLTTTheoNghe
                {
                    TenNghe = "Tổng cộng",
                    SLHS = lstReport.Sum(x => x.SLHS),
                    SLHSNu = lstReport.Sum(x => x.SLHSNu),
                    SLDTUUT = lstReport.Sum(x => x.SLDTUUT),
                    SLTNTHCS = lstReport.Sum(x => x.SLTNTHCS),
                    SLTNTHPTT = lstReport.Sum(x => x.SLTNTHPTT),
                });
                gridControl1.DataSource = lstReport;
                chart.Series.AddRange(new Series[] { series1, series2, series3, series4, series5 });

                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "Số lượng trúng tuyển theo nghề";
                chart.Titles.Add(chartTitle1);
            }

            chart.EndInit();

        }

        private void ButtonTKTheoTruong_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dts < 0)
            {
                XtraMessageBox.Show("Chưa chọn đơt tuyển sinh");
                return;
            }
            gridView.Columns.Clear();
            gridView.CustomDrawCell += HighlightTotal;
            gridControl1.DataSource = chkXetTuyen.Checked ? Data.THSLNgheTheoTruong(dts) : Data.THSLTTNgheTheoTruong(dts);
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.BeginInit();

            if (chkXetTuyen.Checked) LoadThongKeDTTheoTruong();
            else
                LoadThongKeTTTheoTruong();
            chart.EndInit();
            gridView.BestFitColumns(true);
        }

        private void LoadThongKeTTTheoTruong()
        {
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
            //Load biểu đồ            
            //Lay du lieu
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("tentruong", typeof(string)), new DataColumn("tennghe", typeof(string)), new DataColumn("sl", typeof(int)) });
            var dstruongTHCS = Data.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = Data.DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();

            //Thêm các dòng TK trường THCS
            for (int i = 0; i < dstruongTHCS.Count; i++)
            {
                for (int j = 0; j < Data.DsNghe.Count; j++)
                {
                    int sldt = Data.DSHoSoTT.Where(x => x.IdTruong.Equals(dstruongTHCS[i].Id) &&
                    x.IdNgheTrungTuyen.Equals(Data.DsNghe[j].Id) && (dts == 0 ? true : x.DotTS == dts)).Count();
                    table.Rows.Add(Data.DsTruong[i].Ten, Data.DsNghe[j].Ten, sldt);
                }
            }
            var HSTHPT = Data.DSHoSoTT.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm 1 dòng TK trường THPT
            for (int j = 0; j < Data.DsNghe.Count; j++)
            {
                int sldt = HSTHPT.Where(x => x.IdNgheTrungTuyen.Equals(Data.DsNghe[j].Id)
                && (dts == 0 ? true : x.DotTS == dts)).Count();
                table.Rows.Add("Trường THPT", Data.DsNghe[j].Ten, sldt);
            }

            chart.DataSource = table;
            chart.SeriesTemplate.ChangeView(ViewType.StackedBar);
            chart.SeriesTemplate.SeriesDataMember = "tentruong";
            chart.SeriesTemplate.SetDataMembers("tennghe", "sl");

            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng trúng tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);

        }

        private void LoadThongKeDTTheoTruong()
        {
            //Load biểu đồ            
            //Lay du lieu
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("tentruong", typeof(string)), new DataColumn("tennghe", typeof(string)), new DataColumn("sl", typeof(int)) });
            var dstruongTHCS = Data.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = Data.DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();

            //Thêm các dòng TK trường THCS
            for (int i = 0; i < dstruongTHCS.Count; i++)
            {
                for (int j = 0; j < Data.DsNghe.Count; j++)
                {
                    int sldt = Data.DSHoSoDT.Where(x => x.IdTruong.Equals(dstruongTHCS[i].Id) &&
                    x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(Data.DsNghe[j].Id)) is not null
                    && (dts == 0 ? true : x.DotTS == dts)).Count();
                    table.Rows.Add(Data.DsTruong[i].Ten, Data.DsNghe[j].Ten, sldt);
                }
            }
            var HSTHPT = Data.DSHoSoDT.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm 1 dòng TK trường THPT
            for (int j = 0; j < Data.DsNghe.Count; j++)
            {
                int sldt = HSTHPT.Where(x => x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(Data.DsNghe[j].Id)) is not null
                && (dts == 0 ? true : x.DotTS == dts)).Count();
                table.Rows.Add("Trường THPT", Data.DsNghe[j].Ten, sldt);
            }

            chart.DataSource = table;
            chart.SeriesTemplate.ChangeView(ViewType.StackedBar);
            chart.SeriesTemplate.SeriesDataMember = "tentruong";
            chart.SeriesTemplate.SetDataMembers("tennghe", "sl");
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng dự tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);
        }

        private void HighlightTotal(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (gridView != null && e.RowHandle == gridView.RowCount - 1)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = System.Drawing.Color.Yellow;
                e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void F_TK_Load(object sender, EventArgs e)
        {
            LoadMenuTK();
            LoadComboBoxDTS();
            gridView.CustomDrawCell += HighlightTotal;

        }

        private void chkXetTuyen_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            chart = new ChartControl();
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
        }

        private void chkTrungTuyen_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();

        }

        private void barCbbDTS_EditValueChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            chart.DataSource = null;
            chart.Series.Clear();
            chart.Titles.Clear();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dts < 0)
            {
                XtraMessageBox.Show("Chưa chọn đơt tuyển sinh");
                return;
            }
            switch (tabTK.SelectedTabPageIndex)
            {
                case 0:
                    if (gridControl1.DataSource != null)
                    {
                        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                        link.Component = gridControl1;
                        link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        link.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                        link.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        link.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                    else XtraMessageBox.Show("Chưa có dữ liệu");
                    break;
                case 1:
                    if (chart.DataSource != null)
                    {
                        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                        link.Component = chart;
                        link.Landscape = true;
                        link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        link.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                        link.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        link.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                    else XtraMessageBox.Show("Chưa có dữ liệu");
                    break;
                default:
                    break;

            }
        }
    }
}