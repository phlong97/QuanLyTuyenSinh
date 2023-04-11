using DevExpress.CodeParser;
using DevExpress.DataAccess.DataFederation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh
{
    public static class DanhSach
    {
        public static void RefreshDS(string Ten)
        {
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    DsTruong = _Helper.LoadFromJson<Truong>(TuDien.DbName.TruongHoc);
                    break;
                case TuDien.CategoryName.NganhNghe:
                    DsNghe = _Helper.LoadFromJson<Nghe>(TuDien.DbName.NganhNghe);
                    break;
                case TuDien.CategoryName.DoiTuongUuTien:
                    DsDoiTuongUT = _Helper.LoadFromJson<DoiTuongUT>(TuDien.DbName.DoiTuongUuTien);
                    break;
                case TuDien.CategoryName.KhuVucUuTien:
                    DsKhuVucUT = _Helper.LoadFromJson<KhuVucUT>(TuDien.DbName.KhuVucUuTien);
                    break;
                case TuDien.CategoryName.DanToc:
                    DsDanToc = _Helper.LoadFromJson<DanToc>(TuDien.DbName.DanToc);
                    break;
                case TuDien.CategoryName.TonGiao:
                    DsTonGiao = _Helper.LoadFromJson<TonGiao>(TuDien.DbName.TonGiao);
                    break;
                case TuDien.CategoryName.TrinhDo:
                    _Helper.LoadFromJson<TrinhDo>(TuDien.DbName.TrinhDo);
                    break;
                case TuDien.CategoryName.HinhThucDaoTao:
                    _Helper.LoadFromJson<HinhThucDaoTao>(TuDien.DbName.HinhThucDaoTao);
                    break;
                case TuDien.CategoryName.QuocTich:
                    _Helper.LoadFromJson<QuocTich>(TuDien.DbName.QuocTich);
                    break;
                case TuDien.CategoryName.DotXetTuyen:
                    _Helper.LoadFromJson<DotXetTuyen>(TuDien.DbName.DotXetTuyen);
                    break;
                case TuDien.CategoryName.ChiTieu:
                    _Helper.LoadFromJson<ChiTieuXetTuyen>(TuDien.DbName.DotXetTuyen).OrderByDescending(x => x.Nam).ToList();
                    break;
                default: break;
            }
        }
        public static void SaveDS(string Ten)
        {
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    _Helper.SaveToJson(DsTruong,TuDien.DbName.TruongHoc);
                    break;
                case TuDien.CategoryName.NganhNghe:
                    _Helper.SaveToJson(DsNghe, TuDien.DbName.NganhNghe);
                    break;
                case TuDien.CategoryName.DoiTuongUuTien:
                    _Helper.SaveToJson(DsDoiTuongUT, TuDien.DbName.DoiTuongUuTien);
                    break;
                case TuDien.CategoryName.KhuVucUuTien:
                    _Helper.SaveToJson(DsKhuVucUT, TuDien.DbName.KhuVucUuTien);
                    break;
                case TuDien.CategoryName.DanToc:
                    _Helper.SaveToJson(DsDanToc, TuDien.DbName.DanToc);
                    break;
                case TuDien.CategoryName.TonGiao:
                    _Helper.SaveToJson(DsTonGiao, TuDien.DbName.TonGiao);
                    break;
                case TuDien.CategoryName.TrinhDo:
                    _Helper.SaveToJson(DsTrinhDo, TuDien.DbName.TrinhDo);
                    break;
                case TuDien.CategoryName.HinhThucDaoTao:
                    _Helper.SaveToJson(DsHinhThucDT, TuDien.DbName.HinhThucDaoTao);
                    break;
                case TuDien.CategoryName.QuocTich:
                    _Helper.SaveToJson(DsQuocTich, TuDien.DbName.QuocTich);
                    break;
                case TuDien.CategoryName.DotXetTuyen:
                    _Helper.SaveToJson(DsDotXetTuyen, TuDien.DbName.DotXetTuyen);
                    break;
                case TuDien.CategoryName.ChiTieu:
                    _Helper.SaveToJson(DsChiTieu, TuDien.DbName.ChiTieu);
                    break;
                default: break;
            }
        }
        public static bool CheckDupCode(string Code,string Ten)
        {
            bool result = false;
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    result = DsTruong.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.NganhNghe:
                    result = DsNghe.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.DoiTuongUuTien:
                    result = DsDoiTuongUT.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.KhuVucUuTien:
                    result = DsKhuVucUT.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.DanToc:
                    result = DsDanToc.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.TonGiao:
                    result = DsTonGiao.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.TrinhDo:
                    result = DsTrinhDo.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.HinhThucDaoTao:
                    result = DsHinhThucDT.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.QuocTich:
                    result = DsQuocTich.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                case TuDien.CategoryName.DotXetTuyen:
                    result = DsDotXetTuyen.FirstOrDefault(x => x.Ma.Equals(Code)) is not null;
                    break;
                default: break;
            }
            return result;
        }

        public static List<Truong> DsTruong = _Helper.LoadFromJson<Truong>(TuDien.DbName.TruongHoc);
        public static List<Nghe> DsNghe = _Helper.LoadFromJson<Nghe>(TuDien.DbName.NganhNghe);
        public static List<DoiTuongUT> DsDoiTuongUT = _Helper.LoadFromJson<DoiTuongUT>(TuDien.DbName.DoiTuongUuTien);
        public static List<KhuVucUT> DsKhuVucUT = _Helper.LoadFromJson<KhuVucUT>(TuDien.DbName.KhuVucUuTien);
        public static List<DanToc> DsDanToc = _Helper.LoadFromJson<DanToc>(TuDien.DbName.DanToc);
        public static List<TonGiao> DsTonGiao = _Helper.LoadFromJson<TonGiao>(TuDien.DbName.TonGiao);
        public static List<TrinhDo> DsTrinhDo = _Helper.LoadFromJson<TrinhDo>(TuDien.DbName.TrinhDo);
        public static List<HinhThucDaoTao> DsHinhThucDT = _Helper.LoadFromJson<HinhThucDaoTao>(TuDien.DbName.HinhThucDaoTao);
        public static List<QuocTich> DsQuocTich = _Helper.LoadFromJson<QuocTich>(TuDien.DbName.QuocTich);

        public static List<DotXetTuyen> DsDotXetTuyen = _Helper.LoadFromJson<DotXetTuyen>(TuDien.DbName.DotXetTuyen);
        public static List<ChiTieuXetTuyen> DsChiTieu = _Helper.LoadFromJson<ChiTieuXetTuyen>(TuDien.DbName.DotXetTuyen).ToList();
        public static List<HoSoDuTuyen> DSHoSoDT = _Helper.LoadFromJson<HoSoDuTuyen>(TuDien.DbName.HoSoDuTuyen);
        public static List<HoSoTrungTuyen> DSHoSoTT = _Helper.LoadFromJson<HoSoTrungTuyen>(TuDien.DbName.HoSoTrungTuyen);

    }
}
