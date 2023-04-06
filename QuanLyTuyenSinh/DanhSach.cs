using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh
{
    public static class DanhSach
    {
        public static bool Refresh 
        { 
            set
            {
                categories = _LiteDb.GetDb().GetCollection<ObjCategory>(TuDien.DbName.Category_DB).Find(x => true).ToList();
            } 
        }
        public static List<ObjCategory> categories = _LiteDb.GetDb().GetCollection<ObjCategory>(TuDien.DbName.Category_DB).Find(x => true).ToList();
        public static List<Truong> DsTruong = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.TruongHoc)).Select(x => new Truong { Id = x.Id,Ma = x.Ma,Ten = x.Ten,MoTa = x.MoTa}).ToList();
        public static List<Nghe> DsNghe = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.NganhNghe)).Select(x => new Nghe { Id = x.Id, Ma = x.Ma, Ten = x.Ten, MoTa = x.MoTa }).ToList();
        public static List<DoiTuongUT> DsDoiTuongUT = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.DoiTuongUuTien)).Select(x => new DoiTuongUT { Id = x.Id, Ma = x.Ma, Ten = x.Ten,Diem = x.Diem ,GhiChu = x.MoTa }).ToList();
        public static List<KhuVucUT> DsKhuVucUT = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.KhuVucUuTien)).Select(x => new KhuVucUT { Id = x.Id, Ma = x.Ma, Ten = x.Ten, Diem = x.Diem, GhiChu = x.MoTa }).ToList();
        public static List<DanToc> DsDanToc = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.DanToc)).Select(x => new DanToc { Id = x.Id, Ten = x.Ten, MoTa = x.MoTa }).ToList();
        public static List<TonGiao> DsTonGiao = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.TonGiao)).Select(x => new TonGiao { Id = x.Id, Ten = x.Ten, MoTa = x.MoTa }).ToList();
        public static List<TrinhDo> DsTrinhDo = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.TrinhDo)).Select(x => new TrinhDo { Id = x.Id, Ten = x.Ten, MoTa = x.MoTa }).ToList();
        public static List<HinhThucDaoTao> DsHinhThucDT = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.HinhThucDaoTao)).Select(x => new HinhThucDaoTao { Id = x.Id, Ten = x.Ten, MoTa = x.MoTa }).ToList();
        public static List<QuocTich> DsQuocTich = categories.Where(x => x.Loai.Equals(TuDien.CategoryName.QuocTich)).Select(x => new QuocTich { Id = x.Id, Ten = x.Ten, MoTa = x.MoTa }).ToList();

        public static List<DotXetTuyen> DsDotXetTuyen = new();
    }
}
