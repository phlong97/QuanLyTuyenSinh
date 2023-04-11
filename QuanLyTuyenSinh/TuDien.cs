using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh
{
    internal static class TuDien
    {
        public const string JSON_FOLDER_PATH = "DanhMuc";

        public static string LITEDB_LOCAL_PATH { get; internal set; }

        public static class DbName
        {
            public static string TruongHoc = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "TH"),
                NganhNghe = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "NN"), DoiTuongUuTien = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "DTUT"),
                KhuVucUuTien = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "KVUT"), DotXetTuyen = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "DXT"),
                DanToc = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "DT"), TonGiao = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "TG"),
                QuocTich = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "QT"), TrinhDo = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "TDVH"),
                HinhThucDaoTao = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "HTDT"), ChiTieu = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "CT"),
                HoSoDuTuyen = Path.Combine(Environment.CurrentDirectory, "DanhMuc", "HSDT"), HoSoTrungTuyen = Path.Combine(
                Environment.CurrentDirectory,
                "DanhMuc",
                "HSTT");
        }
        public static class CategoryName
        {
            public const string TruongHoc = "TH", NganhNghe = "NN",
                DoiTuongUuTien = "DTUT", KhuVucUuTien = "KVUT", DotXetTuyen = "DXT", DanToc = "DT",
                TonGiao = "TG",QuocTich = "QT", TrinhDo = "TDVH", HinhThucDaoTao = "HTDT",ChiTieu = "CT",
                HoSoDuTuyen = "HSDT", HoSoTrungTuyen = "HSTT";
        }

        public static class Settings
        {
            public const string TEN_TRUONG = "Trường ";
            public const int CHITIEUMACDINH = 250;
            public const double CHITIEUVUOTMUC = 0.1;
        }
    }
}
