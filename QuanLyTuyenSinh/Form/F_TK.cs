using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using QuanLyTuyenSinh.Models;
using System.Data;
using ChartTitle = DevExpress.XtraCharts.ChartTitle;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_TK : DevExpress.XtraEditors.DirectXForm
    {
        private int dts = -1;
        private static F_TK inst;

        public static F_TK GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new F_TK();
                return inst;
            }
        }

        private F_TK()
        {
            InitializeComponent();

        }

        private void LoadComboBoxDTS()
        {
            var ds = DataHelper.DsDotXetTuyen.Where(x => x.NamTS == DataHelper.NamTS).OrderBy(x => x.DotTS).ToList();

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
            gridControl1.DataSource = chkTrungCap.Checked ? DataHelper.THSLTTTheoXaTC(dts) : DataHelper.THSLTTTheoXaGDTX(dts);
            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);

            var lstTTTC = DataHelper.DSHoSoTTTC.Where(x => (dts == 0 ? true : x.DotTS == dts));
            var lstTTTX = DataHelper.DSHoSoTrungTuyenTX.Where(x => (dts == 0 ? true : x.DotTS == dts));

            var lstXa = _Helper.getListWards(mahuyen);

            for (int i = 0; i < DataHelper.DsNghe.Count; i++)
            {
                Series series = new Series(DataHelper.DsNghe[i].Ten, ViewType.Bar);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                for (int j = 0; j < lstXa.Count; j++)
                {
                    int sl = chkTrungCap.Checked ? lstTTTC.Where(x => x.MaXa == lstXa[j].AddressCode && 
                    x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id)).Count() : lstTTTX.Where(x => x.MaXa == lstXa[j].AddressCode &&
                    x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id)).Count();
                    series.Points.Add(new SeriesPoint(lstXa[j].AddressName, sl));
                }

                chart.Series.Add(series);
            }
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng trúng tuyển theo từng xã huyện Vạn Ninh";
            chart.Titles.Add(chartTitle1);
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
        }

        private void ButtonTKTheoNghe_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dts < 0)
            {
                XtraMessageBox.Show("Chưa chọn đơt tuyển sinh");
                return;
            }
            gridView.Columns.Clear();

            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);

            if (chkXetTuyen.Checked)
            {
                List<THSLTheoNghe> lstReport = new();

                List<NguyenVong> lstNV1 = new();
                List<NguyenVong> lstNV2 = new();
                var hsdtTC = DataHelper.DSHoSoXTTC.Where(x => (dts == 0 ? true : x.DotTS == dts));
                var hsdtTX = DataHelper.DSHoSoXetTuyenTX.Where(x => (dts == 0 ? true : x.DotTS == dts));
                if(chkTrungCap.Checked)
                    foreach (var hs in hsdtTC)
                    {
                        var nv1 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 1);
                        if (nv1 != null) lstNV1.Add(nv1);
                        var nv2 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 2);
                        if (nv2 != null) lstNV2.Add(nv2);
                    }
                else
                    foreach (var hs in hsdtTX)
                    {
                        //var nv1 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 1);
                        //if (nv1 != null) lstNV1.Add(nv1);
                        //var nv2 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 2);
                        //if (nv2 != null) lstNV2.Add(nv2);
                    }

                Series series1 = new Series("Nguyện vọng 1", ViewType.Bar);
                Series series2 = new Series("Nguyện vọng 2", ViewType.Bar);
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                for (int i = 0; i < DataHelper.DsNghe.Count; i++)
                {
                    lstReport.Add(new THSLTheoNghe
                    {
                        STT = (i + 1).ToString(),
                        MaNghe = DataHelper.DsNghe[i].Ma,
                        TenNghe = DataHelper.DsNghe[i].Ten,
                        SLNV1 = lstNV1.Where(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)).Count(),
                        SLNV2 = lstNV2.Where(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)).Count(),
                    });
                    series1.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstNV1.Where(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)).Count()));
                    series2.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstNV2.Where(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)).Count()));
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
                if (chkTrungCap.Checked)
                {
                    List<THSLTTTheoNghe> lstReport = new();

                    Series series1 = new Series("Số lượng học sinh", ViewType.Bar);
                    Series series2 = new Series("Học sinh nữ", ViewType.Bar);
                    Series series3 = new Series("Đối tượng ưu tiên", ViewType.Bar);
                    Series series4 = new Series("Tốt nghiệp THCS", ViewType.Bar);
                    Series series5 = new Series("Tốt nghiệp THPT", ViewType.Bar);

                    series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                    for (int i = 0; i < DataHelper.DsNghe.Count; i++)
                    {
                        var lstHDTTNghe = DataHelper.DSHoSoTTTC.Where(x => x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id)
                        && (dts == 0 ? true : x.DotTS == dts));
                        series1.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Count()));
                        series2.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => !x.GioiTinh).Count()));
                        series3.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => !string.IsNullOrEmpty(x.IdDTUT)).Count()));
                        series4.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THCS")).Count()));
                        series5.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THPT")).Count()));
                        lstReport.Add(new THSLTTTheoNghe
                        {
                            STT = (i + 1).ToString(),
                            MaNghe = DataHelper.DsNghe[i].Ma,
                            TenNghe = DataHelper.DsNghe[i].Ten,
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
                }
                else
                {
                    List<THSLTTTheoNgheTX> lstReport = new();

                    Series series1 = new Series("Số lượng học sinh", ViewType.Bar);
                    Series series2 = new Series("Học sinh nữ", ViewType.Bar);
                    Series series3 = new Series("Đối tượng ưu tiên", ViewType.Bar);
                    Series series4 = new Series("Tốt nghiệp THCS", ViewType.Bar);
                    Series series5 = new Series("Tốt nghiệp THPT", ViewType.Bar);

                    series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                    for (int i = 0; i < DataHelper.DsNghe.Count; i++)
                    {
                        var lstHDTTNghe = DataHelper.DSHoSoTrungTuyenTX.Where(x => x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id)
                        && (dts == 0 ? true : x.DotTS == dts));
                        series1.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Count()));
                        series2.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => !x.GioiTinh).Count()));
                        series3.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => !string.IsNullOrEmpty(x.IdDTUT)).Count()));
                        series4.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THCS")).Count()));
                        series5.Points.Add(new SeriesPoint(DataHelper.DsNghe[i].Ten, lstHDTTNghe.Where(x => x.TDHV.Equals("THPT")).Count()));
                        lstReport.Add(new THSLTTTheoNgheTX
                        {
                            STT = (i + 1).ToString(),
                            MaNghe = DataHelper.DsNghe[i].Ma,
                            TenNghe = DataHelper.DsNghe[i].Ten,
                            SLHS = lstHDTTNghe.Count(),
                            SLHSNu = lstHDTTNghe.Where(x => !x.GioiTinh).Count(),
                            SLDTUUT = lstHDTTNghe.Where(x => !string.IsNullOrEmpty(x.IdDTUT)).Count(),
                            SLHSNam = lstHDTTNghe.Where(x => x.GioiTinh).Count(),                            
                        });
                    }
                    lstReport.Add(new THSLTTTheoNgheTX
                    {
                        TenNghe = "Tổng cộng",
                        SLHS = lstReport.Sum(x => x.SLHS),
                        SLHSNu = lstReport.Sum(x => x.SLHSNu),
                        SLDTUUT = lstReport.Sum(x => x.SLDTUUT),
                        SLHSNam = lstReport.Sum(x => x.SLHSNam)
                    });
                    gridControl1.DataSource = lstReport;
                    chart.Series.AddRange(new Series[] { series1, series2, series3, series4, series5 });
                }
                

                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Text = "Số lượng trúng tuyển theo nghề";
                chart.Titles.Add(chartTitle1);
            }

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
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

            if (chkXetTuyen.Checked && chkTrungCap.Checked)
            {
                gridControl1.DataSource = DataHelper.THSLNgheXTTheoTruongTC(dts);
                LoadThongKeDTTheoTruongTC();
            }                
            else if (!chkXetTuyen.Checked && chkTrungCap.Checked)
            {
                gridControl1.DataSource = DataHelper.THSLNgheTTTheoTruongTC(dts);
                LoadThongKeTTTheoTruongTC();
            }                
            else if (chkXetTuyen.Checked && !chkTrungCap.Checked)
            {
                gridControl1.DataSource = DataHelper.THSLNgheXTTheoTruongGDTX(dts);
                LoadThongKeDTTheoTruongGDTX();
            }
            else
            {
                gridControl1.DataSource = DataHelper.THSLNgheTTTheoTruongGDTX(dts);
                LoadThongKeTTTheoTruongGDTX();
            }
                
            gridView.BestFitColumns(true);
        }

        private void ChartSetting(ChartControl chart)
        {
            chart.Name = "chart";
            chart.SelectionMode = ElementSelectionMode.Single;
            chart.SeriesSelectionMode = SeriesSelectionMode.Series;
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void LoadThongKeTTTheoTruongTC()
        {
            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);
            var dstruongTHCS = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();
            var HSTHPT = DataHelper.DSHoSoTTTC.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm các dòng TK trường THCS
            for (int i = 0; i < DataHelper.DsNghe.Count; i++)
            {
                Series series = new Series(DataHelper.DsNghe[i].Ten, ViewType.Bar);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                for (int j = 0; j < dstruongTHCS.Count; j++)
                {
                    int sldt = DataHelper.DSHoSoTTTC.Where(x => x.IdTruong.Equals(dstruongTHCS[j].Id) &&
                    x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id) && (dts == 0 ? true : x.DotTS == dts)).Count();
                    series.Points.Add(new SeriesPoint(dstruongTHCS[j].Ten, sldt));
                }
                int sldtTHPT = HSTHPT.Where(x => x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id)
                && (dts == 0 ? true : x.DotTS == dts)).Count();
                series.Points.Add(new SeriesPoint("THPT", sldtTHPT));
                chart.Series.Add(series);
            }

            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng trúng tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
        }

        private void LoadThongKeDTTheoTruongTC()
        {
            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);

            var dstruongTHCS = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();
            var HSTHPT = DataHelper.DSHoSoXTTC.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm các dòng TK trường THCS
            for (int i = 0; i < DataHelper.DsNghe.Count; i++)
            {
                Series series = new Series(DataHelper.DsNghe[i].Ten, ViewType.Bar);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                for (int j = 0; j < dstruongTHCS.Count; j++)
                {
                    int sldt = DataHelper.DSHoSoXTTC.Where(x => x.IdTruong.Equals(dstruongTHCS[j].Id) &&
                    x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)) is not null
                    && (dts == 0 ? true : x.DotTS == dts)).Count();

                    series.Points.Add(new SeriesPoint(dstruongTHCS[j].Ten, sldt));
                    int sldtTHPT = HSTHPT.Where(x => x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(DataHelper.DsNghe[i].Id)) is not null
                    && (dts == 0 ? true : x.DotTS == dts)).Count();

                    series.Points.Add(new SeriesPoint("THPT", sldtTHPT));
                }
                chart.Series.Add(series);
            }
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng dự tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
        }
        private void LoadThongKeTTTheoTruongGDTX()
        {
            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);
            var dstruong = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
           
            //Thêm các dòng TK trường THCS
            for (int i = 0; i < DataHelper.DsNghe.Count; i++)
            {
                Series series = new Series(DataHelper.DsNghe[i].Ten, ViewType.Bar);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                for (int j = 0; j < dstruong.Count; j++)
                {
                    int sldt = DataHelper.DSHoSoTrungTuyenTX.Where(x => x.IdTruong.Equals(dstruong[j].Id) &&
                    x.IdNgheTrungTuyen.Equals(DataHelper.DsNghe[i].Id) && (dts == 0 ? true : x.DotTS == dts)).Count();
                    series.Points.Add(new SeriesPoint(dstruong[j].Ten, sldt));
                }                
                chart.Series.Add(series);
            }
            //
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng trúng tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
        }

        private void LoadThongKeDTTheoTruongGDTX()
        {
            panelchart.Controls.Clear();
            ChartControl chart = new ChartControl();
            ChartSetting(chart);

            var dstruong = DataHelper.DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
           
            //Thêm các dòng TK trường THCS
            for (int i = 0; i < DataHelper.DsNghe.Count; i++)
            {
                Series series = new Series(DataHelper.DsNghe[i].Ten, ViewType.Bar);
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                for (int j = 0; j < dstruong.Count; j++)
                {
                   
                    var sltheotruong = DataHelper.DSHoSoXetTuyenTX.Where(x => x.IdTruong.Equals(dstruong[j].Id));
                    int slNV1 = 0;
                    foreach (var hs in sltheotruong)
                    {
                        //var NV1 = hs.DsNguyenVong.FirstOrDefault(x => x.NV == 1);
                        //if (NV1 != null && NV1.IdNghe == DataHelper.DsNghe[i].Id)
                        //    slNV1++; 
                    }
                   
                    series.Points.Add(new SeriesPoint(dstruong[j].Ten, slNV1));                    
                }
                chart.Series.Add(series);
            }
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Số lượng dự tuyển theo từng trường";
            chart.Titles.Add(chartTitle1);

            chart.Dock = DockStyle.Fill;
            panelchart.Controls.Add(chart);
        }

        private void HighlightTotal(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (gridView != null && e.RowHandle == gridView.RowCount - 1)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = System.Drawing.Color.Yellow;
                e.Appearance.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void F_TK_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            LoadMenuTK();
            LoadComboBoxDTS();
            gridView.OptionsBehavior.Editable = false;
            gridView.CustomDrawCell += HighlightTotal;
        }

        private void chkXetTuyen_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            panelchart.Controls.Clear();
        }

        private void chkTrungTuyen_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            panelchart.Controls.Clear();
        }

        private void barCbbDTS_EditValueChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            panelchart.Controls.Clear();
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    ChartControl chart = panelchart.Controls.Find("chart", true).FirstOrDefault() as ChartControl;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    if (chart != null)
                    {
                        PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                        link.Component = chart;
                        link.Landscape = true;
                        link.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        link.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                        link.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        link.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                    }
                    else
                        XtraMessageBox.Show("Chưa chọn thống kê");
                    break;

                default:
                    break;
            }
        }

        private void chkTrungCap_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            panelchart.Controls.Clear();
            chkTrungCap.Checked = !chkGDTX.Checked;
        }

        private void chkGDTX_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadMenuTK();
            gridControl1.DataSource = null;
            gridView.Columns.Clear();
            panelchart.Controls.Clear();
            chkGDTX.Checked = !chkTrungCap.Checked;

        }
    }
}