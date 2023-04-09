﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTuyenSinh
{
    internal static class TuDien
    {
        public static string LITEDB_LOCAL_PATH = Path.Combine(Environment.CurrentDirectory, "QLTS.db");
        public static class DbName
        {
            public const string Category_DB = nameof(ObjCategory),DotXetTuyen_DB = nameof(DotXetTuyen),
                ChiTieuXetTuyen_DB = nameof(ChiTieuXetTuyen);
        }
        public static class CategoryName
        {
            public const string TruongHoc = "TH", NganhNghe = "NN", DiaChi = "DC",
                DoiTuongUuTien = "DTUT", KhuVucUuTien = "KVUT", DotXetTuyen = "DXT", DanToc = "DT",
                TonGiao = "TG",QuocTich = "QT", TrinhDo = "TDHV", HinhThucDaoTao = "HTDT",ChiTieu = "CT";
        }

        public static class Settings
        {
            public const int CHITIEUMACDINH = 250;
            public const double CHITIEUVUOTMUC = 0.1;
        }
    }
}
