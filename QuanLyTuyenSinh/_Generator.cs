﻿using DevExpress.Mvvm.Native;

namespace QuanLyTuyenSinh
{
    //public static class _Generator
    //{
    //    private static Random _random = new Random();
    //    private static string[] DsHo = "Nguyễn,Trần,Lê,Phạm,Hoàng,Huỳnh,Phan,Vũ,Võ,Đặng,Bùi,Đỗ,Hồ,Ngô,Dương,Lý,Đào,Đoàn,Vương,Trịnh".Split(',');

    //    private static string[] DsTen = ("Tiểu Bảo,Hữu Cảnh,Trọng Chính,Bá Cường,Hưng Ðạo,Ðắc Di,Tiến Ðức,Nghĩa Dũng,Trọng Dũng,Anh Khoa," +
    //        "Anh Khôi,Trung Kiên,Ðức Hải,Công Hiếu,Nhật Hùng,Tuấn Hùng,Minh Huy,Xuân Lộc,Trí Minh,Xuân Nam,Hữu Nghĩa,Bình " +
    //        "Nguyên,Hoài Phong,Ngọc Quang,Cao Tiến,Minh Toàn,Hữu Trác,Hữu Trí,Ðức Tuấn,Công Thành,Duy Thành,Gia Vinh,").Split(',');

    //    private static List<_Helper.Address> DsHuyenKH = _Helper.getListDistrict("511");

    //    private static string[] lstHanhKiem = { "Trung bình", "Khá", "Tốt" };
    //    private static string[] lstXepLoaiHocTap = { "Yếu", "Trung bình", "Khá", "Giỏi" };
    //    private static string[] lstXepLoaiTotNghiep = { "Chưa tốt nghiệp", "Trung bình", "Khá", "Giỏi" };

    //    public static string RandomString(int length)
    //    {
    //        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //        return new string(Enumerable.Repeat(chars, length)
    //            .Select(s => s[_random.Next(s.Length)]).ToArray());
    //    }

    //    public static string RandomNumberString(int length)
    //    {
    //        const string chars = "0123456789";
    //        return new string(Enumerable.Repeat(chars, length)
    //            .Select(s => s[_random.Next(s.Length)]).ToArray());
    //    }

    //    public static DateTime RandomDay(int YearStart)
    //    {
    //        DateTime start = new DateTime(YearStart, 1, 1);
    //        int range = (DateTime.Today - start).Days;
    //        return start.AddDays(_random.Next(range));
    //    }

    //    public static void RandomDsHSDT(int namTS, string dotTS, int soluong)
    //    {
    //        List<HoSoDuTuyenTC> lstDuTuyen = new();
    //        var dst = dotTS.Split('/');
    //        foreach (var d in dst)
    //        {
    //            for (int i = 0; i < soluong; i++)
    //            {
    //                var truong = Data.DsTruong[_random.Next(Data.DsTruong.Count)];
    //                List<_Helper.Address> lstXa = _Helper.getListWards("51103");
    //                var maxa = lstXa[_random.Next(lstXa.Count)];
    //                string IdNV1 = Data.DsNghe[_random.Next(Data.DsNghe.Count)].Id;
    //                var dsNgheNV2 = Data.DsNghe.Where(x => !x.Id.Equals(IdNV1)).ToList();
    //                string IdNV2 = dsNgheNV2[_random.Next(dsNgheNV2.Count)].Id;

