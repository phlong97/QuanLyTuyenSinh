using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
    public abstract class HoSo : DBClass
    {
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
        
        [Display(Name = "Trường")]
        public string IdTruong { get; set; }

        [Display(Name = "ĐTƯT")]
        public string IdDTUT { get; set; }
        
        [Display(AutoGenerateField = false)]
        public string Anh { get; set; }
        [Display(Name = "Ghi chú",Order = 99)]
        public string GhiChu { get; set; }
        public abstract double TinhDiemXetTuyen();
        public abstract string CheckError();
    }
}
