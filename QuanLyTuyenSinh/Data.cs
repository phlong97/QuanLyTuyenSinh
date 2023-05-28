using DevExpress.Mvvm.Native;
using LiteDB;
using System.Data;
using System.Dynamic;
using System.IO;

namespace QuanLyTuyenSinh
{
    public static class Data
    {
        public static int _NamTS;
        public static User CurrUser { get; set; }
        public static CaiDat CurrSettings { get; set; }
        public static void LoadStaticList()
        {
            _NamTS = Properties.Settings.Default.NamTS;

            using (var db = _LiteDb.GetDatabase())
            {
                CurrSettings = db.GetCollection<CaiDat>(TuDien.CategoryName.CaiDat).FindOne(x => true);
                if (CurrSettings == null) CurrSettings = new();
                DsTrinhDo = db.GetCollection<TrinhDo>().FindAll().ToList();
                DsTonGiao = db.GetCollection<TonGiao>().FindAll().ToList();
                DsQuocTich = db.GetCollection<QuocTich>().FindAll().ToList();
                DsNghe = db.GetCollection<Nghe>().FindAll().ToList();
                DsDanToc = db.GetCollection<DanToc>().FindAll().ToList();
                DsTruong = db.GetCollection<Truong>().FindAll().ToList();
                DsKhuVucUT = db.GetCollection<KhuVucUT>().FindAll().ToList();
                DsDoiTuongUT = db.GetCollection<DoiTuongUTTC>(TuDien.CategoryName.DoiTuongUuTienTC).FindAll().ToList();
                DsDotXetTuyen = db.GetCollection<DotXetTuyen>().Find(Query.EQ("NamTS", _NamTS)).ToList();
                DsChiTieu = db.GetCollection<ChiTieuXetTuyenTC>(TuDien.CategoryName.ChiTieuTC).Find(Query.EQ("Nam", _NamTS)).ToList();
                DSHoSoDT = db.GetCollection<HoSoDuTuyenTC>(TuDien.CategoryName.HoSoDuTuyenTC).Find(Query.EQ("NamTS", _NamTS)).ToList();
                DSHoSoTT = db.GetCollection<HoSoTrungTuyenTC>(TuDien.CategoryName.HoSoTrungTuyenTC).Find(Query.EQ("NamTS", _NamTS)).ToList();
            }

        }

        internal static void RefreshDS(string Ten)
        {
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    DsTruong = _LiteDb.GetDatabase().GetCollection<Truong>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.NganhNghe:
                    DsNghe = _LiteDb.GetDatabase().GetCollection<Nghe>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.DoiTuongUuTienTC:
                    DsDoiTuongUT = _LiteDb.GetDatabase().GetCollection<DoiTuongUTTC>(TuDien.CategoryName.DoiTuongUuTienTC).FindAll().ToList();
                    break;

                case TuDien.CategoryName.KhuVucUuTien:
                    DsKhuVucUT = _LiteDb.GetDatabase().GetCollection<KhuVucUT>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.DanToc:
                    DsDanToc = _LiteDb.GetDatabase().GetCollection<DanToc>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.TonGiao:
                    DsTonGiao = _LiteDb.GetDatabase().GetCollection<TonGiao>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.TrinhDo:
                    DsTrinhDo = _LiteDb.GetDatabase().GetCollection<TrinhDo>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.QuocTich:
                    DsQuocTich = _LiteDb.GetDatabase().GetCollection<QuocTich>().FindAll().ToList();
                    break;

                case TuDien.CategoryName.DotXetTuyen:
                    DsDotXetTuyen = _LiteDb.GetDatabase().GetCollection<DotXetTuyen>().Find(Query.EQ("NamTS", _NamTS)).ToList();
                    break;

                case TuDien.CategoryName.ChiTieuTC:
                    DsChiTieu = _LiteDb.GetDatabase().GetCollection<ChiTieuXetTuyenTC>(TuDien.CategoryName.ChiTieuTC).Find(Query.EQ("Nam", _NamTS)).ToList();
                    break;

                case TuDien.CategoryName.HoSoDuTuyenTC:
                    DSHoSoDT = _LiteDb.GetDatabase().GetCollection<HoSoDuTuyenTC>(TuDien.CategoryName.HoSoDuTuyenTC).Find(Query.EQ("NamTS", _NamTS)).ToList();
                    break;

                case TuDien.CategoryName.HoSoTrungTuyenTC:
                    DSHoSoTT = _LiteDb.GetDatabase().GetCollection<HoSoTrungTuyenTC>(TuDien.CategoryName.HoSoTrungTuyenTC).Find(Query.EQ("NamTS", _NamTS)).ToList();
                    break;

                default: break;
            }
        }

