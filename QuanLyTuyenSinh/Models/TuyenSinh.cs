using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh.Models
{
    public class NguyenVong
    {
        [Display(Name = "Nghề nghiệp")]
        [Required(ErrorMessage = "Chưa chọn nghề nghiệp")]
        public string IdNghe { get; set; }

        [Display(Name = "Nguyện vọng")]
        public int NV { get; set; }
    }


    public class DotXetTuyen : DBClass
    {
        [Display(Name = "Mã đọt")]
        [Required(ErrorMessage = "Chưa nhập mã")]
        public string Ma { get; set; }

        [Display(Name = "Năm tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập năm")]
        public int NamTS { get; set; } = DataHelper.NamTS;

        [Display(Name = "Đợt tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập đợt")]
        public int DotTS { get; set; }

        [Display(Name = "Ngày mở đợt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày mở đợt")]
        public DateTime TuNgay { get; set; } = new DateTime(DataHelper.NamTS, 1, 1);

        [Display(Name = "Ngày kết thúc")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày kết thúc")]
        public DateTime DenNgay { get; set; } = new DateTime(DataHelper.NamTS, 1, 30);

        public override bool Delete() => _LiteDb.Delete<DotXetTuyen>(Id);

        public override bool Save() => _LiteDb.Upsert(this);
    }

    public class ChiTieuTCView : DBClass
    {
        [Display(AutoGenerateField = false)]
        public int Nam { get; set; } = DataHelper.NamTS;

        private string _IdNghe;

        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public string IdNghe
        {
            get => _IdNghe;
            set
            {
                _IdNghe = value;
                var nghe = DataHelper.DsNghe.FirstOrDefault(x => x.Id.Equals(_IdNghe));
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
        public ChiTieuTC ToCTXT() => new ChiTieuTC
        {
            Id = Id,
            Nam = Nam,
            ChiTieu = ChiTieu,
            DiemTTTHCS = DiemTTTHCS,
            DiemTTTHPT = DiemTTTHPT,
            IdNghe = IdNghe,
        };

        public override bool Save() => _LiteDb.Upsert(ToCTXT(), TuDien.CategoryName.ChiTieuTC);
        public override bool Delete() => _LiteDb.Delete<ChiTieuTC>(Id, TuDien.CategoryName.ChiTieuTC);
    }

    public class ChiTieuTC : BaseClass
    {
        public int Nam { get; set; }
        public string IdNghe { get; set; }
        public int ChiTieu { get; set; }
        public double DiemTTTHCS { get; set; } = 5;
        public double DiemTTTHPT { get; set; } = 5;
        public ChiTieuTCView ToCTXT() => new ChiTieuTCView
        {
            Id = Id,
            Nam = Nam,
            IdNghe = IdNghe,
            ChiTieu = ChiTieu,
            DiemTTTHCS = DiemTTTHCS,
            DiemTTTHPT = DiemTTTHPT,
        };
    }
    public class ChiTieuTX : BaseClass
    {
        public int Nam { get; set; }
        public string IdNghe { get; set; }
        public int ChiTieu { get; set; }
        public double DiemTT { get; set; } = 5;       
        public ChiTieuTXView ToCTXT() => new ChiTieuTXView
        {
            Id = Id,
            Nam = Nam,
            ChiTieu = ChiTieu,
            DiemTT = DiemTT,
        };
    }
    public class ChiTieuTXView : DBClass
    {
        [Display(AutoGenerateField = false)]
        public int Nam { get; set; } = DataHelper.NamTS;       

        [Display(Name = "Chỉ tiêu")]
        public int ChiTieu { get; set; }

        [Display(Name = "Điểm trúng tuyển")]
        public double DiemTT { get; set; } = 5;       
        public ChiTieuTX ToCTXT() => new ChiTieuTX
        {
            Id = Id,
            Nam = Nam,
            ChiTieu = ChiTieu,
            DiemTT = DiemTT,
        };

        public override bool Save() => _LiteDb.Upsert(ToCTXT(), TuDien.CategoryName.ChiTieuTX);
        public override bool Delete() => _LiteDb.Delete<ChiTieuTX>(Id, TuDien.CategoryName.ChiTieuTX);
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
    public class KiemTraHoSoGDTX : KiemTraHoSo
    {
        public bool PhieuDKXTGDTX { get; set; }
    }
}
