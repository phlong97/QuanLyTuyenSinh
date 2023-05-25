using DevExpress.Data.Linq.Helpers;
using LiteDB;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QuanLyTuyenSinh
{
    public abstract class BaseClass
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }

        public BaseClass()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

    }
    public abstract class DBClass : BaseClass
    {
        public abstract bool Save();
        public abstract bool Delete();
    }

    public class HoSoDuTuyen : DBClass
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
            if (Data.DSHoSoDT.Count(x => x.MaHoSo.Equals(MaHoSo) && x.DotTS == DotTS) >= 2)
                errs += "Trùng mã hồ sơ\n";
            if (string.IsNullOrEmpty(Ho))
                errs += "Chưa nhập họ học sinh \n";
            if (string.IsNullOrEmpty(Ten))
                errs += "Chưa nhập tên học sinh \n";
            if (!string.IsNullOrEmpty(CCCD) || !string.IsNullOrWhiteSpace(CCCD))
            {
                if (CCCD.Length > 0 && CCCD.Length == 11)
                    errs += "Nhập sai CCCD/CMND! \n";
                if (Data.DSHoSoDT.Count(x => x.CCCD == CCCD) >= 2)
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
                int sldt = Data.DSHoSoDT.Where(x => x.DsNguyenVong.FirstOrDefault(nv => nv.IdNghe == nv1.IdNghe && nv.NV == 1) != null && x.DotTS == DotTS).Count();
                var ctnv1 = Data.DsChiTieu.FirstOrDefault(x => x.IdNghe == nv1.IdNghe);
                if (ctnv1 != null)
                {
                    for (int i = 1; i < DotTS; i++)
                    {
                        sltt += Data.DSHoSoTT.Where(x => x.DotTS == i && x.IdNgheTrungTuyen == nv1.IdNghe).Count();
                    }
                    int ctmax = (int)(ctnv1.ChiTieu + ctnv1.ChiTieu * Data.CurrSettings.CHITIEUVUOTMUC);
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
                        tong += Data.CurrSettings.HANH_KIEM_THCS.TOT; break;
                    case "Khá":
                        tong += Data.CurrSettings.HANH_KIEM_THCS.KHA; break;
                    case "Trung bình":
                        tong += Data.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                    default:
                        tong += Data.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        tong += Data.CurrSettings.XLTN_THCS.GIOI; break;
                    case "Khá":
                        tong += Data.CurrSettings.XLTN_THCS.KHA; break;
                    case "Trung bình":
                        tong += Data.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                    default:
                        tong += Data.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                }
            }
            else if (TDHV.Equals("THPT"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        tong += Data.CurrSettings.HANH_KIEM_THPT.TOT; break;
                    case "Khá":
                        tong += Data.CurrSettings.HANH_KIEM_THPT.KHA; break;
                    case "Trung bình":
                        tong += Data.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                    default:
                        tong += Data.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                }
                switch (XLHocTap)
                {
                    case "Giỏi":
                        tong += Data.CurrSettings.XLHT_THPT.GIOI; break;
                    case "Khá":
                        tong += Data.CurrSettings.XLHT_THPT.KHA; break;
                    case "Trung bình":
                        tong += Data.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                    default:
                        tong += Data.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        tong += Data.CurrSettings.XLTN_THPT.GIOI; break;
                    case "Khá":
                        tong += Data.CurrSettings.XLTN_THPT.KHA; break;
                    case "Trung bình":
                        tong += Data.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                    default:
                        tong += Data.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                }
            }
            if (!string.IsNullOrEmpty(IdKVUT))
            {
                var kv = Data.DsKhuVucUT.FirstOrDefault(kv => kv.Id.Equals(IdKVUT));
                if (kv != null)
                {
                    tong += kv.Diem;
                }
            }
            if (!string.IsNullOrEmpty(IdDTUT))
            {
                var dt = Data.DsDoiTuongUT.FirstOrDefault(dt => dt.Id.Equals(IdDTUT));
                if (dt != null)
                {
                    tong += dt.Diem;
                }
            }
            return tong;
        }

        public TongHopDiemXetTuyen ToTHDXT()
        {
            var th = new TongHopDiemXetTuyen
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
                        : Data.DsDoiTuongUT.FirstOrDefault(x => x.Id.Equals(IdDTUT)).Diem,
                UTKV =
                    string.IsNullOrEmpty(IdKVUT)
                        ? 0
                        : Data.DsKhuVucUT.FirstOrDefault(x => x.Id.Equals(IdKVUT)).Diem,
            };
            if (TDHV.Equals("THCS"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THCS.TOT; break;
                    case "Khá":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THCS.KHA; break;
                    case "Trung bình":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                    default:
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THCS.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        th.XLTN = Data.CurrSettings.XLTN_THCS.GIOI; break;
                    case "Khá":
                        th.XLTN = Data.CurrSettings.XLTN_THCS.KHA; break;
                    case "Trung bình":
                        th.XLTN = Data.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                    default:
                        th.XLTN = Data.CurrSettings.XLTN_THCS.TRUNG_BINH; break;
                }
            }
            else if (TDHV.Equals("THPT"))
            {
                switch (HanhKiem)
                {
                    case "Tốt":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THPT.TOT; break;
                    case "Khá":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THPT.KHA; break;
                    case "Trung bình":
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                    default:
                        th.HanhKiem = Data.CurrSettings.HANH_KIEM_THPT.TRUNG_BINH; break;
                }
                switch (XLHocTap)
                {
                    case "Giỏi":
                        th.XLHT = Data.CurrSettings.XLHT_THPT.GIOI; break;
                    case "Khá":
                        th.XLHT = Data.CurrSettings.XLHT_THPT.KHA; break;
                    case "Trung bình":
                        th.XLHT = Data.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                    default:
                        th.XLHT = Data.CurrSettings.XLHT_THPT.TRUNG_BINH; break;
                }
                switch (XLTN)
                {
                    case "Giỏi":
                        th.XLTN = Data.CurrSettings.XLTN_THPT.GIOI; break;
                    case "Khá":
                        th.XLTN = Data.CurrSettings.XLTN_THPT.KHA; break;
                    case "Trung bình":
                        th.XLTN = Data.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                    default:
                        th.XLTN = Data.CurrSettings.XLTN_THPT.TRUNG_BINH; break;
                }
            }

            return th;
        }

        public HoSoTrungTuyen ToHSTT()
        {
            var hs = new HoSoTrungTuyen()
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

        public HoSoDuTuyenView ToView() => new HoSoDuTuyenView
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
            SDT = SDT,
            NamTS = NamTS,
            DotTS = DotTS,
        };

        public override bool Save() => _LiteDb.Upsert(this);

        public override bool Delete() => _LiteDb.Delete(this);
    }

    public class HoSoDuTuyenView : BaseClass
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

        [Display(Name = "Ảnh thẻ")]
        public string HinhThe { get; set; }

        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        public int DotTS { get; set; }
    }

    public class TongHopDiemXetTuyen
    {
        [Display(AutoGenerateField = false)]
        public string IdHoSo { get; set; }

        [Display(Name = "Mã hồ sơ")]
        public string MaHoSo { get; set; }

        [Display(Name = "Họ")]
        public string Ho { get; set; }

        [Display(Name = "Tên học sinh")]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public string GT { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Hạnh kiểm")]
        public double HanhKiem { get; set; }

        [Display(Name = "Xếp loại TN")]
        public double XLTN { get; set; }

        [Display(Name = "Xếp loại HT")]
        public double XLHT { get; set; }

        [Display(Name = "Ưu tiên đối tượng")]
        public double UTDT { get; set; }

        [Display(Name = "Ưu tiên KV")]
        public double UTKV { get; set; }

        [Display(Name = "Tổng điểm xét tuyển")]
        public double Tong => XLTN + XLHT + HanhKiem + UTDT + UTKV;

        [Display(Name = "Nghề")]
        public string IdNgheNV1 { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdDTUT { get; set; }
        public HoSoTrungTuyen ToHSTT()
        {
            var hsdt = Data.DSHoSoDT.First(x => x.Id.Equals(IdHoSo));
            return hsdt.ToHSTT();
        }
    }

    public class THSLTheoNghe
    {
        [Display(Name = "STT")]
        public string STT { get; set; }

        [Display(Name = "Tên nghề")]
        public string TenNghe { get; set; }

        [Display(Name = "Mã nghề")]
        public string MaNghe { get; set; }

        [Display(Name = "Nguyện vọng 1")]
        public int SLNV1 { get; set; }

        [Display(Name = "Nguyện vọng 2")]
        public int SLNV2 { get; set; }

        [Display(Name = "Tổng cộng")]
        public int Tong => SLNV1 + SLNV2;
    }

    public class HoSoTrungTuyen : DBClass
    {
        [Display(AutoGenerateField = false)]
        public string IdHSDT { get; set; }

        [Display(Name = "Mã HS")]
        [Required(ErrorMessage = "Chưa nhập mã hồ sơ")]
        public string MaHoSo { get; set; }

        [Display(Name = "Họ")]
        public string Ho { get; set; }

        [Display(Name = "Tên")]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } // 0: Nữ 1: Nam

        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(AutoGenerateField = false)]
        public string ThonDuong { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaTinh { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaHuyen { get; set; }

        [Display(AutoGenerateField = false)]
        public string MaXa { get; set; }

        [Display(AutoGenerateField = false)]
        public string CCCD { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdDanToc { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdTonGiao { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdQuocTich { get; set; }

        [Display(AutoGenerateField = false)]
        public string SDT { get; set; }

        [Display(AutoGenerateField = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(AutoGenerateField = false)]
        public string IdTrinhDoVH { get; set; }

        [Display(AutoGenerateField = false)]
        public string TDHV { get; set; }

        [Display(AutoGenerateField = false)]
        public string HTDT { get; set; }

        [Display(AutoGenerateField = false)]
        public string Lop { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamTN { get; set; }

        [Display(AutoGenerateField = false)]
        public string HoTenCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamSinhCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string NgheNghiepCha { get; set; }

        [Display(AutoGenerateField = false)]
        public string HoTenMe { get; set; }

        [Display(AutoGenerateField = false)]
        public string NamSinhMe { get; set; }

        [Display(AutoGenerateField = false)]
        public string NgheNghiepMe { get; set; }

        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        public int DotTS { get; set; }

        [Display(Name = "Tổng số điểm xét tuyển")]
        public double TongDXT { get; set; }

        [Display(Name = "Nghề trúng tuyển")]
        public string IdNgheTrungTuyen { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Trường")]
        public string IdTruong { get; set; }

        [Display(Name = "ĐTƯT")]
        public string IdDTUT { get; set; }

        [Display(Name = "KVƯT")]
        public string IdKVUT { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class THSLTTTheoNghe
    {
        [Display(Name = "STT")]
        public string STT { get; set; }

        [Display(Name = "Mã nghề")]
        public string MaNghe { get; set; }

        [Display(Name = "Tên nghề")]
        public string TenNghe { get; set; }

        [Display(Name = "Số lượng học sinh")]
        public int SLHS { get; set; }

        [Display(Name = "Số học sinh nữ")]
        public int SLHSNu { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public int SLDTUUT { get; set; }

        [Display(Name = "Tốt nghiệp THCS")]
        public int SLTNTHCS { get; set; }

        [Display(Name = "Tốt nghiệp THPT")]
        public int SLTNTHPTT { get; set; }
    }

    public class NguyenVong
    {
        [Display(Name = "Nghề nghiệp")]
        [Required(ErrorMessage = "Chưa chọn nghề nghiệp")]
        public string IdNghe { get; set; }

        [Display(Name = "Nguyện vọng")]
        public int NV { get; set; }
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
        public string GhiChu { get; set; }
    }

    public class DotXetTuyen : DBClass
    {
        [Display(Name = "Mã đọt")]
        [Required(ErrorMessage = "Chưa nhập mã")]
        public string Ma { get; set; }

        [Display(Name = "Năm tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập năm")]
        public int NamTS { get; set; } = Data._NamTS;

        [Display(Name = "Đợt tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập đợt")]
        public int DotTS { get; set; }

        [Display(Name = "Ngày mở đợt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày mở đợt")]
        public DateTime TuNgay { get; set; } = new DateTime(Data._NamTS, 1, 1);

        [Display(Name = "Ngày kết thúc")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày kết thúc")]
        public DateTime DenNgay { get; set; } = new DateTime(Data._NamTS, 1, 30);

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class ChiTieuXetTuyenView : DBClass
    {
        [Display(AutoGenerateField = false)]
        public int Nam { get; set; } = Data._NamTS;

        private string _IdNghe;

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public string IdNghe
        {
            get => _IdNghe;
            set
            {
                _IdNghe = value;
                var nghe = Data.DsNghe.FirstOrDefault(x => x.Id.Equals(_IdNghe));
                MaNghe = nghe is null ? string.Empty : nghe.Ma;
                TenNghe = nghe is null ? string.Empty : nghe.Ten;
            }
        }

        [Display(Name = "Mã nghề")]
        [Editable(false)]
        public string MaNghe { get; set; }

        [Display(Name = "Tên nghề")]
        [Editable(false)]
        public string TenNghe { get; set; }

        [Display(Name = "Chỉ tiêu")]
        public int ChiTieu { get; set; }

        [Display(Name = "Điểm trúng tuyển THCS")]
        public double DiemTTTHCS { get; set; } = 5;

        [Display(Name = "Điểm trúng tuyển THPT")]
        public double DiemTTTHPT { get; set; } = 5;
        public ChiTieuXetTuyen ToCTXT() => new ChiTieuXetTuyen
        {
            Id = Id,
            Nam = Nam,
            ChiTieu = ChiTieu,
            DiemTTTHCS = DiemTTTHCS,
            DiemTTTHPT = DiemTTTHPT,
            IdNghe = IdNghe,
        };

        public override bool Save() => _LiteDb.Upsert(this.ToCTXT());

        public override bool Delete() => _LiteDb.Delete(this.ToCTXT());
    }

    public class ChiTieuXetTuyen : BaseClass
    {
        public int Nam { get; set; } = Data._NamTS;
        public string IdNghe { get; set; }
        public int ChiTieu { get; set; }
        public double DiemTTTHCS { get; set; } = 5;
        public double DiemTTTHPT { get; set; } = 5;
        public ChiTieuXetTuyenView ToCTXT() => new ChiTieuXetTuyenView
        {
            Id = Id,
            Nam = Nam,
            IdNghe = IdNghe,
            ChiTieu = ChiTieu,
            DiemTTTHCS = DiemTTTHCS,
            DiemTTTHPT = DiemTTTHPT,
        };
    }

    public class Nghe : DBClass
    {
        [Display(Name = "Mã nghề")]
        [Required(ErrorMessage = "Chưa nhập mã nghề nghiệp")]
        public string Ma { get; set; }

        [Display(Name = "Mã nội bộ")]
        [StringLength(2, MinimumLength = 2)]
        [Required(ErrorMessage = "Chưa nhập mã nghề nội bộ")]
        public string Ma2 { get; set; }

        [Display(Name = "Tên nghề")]
        [Required(ErrorMessage = "Chưa nhập tên nghề nghiệp")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class Truong : DBClass
    {
        [Display(Name = "Mã trường")]
        [Required(ErrorMessage = "Chưa nhập mã trường")]
        public string Ma { get; set; }

        [Display(Name = "Tên trường")]
        [Required(ErrorMessage = "Chưa nhập tên trường")]
        public string Ten { get; set; }

        [Display(Name = "Loại trường")]
        [Required(ErrorMessage = "Chưa chọn loại trường")]
        public string LoaiTruong { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class KhuVucUT : DBClass
    {
        [Display(Name = "Mã khu vực")]
        [Required(ErrorMessage = "Chưa nhập mã khu vực")]
        public string Ma { get; set; }

        [Display(Name = "Tên khu vực")]
        [Required(ErrorMessage = "Chưa nhập tên khu vực")]
        public string Ten { get; set; }

        [Display(Name = "Điểm")]
        public double Diem { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class DoiTuongUT : DBClass
    {
        [Display(Name = "Mã đối tượng")]
        [Required(ErrorMessage = "Chưa nhập mã đối tượng")]
        public string Ma { get; set; }

        [Display(Name = "Tên đối tượng")]
        [Required(ErrorMessage = "Chưa nhập tên đối tượng")]
        public string Ten { get; set; }

        [Display(Name = "Điểm")]
        public double Diem { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class DanToc : DBClass
    {
        [Display(Name = "Mã dân tộc")]
        [Required(ErrorMessage = "Chưa nhập mã dân tộc")]
        public string Ma { get; set; }

        [Display(Name = "Dân tộc")]
        [Required(ErrorMessage = "Chưa nhập tên dân tộc")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class TonGiao : DBClass
    {
        [Display(Name = "Mã tôn giáo")]
        [Required(ErrorMessage = "Chưa nhập mã tôn giáo")]
        public string Ma { get; set; }

        [Display(Name = "Tôn giáo")]
        [Required(ErrorMessage = "Chưa nhập tên tôn giáo")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class QuocTich : DBClass
    {
        [Display(Name = "Mã quốc tịch")]
        [Required(ErrorMessage = "Chưa nhập mã quốc tịch")]
        public string Ma { get; set; }

        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Chưa nhập tên quốc tịch")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class TrinhDo : DBClass
    {
        [Display(Name = "Mã trình độ")]
        [Required(ErrorMessage = "Chưa nhập mã trình độ")]
        public string Ma { get; set; }

        [Display(Name = "Trình độ học vấn")]
        [Required(ErrorMessage = "Chưa nhập trình độ học vấn")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class User : DBClass
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Permissons { get; set; }

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    #region Settings

    public class CaiDat : DBClass
    {
        public string TENTRUONG { get; set; } = "Trường trung cấp nghề Vạn Ninh";
        public double CHITIEUVUOTMUC { get; set; } = 0.1;
        public double DIEMTRUNGTUYEN { get; set; } = 5;
        public HANH_KIEM_THCS HANH_KIEM_THCS { get; set; } = new();
        public XLTN_THCS XLTN_THCS { get; set; } = new();
        public HANH_KIEM_THPT HANH_KIEM_THPT { get; set; } = new();
        public XLTN_THPT XLTN_THPT { get; set; } = new();
        public XLHT_THPT XLHT_THPT { get; set; } = new();
        public string MaTinh { get; set; } = "511";
        public string MaHuyen { get; set; } = "51103";

        public override bool Delete() => _LiteDb.Delete(this);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class HANH_KIEM_THCS
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double TOT { get; set; } = 4;
    }

    public class XLTN_THCS
    {
        public double TRUNG_BINH { get; set; } = 3;
        public double KHA { get; set; } = 5;
        public double GIOI { get; set; } = 6;
    }

    public class HANH_KIEM_THPT
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double TOT { get; set; } = 4;
    }

    public class XLHT_THPT
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double GIOI { get; set; } = 4;
    }

    public class XLTN_THPT
    {
        public double TRUNG_BINH { get; set; } = 3;
        public double KHA { get; set; } = 5;
        public double GIOI { get; set; } = 6;
    }

    #endregion Settings
}