using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LiteDB;
using Newtonsoft.Json;

namespace QuanLyTuyenSinh
{
    public  class BaseClass
    {
        [Display(AutoGenerateField = false)]
        public string Id { get; set; }
        public BaseClass()
        {
            Id = ObjectId.NewObjectId().ToString();
        }
        
    }
    public abstract class DanhMuc : BaseClass
    {
        public abstract ObjCategory ToObjCategory();
        public bool SaveToDb()
        {
            return _LiteDb.UpsertObj<ObjCategory>(this.ToObjCategory());
        }
    }
    public class HoSoDuTuyen : BaseClass
    {        
        [Display(Name = "Mã hồ sơ")]
        [Editable(false)]
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
        public bool GioiTinh { get; set; }
        [Display(Name = "Nơi sinh")]
        [Required(ErrorMessage = "Chưa nhập nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Số nhà")]
        [Required(ErrorMessage = "Chưa nhập số nhà")]
        public string SoNha { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn tỉnh")]
        public string MaTinh { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn huyện")]
        public string MaHuyen { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn xã")]
        public string MaXa { get; set; }
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
        [Required(ErrorMessage = "Chưa chọn dân tộc")]
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
        [Required(ErrorMessage = "Chưa chọn tôn giáo")]
        public string TonGiao { get; set; }
        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Chưa chọn quốc tịch")]
        public string Quoctich { get; set; }
        [Display(Name = "SĐT")]
        public string SDT { get; set; }
        [DataType(DataType.EmailAddress)]
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
        public HoSoDuTuyenDB ToHoSoDuTuyenDB()
        {
            HoSoDuTuyenDB hs = new()
            {
                Id = this.Id,
                MaHoSo = this.MaHoSo,
                Ho = this.Ho,
                Ten = this.Ten,
                NgaySinh = this.NgaySinh,
                GioiTinh = this.GioiTinh,
                NoiSinh = this.NoiSinh,
                SoNha = this.SoNha,
                MaTinh = this.MaTinh,
                MaHuyen = this.MaHuyen,
                MaXa = this.MaXa,
                CCCD = this.CCCD,
                DanToc = this.DanToc,
                TonGiao = this.TonGiao,
                Quoctich = this.Quoctich,
                SDT = this.SDT,
                Email = this.Email,
                IdHinhThucDT = this.IdHinhThucDT,
                IdTrinhDo = this.IdTrinhDoVH,
                HoTenCha = this.HoTenCha,
                NgheNghiepCha = this.NgheNghiepCha,
                HoTenMe = this.HoTenMe,
                NgheNghiepMe = this.NgheNghiepMe,
                IdTruong = this.IdTruong,
                IdDotXT = this.IdDotXT,
                DXT = this.DXT.ToDiemXetTuyenDB(),
                KiemTraHS = this.KiemTraHS.CloneJson(),
                DsNguyenVong = this.DsNguyenVong.Select(x => x.ToNguyenVongDB()).ToList(),
                GhiChu = this.GhiChu,
            };

            return hs;
        }
    }
    public class HoSoDuTuyenDB : BaseClass
    {       
        public string MaHoSo { get; set; }        
        public string Ho { get; set; }      
        public string Ten { get; set; }        
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string NoiSinh { get; set; }
        public string SoNha { get; set; }
        public string MaTinh { get; set; }
        public string MaHuyen { get; set; }
        public string MaXa { get; set; }
        public string CCCD { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string Quoctich { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string IdTrinhDo { get; set; }
        public string IdHinhThucDT { get; set; }
        public string HoTenCha { get; set; }
        public string NgheNghiepCha { get; set; }
        public string HoTenMe { get; set; }
        public string NgheNghiepMe { get; set; }
        public string IdTruong { get; set; }
        public string IdDotXT { get; set; }
        public DiemXetTuyenDB DXT { get; set; } = new();
        public KiemTraHoSo KiemTraHS { get; set; } = new();
        public List<NguyenVongDB> DsNguyenVong { get; set; } = new();
        public string GhiChu { get; set; }

        public HoSoDuTuyen ToHoSoDuTuyen()
        {
            HoSoDuTuyen hs = new()
            {
                Id = this.Id,
                MaHoSo = this.MaHoSo,
                Ho = this.Ho,
                Ten = this.Ten,
                NgaySinh = this.NgaySinh,
                GioiTinh = this.GioiTinh,
                NoiSinh = this.NoiSinh,
                SoNha = this.SoNha,
                MaTinh = this.MaTinh,
                MaHuyen = this.MaHuyen,
                MaXa = this.MaXa,
                CCCD = this.CCCD,
                DanToc = this.DanToc,
                TonGiao = this.TonGiao,
                Quoctich = this.Quoctich,
                SDT = this.SDT,
                Email = this.Email,
                IdTrinhDoVH = this.IdTrinhDo,
                IdHinhThucDT = this.IdHinhThucDT,
                HoTenCha = this.HoTenCha,
                NgheNghiepCha = this.NgheNghiepCha,
                HoTenMe = this.HoTenMe,
                NgheNghiepMe = this.NgheNghiepMe,
                IdTruong = this.IdTruong,
                IdDotXT = this.IdDotXT,
                DXT = this.DXT.ToDiemXetTuyen(),
                KiemTraHS = this.KiemTraHS.CloneJson(),
                DsNguyenVong = this.DsNguyenVong.Select(x => x.ToNguyenVong()).ToList(),
                GhiChu = this.GhiChu,
            };

            return hs;
        }

    }
    public class HoSoTrungTuyen : BaseClass
    {
        [Display(Name = "Mã hồ sơ")]
        [Editable(false)]
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
        public bool GioiTinh { get; set; }
        [Display(Name = "Nơi sinh")]
        [Required(ErrorMessage = "Chưa nhập nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Số nhà")]
        [Required(ErrorMessage = "Chưa nhập số nhà")]
        public string SoNha { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn tỉnh")]
        public string MaTinh { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn huyện")]
        public string MaHuyen { get; set; }
        [Display(AutoGenerateField = false)]
        [Required(ErrorMessage = "Chưa chọn xã")]
        public string MaXa { get; set; }
        [Display(Name = "CCCD/CMND")]
        public string CCCD { get; set; }
        [Display(Name = "Dân tộc")]
        [Required(ErrorMessage = "Chưa chọn dân tộc")]
        public string DanToc { get; set; }
        [Display(Name = "Tôn giáo")]
        [Required(ErrorMessage = "Chưa chọn tôn giáo")]
        public string TonGiao { get; set; }
        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Chưa chọn quốc tịch")]
        public string Quoctich { get; set; }
        [Display(Name = "SĐT")]
        public string SDT { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Trình độ văn hóa", ShortName = "TĐVH")]
        [Required(ErrorMessage = "Chưa chọn trình độ văn hóa")]
        public string TrinhDo { get; set; }
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
        [Display(AutoGenerateField = false)]
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
        public HoSoTrungTuyenDB ToHoSoTrungTuyenDB()
        {
            HoSoTrungTuyenDB hs = new()
            {
                Id = this.Id,
                MaHoSo = this.MaHoSo,
                Ho = this.Ho,
                Ten = this.Ten,
                NgaySinh = this.NgaySinh,
                GioiTinh = this.GioiTinh,
                NoiSinh = this.NoiSinh,
                SoNha = this.SoNha,
                MaTinh = this.MaTinh,
                MaHuyen = this.MaHuyen,
                MaXa = this.MaXa,
                CCCD = this.CCCD,
                DanToc = this.DanToc,
                TonGiao = this.TonGiao,
                Quoctich = this.Quoctich,
                SDT = this.SDT,
                Email = this.Email,
                TrinhDo = this.TrinhDo,
                HinhThucDT = this.HinhThucDT,
                HoTenCha = this.HoTenCha,
                NgheNghiepCha = this.NgheNghiepCha,
                HoTenMe = this.HoTenMe,
                NgheNghiepMe = this.NgheNghiepMe,
                IdTruong = this.IdTruong,
                IdDotXT = this.IdDotXT,
                DXT = this.DXT.ToDiemXetTuyenDB(),
                IdNgheTrungTuyen = this.IdNgheTrungTuyen,
                GhiChu = this.GhiChu,
            };

            return hs;
        }
    }
    public class HoSoTrungTuyenDB : BaseClass
    {
        public string MaHoSo { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string NoiSinh { get; set; }
        public string SoNha { get; set; }
        public string MaTinh { get; set; }
        public string MaHuyen { get; set; }
        public string MaXa { get; set; }
        public string CCCD { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string Quoctich { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string TrinhDo { get; set; }
        public string HinhThucDT { get; set; }
        public string HoTenCha { get; set; }
        public string NgheNghiepCha { get; set; }
        public string HoTenMe { get; set; }
        public string NgheNghiepMe { get; set; }
        public string IdTruong { get; set; }
        public string IdDotXT { get; set; }
        public DiemXetTuyenDB DXT { get; set; } = new();
        public KiemTraHoSo KiemTraHS { get; set; } = new();
        public string IdNgheTrungTuyen { get; set; }
        public string GhiChu { get; set; }

        public HoSoTrungTuyen ToHoSoTrungTuyen()
        {
            HoSoTrungTuyen hs = new()
            {
                Id = this.Id,
                MaHoSo = this.MaHoSo,
                Ho = this.Ho,
                Ten = this.Ten,
                NgaySinh = this.NgaySinh,
                GioiTinh = this.GioiTinh,
                NoiSinh = this.NoiSinh,
                SoNha = this.SoNha,
                MaTinh = this.MaTinh,
                MaHuyen = this.MaHuyen,
                MaXa = this.MaXa,
                CCCD = this.CCCD,
                DanToc = this.DanToc,
                TonGiao = this.TonGiao,
                Quoctich = this.Quoctich,
                SDT = this.SDT,
                Email = this.Email,
                TrinhDo = this.TrinhDo,
                HinhThucDT = this.HinhThucDT,
                HoTenCha = this.HoTenCha,
                NgheNghiepCha = this.NgheNghiepCha,
                HoTenMe = this.HoTenMe,
                NgheNghiepMe = this.NgheNghiepMe,
                IdTruong = this.IdTruong,
                IdDotXT = this.IdDotXT,
                DXT = this.DXT.ToDiemXetTuyen(),                
                IdNgheTrungTuyen = this.IdNgheTrungTuyen,
                GhiChu = this.GhiChu,
            };

            return hs;
        }

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
        public DiemXetTuyenDB ToDiemXetTuyenDB()
        {
            return new DiemXetTuyenDB
            {
                HanhKiem = this.HanhKiem,
                XLHocTap = this.XLHocTap,
                XLTN = this.XLTN,
                IdDTUT = this.IdDTUT,
                IdKVUT = this.IdKVUT,
            };                 
        }

    }
    public class DiemXetTuyenDB
    {
        public string HanhKiem { get; set; } = "Trung bình";        
        public string XLHocTap { get; set; } = "Trung bình";       
        public string XLTN { get; set; } = "Không";        
        public string IdDTUT { get; set; }        
        public string IdKVUT { get; set; }
        public DiemXetTuyen ToDiemXetTuyen()
        {
            return new DiemXetTuyen
            {
                HanhKiem = this.HanhKiem,
                XLHocTap = this.XLHocTap,
                XLTN = this.XLTN,
                IdDTUT = this.IdDTUT,
                IdKVUT = this.IdKVUT,
            };
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
        public string GhiChu { get; set; }
    }
    public class Nghe : DanhMuc
    {        
        [Display(Name = "Mã nghề")]
        [Required(ErrorMessage = "Chưa nhập mã nghề nghiệp")]        
        public string Ma { get; set; }
        [Display(Name = "Tên nghề")]
        [Required(ErrorMessage = "Chưa nhập tên nghề nghiệp")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory
            {
                Id = this.Id,
                Loai = TuDien.CategoryName.NganhNghe,
                Ma = this.Ma,
                Ten = this.Ten,
                MoTa = this.MoTa
            };
        }
    }
    public class Truong : DanhMuc
    {        
        [Display(Name = "Mã trường")]
        [Required(ErrorMessage = "Chưa nhập mã trường")]
        public string Ma { get; set; }
        [Display(Name = "Tên trường")]
        [Required(ErrorMessage = "Chưa nhập tên trường")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory
            {
                Id = this.Id,
                Loai = TuDien.CategoryName.TruongHoc,
                Ma = this.Ma,
                Ten = this.Ten,
                MoTa = this.MoTa
            };
        }
    }
    public class Tinh : BaseClass
    {       
        [Display(Name = "Mã tỉnh")]
        [Required(ErrorMessage = "Chưa nhập mã tỉnh")]
        public string Ma { get; set; }
        [Display(Name = "Tên tỉnh")]
        [Required(ErrorMessage = "Chưa nhập tên tỉnh")]
        public string Ten { get; set; }
        public List<Huyen> DsHuyen { get; set; } = new();

    }
    public class Huyen
    {
        [Display(Name = "Mã huyện")]
        [Required(ErrorMessage = "Chưa nhập mã huyện")]
        public string Ma { get; set; }
        [Display(Name = "Tên huyện")]
        [Required(ErrorMessage = "Chưa nhập tên huyện")]
        public string Ten { get; set; }
        public List<Xa> DsXa { get; set; } = new();
    }
    public class Xa
    {
        [Display(Name = "Mã xã")]
        [Required(ErrorMessage = "Chưa nhập mã xã")]
        public string Ma { get; set; }
        [Display(Name = "Tên xã")]
        [Required(ErrorMessage = "Chưa nhập tên xã")]
        public string Ten { get; set; }
    }
    public class KhuVucUT : DanhMuc
    {
        [Display(Name = "Mã khu vực")]
        [Required(ErrorMessage = "Chưa nhập mã khu vực")]
        public string Ma { get; set; }
        [Display(Name = "Tên khu vực")]
        [Required(ErrorMessage = "Chưa nhập tên khu vực")]
        public string Ten { get; set; }
        [Display(Name = "Điểm")]
        public int Diem { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory
            {
                Id = this.Id,
                Loai = TuDien.CategoryName.KhuVucUuTien,
                Ma = this.Ma,
                Ten = this.Ten,
                Diem = this.Diem,
                MoTa = this.GhiChu
            };
        }
    }
    public class DoiTuongUT : DanhMuc
    {
        [Display(Name = "Mã đối tượng")]
        [Required(ErrorMessage = "Chưa nhập mã đối tượng")]
        public string Ma { get; set; }
        [Display(Name = "Tên đối tượng")]
        [Required(ErrorMessage = "Chưa nhập tên đối tượng")]
        public string Ten { get; set; }
        [Display(Name = "Điểm")]
        public int Diem { get; set; }
        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory 
            { 
                Id = this.Id,
                Loai = TuDien.CategoryName.DoiTuongUuTien, 
                Ma  = this.Ma,
                Ten = this.Ten, 
                Diem = this.Diem,
                MoTa = this.GhiChu
            };
        }
    }
    public class DotXetTuyen : BaseClass
    {
        [Display(Name = "Mã đọt")]
        [Required(ErrorMessage = "Chưa nhập mã")]
        public string Ma { get; set; }
        [Display(Name = "Đợt TS")]
        [Required(ErrorMessage = "Chưa nhập đợt")]
        public int DotTS { get; set; }

        [Display(Name = "Năm tuyển sinh")]
        [Required(ErrorMessage = "Chưa nhập năm")]
        public int NamTS { get; set; } = DateTime.Now.Year;

    }
    public class ChiTieuXetTuyen
    {
        [Display(Name = "Mã chỉ tiêu")]
        [Required(ErrorMessage = "Chưa nhập mã")]
        public string MaCT { get; set; }
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
        [Display(Name = "Chỉ tiêu")]
        public int ChiTieu { get; set; }
        [Display(Name = "Năm")]
        public int Nam { get; set; } = DateTime.Now.Year;
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
        public NguyenVongDB ToNguyenVongDB()
        {
            return new NguyenVongDB
            {
                IdNghe = IdNghe,
                NV = NV,
            };
        }
    }
    public class NguyenVongDB
    {        
        public string IdNghe { get; set; }
        public int NV { get; set; }
        public NguyenVong ToNguyenVong() 
        {
            return new NguyenVong
            {
                
                IdNghe = IdNghe,
                NV = NV,
            };
        }
    }
    public class DanToc : DanhMuc
    {
        [Display(Name = "Dân tộc")]
        [Required(ErrorMessage = "Chưa nhập tên dân tộc")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory 
            {
                Id = this.Id, 
                Loai = TuDien.CategoryName.DanToc, 
                Ten = this.Ten,
                MoTa = this.MoTa 
            };
        }
    }
    public class TonGiao : DanhMuc
    {
        [Display(Name = "Tôn giáo")]
        [Required(ErrorMessage = "Chưa nhập tên tôn giáo")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory 
            {
                Id = this.Id, 
                Loai = TuDien.CategoryName.TonGiao, 
                Ten = this.Ten, 
                MoTa = this.MoTa, 
            };
        }
    }
    public class QuocTich : DanhMuc
    {
        [Display(Name = "Quốc tịch")]
        [Required(ErrorMessage = "Chưa nhập tên quốc tịch")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory 
            { 
                Id = this.Id, 
                Loai = TuDien.CategoryName.QuocTich, 
                Ten = this.Ten,
                MoTa = this.MoTa, 
            };
        }
    }
    public class HinhThucDaoTao : DanhMuc
    {
        [Display(Name = "Hình thức đào tạo")]
        [Required(ErrorMessage = "Chưa nhập hình thức đào tạo")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override ObjCategory ToObjCategory()
        {
            return new ObjCategory 
            {
                Id= this.Id,
                Loai = TuDien.CategoryName.HinhThucDaoTao,
                Ten = this.Ten, 
                MoTa = this.MoTa, 
            };
        }
    }
    public class TrinhDo : DanhMuc
    {
        [Display(Name = "Trình độ văn hóa")]
        [Required(ErrorMessage = "Chưa nhập trình độ văn hóa")]
        public string Ten { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public override ObjCategory ToObjCategory() 
        {
            return new ObjCategory 
            { 
                Id = this.Id,
                Loai = TuDien.CategoryName.TrinhDo, 
                Ten = this.Ten, 
                MoTa = this.MoTa, 
            };
        }
    }

    public class ObjCategory : BaseClass
    {
        public string Loai { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int Diem { get; set; }
        public string Ma2 { get; set; }
        public string MoTa { get; set; }

    }
}
