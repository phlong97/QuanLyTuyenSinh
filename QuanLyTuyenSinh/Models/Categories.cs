using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
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

        public override bool Delete() => _LiteDb.Delete<Nghe>(Id);

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

        public override bool Delete() => _LiteDb.Delete<Truong>(Id);

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

        public override bool Delete() => _LiteDb.Delete<KhuVucUT>(Id);

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

        [Display(Name = "Áp dụng cho")]
        public string ApDung { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public override bool Delete() => _LiteDb.Delete<DoiTuongUT>(Id);

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

        public override bool Delete() => _LiteDb.Delete<DanToc>(Id);

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

        public override bool Delete() => _LiteDb.Delete<TonGiao>(Id);

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

        public override bool Delete() => _LiteDb.Delete<QuocTich>(Id);

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

        public override bool Delete() => _LiteDb.Delete<TrinhDo>(Id);

        public override bool Save() => _LiteDb.Upsert(this);
    }
}
