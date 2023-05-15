using DevExpress.Mvvm.Native;
using System.Data;
using System.Dynamic;

namespace QuanLyTuyenSinh
{
    public static class DanhSach
    {
        public static int _NamTS;
        public static User CurrUser { get; set; }
        public static CaiDat CurrSettings { get; set; }

        public static void LoadStaticList()
        {
            System.IO.Directory.CreateDirectory(TuDien.JSON_FOLDER_PATH);
            _NamTS = Properties.Settings.Default.NamTS;
            CurrSettings = _Helper.LoadFromJson<CaiDat>(TuDien.DbName.Settings).FirstOrDefault();
            if (CurrSettings == null) { CurrSettings = new(); }
            DsTrinhDo = _Helper.LoadFromJson<TrinhDo>(TuDien.DbName.TrinhDo);
            DsTonGiao = _Helper.LoadFromJson<TonGiao>(TuDien.DbName.TonGiao);
            DsQuocTich = _Helper.LoadFromJson<QuocTich>(TuDien.DbName.QuocTich);
            DsNghe = _Helper.LoadFromJson<Nghe>(TuDien.DbName.NganhNghe);
            DsDanToc = _Helper.LoadFromJson<DanToc>(TuDien.DbName.DanToc);
            DsDoiTuongUT = _Helper.LoadFromJson<DoiTuongUT>(TuDien.DbName.DoiTuongUuTien);
            DsKhuVucUT = _Helper.LoadFromJson<KhuVucUT>(TuDien.DbName.KhuVucUuTien);
            DsTruong = _Helper.LoadFromJson<Truong>(TuDien.DbName.TruongHoc);
            DsDotXetTuyen = _Helper.LoadFromJson<DotXetTuyen>(TuDien.DbName.DotXetTuyen).Where(x => x.NamTS == _NamTS).ToList();
            DSHoSoDT = _Helper.LoadFromJson<HoSoDuTuyen>(TuDien.DbName.HoSoDuTuyen).Where(x => x.NamTS == _NamTS).ToList();
            DSHoSoTT = _Helper.LoadFromJson<HoSoTrungTuyen>(TuDien.DbName.HoSoTrungTuyen).Where(x => x.NamTS == _NamTS).ToList();
            DsChiTieu = _Helper.LoadFromJson<ChiTieuXetTuyen>(TuDien.DbName.ChiTieu).Where(x => x.Nam == _NamTS).ToList();
        }

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
                    DsTrinhDo = _Helper.LoadFromJson<TrinhDo>(TuDien.DbName.TrinhDo);
                    break;

                case TuDien.CategoryName.QuocTich:
                    DsQuocTich = _Helper.LoadFromJson<QuocTich>(TuDien.DbName.QuocTich);
                    break;

                case TuDien.CategoryName.DotXetTuyen:
                    DsDotXetTuyen = _Helper.LoadFromJson<DotXetTuyen>(TuDien.DbName.DotXetTuyen);
                    break;

                case TuDien.CategoryName.ChiTieu:
                    DsChiTieu = _Helper.LoadFromJson<ChiTieuXetTuyen>(TuDien.DbName.ChiTieu);
                    break;

                case TuDien.CategoryName.HoSoDuTuyen:
                    DSHoSoDT = _Helper.LoadFromJson<HoSoDuTuyen>(TuDien.DbName.HoSoDuTuyen);
                    break;

                case TuDien.CategoryName.HoSoTrungTuyen:
                    DSHoSoTT = _Helper.LoadFromJson<HoSoTrungTuyen>(TuDien.DbName.HoSoTrungTuyen);
                    break;

