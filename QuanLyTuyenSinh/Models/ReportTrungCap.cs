using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh.Models
{
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

    public class HoSoTrungTuyenTC : DBClass
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

        public override bool Delete() => _LiteDb.Delete<HoSoTrungTuyenTC>(Id, TuDien.CategoryName.HoSoTrungTuyenTC);

        public override bool Save() => _LiteDb.Upsert(this, TuDien.CategoryName.HoSoTrungTuyenTC);
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
}
