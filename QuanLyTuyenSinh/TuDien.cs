﻿using System.Configuration;
using System.IO;

namespace QuanLyTuyenSinh
{
    internal static class TuDien
    {
        public const string LITEDB_DATABASE = "database.db";
        public static string IMG_FOLDER = Path.Combine(Properties.Settings.Default.DBPATH, "Img");
        public static string EXEL_FOLDER = Path.Combine(Application.StartupPath, "Mau_Exel");

        public static class CategoryName
        {
            public const string TruongHoc = "DMTH", NganhNghe = "DMNN",
                DoiTuongUuTien = "DMDTUT", KhuVucUuTien = "DMKVUT", DotXetTuyen = "DXT", DanToc = "DMDT",
                TonGiao = "DMTG", QuocTich = "DMQT", TrinhDo = "DMTDVH", ChiTieuTC = "ChiTieuTC", ChiTieuTX = "ChiTieuTX",
                HoSoDuTuyenTC = "HSDTTC", HoSoTrungTuyenTC = "HSTTTC", HoSoDuTuyenGDTX = "HSDTGDTX",
                HoSoTrungTuyenGDTX = "HSTTGDTX", DiemXetTuyenTC = "DiemXTTC", 
                ThongKe = "TK", CaiDat = "Setting";
        }

    }
}