        internal static List<DanToc> DsDanToc { get; set; }

        internal static List<DoiTuongUTTC> DsDoiTuongUT { get; set; }

        internal static List<DotXetTuyen> DsDotXetTuyen { get; set; }

        internal static List<HoSoDuTuyenTC> DSHoSoDT { get; set; }

        internal static List<HoSoTrungTuyenTC> DSHoSoTT { get; set; }

        internal static List<KhuVucUT> DsKhuVucUT { get; set; }

        internal static List<Nghe> DsNghe { get; set; }

        internal static List<QuocTich> DsQuocTich { get; set; }

        internal static List<TonGiao> DsTonGiao { get; set; }

        internal static List<TrinhDo> DsTrinhDo { get; set; }

        internal static List<Truong> DsTruong { get; set; }

        internal static List<ChiTieuXetTuyenTC> DsChiTieu { get; set; }

        internal static List<User> DsUser = _Helper.LoadFromJson<User>(TuDien.DbName.User);

        internal static User? GetUser(string userName, string userPwd)
        {
            User? user = DsUser.FirstOrDefault(u => u.UserName == userName);

            if (user == null || !PasswordHasher.VerifyPassword(userPwd, user.PasswordHash, user.Salt))
            {
                return null;
            }

            return user;
        }

        internal static bool CreateUser(string userName, string userPwd, string FullName = "", string userPer = "")
        {
            User? user = DsUser.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                return false;
            }

            User newUser = new User()
            {
                UserName = userName,
                FullName = FullName,
            };
            var salt = PasswordHasher.GenerateSalt();
            newUser.Salt = salt;
            newUser.PasswordHash = PasswordHasher.HashPassword(userPwd, salt);
            DsUser.Add(newUser);

