using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
    public class HoSoDuTuyenTC : DBClass
    {
        [Display(Name = "Mã hồ sơ")]
        public string MaHoSo { get; set; }

        [Display(Name = "Họ học sinh")]
        public string Ho { get; set; }

        [Display(Name = "Tên học sinh")]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } = true;// 0: Nữ 1: Nam

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(AutoGenerateField = false)]
        public string ThonDuong { get; set; }

        [Display(AutoGenerateField = false)]
        public string NoiSinh { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaTinh { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaHuyen { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaXa { get; set; }

        [Display(AutoGenerateField = false)]
        public string CCCD { get; set; } = string.Empty;

        [Display(AutoGenerateField = false)]
        public string IdDanToc { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdTonGiao { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdQuocTich { get; set; }

        [Display(AutoGenerateField = false)]
        public string SDT { get; set; }

        [Display(AutoGenerateField = false)]
        public string Email { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdTrinhDoVH { get; set; }

        [Display(AutoGenerateField = false)]
        public string TDHV { get; set; }

        [Display(AutoGenerateField = false)]
        public string Lop { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamTN { get; set; }

        [Display(AutoGenerateField = false)]
        public string HTDT { get; set; }

        [Display(AutoGenerateField = false)]
        public string HoTenCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string NgheNghiepCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamSinhCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string HoTenMe { get; set; }

        [Display(AutoGenerateField = false)]
        public string NgheNghiepMe { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamSinhMe { get; set; }

        [Display(Name = "Hạnh kiểm")]
        public string HanhKiem { get; set; }

        [Display(Name = "Xếp loại HT")]
        public string XLHocTap { get; set; }

        [Display(Name = "Xếp loại TN")]
        public string XLTN { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public string IdDTUT { get; set; }

        [Display(Name = "Khu vực ưu tiên")]
        public string IdKVUT { get; set; }

        [Display(Name = "Trường")]
        public string IdTruong { get; set; }

        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        public int DotTS { get; set; }

        [Display(AutoGenerateField = false)]
        public KiemTraHoSo KiemTraHS { get; set; } = new();

        [Display(AutoGenerateField = false)]
        public List<NguyenVong> DsNguyenVong { get; set; } = new();

        [Display(AutoGenerateField = false)]
        public string Anh { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public string CheckError()
        {
            string errs = string.Empty;
            if (string.IsNullOrEmpty(MaHoSo))
                errs += "Chưa nhập mã hồ sơ\n";
            if (DataHelper.DSHoSoXTTC.Count(x => x.MaHoSo.Equals(MaHoSo) && x.DotTS == DotTS) > 1)
                errs += "Trùng mã hồ sơ\n";
            if (string.IsNullOrEmpty(Ho))
                errs += "Chưa nhập họ học sinh \n";
            if (string.IsNullOrEmpty(Ten))
                errs += "Chưa nhập tên học sinh \n";
            if (!string.IsNullOrEmpty(CCCD) || !string.IsNullOrWhiteSpace(CCCD))
            {
                if (CCCD.Length > 0 && CCCD.Length == 11)
                    errs += "Nhập sai CCCD/CMND! \n";
                if (DataHelper.DSHoSoXTTC.Count(x => x.CCCD == CCCD) >= 2)
                    errs += "Đã tồn tại CCCD/CMND!\n";
            }
            if (NgaySinh == DateTime.MinValue)
                errs += "Chưa nhập ngày sinh\n";
            if (string.IsNullOrEmpty(NoiSinh))
                errs += "Chưa nhập nơi sinh\n";
            if (string.IsNullOrEmpty(DiaChi))
                errs += "Chưa nhập địa chỉ\n";
            if (string.IsNullOrEmpty(MaTinh))
                errs += "Chưa chọn tỉnh\n";
            if (string.IsNullOrEmpty(MaHuyen))
                errs += "Chưa chọn quận/huyện\n";
            if (string.IsNullOrEmpty(MaXa))
                errs += "Chưa chọn phường/xã\n";
            if (string.IsNullOrEmpty(IdDanToc))
                errs += "Chưa chọn dân tộc\n";
            if (string.IsNullOrEmpty(IdTonGiao))
                errs += "Chưa chọn tôn giáo\n";
            if (string.IsNullOrEmpty(IdQuocTich))
                errs += "Chưa chọn quốc tịch\n";
            if (string.IsNullOrEmpty(IdTrinhDoVH))
                errs += "Chưa chọn trình độ văn hóa\n";
            if (string.IsNullOrEmpty(IdTruong))
                errs += "Chưa chọn trường\n";
            if (DsNguyenVong.Count() == 0)
                errs += "Chưa chọn nguyện vọng\n";
            if (DsNguyenVong.Count(x => x.NV == 1) == 0)
                errs += "Chưa chọn nguyện vọng 1\n";
            if (DsNguyenVong.Count(x => x.IdNghe == null) > 0)
                errs += "Chưa chọn nghề cho nguyện vọng\n";
            if (DsNguyenVong.Count(x => x.NV == 1) == 1)
            {
                int sltt = 0;
                var nv1 = DsNguyenVong.First(x => x.NV == 1);
                int sldt = DataHelper.DSHoSoXTTC.Where(x => x.DsNguyenVong.FirstOrDefault(nv => nv.IdNghe == nv1.IdNghe && nv.NV == 1) != null && x.DotTS == DotTS).Count();
                var ctnv1 = DataHelper.DsChiTieu.FirstOrDefault(x => x.IdNghe == nv1.IdNghe);
                if (ctnv1 != null)
                {
                    for (int i = 1; i < DotTS; i++)
                    {
                        sltt += DataHelper.DSHoSoTTTC.Where(x => x.DotTS == i && x.IdNgheTrungTuyen == nv1.IdNghe).Count();
                    }
                    int ctmax = (int)(ctnv1.ChiTieu + ctnv1.ChiTieu * DataHelper.CurrSettings.CHITIEUVUOTMUC);
                    if (sldt + sltt >= ctmax)
                    {
                        errs += $"Nguyện vọng 1 đã vượt chỉ tiêu tối đa! Chỉ tiêu tối đa: {ctmax}\n";
                    }
                }
            }
            return errs;
        }

        public double TinhDiemXT()
        {
            double tong = 0;
            if (TDHV.Equals("THCS"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THCS.TOT; break;
                    case "Khá":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THCS.KHA; break;
                    case "Trung bình":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                    default:
                        tong += DataHelper.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        tong += DataHelper.CurrSettings.XLTN_THCS.GIOI; break;
                    case "Khá":
                        tong += DataHelper.CurrSettings.XLTN_THCS.KHA; break;
                    case "Trung bình":
                        tong += DataHelper.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                    default:
                        tong += DataHelper.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                }
            }
            else if (TDHV.Equals("THPT"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THPT.TOT; break;
                    case "Khá":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THPT.KHA; break;
                    case "Trung bình":
                        tong += DataHelper.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                    default:
                        tong += DataHelper.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                }
                switch (XLHocTap)
                {
                    case "Giỏi":
                        tong += DataHelper.CurrSettings.XLHT_THPT.GIOI; break;
                    case "Khá":
                        tong += DataHelper.CurrSettings.XLHT_THPT.KHA; break;
                    case "Trung bình":
                        tong += DataHelper.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                    default:
                        tong += DataHelper.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        tong += DataHelper.CurrSettings.XLTN_THPT.GIOI; break;
                    case "Khá":
                        tong += DataHelper.CurrSettings.XLTN_THPT.KHA; break;
                    case "Trung bình":
                        tong += DataHelper.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                    default:
                        tong += DataHelper.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                }
            }
            if (!string.IsNullOrEmpty(IdKVUT))
            {
                var kv = DataHelper.DsKhuVucUT.FirstOrDefault(kv => kv.Id.Equals(IdKVUT));
                if (kv != null)
                {
                    tong += kv.Diem;
                }
            }
            if (!string.IsNullOrEmpty(IdDTUT))
            {
                var dt = DataHelper.DsDoiTuongUT.FirstOrDefault(dt => dt.Id.Equals(IdDTUT));
                if (dt != null)
                {
                    tong += dt.Diem;
                }
            }
            return tong;
        }

        public TongHopDiemXetTuyenTC ToTHDXT()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var th = new TongHopDiemXetTuyenTC
            {
                IdHoSo = Id,
                MaHoSo = MaHoSo,
                Ho = Ho,
                Ten = Ten,
                NgaySinh = NgaySinh,
                GT = GioiTinh ? "Nam" : "Nữ",
                DiaChi = DiaChi,
                IdNgheNV1 = DsNguyenVong.First().IdNghe,
                GhiChu = GhiChu,
                IdDTUT = string.IsNullOrEmpty(IdDTUT)
                        ? string.Empty
                        : IdDTUT,
                UTDT =
                    string.IsNullOrEmpty(IdDTUT)
                        ? 0
                        : DataHelper.DsDoiTuongUT.FirstOrDefault(x => x.Id.Equals(IdDTUT)).Diem,
                UTKV =
                    string.IsNullOrEmpty(IdKVUT)
                        ? 0
                        : DataHelper.DsKhuVucUT.FirstOrDefault(x => x.Id.Equals(IdKVUT)).Diem,
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (TDHV.Equals("THCS"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THCS.TOT; break;
                    case "Khá":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THCS.KHA; break;
                    case "Trung bình":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                    default:
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THCS.GIOI; break;
                    case "Khá":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THCS.KHA; break;
                    case "Trung bình":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                    default:
                        th.XLTN = DataHelper.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                }
            }
            else if (TDHV.Equals("THPT"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THPT.TOT; break;
                    case "Khá":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THPT.KHA; break;
                    case "Trung bình":
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                    default:
                        th.HanhKiem = DataHelper.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                }
                switch (XLHocTap)
                {
                    case "Giỏi":
                        th.XLHT = DataHelper.CurrSettings.XLHT_THPT.GIOI; break;
                    case "Khá":
                        th.XLHT = DataHelper.CurrSettings.XLHT_THPT.KHA; break;
                    case "Trung bình":
                        th.XLHT = DataHelper.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                    default:
                        th.XLHT = DataHelper.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THPT.GIOI; break;
                    case "Khá":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THPT.KHA; break;
                    case "Trung bình":
                        th.XLTN = DataHelper.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                    default:
                        th.XLTN = DataHelper.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                }
            }

            return th;
        }

        public HoSoTrungTuyenTC ToHSTT()
        {
            var hs = new HoSoTrungTuyenTC()
            {
                IdHSDT = Id,
                MaHoSo = MaHoSo,
                Ho = Ho,
                Ten = Ten,
                DotTS = DotTS,
                CCCD = CCCD,
                TongDXT = TinhDiemXT(),
                Email = Email,
                GhiChu = GhiChu,
                GioiTinh = GioiTinh,
                HoTenCha = HoTenCha,
                NamSinhCha = NamSinhCha,
                HoTenMe = HoTenMe,
                NamSinhMe = NamSinhMe,
                IdDanToc = IdDanToc,
                NamTN = NamTN,
                NamTS = NamTS,
                HTDT = HTDT,
                TDHV = TDHV,
                Lop = Lop,
                IdNgheTrungTuyen = DsNguyenVong.First(x => x.NV == 1).IdNghe,
                IdQuocTich = IdQuocTich,
                IdTonGiao = IdTonGiao,
                IdTrinhDoVH = IdTrinhDoVH,
                IdTruong = IdTruong,
                MaTinh = MaTinh,
                MaHuyen = MaHuyen,
                ThonDuong = ThonDuong,
                MaXa = MaXa,
                NgaySinh = NgaySinh,
                NgheNghiepCha = NgheNghiepCha,
                NgheNghiepMe = NgheNghiepMe,
                NoiSinh = NoiSinh,
                SDT = SDT,
                DiaChi = DiaChi,
                IdDTUT = IdDTUT,
                IdKVUT = IdKVUT
            };

            return hs;
        }

        public HoSoDuTuyenTCView ToView() => new HoSoDuTuyenTCView
        {
            Id = Id,
            MaHoSo = MaHoSo,
            Ho = Ho,
            Ten = Ten,
            NgaySinh = NgaySinh,
            GT = GioiTinh ? "Nam" : "Nữ",
            HanhKiem = HanhKiem,
            XLTN = XLTN,
            XLHT = XLHocTap,
            IdDTUT = IdDTUT,
            IdKVUT = IdKVUT,
            IdNgheDT1 = DsNguyenVong.First().IdNghe,
            IdNgheDT2 = DsNguyenVong.FirstOrDefault(x => x.NV == 2) != null ? DsNguyenVong.First(x => x.NV == 2).IdNghe : string.Empty,
            GhiChu = GhiChu,
            IdTruong = IdTruong,
            ThonDuong = ThonDuong,
            MaTinh = MaTinh,
            MaHuyen = MaHuyen,
            MaXa = MaXa,
            Lop = Lop,
            NamTN = NamTN,
            HoTenCha = HoTenCha,
            NamSinhCha = NamSinhCha,
            HoTenMe = HoTenMe,
            NamSinhMe = NamSinhMe,
            NoiSinh = NoiSinh,
            DiaChi = DiaChi,
            CCCD = CCCD,
            BangTN = KiemTraHS.BangTN ? "X" : string.Empty,
            GCNTT = KiemTraHS.GCNTT ? "X" : string.Empty,
            GiayCNUT = KiemTraHS.GiayCNUT ? "X" : string.Empty,
            GiayKhaiSinh = KiemTraHS.GiayKhaiSinh ? "X" : string.Empty,
            GKSK = KiemTraHS.GKSK ? "X" : string.Empty,
            HinhThe = KiemTraHS.HinhThe ? "X" : string.Empty,
            HocBa = KiemTraHS.HocBa ? "X" : string.Empty,
            PhieuDKDT = KiemTraHS.PhieuDKDT ? "X" : string.Empty,
            CCCDX = KiemTraHS.CCCD ? "X" : string.Empty,
            SYLL = KiemTraHS.SYLL ? "X" : string.Empty,
            SDT = SDT,
            NamTS = NamTS,
            DotTS = DotTS,
        };

        public override bool Save() => _LiteDb.Upsert(this, TuDien.CategoryName.HoSoDuTuyenTC);

        public override bool Delete() => _LiteDb.Delete<HoSoDuTuyenTC>(Id, TuDien.CategoryName.HoSoDuTuyenTC);
    }

    public class HoSoDuTuyenTCView : DBClass
    {
        [Display(Name = "Mã hồ sơ")]
        public string MaHoSo { get; set; }

        [Display(Name = "Họ học sinh")]
        public string Ho { get; set; }

        [Display(Name = "Tên học sinh")]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public string GT { get; set; }

        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Thôn/đường")]
        public string ThonDuong { get; set; }
        [Display(Name = "Tỉnh")]
        public string MaTinh { get; set; }

        [Display(Name = "Huyện")]
        public string MaHuyen { get; set; }

        [Display(Name = "Xã")]
        public string MaXa { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Hạnh kiểm")]
        public string HanhKiem { get; set; }

        [Display(Name = "Xếp loại HT")]
        public string XLHT { get; set; }

        [Display(Name = "Xếp loại TN")]
        public string XLTN { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public string IdDTUT { get; set; }

        [Display(Name = "Khu vực ưu tiên")]
        public string IdKVUT { get; set; }

        [Display(Name = "Nghề xét tuyển NV1")]
        public string IdNgheDT1 { get; set; }

        [Display(Name = "Nghề xét tuyển NV2")]
        public string IdNgheDT2 { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "CCCD")]
        public string CCCD { get; set; }

        [Display(Name = "Trường")]
        public string IdTruong { get; set; }

        [Display(Name = "Lớp")]
        public string Lop { get; set; }

        [Display(Name = "Năm TN")]
        public string NamTN { get; set; }

        [Display(Name = "Họ tên cha")]
        public string HoTenCha { get; set; }

        [Display(Name = "Năm sinh cha")]
        public string NamSinhCha { get; set; }

        [Display(Name = "Họ tên mẹ")]
        public string HoTenMe { get; set; }

        [Display(Name = "Năm sinh mẹ")]
        public string NamSinhMe { get; set; }

        [Display(Name = "Số ĐT")]
        public string SDT { get; set; }

        [Display(Name = "Phiếu đăng ký dự tuyển")]
        public string PhieuDKDT { get; set; }

        [Display(Name = "Chứng nhận tôt nghiệp")]
        public string GCNTT { get; set; }

        [Display(Name = "GKSK")]
        public string GKSK { get; set; }

        [Display(Name = "GCNUT")]
        public string GiayCNUT { get; set; }

        [Display(Name = "Học bạ")]
        public string HocBa { get; set; }

        [Display(Name = "Bằng TN")]
        public string BangTN { get; set; }

        [Display(Name = "Giấy khai sinh")]
        public string GiayKhaiSinh { get; set; }

        [Display(Name = "CCCD")]
        public string CCCDX { get; set; }
        [Display(Name = "Sơ yếu lý lịch")]
        public string SYLL { get; set; }

        [Display(Name = "Ảnh thẻ")]
        public string HinhThe { get; set; }

        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        public int DotTS { get; set; }

        public override bool Delete()
        {
            return _LiteDb.Delete<HoSoDuTuyenTC>(Id, TuDien.CategoryName.HoSoDuTuyenTC);
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

    }
    public class KiemTraHoSo
    {
        public bool PhieuDKDT { get; set; }
        public bool HocBa { get; set; }
        public bool GCNTT { get; set; }
        public bool BangTN { get; set; }
        public bool HinhThe { get; set; }
        public bool GiayKhaiSinh { get; set; }
        public bool GiayCNUT { get; set; }
        public bool GKSK { get; set; }
        public bool CCCD { get; set; }
        public bool SYLL { get; set; }
        public string GhiChu { get; set; }
    }

}
