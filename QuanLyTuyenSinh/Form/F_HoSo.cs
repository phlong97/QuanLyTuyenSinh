﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_HoSo : DevExpress.XtraEditors.DirectXForm
    {
        private HoSoDuTuyen _hoSo;
        private BindingSource _sourceHS;
        private BindingSource _sourceNV;

        private bool EditMode;
        private string Diachi = string.Empty;
        private List<_Helper.Adress> lstTinh = _Helper.getListProvince();
        private List<_Helper.Adress> lstQuanHuyen;
        private List<_Helper.Adress> lstPhuongXa;

        public F_HoSo(HoSoDuTuyen hoSo)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - Height;
            Location = new Point(0, y);
            TopMost = true;
            MinimumSize = MaximumSize = Size;
            MaximizeBox = false;
            //rdNam.CheckedChanged += RdNam_CheckedChanged;
            txtMaHS.Enabled = false;
            _hoSo = hoSo;
            Text = DanhSach.CurrSettings.TENTRUONG;
            if (!string.IsNullOrEmpty(_hoSo.MaTinh))
            {
                lstQuanHuyen = _Helper.getListDistrict(_hoSo.MaTinh);
                lookQuanHuyen.Properties.DataSource = lstQuanHuyen;
            }

            if (!string.IsNullOrEmpty(_hoSo.MaHuyen))
            {
                lstPhuongXa = _Helper.getListWards(_hoSo.MaHuyen);
                lookXa.Properties.DataSource = lstPhuongXa;
            }
        }

        private void F_HoSo_Load(object sender, EventArgs e)
        {
            _sourceHS = new BindingSource();
            _sourceNV = new BindingSource();

            _sourceHS.DataSource = _hoSo;
            _sourceNV.DataSource = _hoSo.DsNguyenVong;
            gridControl1.DataSource = _sourceNV;

            InitLookupEdits();
            LoadComboBox();
            CreateBinding();
            InitGridView();
            TxtTenAutoComplete();
            Shown += F_HoSo_Shown;
        }

        private void F_HoSo_Shown(object? sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            btnSaveAndClose.ItemClick += btnSaveCloseHS_Click;
            btnSaveAndNew.ItemClick += btnSaveNewHS_Click;
            txtHo.TextChanged += TxtHo_TextChanged;
            txtTen.TextChanged += TxtTen_TextChanged;
            dtNgaySinh.LostFocus += DtNgaySinh_LostFocus; ;
            lookQuanHuyen.TextChanged += LookQuanHuyen_TextChanged;
            lookTinh.TextChanged += LookTinh_TextChanged;
            lookTruong.EditValueChanged += lookTruong_EditValueChanged;
            txtDiaChi.ButtonClick += TxtDiaChi_ButtonClick;
            txtThonDuong.TextChanged += txtThonDuong_TextChanged;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
        }

        private void DtNgaySinh_LostFocus(object? sender, EventArgs e)
        {
            if (dtNgaySinh.EditValue != null)
            {
                DateTime ns = DateTime.Parse(dtNgaySinh.EditValue.ToString());
                if (DateTime.Now.Year - ns.Year < 15)
                {
                    XtraMessageBox.Show("Tuổi phải >= 15");
                    dtNgaySinh.EditValue = dtNgaySinh.OldEditValue;
                    dtNgaySinh.Focus();
                }
            }
        }

        private void TxtTenAutoComplete()
        {
            var collection = new AutoCompleteStringCollection();
            collection.AddRange(DanhSach.DSHoSoDT.Select(x => x.Ten).Distinct().ToArray());
            txtTen.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteMode =
                TextEditAutoCompleteMode.SuggestAppend;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteSource =
                AutoCompleteSource.CustomSource;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteCustomSource = collection;
        }

        private void LookQuanHuyen_TextChanged(object? sender, EventArgs e)
        {
            if (lookQuanHuyen.EditValue != null)
            {
                lstPhuongXa = _Helper.getListWards(lookQuanHuyen.EditValue.ToString());
                lookXa.Properties.DataSource = lstPhuongXa;
                lookXa.EditValue = null;
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
            }
        }

        private void TxtDiaChi_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CapNhatDiaChi();
        }

        private void GridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_hoSo.DsNguyenVong.Count >= 1 && e.Column.FieldName.Equals("IdNghe"))
            {
                var nv = _hoSo.DsNguyenVong.First();
                if (nv.NV != 1 || nv.IdNghe == null)
                    return;
                var nv1 = DanhSach.DsNghe.FirstOrDefault(x => x.Id == nv.IdNghe);
                string manghe = "";
                if (nv1 != null)
                    manghe = nv1.Ma2;
                else
                    return;
                int maxCount = DanhSach.DSHoSoDT.Where(x => x.DotTS == _hoSo.DotTS && x.MaHoSo.Substring(4, 2).Equals(manghe)).Count();
                txtMaHS.Text = $"{_hoSo.NamTS}{manghe}{(maxCount + 1).ToString("D3")}";
            }
        }

        private void TxtTen_TextChanged(object? sender, EventArgs e)
        {
            string word = txtTen.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtTen.Text = word.ToTitleCase();
        }

        private void TxtHo_TextChanged(object? sender, EventArgs e)
        {
            string word = txtHo.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtHo.Text = word.ToTitleCase();
        }

        private void InitGridView()
        {
            gridView1.IndicatorWidth = 35;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.HideFindPanel();
            gridView1.CellValueChanged += GridView_CellValueChanged;
            gridView1.ShowingEditor += GridView_ShowingEditor;
            gridView1.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

            gridView1.Columns.ColumnByFieldName("IdNghe").ColumnEdit = new RepositoryItemLookUpEdit()
            {
                DataSource = DanhSach.DsNghe,
                DisplayMember = "Ten",
                ValueMember = "Id",
                NullText = "(Trống)",
            };
        }

        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString("D");
        }

        private void GridView_ShowingEditor(object? sender, CancelEventArgs e)
        {
            e.Cancel = EditMode ^ (gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
        }

        private void GridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            gridView1.CellValueChanged -= GridView_CellValueChanged;
            if (e.Column.FieldName.Equals("IdNghe"))
            {
                string? value = e.Value.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (CheckDuplicateId(value))
                    {
                        XtraMessageBox.Show(this, "Đã tồn tại nghề");
                        gridView1.SetFocusedRowCellValue("IdNghe", string.Empty);
                    }
                    int SLdutuyen = DanhSach.DSHoSoDT.Where(hs => hs.NamTS == _hoSo.NamTS &&
                    hs.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(value)) is not null).Count();
                    ChiTieuXetTuyen? chitieu = DanhSach.DsChiTieu.FirstOrDefault(x => x.Nam == _hoSo.NamTS && x.IdNghe == value);
                    if (chitieu is null)
                    {
                        XtraMessageBox.Show(this, "Chưa lập chỉ tiêu ngành nghề này!");
                        gridView1.SetFocusedRowCellValue("IdNghe", string.Empty);
                        return;
                    }
                    int sl = chitieu.ChiTieu;
                    int chitieutoida = int.Parse((sl + sl * DanhSach.CurrSettings.CHITIEUVUOTMUC).ToString());
                    int sltt = DanhSach.DSHoSoTT.Where(x => x.IdNgheTrungTuyen.Equals(value)).Count();
                    if (SLdutuyen + sltt >= sl + (sl * DanhSach.CurrSettings.CHITIEUVUOTMUC))
                    {
                        XtraMessageBox.Show(this, $"Đã vượt mức chỉ tiêu tối đa!\n" +
                            $" SL xét tuyển: {SLdutuyen} SL trúng tuyển: {sltt} Chỉ tiêu tối đa: {chitieutoida}");
                        gridView1.SetFocusedRowCellValue("IdNghe", string.Empty);
                        txtMaHS.Text = string.Empty;
                    }
                }
            }
            else if (e.Column.FieldName.Equals("NV"))
            {
                int value;
                if (!int.TryParse(e.Value.ToString(), out value))
                    return;
                if (CheckDuplicateNV(value))
                {
                    XtraMessageBox.Show(this, "Dã tồn tại nguyện vọng");
                    gridView1.SetFocusedRowCellValue("NV", string.Empty);
                }
            }
            gridView1.CellValueChanged += GridView_CellValueChanged;
        }

        private bool CheckDuplicateId(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return _hoSo.DsNguyenVong.Count(x => x.IdNghe.Equals(value)) >= 2;
        }

        private bool CheckDuplicateNV(int NV)
        {
            if (NV <= 0)
            {
                return false;
            }

            return _hoSo.DsNguyenVong.Where(x => x.NV.Equals(NV)).Count() >= 2;
        }

        private void CreateBinding()
        {
            //Hồ sơ
            txtMaHS.DataBindings.Clear();
            txtMaHS.DataBindings.Add("Text", _sourceHS, "MaHoSo", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHo.DataBindings.Clear();
            txtHo.DataBindings.Add("Text", _sourceHS, "Ho", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTen.DataBindings.Clear();
            txtTen.DataBindings.Add("Text", _sourceHS, "Ten", true, DataSourceUpdateMode.OnPropertyChanged);
            dtNgaySinh.DataBindings.Clear();
            dtNgaySinh.DataBindings.Add("EditValue", _sourceHS, "NgaySinh", true, DataSourceUpdateMode.OnPropertyChanged);
            rdGioiTinh.DataBindings.Clear();
            rdGioiTinh.DataBindings.Add("EditValue", _sourceHS, "GioiTinh", true, DataSourceUpdateMode.OnPropertyChanged);
            dtNgaySinh.DataBindings.Clear();
            dtNgaySinh.DataBindings.Add("EditValue", _sourceHS, "NgaySinh", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbNoiSinh.DataBindings.Clear();
            cbbNoiSinh.DataBindings.Add("Text", _sourceHS, "NoiSinh", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCCCD.DataBindings.Clear();
            txtCCCD.DataBindings.Add("Text", _sourceHS, "CCCD", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTenCha.DataBindings.Clear();
            txtTenCha.DataBindings.Add("Text", _sourceHS, "HoTenCha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNNCha.DataBindings.Clear();
            txtNNCha.DataBindings.Add("Text", _sourceHS, "NgheNghiepCha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTenMe.DataBindings.Clear();
            txtTenMe.DataBindings.Add("Text", _sourceHS, "HoTenMe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNNMe.DataBindings.Clear();
            txtNNMe.DataBindings.Add("Text", _sourceHS, "NgheNghiepMe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtThonDuong.DataBindings.Clear();
            txtThonDuong.DataBindings.Add("Text", _sourceHS, "ThonDuong", true, DataSourceUpdateMode.OnPropertyChanged);
            txtLop.DataBindings.Clear();
            txtLop.DataBindings.Add("Text", _sourceHS, "Lop", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", _sourceHS, "DiaChi", true, DataSourceUpdateMode.OnPropertyChanged);
            lookTinh.DataBindings.Clear();
            lookTinh.DataBindings.Add("EditValue", _sourceHS, "MaTinh", true, DataSourceUpdateMode.OnPropertyChanged);
            lookQuanHuyen.DataBindings.Clear();
            lookQuanHuyen.DataBindings.Add("EditValue", _sourceHS, "MaHuyen", true, DataSourceUpdateMode.OnPropertyChanged);
            lookXa.DataBindings.Clear();
            lookXa.DataBindings.Add("EditValue", _sourceHS, "MaXa", true, DataSourceUpdateMode.OnPropertyChanged);
            lookDanToc.DataBindings.Clear();
            lookDanToc.DataBindings.Add("EditValue", _sourceHS, "IdDanToc", true, DataSourceUpdateMode.OnPropertyChanged);
            lookTonGiao.DataBindings.Clear();
            lookTonGiao.DataBindings.Add("EditValue", _sourceHS, "IdTonGiao", true, DataSourceUpdateMode.OnPropertyChanged);
            lookQuocTich.DataBindings.Clear();
            lookQuocTich.DataBindings.Add("EditValue", _sourceHS, "IdQuocTich", true, DataSourceUpdateMode.OnPropertyChanged);
            lookTDVH.DataBindings.Clear();
            lookTDVH.DataBindings.Add("EditValue", _sourceHS, "IdTrinhDoVH", true, DataSourceUpdateMode.OnPropertyChanged);
            lookTruong.DataBindings.Clear();
            lookTruong.DataBindings.Add("EditValue", _sourceHS, "IdTruong", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", _sourceHS, "SDT", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("Text", _sourceHS, "Email", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbHTDT.DataBindings.Clear();
            cbbHTDT.DataBindings.Add("SelectedItem", _sourceHS, "HTDT", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHSGhiChu.DataBindings.Clear();
            txtHSGhiChu.DataBindings.Add("Text", _sourceHS, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);

            //Kiểm tra hồ sơ
            chkPhieuDKDT.DataBindings.Clear();
            chkPhieuDKDT.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.PhieuDKDT", true, DataSourceUpdateMode.OnPropertyChanged);
            chkHocBa.DataBindings.Clear();
            chkHocBa.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.HocBa", true, DataSourceUpdateMode.OnPropertyChanged);
            chkCNTN.DataBindings.Clear();
            chkCNTN.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GCNTT", true, DataSourceUpdateMode.OnPropertyChanged);
            chkBangTN.DataBindings.Clear();
            chkBangTN.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.BangTN", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAnh.DataBindings.Clear();
            chkAnh.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.HinhThe", true, DataSourceUpdateMode.OnPropertyChanged);
            chkGKS.DataBindings.Clear();
            chkGKS.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GiayKhaiSinh", true, DataSourceUpdateMode.OnPropertyChanged);
            chkCNUT.DataBindings.Clear();
            chkCNUT.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GiayCNUT", true, DataSourceUpdateMode.OnPropertyChanged);
            chkGKSK.DataBindings.Clear();
            chkGKSK.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GKSK", true, DataSourceUpdateMode.OnPropertyChanged);
            txtGhiChu.DataBindings.Clear();
            txtGhiChu.DataBindings.Add("Text", _sourceHS, "KiemTraHS.GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
            //Điểm xét tuyển
            cbbHanhKiem.DataBindings.Clear();
            cbbHanhKiem.DataBindings.Add("SelectedItem", _sourceHS, "HanhKiem", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbXLHT.DataBindings.Clear();
            cbbXLHT.DataBindings.Add("SelectedItem", _sourceHS, "XLHocTap", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbXLTN.DataBindings.Clear();
            cbbXLTN.DataBindings.Add("SelectedItem", _sourceHS, "XLTN", true, DataSourceUpdateMode.OnPropertyChanged);
            lookDTUT.DataBindings.Clear();
            lookDTUT.DataBindings.Add("EditValue", _sourceHS, "IdDTUT", true, DataSourceUpdateMode.OnPropertyChanged);
            lookKVUT.DataBindings.Clear();
            lookKVUT.DataBindings.Add("EditValue", _sourceHS, "IdKVUT", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void InitLookupEdits()
        {
            _Helper.InitSearchLookupEdit(lookDanToc, "Ten", "Id", DanhSach.DsDanToc);
            _Helper.InitSearchLookupEdit(lookQuocTich, "Ten", "Id", DanhSach.DsQuocTich);
            _Helper.InitSearchLookupEdit(lookTonGiao, "Ten", "Id", DanhSach.DsTonGiao);
            _Helper.InitSearchLookupEdit(lookTDVH, "Ten", "Id", DanhSach.DsTrinhDo);
            _Helper.InitSearchLookupEdit(lookDTUT, "Ten", "Id", DanhSach.DsDoiTuongUT);
            _Helper.InitSearchLookupEdit(lookKVUT, "Ten", "Id", DanhSach.DsKhuVucUT);
            _Helper.InitSearchLookupEdit(lookTruong, "Ten", "Id", DanhSach.DsTruong);
            _Helper.InitSearchLookupEdit(lookTinh, "AdressName", "AdressCode", lstTinh);
            _Helper.InitSearchLookupEdit<_Helper.Adress>(lookQuanHuyen, "AdressName", "AdressCode");
            _Helper.InitSearchLookupEdit<_Helper.Adress>(lookXa, "AdressName", "AdressCode");
        }

        private void LoadComboBox()
        {
            string[] lstHanhKiem = { "Trung bình", "Khá", "Tốt" };
            string[] lstXepLoaiHocTap = { "Yếu", "Trung bình", "Khá", "Giỏi" };
            string[] lstXepLoaiTotNghiep = { "Trung bình", "Khá", "Giỏi" };
            string[] lstHTDT = { "Chính quy", "Đào tạo kỹ thuật an toàn lao động", "Đào tạo huấn luyện kỹ năng", "Đào tạo Và nâng cao trình độ chuyên môn kỹ thuật", "Đào tạo thường xuyên", "Đào tạo từ xa, tự học có hướng dẫn", "Vừa làm vừa học" };

            _Helper.InitComboboxEdit(cbbHanhKiem, lstHanhKiem);
            _Helper.InitComboboxEdit(cbbXLHT, lstXepLoaiHocTap);
            _Helper.InitComboboxEdit(cbbXLTN, lstXepLoaiTotNghiep);
            _Helper.InitComboboxEdit(cbbHTDT, lstHTDT);
            _Helper.InitComboboxEdit(cbbNoiSinh, lstTinh.Select(x => x.AdressName).ToArray(), "", true, true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (EditMode)
                EditMode = !EditMode;
            gridView1.AddNewRow();
            int nvmax = _hoSo.DsNguyenVong.Count();
            gridView1.SetFocusedRowCellValue("NV", nvmax);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Text = EditMode ? "Lưu" : "Sửa";
            EditMode = !EditMode;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = gridView1.GetFocusedRow();
            if (r is not null)
            {
                if (XtraMessageBox.Show(this, "Xác nhận xóa?", "Xóa", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _sourceNV.Remove(r);
                }
            }
        }

        private void btnSaveNewHS_Click(object sender, EventArgs e)
        {
            string errs = _hoSo.CheckError();
            if (string.IsNullOrEmpty(errs))
            {
                var index = DanhSach.DSHoSoDT.FindIndex(x => x.Id.Equals(_hoSo.Id));
                if (index >= 0)
                {
                    DanhSach.DSHoSoDT[index] = _hoSo;
                }
                else
                {
                    DanhSach.DSHoSoDT.Add(_hoSo);
                }
                DanhSach.SaveDS(TuDien.CategoryName.HoSoDuTuyen);
                int dts = _hoSo.DotTS, nts = _hoSo.NamTS;
                var dsdt = DanhSach.DSHoSoDT.Where(x => x.NamTS == nts && x.DotTS == dts);
                var dt = DanhSach.DsDanToc.FirstOrDefault();
                var qt = DanhSach.DsQuocTich.FirstOrDefault();
                var tg = DanhSach.DsTonGiao.FirstOrDefault();
                var tdvh = DanhSach.DsTrinhDo.FirstOrDefault();
                _hoSo = new HoSoDuTuyen
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
                };
                F_HoSo_Load(null, null);
            }
            else { MessageBox.Show(this, errs, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSaveCloseHS_Click(object sender, EventArgs e)
        {
            string errs = _hoSo.CheckError();
            if (string.IsNullOrEmpty(errs))
            {
                var index = DanhSach.DSHoSoDT.FindIndex(x => x.Id.Equals(_hoSo.Id));
                if (index >= 0)
                {
                    DanhSach.DSHoSoDT[index] = _hoSo;
                }
                else
                {
                    DanhSach.DSHoSoDT.Add(_hoSo);
                }
                DanhSach.SaveDS(TuDien.CategoryName.HoSoDuTuyen);
                Close();
            }
            else { XtraMessageBox.Show(this, errs, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lookTruong_EditValueChanged(object sender, EventArgs e)
        {
            var truong = DanhSach.DsTruong.FirstOrDefault(x => x.Id.Equals(lookTruong.EditValue.ToString()));
            if (truong is null)
            {
                return;
            }
            _hoSo.TDHV = truong.LoaiTruong;
        }

        private void CapNhatDiaChi()
        {
            if (lstQuanHuyen == null || lstPhuongXa == null)
                return;
            Diachi = string.Empty;

            string maXa = lookXa.EditValue.ToString();
            if (!string.IsNullOrEmpty(maXa))
            {
                string tenXa = lstPhuongXa.First(x => x.AdressCode == lookXa.EditValue.ToString()).AdressName;
                string tenHuyen = lstQuanHuyen.First(x => x.AdressCode == lookQuanHuyen.EditValue.ToString()).AdressName;
                string tenTinh = lstTinh.First(x => x.AdressCode == lookTinh.EditValue.ToString()).AdressName;
                Diachi += $"{txtThonDuong.Text}, {tenXa}, {tenHuyen}, {tenTinh}";
            }

            txtDiaChi.Text = Diachi;
        }

        private void txtThonDuong_TextChanged(object sender, EventArgs e)
        {
            string word = txtThonDuong.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtThonDuong.Text = word.ToTitleCase();
        }
    }
}