                default: break;
            }
        }

        public static List<DanToc> DsDanToc { get; set; }

        public static List<DoiTuongUT> DsDoiTuongUT { get; set; }

        public static List<DotXetTuyen> DsDotXetTuyen { get; set; }

        public static List<HoSoDuTuyen> DSHoSoDT { get; set; }

        public static List<HoSoTrungTuyen> DSHoSoTT { get; set; }

        public static List<KhuVucUT> DsKhuVucUT { get; set; }

        public static List<Nghe> DsNghe { get; set; }

        public static List<QuocTich> DsQuocTich { get; set; }

        public static List<TonGiao> DsTonGiao { get; set; }

        public static List<TrinhDo> DsTrinhDo { get; set; }

        public static List<Truong> DsTruong { get; set; }

        public static List<ChiTieuXetTuyen> DsChiTieu { get; set; }

        private static List<User> DsUser = _Helper.LoadFromJson<User>(TuDien.DbName.User);

        public static User? GetUser(string userName, string userPwd)
        {
            User? user = DsUser.FirstOrDefault(u => u.UserName == userName);

            if (user == null || !PasswordHasher.VerifyPassword(userPwd, user.PasswordHash, user.Salt))
            {
                return null;
            }

            return user;
        }

        public static bool CreateUser(string userName, string userPwd, string FullName = "", string userPer = "")
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

            _Helper.SaveToJson(DsUser, TuDien.DbName.User);

            return true;
        }

        public static bool UpdateUser(string userId, string FullName, string userPwd = "", string userPer = "")
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

        public static void CheckUsers()
        {
            if (DsUser.Count <= 0)
                CreateUser("admin", "1", "admin");
        }

        public static bool CheckDupCode(string Code, string Ten)
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

                case TuDien.CategoryName.DoiTuongUuTien:
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

        public static List<HoSoDuTuyenView> GetDSDuTuyen(int DotTS, string TDHV = "THCS", string? IdTruong = null, string? IdNghe = null, string? IdDTUT = null, string? IdKVUT = null)
        {
            List<HoSoDuTuyenView> lst = new();
            lst = DSHoSoDT.Where(
                hs => (DotTS == 0 ? true : hs.DotTS == DotTS) && (hs.TDHV != null ? hs.TDHV.Equals(TDHV) : true)
                && (string.IsNullOrEmpty(IdTruong) ? true : hs.IdTruong.Equals(IdTruong)) &&
                (string.IsNullOrEmpty(IdNghe) ? true : hs.DsNguyenVong.FirstOrDefault(x => x.IdNghe.Equals(IdNghe)) is not null)
                && (string.IsNullOrEmpty(IdDTUT) ? true : string.IsNullOrEmpty(hs.IdDTUT) ? false : hs.IdDTUT.Equals(IdDTUT))
                && (string.IsNullOrEmpty(IdKVUT) ? true : string.IsNullOrEmpty(hs.IdKVUT) ? false : hs.IdKVUT.Equals(IdKVUT)))
                .Select(x => x.ToView())
                .OrderByDescending(x => x.DotTS).ThenBy(x => x.MaHoSo)
                .ToList();

            return lst;
        }

        public static List<HoSoTrungTuyen> GetDSTrungTuyen(int DotTS, string TDHV = "THCS", string? IdTruong = null, string? IdNghe = null, string? IdDTUT = null, string? IdKVUT = null, string? MaTinh = null, string? MaHuyen = null, string? MaXa = null)
        {
            List<HoSoTrungTuyen> lst = new();
            lst = DSHoSoTT.Where(
                hs => (DotTS == 0 ? true : hs.DotTS == DotTS) && hs.TDHV.Equals(TDHV)
                 && (string.IsNullOrEmpty(IdTruong) ? true : hs.IdTruong.Equals(IdTruong)) &&
                (string.IsNullOrEmpty(IdNghe) ? true : hs.IdNgheTrungTuyen.Equals(IdNghe))
                && (string.IsNullOrEmpty(IdDTUT) ? true : string.IsNullOrEmpty(hs.IdDTUT) ? false : hs.IdDTUT.Equals(IdDTUT))
                && (string.IsNullOrEmpty(IdKVUT) ? true : string.IsNullOrEmpty(hs.IdKVUT) ? false : hs.IdKVUT.Equals(IdKVUT))
                && (string.IsNullOrEmpty(MaTinh) ? true : hs.MaTinh.Equals(MaTinh))
                && (string.IsNullOrEmpty(MaHuyen) ? true : hs.MaHuyen.Equals(MaHuyen))
                && (string.IsNullOrEmpty(MaXa) ? true : hs.MaXa.Equals(MaXa))
                ).OrderByDescending(x => x.DotTS).ToList();

            return lst;
        }

        public static void SaveDS(string Ten)
        {
            switch (Ten)
            {
                case TuDien.CategoryName.TruongHoc:
                    _Helper.SaveToJson(DsTruong, TuDien.DbName.TruongHoc);
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

                case TuDien.CategoryName.QuocTich:
                    _Helper.SaveToJson(DsQuocTich, TuDien.DbName.QuocTich);
                    break;

                case TuDien.CategoryName.DotXetTuyen:
                    _Helper.SaveToJson(DsDotXetTuyen, TuDien.DbName.DotXetTuyen);
                    break;

                case TuDien.CategoryName.ChiTieu:
                    _Helper.SaveToJson(DsChiTieu, TuDien.DbName.ChiTieu);
                    break;

                case TuDien.CategoryName.HoSoDuTuyen:
                    _Helper.SaveToJson(DSHoSoDT, TuDien.DbName.HoSoDuTuyen);
                    break;

                case TuDien.CategoryName.HoSoTrungTuyen:
                    _Helper.SaveToJson(DSHoSoTT, TuDien.DbName.HoSoTrungTuyen);
                    break;

                default: break;
            }
        }

        public static List<TongHopDiemXetTuyen> THDiemXetTuyen(int DotTS, string htdt = "THCS")
        {
            List<TongHopDiemXetTuyen> lst;
            lst = DSHoSoDT.Where(
                hs => (DotTS <= 0 ? true : hs.DotTS.Equals(DotTS)) && hs.TDHV.Equals(htdt)).OrderBy(x => x.MaHoSo).Select(x => x.ToTHDXT())
                .ToList();

            return lst;
        }

        public static List<HoSoTrungTuyen> LapDSTrungTuyen(int DotTS)
        {
            List<HoSoTrungTuyen> lstreturn = new();
            if (DotTS <= 0)
                return lstreturn;

            List<HoSoDuTuyen> HSDT_THCS = DSHoSoDT.Where(
                hs => (DotTS == 0 ? true : hs.DotTS.Equals(DotTS)) && hs.TDHV.Equals("THCS")).OrderBy(x => x.MaHoSo)
                .ToList();
            List<HoSoDuTuyen> HSDT_THPT = DSHoSoDT.Where(
                hs => (DotTS == 0 ? true : hs.DotTS.Equals(DotTS)) && hs.TDHV.Equals("THPT")).OrderBy(x => x.MaHoSo)
                .ToList();

            foreach (var item in DsChiTieu)
            {
                var THCS_Nghe = HSDT_THCS.Where(x => x.DsNguyenVong.First().IdNghe.Equals(item.IdNghe)).ToList();
                var THPT_Nghe = HSDT_THPT.Where(x => x.DsNguyenVong.First().IdNghe.Equals(item.IdNghe)).ToList();

                int ctmax = (int)(item.ChiTieu + (item.ChiTieu * CurrSettings.CHITIEUVUOTMUC));
                int sl = DSHoSoTT.Where(x => !x.DotTS.Equals(DotTS) && x.IdNgheTrungTuyen.Equals(item.IdNghe)).Count();

                if (sl < ctmax)
                {
                    var lstHSTT_THCS = THCS_Nghe.Select(x => x.ToHSTT()).OrderByDescending(x => x.TongDXT).ToList();
                    var lstHSTT_THPT = THPT_Nghe.Select(x => x.ToHSTT()).OrderByDescending(x => x.TongDXT).ToList();

                    if (sl + lstHSTT_THCS.Count() <= ctmax)
                    {
                        lstreturn.AddRange(lstHSTT_THCS);
                        if (sl + lstHSTT_THCS.Count() + lstHSTT_THPT.Count() <= ctmax)
                        {
                            lstreturn.AddRange(lstHSTT_THPT);
                        }
                        else
                        {
                            lstreturn.AddRange(lstHSTT_THPT.GetRange(0, ctmax - sl - lstHSTT_THCS.Count()));
                        }
                    }
                    else
                        lstreturn.AddRange(lstHSTT_THCS.GetRange(0, ctmax - sl));
                }
            }

            return lstreturn.OrderBy(x => x.IdNgheTrungTuyen).ToList();
        }

        #region Thống kê

        public static DataTable THSLNgheTheoTruong(int madot = 0)
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
                dynamicObj.Add("Tổng", tongTHCS);
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
            objTHPT.Add("Tổng", tongTHPT);
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
            DsThongKe.Add(objTongCong);

            return DsThongKe.ToDataTable();
        }

        public static DataTable THSLTTNgheTheoTruong(int dotts = 0)
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
                dynamicObj.Add("Tổng", tongTHCS);
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
            objTHPT.Add("Tổng", tongTHPT);
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
            DsThongKe.Add(objTongCong);

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
                    int sl = lstTT.Where(x => x.MaXa == lstXa[j].AdressCode && x.IdNgheTrungTuyen.Equals(DsNghe[i].Id)).Count();
                    tongNghe += sl;
                    dynamicObj.Add(lstXa[j].AdressName, sl);
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
                int sum = DsThongKe.Sum(x => (int)x[lstXa[j].AdressName]);
                tongCong += sum;
                tongObj.Add(lstXa[j].AdressName, sum);
            }
            tongObj.Add("Tổng", tongCong);
            DsThongKe.Add(tongObj);

            return DsThongKe.ToDataTable();
        }

        #endregion Thống kê
    }
}