            if (!string.IsNullOrEmpty(userPer))
            {
                newUser.Permissons = userPer;
            }
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, TuDien.JSON_FOLDER_PATH));
            _Helper.SaveToJson(DsUser, TuDien.DbName.User);

            return true;
        }

        internal static bool UpdateUser(string userId, string FullName, string userPwd = "", string userPer = "")
        {
            User? user = DsUser.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(userPwd))
                {
                    var salt = PasswordHasher.GenerateSalt();
                    user.Salt = salt;
                    user.PasswordHash = PasswordHasher.HashPassword(userPwd, salt);
                }

                user.FullName = FullName;
                if (!string.IsNullOrEmpty(userPer))
                {
                    user.Permissons = userPer;
                }
                _Helper.SaveToJson(DsUser, TuDien.DbName.User);
                return true;
            }
        }

        internal static void CheckUsers()
        {
            if (DsUser.Count <= 0)
                CreateUser("admin", "1", "admin");
        }

        internal static bool CheckDupCode(string Code, string Ten)
        {
            bool result = false;
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    result = DsTruong.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.NganhNghe:
                    result = DsNghe.Where(x => x.Ma.Equals(Code)).Count() >= 2 || DsNghe.Where(x => x.Ma2.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.DoiTuongUuTienTC:
                    result = DsDoiTuongUT.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.KhuVucUuTien:
                    result = DsKhuVucUT.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.DanToc:
                    result = DsDanToc.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.TonGiao:
                    result = DsTonGiao.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.TrinhDo:
                    result = DsTrinhDo.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.QuocTich:
                    result = DsQuocTich.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                case TuDien.CategoryName.DotXetTuyen:
                    result = DsDotXetTuyen.Where(x => x.Ma.Equals(Code)).Count() >= 2;
                    break;

                default: break;
            }
            return result;
        }

        internal static List<HoSoDuTuyenTCView> GetDSDuTuyen(int DotTS, string TDHV = "THCS")
        {
            List<HoSoDuTuyenTCView> lst = new();
            lst = DSHoSoDT.Where(
                hs => (DotTS <= 0 ? true : hs.DotTS == DotTS) && (hs.TDHV != null ? hs.TDHV.Equals(TDHV) : true))
                .Select(x => x.ToView())
                .OrderByDescending(x => x.DotTS).ThenBy(x => x.MaHoSo)
                .ToList();

            return lst;
        }

        internal static List<HoSoTrungTuyenTC> GetDSTrungTuyen(int DotTS, string TDHV = "THCS", string? MaTinh = null, string? MaHuyen = null, string? MaXa = null, string? IdTruong = null, bool KhongTT = true)
        {
            List<HoSoTrungTuyenTC> lst = new();
            if (KhongTT)
            {
                var lstHSDT = Data.DSHoSoDT.Where(hs => hs.DotTS == DotTS && hs.TDHV.Equals(TDHV)
                        && (string.IsNullOrEmpty(MaTinh) ? true : hs.MaTinh.Equals(MaTinh))
                        && (string.IsNullOrEmpty(MaHuyen) ? true : hs.MaHuyen.Equals(MaHuyen))
                        && (string.IsNullOrEmpty(MaXa) ? true : hs.MaXa.Equals(MaXa)))
                        .Select(x => x.Id).ToList();
                var lstHSTT = Data.DSHoSoTT.Where(hs => hs.DotTS == DotTS && hs.TDHV.Equals(TDHV)
                        && (string.IsNullOrEmpty(MaTinh) ? true : hs.MaTinh.Equals(MaTinh))
                        && (string.IsNullOrEmpty(MaHuyen) ? true : hs.MaHuyen.Equals(MaHuyen))
                        && (string.IsNullOrEmpty(MaXa) ? true : hs.MaXa.Equals(MaXa))
                        && (string.IsNullOrEmpty(IdTruong) ? true : hs.IdTruong.Equals(IdTruong)))
                        .Select(x => x.IdHSDT).ToList();
                var lstHSKhongTT = lstHSDT.Except(lstHSTT).ToList();
                foreach (var id in lstHSKhongTT)
                {
                    var hs = Data.DSHoSoDT.FirstOrDefault(x => x.DotTS == DotTS && x.Id == id);
                    if (hs != null)
                        lst.Add(hs.ToHSTT());
                }
            }
            else
                lst = DSHoSoTT.Where(
                        hs => (DotTS <= 0 ? true : hs.DotTS.Equals(DotTS)) && hs.TDHV.Equals(TDHV)
                        && (string.IsNullOrEmpty(MaTinh) ? true : hs.MaTinh.Equals(MaTinh))
                        && (string.IsNullOrEmpty(MaHuyen) ? true : hs.MaHuyen.Equals(MaHuyen))
                        && (string.IsNullOrEmpty(MaXa) ? true : hs.MaXa.Equals(MaXa))
                        && (string.IsNullOrEmpty(IdTruong) ? true : hs.IdTruong.Equals(IdTruong)))
                    .ToList();

            return lst.OrderBy(hs => hs.MaHoSo).ToList();
        }



        internal static List<TongHopDiemXetTuyenTC> THDiemXetTuyen(int DotTS, string htdt = "THCS")
        {
            List<TongHopDiemXetTuyenTC> lst = new();
            if (DotTS <= 0)
                return lst;
            lst = DSHoSoDT.Where(
                hs => (hs.DotTS.Equals(DotTS) && hs.TDHV.Equals(htdt))).OrderBy(x => x.MaHoSo).Select(x => x.ToTHDXT()).OrderByDescending(x => x.Tong)
                .ToList();

            return lst;
        }

        internal static void LapDSTrungTuyen(int DotTS, List<HoSoTrungTuyenTC> DsTT, List<HoSoTrungTuyenTC> DsKhongTT)
        {
            if (DotTS <= 0)
                return;
            DsTT.Clear();
            DsKhongTT.Clear();
            var HSDT_THCS = DSHoSoDT.Where(
                hs => hs.DotTS.Equals(DotTS) && hs.TDHV.Equals("THCS")).Select(x => x.ToTHDXT()).OrderBy(x => x.MaHoSo)
                .ToList();
            var HSDT_THPT = DSHoSoDT.Where(
                hs => hs.DotTS.Equals(DotTS) && hs.TDHV.Equals("THPT")).Select(x => x.ToTHDXT()).OrderBy(x => x.MaHoSo)
                .ToList();

            foreach (var ct in DsChiTieu)
            {
                var THCS_Nghe = HSDT_THCS.Where(x => x.IdNgheNV1.Equals(ct.IdNghe) && x.Tong >= ct.DiemTTTHCS).ToList();
                var THPT_Nghe = HSDT_THPT.Where(x => x.IdNgheNV1.Equals(ct.IdNghe) && x.Tong >= ct.DiemTTTHPT).ToList();

                int ctmax = (int)(ct.ChiTieu + (ct.ChiTieu * CurrSettings.CHITIEUVUOTMUC));
                int sl = DSHoSoTT.Where(x => !x.DotTS.Equals(DotTS) && x.IdNgheTrungTuyen.Equals(ct.IdNghe)).Count();

                if (sl < ctmax)
                {
                    var lstHSTT_THCS = THCS_Nghe.Select(x => x.ToHSTT()).OrderByDescending(x => x.TongDXT).ToList();
                    var lstHSTT_THPT = THPT_Nghe.Select(x => x.ToHSTT()).OrderByDescending(x => x.TongDXT).ToList();

                    if (sl + lstHSTT_THCS.Count() <= ctmax)
                    {
                        DsTT.AddRange(lstHSTT_THCS);
                        if (sl + lstHSTT_THCS.Count() + lstHSTT_THPT.Count() <= ctmax)
                        {
                            DsTT.AddRange(lstHSTT_THPT);
                        }
                        else
                        {
                            if (ctmax - sl - lstHSTT_THCS.Count() > 0)
                            {
                                DsTT.AddRange(lstHSTT_THPT.GetRange(0, ctmax - sl - lstHSTT_THCS.Count()));
                                if (lstHSTT_THPT.Count() > 0)
                                    DsKhongTT.AddRange(lstHSTT_THPT.GetRange(ctmax - sl - lstHSTT_THCS.Count(), lstHSTT_THPT.Count() - 1));
                            }

                        }
                    }
                    else
                    {
                        DsTT.AddRange(lstHSTT_THCS.GetRange(0, ctmax - sl));
                        if (lstHSTT_THPT.Count() > 0)
                            DsKhongTT.AddRange(lstHSTT_THPT.GetRange(ctmax - sl, lstHSTT_THPT.Count() - 1));
                    }
                }
            }
        }

        #region Thống kê

        public static DataTable THSLNgheTheoTruong(int madot = 0, bool ToTal = true)
        {
            List<IDictionary<string, object>> DsThongKe = new();
            var dstruongTHCS = DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();

            //Thêm các dòng TK trường THCS
            for (int i = 0; i < dstruongTHCS.Count; i++)
            {
                var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                dynamicObj.Add("STT", i + 1);
                dynamicObj.Add("Trường", dstruongTHCS[i].Ten);
                int tongTHCS = 0;
                for (int j = 0; j < DsNghe.Count; j++)
                {
                    int sldt = DSHoSoDT.Where(x => x.IdTruong.Equals(dstruongTHCS[i].Id) &&
                    x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(DsNghe[j].Id)) is not null
                    && (madot == 0 ? true : x.DotTS == madot)).Count();
                    tongTHCS += sldt;
                    dynamicObj.Add(DsNghe[j].Ten, sldt);
                }
                if (ToTal) dynamicObj.Add("Tổng", tongTHCS);
                DsThongKe.Add(dynamicObj);
            }

            var HSTHPT = DSHoSoDT.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm 1 dòng TK trường THPT
            var objTHPT = new ExpandoObject() as IDictionary<string, object>;
            objTHPT.Add("STT", dstruongTHCS.Count + 1);
            objTHPT.Add("Trường", "Trường THPT");
            int tongTHPT = 0;
            for (int j = 0; j < DsNghe.Count; j++)
            {
                int sldt = HSTHPT.Where(x => x.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(DsNghe[j].Id)) is not null
                && (madot == 0 ? true : x.DotTS == madot)).Count();
                tongTHPT += sldt;
                objTHPT.Add(DsNghe[j].Ten, sldt);
            }
            if (ToTal) objTHPT.Add("Tổng", tongTHPT);
            DsThongKe.Add(objTHPT);

            //Thêm 1 dòng tổng cộng
            var objTongCong = new ExpandoObject() as IDictionary<string, object>;
            objTongCong.Add("STT", string.Empty);
            objTongCong.Add("Trường", "Tổng cộng");
            int tongcong = 0;
            for (int i = 0; i < DsNghe.Count; i++)
            {
                int sum = DsThongKe.Sum(x => (int)x[DsNghe[i].Ten]);
                tongcong += sum;
                objTongCong.Add(DsNghe[i].Ten, sum);
            }

            objTongCong.Add("Tổng", tongcong);
            if (ToTal) DsThongKe.Add(objTongCong);

            return DsThongKe.ToDataTable();
        }

        public static DataTable THSLTTNgheTheoTruong(int dotts = 0, bool Total = true)
        {
            List<IDictionary<string, object>> DsThongKe = new();
            var dstruongTHCS = DsTruong.Where(x => x.LoaiTruong == "THCS").ToList();
            var dstruongTHPT = DsTruong.Where(x => x.LoaiTruong == "THPT").ToList();

            //Thêm các dòng TK trường THCS
            for (int i = 0; i < dstruongTHCS.Count; i++)
            {
                var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                dynamicObj.Add("STT", i + 1);
                dynamicObj.Add("Trường", dstruongTHCS[i].Ten);
                int tongTHCS = 0;
                for (int j = 0; j < DsNghe.Count; j++)
                {
                    int sltt = DSHoSoTT.Where(x => x.IdTruong.Equals(dstruongTHCS[i].Id) &&
                    x.IdNgheTrungTuyen == DsNghe[j].Id && (dotts == 0 ? true : x.DotTS == dotts)).Count();
                    tongTHCS += sltt;
                    dynamicObj.Add(DsNghe[j].Ten, sltt);
                }
                if (Total) dynamicObj.Add("Tổng", tongTHCS);
                DsThongKe.Add(dynamicObj);
            }

            var HSTHPT = DSHoSoTT.Join(dstruongTHPT, hs => hs.IdTruong, tr => tr.Id, (hs, tr) =>
            {
                return hs;
            }).ToList();
            //Thêm 1 dòng TK trường THPT
            var objTHPT = new ExpandoObject() as IDictionary<string, object>;
            objTHPT.Add("STT", dstruongTHCS.Count + 1);
            objTHPT.Add("Trường", "Trường THPT");
            int tongTHPT = 0;
            for (int j = 0; j < DsNghe.Count; j++)
            {
                int sldt = HSTHPT.Where(x => x.IdNgheTrungTuyen.Equals(DsNghe[j].Id)
                && (dotts == 0 ? true : x.DotTS == dotts)).Count();
                tongTHPT += sldt;
                objTHPT.Add(DsNghe[j].Ten, sldt);
            }
            if (Total) objTHPT.Add("Tổng", tongTHPT);
            DsThongKe.Add(objTHPT);

            //Thêm 1 dòng tổng cộng
            var objTongCong = new ExpandoObject() as IDictionary<string, object>;
            if (Total)
            {
                objTongCong.Add("STT", string.Empty);
                objTongCong.Add("Trường", "Tổng cộng");
            }

            int tongcong = 0;
            for (int i = 0; i < DsNghe.Count; i++)
            {
                int sum = DsThongKe.Sum(x => (int)x[DsNghe[i].Ten]);
                tongcong += sum;
                objTongCong.Add(DsNghe[i].Ten, sum);
            }
            objTongCong.Add("Tổng", tongcong);
            if (Total) DsThongKe.Add(objTongCong);

            return DsThongKe.ToDataTable();
        }

        public static DataTable THSLTTTheoXa(int dotts = 0, string mahuyen = "51103")
        {
            List<IDictionary<string, object>> DsThongKe = new();

            var lstTT = DSHoSoTT.Where(x => (dotts == 0 ? true : x.DotTS == dotts));
            var lstXa = _Helper.getListWards(mahuyen);

            for (int i = 0; i < DsNghe.Count; i++)
            {
                int tongNghe = 0;
                var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                dynamicObj.Add("STT", i + 1);
                dynamicObj.Add("Mã nghề", DsNghe[i].Ma);
                dynamicObj.Add("Tên nghề", DsNghe[i].Ten);
                for (int j = 0; j < lstXa.Count; j++)
                {
                    int sl = lstTT.Where(x => x.MaXa == lstXa[j].AddressCode && x.IdNgheTrungTuyen.Equals(DsNghe[i].Id)).Count();
                    tongNghe += sl;
                    dynamicObj.Add(lstXa[j].AddressName, sl);
                }
                dynamicObj.Add("Tổng", tongNghe);
                DsThongKe.Add(dynamicObj);
            }

            //Thêm dòng tổng cộng
            var tongObj = new ExpandoObject() as IDictionary<string, object>;
            int tongCong = 0;
            tongObj.Add("STT", string.Empty);
            tongObj.Add("Mã nghề", string.Empty);
            tongObj.Add("Tên nghề", "Tổng cộng");
            for (int j = 0; j < lstXa.Count; j++)
            {
                int sum = DsThongKe.Sum(x => (int)x[lstXa[j].AddressName]);
                tongCong += sum;
                tongObj.Add(lstXa[j].AddressName, sum);
            }
            tongObj.Add("Tổng", tongCong);
            DsThongKe.Add(tongObj);

            return DsThongKe.ToDataTable();
        }

        #endregion Thống kê
    }

    public static class Converter
    {
        public static void ConvertToLiteDb()
        {
            //Danh mục
            _LiteDb.GetDatabase().GetCollection<Truong>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<Truong>().InsertBulk(Data.DsTruong);
            _LiteDb.GetDatabase().GetCollection<Nghe>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<Nghe>().InsertBulk(Data.DsNghe);
            _LiteDb.GetDatabase().GetCollection<DanToc>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<DanToc>().InsertBulk(Data.DsDanToc);
            _LiteDb.GetDatabase().GetCollection<QuocTich>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<QuocTich>().InsertBulk(Data.DsQuocTich);
            _LiteDb.GetDatabase().GetCollection<TonGiao>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<TonGiao>().InsertBulk(Data.DsTonGiao);
            _LiteDb.GetDatabase().GetCollection<TrinhDo>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<TrinhDo>().InsertBulk(Data.DsTrinhDo);
            _LiteDb.GetDatabase().GetCollection<DoiTuongUTTC>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<DoiTuongUTTC>().InsertBulk(Data.DsDoiTuongUT);
            _LiteDb.GetDatabase().GetCollection<KhuVucUT>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<KhuVucUT>().InsertBulk(Data.DsKhuVucUT);
            _LiteDb.GetDatabase().GetCollection<DotXetTuyen>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<DotXetTuyen>().InsertBulk(_Helper.LoadFromJson<DotXetTuyen>(TuDien.DbName.DotXetTuyen));
            _LiteDb.GetDatabase().GetCollection<ChiTieuXetTuyenTC>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<ChiTieuXetTuyenTC>().InsertBulk(Data.DsChiTieu);
            _LiteDb.GetDatabase().GetCollection<User>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<User>().InsertBulk(Data.DsUser);

            //Hồ sơ
            _LiteDb.GetDatabase().GetCollection<HoSoDuTuyenTC>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<HoSoDuTuyenTC>().InsertBulk(_Helper.LoadFromJson<HoSoDuTuyenTC>(TuDien.DbName.HoSoDuTuyen));
            _LiteDb.GetDatabase().GetCollection<HoSoTrungTuyenTC>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<HoSoTrungTuyenTC>().InsertBulk(_Helper.LoadFromJson<HoSoTrungTuyenTC>(TuDien.DbName.HoSoTrungTuyen));

            //setting
            _LiteDb.GetDatabase().GetCollection<CaiDat>().DeleteAll();
            _LiteDb.GetDatabase().GetCollection<CaiDat>().Insert(Data.CurrSettings);
        }
    }
}