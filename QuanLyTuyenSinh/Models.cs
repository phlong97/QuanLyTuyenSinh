using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using DevExpress.XtraSpreadsheet.Import.Xls;
using LiteDB;
using Newtonsoft.Json;

namespace QuanLyTuyenSinh
{
    public class BaseClass
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }
        public BaseClass()
        {
            Id = ObjectId.NewObjectId().ToString();
        }        
    }
    public class HoSoDuTuyen : BaseClass
    {        
        [Display(Name = "Mã hồ sơ")]
        [Required(ErrorMessage = "Chưa nhập mã hồ sơ")]
        public string MaHoSo { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa nhập họ học sinh")]
        public string Ho { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa nhập tên học sinh")]
        public string Ten { get; set; }
        [Display(Name = "Họ và tên")]
        public string HoVaTen => $"{Ho} {Ten}";
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString ="dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày sinh")]
        public DateTime NgaySinh  { get; set; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Chưa chọn giới tính")]
        public bool GioiTinh { get; set; } // 0: Nữ 1: Nam
        [Display(Name = "Nơi sinh")]
        [Required(ErrorMessage = "Chưa nhập nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Số nhà")]
        [Required(ErrorMessage = "Chưa nhập số nhà")]
        public string SoNha { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn tỉnh")]
        public string Tinh { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn huyện")]
        public string Huyen { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn xã")]
        public string Xa { get; set; }
        [Display(Name = "CCCD/CMND")]
        public string CCCD { get; set; }
        private string _IdDanToc;
        public string IdDanToc 
        { 
            get => _IdDanToc;
            set
            {
                _IdDanToc = value;
                var dt = DanhSach.DsDanToc.FirstOrDefault(x => x.Id.Equals(_IdDanToc));
                DanToc = dt is null ? string.Empty : dt.Ten;
            } 
        }
        [Display(Name = "Dân tộc")]
        public string DanToc { get; set; }
        private string _IdTonGiao;
        [Display(AutoGenerateField = false)]
        public string IdTonGiao
        {
            get => _IdTonGiao;
            set
            {
                _IdTonGiao = value;
                var tg = DanhSach.DsTonGiao.FirstOrDefault(x => x.Id.Equals(_IdTonGiao));
                TonGiao = tg is null ? string.Empty : tg.Ten;
            }
        }
        
        [Display(Name = "Tôn giáo")]        
        public string TonGiao { get; set; }
        private string _IdQuocTich;
        [Display(AutoGenerateField = false)]
        public string IdQuocTich
        {
            get => _IdQuocTich;
            set
            {
                _IdQuocTich = value;
                var qt = DanhSach.DsQuocTich.FirstOrDefault(x => x.Id.Equals(_IdQuocTich));
                QT = qt is null ? string.Empty : qt.Ten;
            }
        }
        [Display(Name = "Quốc tịch")]        
        public string QT { get; set; }
        [Display(Name = "SĐT")]
        public string SDT { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage = "Sai Emmail")]
        public string Email { get; set; }
        private string _IdTrinhDoVH;
        [Display(AutoGenerateField = false)]
        public string IdTrinhDoVH
        {
            get => _IdTrinhDoVH;
            set
            {
                _IdTrinhDoVH = value;
                var td = DanhSach.DsTrinhDo.FirstOrDefault(x => x.Id.Equals(_IdTrinhDoVH));
                TrinhDo = td is null ? string.Empty : td.Ten;
            }
        }

        [Display(Name = "Trình độ văn hóa",ShortName = "TĐVH")]
        [Required(ErrorMessage = "Chưa chọn trình độ văn hóa")]
        public string TrinhDo { get; set; }
        private string _IdHinhThucDT;
        [Display(AutoGenerateField = false)]
        public string IdHinhThucDT
        {
            get => _IdHinhThucDT;
            set
            {
                _IdHinhThucDT = value;
                var ht = DanhSach.DsHinhThucDT.FirstOrDefault(x => x.Id.Equals(_IdHinhThucDT));
                TrinhDo = ht is null ? string.Empty : ht.Ten;
            }
        }
        [Display(Name = "Hình thức đào tạo", ShortName = "HTĐT")]
        [Required(ErrorMessage = "Chưa chọn hình thức đào tạo")]
        public string HinhThucDT { get; set; }
        
        [Display(AutoGenerateField = false)]
        public string HoTenCha { get; set; }
        [Display(AutoGenerateField = false)]
        public string NgheNghiepCha { get; set; }
        [Display(AutoGenerateField = false)]
        public string HoTenMe { get; set; }
        [Display(AutoGenerateField = false)]
        public string NgheNghiepMe { get; set; }
        private string _IdTruong;
        [Display(AutoGenerateField = false)]
        public string IdTruong
        {
            get => _IdTruong;
            set
            {
                _IdTruong = value;
                var truong = DanhSach.DsTruong.FirstOrDefault(x => x.Id.Equals(_IdTruong));
                MaTruong = truong is null ? string.Empty : truong.Ma;
                TenTruong = truong is null ? string.Empty : truong.Ten;
            }
        }
        [Display(Name = "Mã trường", ShortName = "Trường")]
        public string MaTruong { get; set; }
        [Display(Name = "Tên trường", ShortName = "Trường")]
        public string TenTruong { get; set; }        
        private string _IdDotXT;
        [Display(AutoGenerateField = false)]
        public string IdDotXT 
        { 
            get => _IdDotXT;
            set
            {
                _IdDotXT = value;
                var dotxt = DanhSach.DsDotXetTuyen.FirstOrDefault(x => x.Id.Equals(_IdDotXT));
                MaDotXT = dotxt is null ? string.Empty : dotxt.Ma;
                NamTS = dotxt is null ? 0 : dotxt.NamTS;
                DotTS = dotxt is null ? 0 : dotxt.DotTS;
            }
        }
        [Display(AutoGenerateField = false)]
        public string MaDotXT { get; set; }
        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }
        [Display(AutoGenerateField = false)]
        public int DotTS { get; set; }
        [Display(AutoGenerateField = false)]
        public DiemXetTuyen DXT { get; set; } = new();
        [Display(AutoGenerateField = false)]
        public KiemTraHoSo KiemTraHS { get; set; } = new();
        [Display(AutoGenerateField = false)]
        public List<NguyenVong> DsNguyenVong { get; set; } = new();
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    }    
    public class HoSoTrungTuyen : BaseClass
    {
        [Display(Name = "Mã hồ sơ")]
        [Required(ErrorMessage = "Chưa nhập mã hồ sơ")]
        public string MaHoSo { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa nhập họ học sinh")]
        public string Ho { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa nhập tên học sinh")]
        public string Ten { get; set; }
        [Display(Name = "Họ và tên")]
        public string HoVaTen => $"{Ho} {Ten}";
        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        [Required(ErrorMessage = "Chưa nhập ngày sinh")]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Chưa chọn giới tính")]
        public bool GioiTinh { get; set; } // 0: Nữ 1: Nam
        [Display(Name = "Nơi sinh")]
        [Required(ErrorMessage = "Chưa nhập nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Số nhà")]
        [Required(ErrorMessage = "Chưa nhập số nhà")]
        public string SoNha { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn tỉnh")]
        public string Tinh { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn huyện")]
        public string Huyen { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn xã")]
        public string Xa { get; set; }
        [Display(Name = "CCCD/CMND")]
        public string CCCD { get; set; }
        private string _IdDanToc;
        public string IdDanToc
        {
            get => _IdDanToc;
            set
            {
                _IdDanToc = value;
                var dt = DanhSach.DsDanToc.FirstOrDefault(x => x.Id.Equals(_IdDanToc));
                DanToc = dt is null ? string.Empty : dt.Ten;
            }
        }
        [Display(Name = "Dân tộc")]
        public string DanToc { get; set; }
        private string _IdTonGiao;
        [Display(AutoGenerateField = false)]
        public string IdTonGiao
        {
            get => _IdTonGiao;
            set
            {
                _IdTonGiao = value;
                var tg = DanhSach.DsTonGiao.FirstOrDefault(x => x.Id.Equals(_IdTonGiao));
                TonGiao = tg is null ? string.Empty : tg.Ten;
            }
        }

        [Display(Name = "Tôn giáo")]
        public string TonGiao { get; set; }
        private string _IdQuocTich;
        [Display(AutoGenerateField = false)]
        public string IdQuocTich
        {
            get => _IdQuocTich;
            set
            {
                _IdQuocTich = value;
                var qt = DanhSach.DsQuocTich.FirstOrDefault(x => x.Id.Equals(_IdQuocTich));
                QT = qt is null ? string.Empty : qt.Ten;
            }
        }
        [Display(Name = "Quốc tịch")]
        public string QT { get; set; }
        [Display(Name = "SĐT")]
        public string SDT { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Sai Emmail")]
        public string Email { get; set; }
        private string _IdTrinhDoVH;
        [Display(AutoGenerateField = false)]
        public string IdTrinhDoVH
        {
            get => _IdTrinhDoVH;
            set
            {
                _IdTrinhDoVH = value;
                var td = DanhSach.DsTrinhDo.FirstOrDefault(x => x.Id.Equals(_IdTrinhDoVH));
                TrinhDo = td is null ? string.Empty : td.Ten;
            }
        }

        [Display(Name = "Trình độ văn hóa", ShortName = "TĐVH")]
        [Required(ErrorMessage = "Chưa chọn trình độ văn hóa")]
        public string TrinhDo { get; set; }
        private string _IdHinhThucDT;
        [Display(AutoGenerateField = false)]
        public string IdHinhThucDT
        {
            get => _IdHinhThucDT;
            set
            {
                _IdHinhThucDT = value;
                var ht = DanhSach.DsHinhThucDT.FirstOrDefault(x => x.Id.Equals(_IdHinhThucDT));
                TrinhDo = ht is null ? string.Empty : ht.Ten;
            }
        }
        [Display(Name = "Hình thức đào tạo", ShortName = "HTĐT")]
        [Required(ErrorMessage = "Chưa chọn hình thức đào tạo")]
        public string HinhThucDT { get; set; }

        [Display(AutoGenerateField = false)]
        public string HoTenCha { get; set; }
        [Display(AutoGenerateField = false)]
        public string NgheNghiepCha { get; set; }
        [Display(AutoGenerateField = false)]
        public string HoTenMe { get; set; }
        [Display(AutoGenerateField = false)]
        public string NgheNghiepMe { get; set; }
        private string _IdTruong;
        [Display(AutoGenerateField = false)]
        public string IdTruong
        {
            get => _IdTruong;
            set
            {
                _IdTruong = value;
                var truong = DanhSach.DsTruong.FirstOrDefault(x => x.Id.Equals(_IdTruong));
                MaTruong = truong is null ? string.Empty : truong.Ma;
                TenTruong = truong is null ? string.Empty : truong.Ten;
            }
        }
        [Display(Name = "Mã trường", ShortName = "Trường")]
        public string MaTruong { get; set; }
        [Display(Name = "Tên trường", ShortName = "Trường")]
        public string TenTruong { get; set; }
        private string _IdDotXT;
        [Display(AutoGenerateField = false)]
        public string IdDotXT
        {
            get => _IdDotXT;
            set
            {
                _IdDotXT = value;
                var dotxt = DanhSach.DsDotXetTuyen.FirstOrDefault(x => x.Id.Equals(_IdDotXT));
                MaDotXT = dotxt is null ? string.Empty : dotxt.Ma;
                NamTS = dotxt is null ? 0 : dotxt.NamTS;
                DotTS = dotxt is null ? 0 : dotxt.DotTS;
            }
        }
        [Display(AutoGenerateField = false)]
        public string MaDotXT { get; set; }
        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }
        [Display(AutoGenerateField = false)]
        public int DotTS { get; set; }
        public DiemXetTuyen DXT { get; set; } = new();        
        private string _IdNgheTrungTuyen;
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn nghề nghiệp")]
        public string IdNgheTrungTuyen
        {
            get => _IdNgheTrungTuyen;
            set
            {
                _IdNgheTrungTuyen = value;
                var nghe = DanhSach.DsNghe.FirstOrDefault(x => x.Id.Equals(_IdNgheTrungTuyen));
                MaNghe = nghe is null ? string.Empty : nghe.Ma;
                TenNghe = nghe is null ? string.Empty : nghe.Ten;
            }
        }
        [Display(Name = "Mã nghề trúng tuyển")]
        public string MaNghe { get; set; }
        [Display(Name = "Tên nghề trúng tuyển")]
        public string TenNghe { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }   
    }
    public class NguyenVong
    {
        private string _IdNghe;
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn nghề nghiệp")]
        public string IdNghe
        {
            get => _IdNghe;
            set
            {
                _IdNghe = value;
                var nghe = DanhSach.DsNghe.FirstOrDefault(x => x.Id.Equals(_IdNghe));
                MaNghe = nghe is null ? string.Empty : nghe.Ma;
                TenNghe = nghe is null ? string.Empty : nghe.Ten;
            }
        }
        [Display(Name = "Mã nghề")]
        public string MaNghe { get; set; }
        [Display(Name = "Tên nghề")]
        public string TenNghe { get; set; }
        [Display(Name = "Nguyện vọng")]
        [Editable(false)]
        public int NV { get; set; }
    }
    public class DiemXetTuyen
    {
        [Display(Name = "Hạnh kiểm")]
        public string HanhKiem { get; set; } = "Trung bình";
        [Display(Name = "XL học tập")]
        public string XLHocTap { get; set; } = "Trung bình";
        [Display(Name = "XL tốt nghiệp")]
        public string XLTN { get; set; } = "Không";
        private string _IdDTUT;
        [Display(AutoGenerateField = false)]
        public string IdDTUT 
        { 
            get => _IdDTUT;
            set
            {
                _IdDTUT= value;
                var dtut = DanhSach.DsDoiTuongUT.FirstOrDefault(x => x.Id.Equals(_IdDTUT));
                MaDTUT = dtut is null ? string.Empty : dtut.Ma;
                DTUT = dtut is null ? string.Empty : dtut.Ten;
            } 
        }
        [Display(Name = "Mã đối tượng ưu tiên")]
        public string MaDTUT { get; set; }
        [Display(Name = "Đối tượng ưu tiên")]
        public string DTUT { get; set; }
        [Display(AutoGenerateField = false)]
        private string _IdKVUT;
        [Display(AutoGenerateField = false)]
        public string IdKVUT
        {
            get => _IdKVUT;
            set
            {
                _IdKVUT = value;
                var kvut = DanhSach.DsDoiTuongUT.FirstOrDefault(x => x.Id.Equals(_IdKVUT));
                MaKVUT = kvut is null ? string.Empty : kvut.Ma;
                KVUT = kvut is null ? string.Empty : kvut.Ten;
            }
        }
        [Display(Name = "Mã khu vực ưu tiên")]
        public string MaKVUT { get; set; }
        [Display(Name = "Khu vực ưu tiên")]
        public string KVUT { get; set; } 

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
        public string GhiChu { get; set; } 

    }
    public class DotXetTuyen : BaseClass
    {
        [Display(Name = "Mã đọt")]
        [Required(ErrorMessage = "Chưa nhập mã")]
        public string Ma { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập đợt")]
        public int DotTS { get; set; }

        [Display(Name = "Năm tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập năm")]
        public int NamTS { get; set; } = DateTime.Now.Year;
    }
    public class ChiTieuXetTuyen : BaseClass
    {

        [Display(Name = "Năm")]
        [Editable(false)]
        public int Nam { get; set; } = DateTime.Now.Year;

        private string _IdNghe;
        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public string IdNghe
        {
            get => _IdNghe;
            set
            {
                _IdNghe = value;
                var nghe = DanhSach.DsNghe.FirstOrDefault(x => x.Id.Equals(_IdNghe));
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
    }
    public class Nghe : BaseClass
    {        
        [Display(Name = "Mã nghề")]
        [Required(ErrorMessage = "Chưa nhập mã nghề nghiệp")]
        public string Ma { get; set; }
        [Display(Name = "Tên nghề")]
        [Required(ErrorMessage = "Chưa nhập tên nghề nghiệp")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
  
    }
    public class Truong : BaseClass
    {        
        [Display(Name = "Mã trường")]
        [Required(ErrorMessage = "Chưa nhập mã trường")]
        public string Ma { get; set; }
        [Display(Name = "Tên trường")]
        [Required(ErrorMessage = "Chưa nhập tên trường")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        
    }
    public class KhuVucUT : BaseClass
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
    }
    public class DoiTuongUT : BaseClass
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
        
    }    
    public class DanToc : BaseClass
    {
        [Display(Name = "Mã dân tộc")]
        [Required(ErrorMessage = "Chưa nhập mã dân tộc")]
        public string Ma { get; set; }
        [Display(Name = "Dân tộc")]
        [Required(ErrorMessage = "Chưa nhập tên dân tộc")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

    }
    public class TonGiao : BaseClass
    {
        [Display(Name = "Mã tôn giáo")]
        [Required(ErrorMessage = "Chưa nhập mã tôn giáo")]
        public string Ma { get; set; }
        [Display(Name = "Tôn giáo")]
        [Required(ErrorMessage = "Chưa nhập tên tôn giáo")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

    }
    public class QuocTich : BaseClass
    {
        [Display(Name = "Mã quốc tịch")]
        [Required(ErrorMessage = "Chưa nhập mã quốc tịch")]
        public string Ma { get; set; }
        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Chưa nhập tên quốc tịch")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
    }
    public class HinhThucDaoTao : BaseClass
    {
        [Display(Name = "Mã hình thức")]
        [Required(ErrorMessage = "Chưa nhập mã hình thức")]
        public string Ma { get; set; }
        [Display(Name = "Hình thức đào tạo")]
        [Required(ErrorMessage = "Chưa nhập hình thức đào tạo")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        
    }
    public class TrinhDo : BaseClass
    {
        [Display(Name = "Mã trình độ")]
        [Required(ErrorMessage = "Chưa nhập mã trình độ")]
        public string Ma { get; set; }
        [Display(Name = "Trình độ học vấn")]
        [Required(ErrorMessage = "Chưa nhập trình độ học vấn")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        
    }

}