    //                HoSoDuTuyenTC hsdt = new HoSoDuTuyenTC
    //                {
    //                    NamTS = namTS,
    //                    DotTS = int.Parse(d),
    //                    IdQuocTich = Data.DsQuocTich.First().Id,
    //                    IdDanToc = Data.DsDanToc[_random.Next(Data.DsDanToc.Count)].Id,
    //                    IdTrinhDoVH = Data.DsTrinhDo[_random.Next(Data.DsTrinhDo.Count)].Id,
    //                    IdTonGiao = Data.DsTonGiao[_random.Next(Data.DsTonGiao.Count)].Id,
    //                    IdTruong = truong.Id,
    //                    TDHV = truong.LoaiTruong,
    //                    HTDT = "Chính quy",
    //                    Ho = DsHo[_random.Next(DsHo.Length)],
    //                    Ten = DsTen[_random.Next(DsTen.Length)],
    //                    CCCD = RandomNumberString(12),
    //                    Email = RandomString(10) + "@gmail.com",
    //                    SDT = "0" + RandomNumberString(9),
    //                    GioiTinh = _random.NextDouble() > 0.5,
    //                    NgaySinh = RandomDay(2005),
    //                    HoTenCha = DsHo[_random.Next(DsHo.Length)] + " " + DsTen[_random.Next(DsTen.Length)],
    //                    HoTenMe = DsHo[_random.Next(DsHo.Length)] + " " + DsTen[_random.Next(DsTen.Length)],
    //                    NoiSinh = "Khánh Hòa",
    //                    DiaChi = $"{RandomNumberString(2)} đường {RandomString(5)} {maxa.AddressName}",
    //                    MaTinh = "511",
    //                    MaHuyen = "51103",
    //                    MaXa = maxa.AddressCode,
    //                    DsNguyenVong = new List<NguyenVong>
    //                        {
    //                            new NguyenVong()
    //                            {
    //                                IdNghe = IdNV1,
    //                                NV = 1
    //                            },
    //                        },
    //                    HanhKiem = lstHanhKiem[_random.Next(lstHanhKiem.Length)],
    //                    XLHocTap = truong.LoaiTruong == "THPT" ? lstXepLoaiHocTap[_random.Next(lstXepLoaiHocTap.Length)] : string.Empty,
    //                    XLTN = lstXepLoaiTotNghiep[_random.Next(lstXepLoaiTotNghiep.Length)],
    //                    IdDTUT = _random.NextDouble() > 0.5 ? null : Data.DsDoiTuongUT[_random.Next(Data.DsDoiTuongUT.Count)].Id,
    //                    IdKVUT = _random.NextDouble() > 0.5 ? null : Data.DsKhuVucUT[_random.Next(Data.DsKhuVucUT.Count)].Id,
    //                    KiemTraHS = new()
    //                    {
    //                        BangTN = _random.NextDouble() > 0.5,
    //                        GCNTT = _random.NextDouble() > 0.5,
    //                        GiayCNUT = _random.NextDouble() > 0.5,
    //                        GiayKhaiSinh = _random.NextDouble() > 0.5,
    //                        GKSK = _random.NextDouble() > 0.5,
    //                        HinhThe = _random.NextDouble() > 0.5,
    //                        HocBa = _random.NextDouble() > 0.5,
    //                        PhieuDKDT = _random.NextDouble() > 0.5,
    //                    }
    //                };
    //                if (_random.NextDouble() > 0.5)
    //                    hsdt.DsNguyenVong.Add(new NguyenVong { IdNghe = IdNV2, NV = 2 });
    //                var nv1 = Data.DsNghe.First(x => x.Id == hsdt.DsNguyenVong.First().IdNghe);
    //                int maxCount = lstDuTuyen.Where(x => x.DotTS == hsdt.DotTS && x.MaHoSo.Substring(4, 2).Equals(nv1.Ma2)).Count();
    //                hsdt.MaHoSo = $"{hsdt.NamTS}{nv1.Ma2}{(maxCount + 1).ToString("D3")}";
    //                lstDuTuyen.Add(hsdt);
    //            }
    //        }

    //        _Helper.SaveToJson(lstDuTuyen, TuDien.DbName.HoSoDuTuyen);
    //    }

    //    public static void ResetDSTonGiao()
    //    {
    //        string DsTG = "Không,Phật giáo,Công giáo,Bùi Sơn Kì hương,Cao đài,Chăm Bà la môn," +
    //            "Đạo tứ Ân Hiếu nghĩa,Giáo hội Các thành hữu Ngày sau của Chúa Giê su Ky tô (Mormon)," +
    //            "Giáo hội Cơ đốc Phục lâm Việt Nam,Giáo hội Phật đường Nam Tông Minh Sư đạo,Hồi giáo," +
    //            "Hội thánh Minh lý đạo - Tam Tông Miếu,Phật giáo Hiếu Nghĩa Tà Lơn (Cấp đăng ký hoạt động)," +
    //            "Phật giáo Hòa Hảo,Tin lành,Tịnh độ Cư sỹ Phật hội Việt Nam,Tôn giáo Baha'i";

    //        var dstg = DsTG.Split(',');
    //        List<TonGiao> tonGiaos = new();
    //        for (int i = 0; i < dstg.Count(); i++)
    //        {
    //            tonGiaos.Add(new TonGiao { Ma = i.ToString("d2"), Ten = dstg[i] });
    //        };
    //        _Helper.SaveToJson(tonGiaos, TuDien.DbName.TonGiao);
    //    }
    //}
}