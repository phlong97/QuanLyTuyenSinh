namespace QuanLyTuyenSinh.Models
{
    public class CaiDat : DBClass
    {
        public string TENTRUONG { get; set; } = "Trường trung cấp nghề Vạn Ninh";
        public double CHITIEUVUOTMUC { get; set; } = 0.1;
        public double DIEMTRUNGTUYEN { get; set; } = 5;
        public HANH_KIEM_THCS HANH_KIEM_THCS { get; set; } = new();
        public XLTN_THCS XLTN_THCS { get; set; } = new();
        public HANH_KIEM_THPT HANH_KIEM_THPT { get; set; } = new();
        public XLTN_THPT XLTN_THPT { get; set; } = new();
        public XLHT_THPT XLHT_THPT { get; set; } = new();

        public override bool Delete() => _LiteDb.GetCollection<CaiDat>(TuDien.CategoryName.CaiDat).Delete(Id);
        public override bool Save() => _LiteDb.GetCollection<CaiDat>(TuDien.CategoryName.CaiDat).Upsert(this);
    }

    public class HANH_KIEM_THCS
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double TOT { get; set; } = 4;
    }

    public class XLTN_THCS
    {
        public double TRUNG_BINH { get; set; } = 3;
        public double KHA { get; set; } = 5;
        public double GIOI { get; set; } = 6;
    }

    public class HANH_KIEM_THPT
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double TOT { get; set; } = 4;
    }

    public class XLHT_THPT
    {
        public double TRUNG_BINH { get; set; } = 2;
        public double KHA { get; set; } = 3;
        public double GIOI { get; set; } = 4;
    }

    public class XLTN_THPT
    {
        public double TRUNG_BINH { get; set; } = 3;
        public double KHA { get; set; } = 5;
        public double GIOI { get; set; } = 6;
    }
}
