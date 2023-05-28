using System.IO;

namespace QuanLyTuyenSinh
{
    internal static class TuDien
    {
        public const string LITEDB_DATABASE = "database.db";
        public const string JSON_FOLDER_PATH = "Data";
        public static string IMG_FOLDER = Path.Combine(Application.StartupPath, JSON_FOLDER_PATH, "Img");
        public static string EXEL_FOLDER = Path.Combine(Application.StartupPath, "Mau_Exel");

        public static class CategoryName
        {
            public const string TruongHoc = "DMTH", NganhNghe = "DMNN",
                DoiTuongUuTienTC = "DMDTUTTC", KhuVucUuTien = "DMKVUT", DotXetTuyen = "DXT", DanToc = "DMDT",
                TonGiao = "DMTG", QuocTich = "DMQT", TrinhDo = "DMTDVH", ChiTieuTC = "CTTC",
                HoSoDuTuyenTC = "HSDTTC", HoSoTrungTuyenTC = "HSTTTC", DiemXetTuyenTC = "DiemXTTC", ThongKe = "TK", CaiDat = "Setting";
        }

        public static class DbName
        {
            public static string TruongHoc = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "TH"),
                NganhNghe = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "NN"), DoiTuongUuTien = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "DTUT"),
                KhuVucUuTien = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "KVUT"), DotXetTuyen = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "DXT"),
                DanToc = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "DT"), TonGiao = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "TG"),
                QuocTich = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "QT"), TrinhDo = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "TDVH")
                , ChiTieu = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "CT"),
                HoSoDuTuyen = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "HSDT"), HoSoTrungTuyen = Path.Combine(
                Environment.CurrentDirectory,
                JSON_FOLDER_PATH,
                "HSTT"),
                User = Path.Combine(Environment.CurrentDirectory, JSON_FOLDER_PATH, "USER"), Settings = Path.Combine(Environment.CurrentDirectory,
                JSON_FOLDER_PATH, "Settings"),
                Img = Path.Combine(Environment.CurrentDirectory, TuDien.JSON_FOLDER_PATH, "Img")
                ;
        }
    }
}