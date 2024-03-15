using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
    #region Trung cấp
    public class TongHopDiemXetTuyenTC
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
        public HoSoTrungTuyenTC ToHSTT()
        {
            var hsdt = DataHelper.DSHoSoXTTC.First(x => x.Id.Equals(IdHoSo));
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
    #endregion
    #region GDTX
    public class THSLTTTheoTruongTX
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
        [Display(Name = "Số học sinh nam")]
        public int SLHSNam { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public int SLDTUUT { get; set; }
        [Display(Name = "Chỉ học văn hóa")]
        public int SLCHVH { get; set; }
        [Display(Name = "Có học thêm nghề")]
        public int SLCHTN { get; set; }
    }
    public class THSLTTTheoNgheTX
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
        [Display(Name = "Số học sinh nam")]
        public int SLHSNam { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public int SLDTUUT { get; set; }
    }
    public class TongHopDiemXetTuyenGDTX
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
        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Học lực 6")]
        public string HocLuc6 { get; set; }
        [Display(Name = "Hạnh kiểm 6")]
        public string HanhKiem6 { get; set; }
        [Display(Name = "Điểm lớp 6")]
        public double DiemLop6 { get; set; }
        [Display(Name = "Học lực 7")]
        public string HocLuc7 { get; set; }
        [Display(Name = "Hạnh kiểm 7")]
        public string HanhKiem7 { get; set; }
        [Display(Name = "Điểm lớp 7")]
        public double DiemLop7 { get; set; }
        [Display(Name = "Học lực 8")]
        public string HocLuc8 { get; set; }
        [Display(Name = "Hạnh kiểm 8")]
        public string HanhKiem8 { get; set; }
        [Display(Name = "Điểm lớp 8")]
        public double DiemLop8 { get; set; }
        [Display(Name = "Học lực 9")]
        public string HocLuc9 { get; set; }
        [Display(Name = "Hạnh kiểm 9")]
        public string HanhKiem9 { get; set; }
        [Display(Name = "Điểm lớp 9")]
        public double DiemLop9 { get; set; }

        [Display(Name = "Ưu tiên đối tượng")]
        public string IdDTUT { get; set; }

        [Display(Name = "Điểm ưu tiên")]
        public double DiemUT { get; set; }

        [Display(Name = "Tổng điểm xét tuyển")]
        public double Tong => DiemLop6 + DiemLop7 + DiemLop8 + DiemLop9 + DiemUT;

        [Display(Name = "Nghề")]
        public string? IdNgheNV1 { get; set; }
        [Display(Name = "Loại đăng ký")]
        public string LoaiXetTuyen { get; set; } // Chỉ học văn hóa/ Học thêm nghề

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public HoSoTrungTuyenGDTX ToHSTT()
        {
            var hsdt = DataHelper.DSHoSoXetTuyenTX.First(x => x.Id.Equals(IdHoSo));
            return hsdt.ToHSTT();
        }
    }

    #endregion
}
