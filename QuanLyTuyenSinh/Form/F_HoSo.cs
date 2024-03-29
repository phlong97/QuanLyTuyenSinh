﻿using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LiteDB;
using QuanLyTuyenSinh.Models;
using System.ComponentModel;
using System.Data;
using System.IO;
using Image = System.Drawing.Image;

namespace QuanLyTuyenSinh.Form
{
    public partial class F_HoSo : DirectXForm
    {
        private HoSoDuTuyenTC _hoSo;
        private BindingSource _sourceHS;
        private BindingSource _sourceNV;

        private bool ImportMode = false;
        private bool EditMode;
        private string Diachi = string.Empty;
        private List<_Helper.Address> lstTinh = _Helper.getListProvince();
        private List<_Helper.Address> lstQuanHuyen;
        private List<_Helper.Address> lstPhuongXa;

        public F_HoSo(HoSoDuTuyenTC hoSo, bool ImportMode = false)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - Height;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Location = new Point(0, y);
            TopMost = true;
            MinimumSize = MaximumSize = Size;
            MaximizeBox = false;

            _hoSo = hoSo;
            Text = DataHelper.CurrSettings.TENTRUONG;
            HeaderText.Caption = $"Hồ sơ dự tuyển - Đợt {_hoSo.DotTS}";
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
            if (ImportMode) SetReadOnly();
        }
        private void SetReadOnly()
        {
            ImportMode = true;
            btnSaveAndNew.Enabled = false;
            Anh.Enabled = false;
        }
        private void F_HoSo_Load(object? sender, EventArgs e)
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
            TxtHoTenAutoComplete();
            LoadAnh();
            Shown += F_HoSo_Shown;
        }

        private void F_HoSo_Shown(object? sender, EventArgs e)
        {
            Thread.Sleep(100);
            btnSaveAndClose.ItemClick += btnSaveCloseHS_Click;
            btnSaveAndNew.ItemClick += btnSaveNewHS_Click;
            btnCreateGDTX.Click += BtnCreateGDTX_Click;
            txtHo.TextChanged += TxtHo_TextChanged;
            txtTen.TextChanged += TxtTen_TextChanged;
            txtTenCha.TextChanged += TxtTenCha_TextChanged;
            txtTenMe.TextChanged += TxtTenMe_TextChanged;
            dtNgaySinh.LostFocus += DtNgaySinh_LostFocus; ;
            lookQuanHuyen.TextChanged += LookQuanHuyen_TextChanged;
            lookTinh.TextChanged += LookTinh_TextChanged;
            lookTruong.EditValueChanged += lookTruong_EditValueChanged;
            txtDiaChi.ButtonClick += TxtDiaChi_ButtonClick;
            txtMaHS.ButtonClick += TxtMaHS_ButtonClick;
            txtThonDuong.TextChanged += txtThonDuong_TextChanged;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            Anh.ContextButtonClick += Anh_ContextButtonClick;
            gridView1.CellValueChanging += GridView1_CellValueChanging;
            FormClosing += HS_FormClosing;
            ActiveControl = txtHo;
        }

        private void BtnCreateGDTX_Click(object? sender, EventArgs e)
        {
            string errs = string.Empty;
            if (string.IsNullOrEmpty(_hoSo.Ho))
                errs += "Chưa nhập họ học sinh \n";
            if (string.IsNullOrEmpty(_hoSo.Ten))
                errs += "Chưa nhập tên học sinh \n";
            if (!string.IsNullOrEmpty(_hoSo.CCCD) || !string.IsNullOrWhiteSpace(_hoSo.CCCD))
            {
                if (_hoSo.CCCD.Length > 0 && _hoSo.CCCD.Length == 11)
                    errs += "Nhập sai CCCD/CMND! \n";
                if (DataHelper.DSHoSoXTTC.Count(x => x.CCCD == _hoSo.CCCD) >= 2)
                    errs += "Đã tồn tại CCCD/CMND!\n";
            }
            if (_hoSo.NgaySinh == DateTime.MinValue)
                errs += "Chưa nhập ngày sinh\n";
            if (string.IsNullOrEmpty(_hoSo.NoiSinh))
                errs += "Chưa nhập nơi sinh\n";
            if (string.IsNullOrEmpty(_hoSo.DiaChi))
                errs += "Chưa nhập địa chỉ\n";
            if (string.IsNullOrEmpty(_hoSo.MaTinh))
                errs += "Chưa chọn tỉnh\n";
            if (string.IsNullOrEmpty(_hoSo.MaHuyen))
                errs += "Chưa chọn quận/huyện\n";
            if (string.IsNullOrEmpty(_hoSo.MaXa))
                errs += "Chưa chọn phường/xã\n";
            if (string.IsNullOrEmpty(_hoSo.IdDanToc))
                errs += "Chưa chọn dân tộc\n";
            if (string.IsNullOrEmpty(_hoSo.IdTonGiao))
                errs += "Chưa chọn tôn giáo\n";
            if (string.IsNullOrEmpty(_hoSo.IdQuocTich))
                errs += "Chưa chọn quốc tịch\n";
            if (string.IsNullOrEmpty(_hoSo.IdTrinhDoVH))
                errs += "Chưa chọn trình độ văn hóa\n";
            if (string.IsNullOrEmpty(_hoSo.IdTruong))
                errs += "Chưa chọn trường\n";
            else
            {
                var truong = DataHelper.DsTruong.FirstOrDefault(x => x.Id == _hoSo.IdTruong);
                if ((truong != null) && truong.LoaiTruong.Equals("THPT"))
                    errs += "Phải là trường THCS";
                else if (truong == null) errs += "Trường không tồn tại";
            }
            if (!_hoSo.TDHV.Equals("THCS"))
                errs += "Trình độ học vấn phải là THCS\n";

            if (!string.IsNullOrEmpty(errs))
            {
                XtraMessageBox.Show(this, errs, "Chưa đủ thông tin!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoSoDuTuyenGDTX hsgdtx = _hoSo.ToHoSoGDTX();
            if (hsgdtx != null)
            {
                F_HoSo_TX f = new(hsgdtx.CloneJson(),IdHSTC: _hoSo.Id);
                f.Show();
            }
        }

        private void GridView1_CellValueChanging(object? sender, CellValueChangedEventArgs e)
        {
            string? value = e.Value.ToString();
            gridView1.CellValueChanged -= GridView1_CellValueChanging;
            if (e.Column.FieldName.Equals("IdNghe"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (_hoSo.DsNguyenVong.FirstOrDefault(x => x.IdNghe == value) is not null)
                    {
                        XtraMessageBox.Show(this, "Đã tồn tại nguyện vọng này");
                        gridView1.SetFocusedRowCellValue("IdNghe", null);
                    }
                }
            }
            gridView1.CellValueChanging += GridView1_CellValueChanging;
        }

        private void Anh_ContextButtonClick(object? sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            Anh.LoadImage();
        }

        private void LoadAnh()
        {
            if (!string.IsNullOrEmpty(_hoSo.Anh))
            {
                try
                {
                    using (var db = _LiteDb.GetDatabase())
                    {
                        var fileInfo = db.FileStorage.FindById($@"$/photos/{_hoSo.Id}");
                        if (fileInfo != null)
                        {
                            var stream = new MemoryStream();
                            db.FileStorage.Download($@"$/photos/{_hoSo.Id}", stream);
                            Image img = Image.FromStream(stream);
                            Anh.EditValue = img;
                        }
                        else
                        {
                            _hoSo.Anh = string.Empty;
                        }
                    }
                }
                catch { }
            }
            else
            {
                Anh.Image = null;
                Anh.EditValue = null;
            }

        }

        private void SaveAnh()
        {
            if ((Anh.Image == null) && !string.IsNullOrEmpty(_hoSo.Anh))
            {
                using (var db = _LiteDb.GetDatabase())
                {
                    var fileInfo = db.FileStorage.FindById($@"$/photos/{_hoSo.Id}");
                    if (fileInfo != null)
                    {
                        db.FileStorage.Delete($@"$/photos/{_hoSo.Id}");
                        _hoSo.Anh = string.Empty;
                    }
                }
                return;
            }
            else if (!string.IsNullOrEmpty(Anh.GetLoadedImageLocation()) && Anh.Image != null)
            {
                using (var db = _LiteDb.GetDatabase())
                {
                    var fileInfo = db.FileStorage.FindById($@"$/photos/{_hoSo.Id}");
                    if (fileInfo != null)
                    {
                        db.FileStorage.Delete($@"$/photos/{_hoSo.Id}");
                    }
                    db.FileStorage.Upload($@"$/photos/{_hoSo.Id}", Anh.GetLoadedImageLocation());
                    _hoSo.Anh = $@"$/photos/{_hoSo.Id}";
                }
            }
        }

        private void HS_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (ImportMode)
                MainWorkspace.FormUploadHS.RefreshData();
            else
            {
                MainWorkspace.FormMain.RefreshData();
            }
        }

        private void TxtMaHS_ButtonClick(object? sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (_hoSo.DsNguyenVong.Count >= 1)
            {
                var nv = _hoSo.DsNguyenVong.FirstOrDefault(x => x.NV == 1);
                if (nv == null) return;
                if (string.IsNullOrEmpty(nv.IdNghe))
                    return;

                var nv1 = DataHelper.DsNghe.FirstOrDefault(x => x.Id == nv.IdNghe);
                if (nv1 == null)
                    return;
                string manghe = nv1.Ma2;
                var max = DataHelper.DSHoSoXTTC.Where(x => x.DotTS == _hoSo.DotTS && x.MaHoSo.Substring(4, 2).Equals(manghe)).OrderByDescending(x => x.MaHoSo).FirstOrDefault();
                if (max == null) txtMaHS.Text = $"{_hoSo.NamTS}{manghe}001";
                else
                {
                    int maxstt = int.Parse(max.MaHoSo.Substring(6, 3)) + 1;
                    txtMaHS.Text = $"{_hoSo.NamTS}{manghe}{maxstt.ToString("D3")}";
                }
            }
        }

        private void DtNgaySinh_LostFocus(object? sender, EventArgs e)
        {
            if (dtNgaySinh.EditValue != null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                DateTime ns = DateTime.Parse(dtNgaySinh.EditValue.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
                if (DateTime.Now.Year - ns.Year < 15)
                {
                    XtraMessageBox.Show("Tuổi phải >= 15");
                    dtNgaySinh.EditValue = dtNgaySinh.OldEditValue;
                    dtNgaySinh.Focus();
                }
            }
        }

        private void TxtHoTenAutoComplete()
        {
            var collection = new AutoCompleteStringCollection();
            collection.AddRange(DataHelper.DSHoSoXTTC.Where(x => x.DotTS == _hoSo.DotTS).Select(x => x.Ten).Distinct().ToArray());
            txtTen.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteMode =
                TextEditAutoCompleteMode.SuggestAppend;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteSource =
                AutoCompleteSource.CustomSource;
            txtTen.Properties.AdvancedModeOptions.AutoCompleteCustomSource = collection;

            var collection2 = new AutoCompleteStringCollection();
            collection2.AddRange(DataHelper.DSHoSoXTTC.Where(x => x.DotTS == _hoSo.DotTS).Select(x => x.Ho).Distinct().ToArray());
            txtHo.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            txtHo.Properties.AdvancedModeOptions.AutoCompleteMode =
                TextEditAutoCompleteMode.SuggestAppend;
            txtHo.Properties.AdvancedModeOptions.AutoCompleteSource =
                AutoCompleteSource.CustomSource;
            txtHo.Properties.AdvancedModeOptions.AutoCompleteCustomSource = collection2;
        }

        private void LookQuanHuyen_TextChanged(object? sender, EventArgs e)
        {
            if (lookQuanHuyen.EditValue != null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                lstPhuongXa = _Helper.getListWards(lookQuanHuyen.EditValue.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
            }
            lookXa.Properties.DataSource = lstPhuongXa;
            lookXa.EditValue = null;


        }

        private void LookTinh_TextChanged(object? sender, EventArgs e)
        {
            if (lookTinh.EditValue != null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                lstQuanHuyen = _Helper.getListDistrict(lookTinh.EditValue.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
            }
            lookQuanHuyen.Properties.DataSource = lstQuanHuyen;
            lookXa.EditValue = null;
            lookQuanHuyen.EditValue = null;
        }

        private void TxtDiaChi_ButtonClick(object? sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CapNhatDiaChi();
        }

        private void TxtTen_TextChanged(object? sender, EventArgs e)
        {
            string word = txtTen.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtTen.EditValue = word.ToTitleCase();
        }

        private void TxtHo_TextChanged(object? sender, EventArgs e)
        {
            string word = txtHo.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtHo.EditValue = word.ToTitleCase();
        }
        private void TxtTenMe_TextChanged(object? sender, EventArgs e)
        {
            string word = txtTenMe.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtTenMe.EditValue = word.ToTitleCase();
        }

        private void TxtTenCha_TextChanged(object? sender, EventArgs e)
        {
            string word = txtTenCha.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtTenCha.EditValue = word.ToTitleCase();
        }

        private void InitGridView()
        {
            gridView1.IndicatorWidth = 35;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.HideFindPanel();
            gridView1.ShowingEditor += GridView_ShowingEditor;
            gridView1.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

            DevForm.CreateRepositoryItemLookUpEdit(gridView1, DataHelper.DsNghe, "IdNghe", "Ten", "Id");
        }

        private void GridView_CustomDrawRowIndicator(object? sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString("D");
        }

        private void GridView_ShowingEditor(object? sender, CancelEventArgs e)
        {
            e.Cancel = EditMode ^ (gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
        }

        private void CreateBinding()
        {
            //Hồ sơ
            txtMaHS.DataBindings.Clear();
            txtMaHS.DataBindings.Add("Text", _sourceHS, "MaHoSo", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHo.DataBindings.Clear();
            txtHo.DataBindings.Add("EditValue", _sourceHS, "Ho", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTen.DataBindings.Clear();
            txtTen.DataBindings.Add("EditValue", _sourceHS, "Ten", true, DataSourceUpdateMode.OnPropertyChanged);
            dtNgaySinh.DataBindings.Clear();
            dtNgaySinh.DataBindings.Add("EditValue", _sourceHS, "NgaySinh", true, DataSourceUpdateMode.OnPropertyChanged);
            rdGioiTinh.DataBindings.Clear();
            rdGioiTinh.DataBindings.Add("EditValue", _sourceHS, "GioiTinh", true, DataSourceUpdateMode.OnPropertyChanged);
            dtNgaySinh.DataBindings.Clear();
            dtNgaySinh.DataBindings.Add("EditValue", _sourceHS, "NgaySinh", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbNoiSinh.DataBindings.Clear();
            cbbNoiSinh.DataBindings.Add("EditValue", _sourceHS, "NoiSinh", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCCCD.DataBindings.Clear();
            txtCCCD.DataBindings.Add("Text", _sourceHS, "CCCD", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTenCha.DataBindings.Clear();
            txtTenCha.DataBindings.Add("EditValue", _sourceHS, "HoTenCha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNNCha.DataBindings.Clear();
            txtNNCha.DataBindings.Add("Text", _sourceHS, "NgheNghiepCha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTenMe.DataBindings.Clear();
            txtTenMe.DataBindings.Add("EditValue", _sourceHS, "HoTenMe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNNMe.DataBindings.Clear();
            txtNNMe.DataBindings.Add("Text", _sourceHS, "NgheNghiepMe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtThonDuong.DataBindings.Clear();
            txtThonDuong.DataBindings.Add("EditValue", _sourceHS, "ThonDuong", true, DataSourceUpdateMode.OnPropertyChanged);
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
            cbbHTDT.DataBindings.Add("EditValue", _sourceHS, "HTDT", true, DataSourceUpdateMode.OnPropertyChanged);
            txtHSGhiChu.DataBindings.Clear();
            txtHSGhiChu.DataBindings.Add("Text", _sourceHS, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNSCha.DataBindings.Clear();
            txtNSCha.DataBindings.Add("Text", _sourceHS, "NamSinhCha", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNSMe.DataBindings.Clear();
            txtNSMe.DataBindings.Add("Text", _sourceHS, "NamSinhMe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNamTN.DataBindings.Clear();
            txtNamTN.DataBindings.Add("Text", _sourceHS, "NamTN", true, DataSourceUpdateMode.OnPropertyChanged);
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
            chkCCCD.DataBindings.Clear();
            chkCCCD.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.CCCD", true, DataSourceUpdateMode.OnPropertyChanged);
            chkGKS.DataBindings.Clear();
            chkGKS.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GiayKhaiSinh", true, DataSourceUpdateMode.OnPropertyChanged);
            chkCNUT.DataBindings.Clear();
            chkCNUT.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GiayCNUT", true, DataSourceUpdateMode.OnPropertyChanged);
            chkGKSK.DataBindings.Clear();
            chkGKSK.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.GKSK", true, DataSourceUpdateMode.OnPropertyChanged);
            chkSYLL.DataBindings.Clear();
            chkSYLL.DataBindings.Add("Checked", _sourceHS, "KiemTraHS.SYLL", true, DataSourceUpdateMode.OnPropertyChanged);
            txtGhiChu.DataBindings.Clear();
            txtGhiChu.DataBindings.Add("Text", _sourceHS, "KiemTraHS.GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
            //Điểm xét tuyển
            cbbHanhKiem.DataBindings.Clear();
            cbbHanhKiem.DataBindings.Add("EditValue", _sourceHS, "HanhKiem", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbXLHT.DataBindings.Clear();
            cbbXLHT.DataBindings.Add("EditValue", _sourceHS, "XLHocTap", true, DataSourceUpdateMode.OnPropertyChanged);
            cbbXLTN.DataBindings.Clear();
            cbbXLTN.DataBindings.Add("EditValue", _sourceHS, "XLTN", true, DataSourceUpdateMode.OnPropertyChanged);
            lookDTUT.DataBindings.Clear();
            lookDTUT.DataBindings.Add("EditValue", _sourceHS, "IdDTUT", true, DataSourceUpdateMode.OnPropertyChanged);
            lookKVUT.DataBindings.Clear();
            lookKVUT.DataBindings.Add("EditValue", _sourceHS, "IdKVUT", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void InitLookupEdits()
        {
            DevForm.CreateSearchLookupEdit(lookDanToc, "Ten", "Id", DataHelper.DsDanToc);
            DevForm.CreateSearchLookupEdit(lookQuocTich, "Ten", "Id", DataHelper.DsQuocTich);
            DevForm.CreateSearchLookupEdit(lookTonGiao, "Ten", "Id", DataHelper.DsTonGiao);
            DevForm.CreateSearchLookupEdit(lookTDVH, "Ten", "Id", DataHelper.DsTrinhDo);
            DevForm.CreateSearchLookupEdit(lookDTUT, "Ten", "Id", DataHelper.DsDoiTuongUT.Where(x => x.ApDung.Equals("Trung cấp")).ToList());
            DevForm.CreateSearchLookupEdit(lookKVUT, "Ten", "Id", DataHelper.DsKhuVucUT);
            DevForm.CreateSearchLookupEdit(lookTruong, "Ten", "Id", DataHelper.DsTruong);
            DevForm.CreateSearchLookupEdit(lookTinh, "AddressName", "AddressCode", lstTinh);
            DevForm.CreateSearchLookupEdit(lookQuanHuyen, "AddressName", "AddressCode");
            DevForm.CreateSearchLookupEdit(lookXa, "AddressName", "AddressCode");

        }

        private void LoadComboBox()
        {
            string[] lstHanhKiem = { "Trung bình", "Khá", "Tốt" };
            string[] lstXepLoaiHocTap = { "Trung bình", "Khá", "Giỏi" };
            string[] lstXepLoaiTotNghiep = { "Trung bình", "Khá", "Giỏi" };
            string[] lstHTDT = { "Chính quy", "Đào tạo kỹ thuật an toàn lao động", "Đào tạo huấn luyện kỹ năng", "Đào tạo Và nâng cao trình độ chuyên môn kỹ thuật", "Đào tạo thường xuyên", "Đào tạo từ xa, tự học có hướng dẫn", "Vừa làm vừa học" };

            DevForm.CreateComboboxEdit(cbbHanhKiem, lstHanhKiem);
            DevForm.CreateComboboxEdit(cbbXLHT, lstXepLoaiHocTap);
            DevForm.CreateComboboxEdit(cbbXLTN, lstXepLoaiTotNghiep);
            DevForm.CreateComboboxEdit(cbbHTDT, lstHTDT);
            DevForm.CreateComboboxEdit(cbbNoiSinh, lstTinh.Select(x => x.AddressName.Replace("Tỉnh ", "").Replace("Thành Phố", "TP")).ToArray(), "", true, true);
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (EditMode)
                EditMode = !EditMode;
            gridView1.AddNewRow();
            gridView1.SetFocusedRowCellValue("NV", _hoSo.DsNguyenVong.Count());
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            btnEdit.Text = EditMode ? "Sửa" : "Lưu";
            EditMode = !EditMode;
        }

        private void btnDelete_Click(object? sender, EventArgs e)
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

        private void btnSaveNewHS_Click(object? sender, EventArgs e)
        {
            string errs = _hoSo.CheckError();
            if (string.IsNullOrEmpty(errs))
            {
                var index = DataHelper.DSHoSoXTTC.FindIndex(x => x.Id.Equals(_hoSo.Id));
                if (index >= 0)
                {
                    DataHelper.DSHoSoXTTC[index] = _hoSo;
                }
                else
                {
                    DataHelper.DSHoSoXTTC.Add(_hoSo);
                }
                SaveAnh();
                _hoSo.Save();
                int dts = _hoSo.DotTS, nts = _hoSo.NamTS;
                var dsdt = DataHelper.DSHoSoXTTC.Where(x => x.NamTS == nts && x.DotTS == dts);
                var dt = DataHelper.DsDanToc.FirstOrDefault();
                var qt = DataHelper.DsQuocTich.FirstOrDefault();
                var tg = DataHelper.DsTonGiao.FirstOrDefault();
                var tdvh = DataHelper.DsTrinhDo.FirstOrDefault();
                _hoSo = new HoSoDuTuyenTC
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
                };
                F_HoSo_Load(null, null);
            }
            else { MessageBox.Show(this, errs, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSaveCloseHS_Click(object? sender, EventArgs e)
        {
            string errs = _hoSo.CheckError();
            if (string.IsNullOrEmpty(errs))
            {
                if (ImportMode)
                {

                }
                else
                {
                    SaveAnh();
                    _hoSo.Save();
                }
                Close();
            }
            else { XtraMessageBox.Show(this, errs, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lookTruong_EditValueChanged(object? sender, EventArgs e)
        {
            var truong = DataHelper.DsTruong.FirstOrDefault(x => x.Id.Equals(lookTruong.EditValue.ToString()));
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

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string maXa = lookXa.EditValue.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (!string.IsNullOrEmpty(maXa))
            {
                string tenXa = lstPhuongXa.First(x => x.AddressCode == lookXa.EditValue.ToString()).AddressName;
                string tenHuyen = lstQuanHuyen.First(x => x.AddressCode == lookQuanHuyen.EditValue.ToString()).AddressName;
                string tenTinh = lstTinh.First(x => x.AddressCode == lookTinh.EditValue.ToString()).AddressName;
                Diachi += $"{txtThonDuong.Text}, {tenXa}, {tenHuyen}, {tenTinh}";
            }

            txtDiaChi.Text = Diachi;
        }

        private void txtThonDuong_TextChanged(object? sender, EventArgs e)
        {
            string word = txtThonDuong.Text;
            if (string.IsNullOrEmpty(word))
                return;
            txtThonDuong.EditValue = word.ToTitleCase();
        }
    }
}