using System.ComponentModel.DataAnnotations;

namespace QuanLyTuyenSinh.Models
{
    public class HoSoDuTuyenGDTX : HoSo
    {
        public string HanhKiem6 { get; set; }
        public string HanhKiem7 {  get; set; }
        public string HanhKiem8 { get; set; }
        public string HanhKiem9 { get; set; }
        public string HocLuc6 { get; set; }
        public string HocLuc7 { get; set; }
        public string HocLuc8 { get; set; }
        public string HocLuc9 { get; set; }

        public KiemTraHoSoGDTX KiemTraHS { get; set; } = new();      

        public override string CheckError()
        {
            string errs = string.Empty;
            if (string.IsNullOrEmpty(MaHoSo))
                errs += "Chưa nhập mã hồ sơ\n";
            if (DataHelper.DSHoSoXetTuyenTX.Count(x => x.MaHoSo.Equals(MaHoSo) && x.DotTS == DotTS) > 1)
                errs += "Trùng mã hồ sơ\n";
            if (string.IsNullOrEmpty(Ho))
                errs += "Chưa nhập họ học sinh \n";
            if (string.IsNullOrEmpty(Ten))
                errs += "Chưa nhập tên học sinh \n";
            if (!string.IsNullOrEmpty(CCCD) || !string.IsNullOrWhiteSpace(CCCD))
            {
                if (CCCD.Length > 0 && CCCD.Length == 11)
                    errs += "Nhập sai CCCD/CMND! \n";
                if (DataHelper.DSHoSoXetTuyenTX.Count(x => x.CCCD == CCCD) >= 2)
                    errs += "Đã tồn tại CCCD/CMND!\n";
            }
            if (NgaySinh == DateTime.MinValue)
                errs += "Chưa nhập ngày sinh\n";
            if (string.IsNullOrEmpty(NoiSinh))
                errs += "Chưa nhập nơi sinh\n";
            if (string.IsNullOrEmpty(DiaChi))
                errs += "Chưa nhập địa chỉ\n";
            if (string.IsNullOrEmpty(MaTinh))
                errs += "Chưa chọn tỉnh\n";
            if (string.IsNullOrEmpty(MaHuyen))
                errs += "Chưa chọn quận/huyện\n";
            if (string.IsNullOrEmpty(MaXa))
                errs += "Chưa chọn phường/xã\n";
            if (string.IsNullOrEmpty(IdDanToc))
                errs += "Chưa chọn dân tộc\n";
            if (string.IsNullOrEmpty(IdTonGiao))
                errs += "Chưa chọn tôn giáo\n";
            if (string.IsNullOrEmpty(IdQuocTich))
                errs += "Chưa chọn quốc tịch\n";
            if (string.IsNullOrEmpty(IdTrinhDoVH))
                errs += "Chưa chọn trình độ văn hóa\n";
            if (string.IsNullOrEmpty(IdTruong))
                errs += "Chưa chọn trường\n";
            if (string.IsNullOrEmpty(HanhKiem6) || 
                string.IsNullOrEmpty(HanhKiem7) || 
                string.IsNullOrEmpty(HanhKiem8) ||
                string.IsNullOrEmpty(HanhKiem9))
                errs += "Chưa nhập hạnh kiểm\n";
            if (string.IsNullOrEmpty(HocLuc6) ||
                string.IsNullOrEmpty(HocLuc7) ||
                string.IsNullOrEmpty(HocLuc8) ||
                string.IsNullOrEmpty(HocLuc9))
                errs += "Chưa nhập học lực\n";
            if (DsNguyenVong.Count() == 0)
                errs += "Chưa chọn nguyện vọng\n";
            if (DsNguyenVong.Count(x => x.NV == 1) == 0)
                errs += "Chưa chọn nguyện vọng 1\n";
            if (DsNguyenVong.Count(x => x.IdNghe == null) > 0)
                errs += "Chưa chọn nghề cho nguyện vọng\n";
            if (DsNguyenVong.Count(x => x.NV == 1) == 1)
            {
                int sltt = 0;
                var nv1 = DsNguyenVong.First(x => x.NV == 1);
                int sldt = DataHelper.DSHoSoXetTuyenTX.Where(x => x.DsNguyenVong.FirstOrDefault(nv => nv.IdNghe == nv1.IdNghe && nv.NV == 1) != null && x.DotTS == DotTS).Count();
                var ctnv1 = DataHelper.DsChiTieuTX.FirstOrDefault(x => x.IdNghe == nv1.IdNghe);
                if (ctnv1 != null)
                {
                    for (int i = 1; i < DotTS; i++)
                    {
                        sltt += DataHelper.DSHoSoTTTC.Where(x => x.DotTS == i && x.IdNgheTrungTuyen == nv1.IdNghe).Count();
                    }
                    int ctmax = (int)(ctnv1.ChiTieu + ctnv1.ChiTieu * DataHelper.CurrSettings.CHITIEUVUOTMUC);
                    if (sldt + sltt >= ctmax)
                    {
                        errs += $"Nguyện vọng 1 đã vượt chỉ tiêu tối đa! Chỉ tiêu tối đa: {ctmax}\n";
                    }
                }
            }
            return errs;
        }

        public TongHopDiemXetTuyenGDTX ToTHDXT()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var th = new TongHopDiemXetTuyenGDTX
            {
                IdHoSo = Id,
                MaHoSo = MaHoSo,
                Ho = Ho,
                Ten = Ten,
                NgaySinh = NgaySinh,
                GT = GioiTinh ? "Nam" : "Nữ",
                DiaChi = DiaChi,
                IdNgheNV1 = DsNguyenVong.First().IdNghe,
                GhiChu = GhiChu,
                IdDTUT = string.IsNullOrEmpty(IdDTUT)
                        ? string.Empty
                        : IdDTUT,
                UTDT =
                    string.IsNullOrEmpty(IdDTUT)
                        ? 0
                        : DataHelper.DsDoiTuongUT.FirstOrDefault(x => x.Id.Equals(IdDTUT)).Diem,
                UTKV =
                    string.IsNullOrEmpty(IdKVUT)
                        ? 0
                        : DataHelper.DsKhuVucUT.FirstOrDefault(x => x.Id.Equals(IdKVUT)).Diem,
                HocLuc6 = HocLuc6,
                HocLuc7 = HocLuc7,
                HocLuc8 = HocLuc8,
                HocLuc9 = HocLuc9,
                HanhKiem6 = HanhKiem6,
                HanhKiem7 = HanhKiem7,
                HanhKiem8 = HanhKiem8,
                HanhKiem9 = HanhKiem9,
                DiemLop6 = TinhDiemXL(HocLuc7, HanhKiem7),
                DiemLop7 = TinhDiemXL(HocLuc7,HanhKiem7),
                DiemLop8 = TinhDiemXL(HocLuc8, HanhKiem8),
                DiemLop9 = TinhDiemXL(HocLuc9, HanhKiem9),
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return th;
        }
        double TinhDiemXL(string XepLoaiHL,string XepLoaiHK)
        {
            double DiemHK = XepLoaiHK.Equals("Tốt") ? DataHelper.CurrSettings.HK_GDTX.TOT : XepLoaiHK.Equals("Khá") ?
                    DataHelper.CurrSettings.HK_GDTX.KHA : DataHelper.CurrSettings.HK_GDTX.TRUNG_BINH;
            double DiemHL = XepLoaiHL.Equals("Giỏi") ? DataHelper.CurrSettings.HL_GDTX.GIOI : XepLoaiHL.Equals("Khá") ?
                    DataHelper.CurrSettings.HL_GDTX.KHA : DataHelper.CurrSettings.HL_GDTX.TRUNG_BINH;

            double TongDiem = DiemHK + DiemHL;

            if (TongDiem <= 4) TongDiem = 5;

            return TongDiem;
        }
        public HoSoTrungTuyenGDTX ToHSTT()
        {
            var hs = new HoSoTrungTuyenGDTX()
            {
                IdHSDT = Id,
                MaHoSo = MaHoSo,
                Ho = Ho,
                Ten = Ten,
                DotTS = DotTS,
                CCCD = CCCD,
                TongDXT = TinhDiemXetTuyen(),
                Email = Email,
                GhiChu = GhiChu,
                GioiTinh = GioiTinh,
                HoTenCha = HoTenCha,
                NamSinhCha = NamSinhCha,
                HoTenMe = HoTenMe,
                NamSinhMe = NamSinhMe,
                IdDanToc = IdDanToc,
                NamTN = NamTN,
                NamTS = NamTS,
                HTDT = HTDT,
                TDHV = TDHV,
                Lop = Lop,
                IdNgheTrungTuyen = DsNguyenVong.First(x => x.NV == 1).IdNghe,
                IdQuocTich = IdQuocTich,
                IdTonGiao = IdTonGiao,
                IdTrinhDoVH = IdTrinhDoVH,
                IdTruong = IdTruong,
                MaTinh = MaTinh,
                MaHuyen = MaHuyen,
                ThonDuong = ThonDuong,
                MaXa = MaXa,
                NgaySinh = NgaySinh,
                NgheNghiepCha = NgheNghiepCha,
                NgheNghiepMe = NgheNghiepMe,
                NoiSinh = NoiSinh,
                SDT = SDT,
                DiaChi = DiaChi,
                IdDTUT = IdDTUT,
                IdKVUT = IdKVUT
            };

            return hs;
        }

        public HoSoDuTuyenGDTXView ToView() => new HoSoDuTuyenGDTXView
        {
            Id = Id,
            MaHoSo = MaHoSo,
            Ho = Ho,
            Ten = Ten,
            NgaySinh = NgaySinh,
            GT = GioiTinh ? "Nam" : "Nữ",
            HanhKiem6 = HanhKiem6,
            HanhKiem7 = HanhKiem7,
            HanhKiem8 = HanhKiem8,
            HanhKiem9 = HanhKiem9,
            HocLuc6 = HocLuc6,
            HocLuc7 = HocLuc7,
            HocLuc8 = HocLuc8,
            HocLuc9 = HocLuc9,
            IdDTUT = IdDTUT,
            IdKVUT = IdKVUT,
            IdNgheDT1 = DsNguyenVong.First().IdNghe,
            IdNgheDT2 = DsNguyenVong.FirstOrDefault(x => x.NV == 2) != null ? DsNguyenVong.First(x => x.NV == 2).IdNghe : string.Empty,
            GhiChu = GhiChu,
            IdTruong = IdTruong,
            ThonDuong = ThonDuong,
            MaTinh = MaTinh,
            MaHuyen = MaHuyen,
            MaXa = MaXa,
            Lop = Lop,
            NamTN = NamTN,
            HoTenCha = HoTenCha,
            NamSinhCha = NamSinhCha,
            HoTenMe = HoTenMe,
            NamSinhMe = NamSinhMe,
            NoiSinh = NoiSinh,
            DiaChi = DiaChi,
            CCCD = CCCD,
            BangTN = KiemTraHS.BangTN ? "X" : string.Empty,
            GCNTT = KiemTraHS.GCNTT ? "X" : string.Empty,
            GiayCNUT = KiemTraHS.GiayCNUT ? "X" : string.Empty,
            GiayKhaiSinh = KiemTraHS.GiayKhaiSinh ? "X" : string.Empty,
            GKSK = KiemTraHS.GKSK ? "X" : string.Empty,
            HinhThe = KiemTraHS.HinhThe ? "X" : string.Empty,
            HocBa = KiemTraHS.HocBa ? "X" : string.Empty,
            PhieuDKDT = KiemTraHS.PhieuDKDT ? "X" : string.Empty,
            PhieuDKXTGDTX = KiemTraHS.PhieuDKXTGDTX ? "X" : string.Empty,
            CCCDX = KiemTraHS.CCCD ? "X" : string.Empty,
            SYLL = KiemTraHS.SYLL ? "X" : string.Empty,
            SDT = SDT,
            NamTS = NamTS,
            DotTS = DotTS,
        };

        public override bool Save() => _LiteDb.Upsert(this, TuDien.CategoryName.HoSoDuTuyenGDTX);

        public override bool Delete() => _LiteDb.Delete<HoSoDuTuyenGDTX>(Id, TuDien.CategoryName.HoSoDuTuyenGDTX);

        public override double TinhDiemXetTuyen()
        {
            double DiemXL = TinhDiemXL(HocLuc6, HanhKiem6) + TinhDiemXL(HocLuc7, HanhKiem7) + TinhDiemXL(HocLuc8, HanhKiem8) + TinhDiemXL(HocLuc9, HanhKiem9);
            double DiemUTDT = string.IsNullOrEmpty(IdDTUT) ? 0 : DataHelper.DsDoiTuongUT.First(x => x.Id == IdDTUT).Diem;
            double DiemUTKV = string.IsNullOrEmpty(IdKVUT) ? 0 : DataHelper.DsKhuVucUT.First(x => x.Id == IdKVUT).Diem;
            return DiemXL + DiemUTDT + DiemUTKV;
        }
    }

    public class HoSoDuTuyenGDTXView : DBClass
    {
        [Display(Name = "Mã hồ sơ")]
        public string MaHoSo { get; set; }

        [Display(Name = "Họ học sinh")]
        public string Ho { get; set; }

        [Display(Name = "Tên học sinh")]
        public string Ten { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Giới tính")]
        public string GT { get; set; }
        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Thôn/đường")]
        public string ThonDuong { get; set; }
        [Display(Name = "Tỉnh")]
        public string MaTinh { get; set; }

        [Display(Name = "Huyện")]
        public string MaHuyen { get; set; }
        [Display(Name = "Xã")]
        public string MaXa { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Hạnh kiểm 6")]
        public string HanhKiem6 { get; set; }
        [Display(Name = "Học lực 6")]
        public string HocLuc6 { get; set; }

        [Display(Name = "Hạnh kiểm 7")]
        public string HanhKiem7 { get; set; }
        [Display(Name = "Học lực 7")]
        public string HocLuc7 { get; set; }
        [Display(Name = "Hạnh kiểm 8")]
        public string HanhKiem8 { get; set; }
        [Display(Name = "Học lực 8")]
        public string HocLuc8 { get; set; }
        [Display(Name = "Hạnh kiểm 9")]
        public string HanhKiem9 { get; set; }
        [Display(Name = "Học lực 9")]
        public string HocLuc9 { get; set; }

        [Display(Name = "Đối tượng ưu tiên")]
        public string IdDTUT { get; set; }

        [Display(Name = "Khu vực ưu tiên")]
        public string IdKVUT { get; set; }

        [Display(Name = "Nghề xét tuyển NV1")]
        public string IdNgheDT1 { get; set; }

        [Display(Name = "Nghề xét tuyển NV2")]
        public string IdNgheDT2 { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "CCCD")]
        public string CCCD { get; set; }

        [Display(Name = "Trường")]
        public string IdTruong { get; set; }

        [Display(Name = "Lớp")]
        public string Lop { get; set; }

        [Display(Name = "Năm TN")]
        public string NamTN { get; set; }

        [Display(Name = "Họ tên cha")]
        public string HoTenCha { get; set; }

        [Display(Name = "Năm sinh cha")]
        public string NamSinhCha { get; set; }

        [Display(Name = "Họ tên mẹ")]
        public string HoTenMe { get; set; }

        [Display(Name = "Năm sinh mẹ")]
        public string NamSinhMe { get; set; }

        [Display(Name = "Số ĐT")]
        public string SDT { get; set; }
        [Display(Name = "Phiếu đăng ký xét tuyển GDTX")]
        public string PhieuDKXTGDTX { get; set; }

        [Display(Name = "Phiếu đăng ký dự tuyển")]
        public string PhieuDKDT { get; set; }

        [Display(Name = "Chứng nhận tôt nghiệp")]
        public string GCNTT { get; set; }

        [Display(Name = "GKSK")]
        public string GKSK { get; set; }

        [Display(Name = "GCNUT")]
        public string GiayCNUT { get; set; }

        [Display(Name = "Học bạ")]
        public string HocBa { get; set; }

        [Display(Name = "Bằng TN")]
        public string BangTN { get; set; }

        [Display(Name = "Giấy khai sinh")]
        public string GiayKhaiSinh { get; set; }

        [Display(Name = "CCCD")]
        public string CCCDX { get; set; }
        [Display(Name = "Sơ yếu lý lịch")]
        public string SYLL { get; set; }

        [Display(Name = "Ảnh thẻ")]
        public string HinhThe { get; set; }

        [Display(AutoGenerateField = false)]
        public int NamTS { get; set; }

        [Display(Name = "Đợt tuyển sinh")]
        public int DotTS { get; set; }

        public override bool Delete()
        {
            return _LiteDb.Delete<HoSoDuTuyenGDTX>(Id, TuDien.CategoryName.HoSoDuTuyenGDTX);
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

    }

    public class HoSoTrungTuyenGDTX : HoSo
    {
        [Display(AutoGenerateField = false)]
        public string IdHSDT { get; set; }

        [Display(Name = "Tổng số điểm xét tuyển",Order = 98)]
        public double TongDXT { get; set; }

        [Display(Name = "Nghề trúng tuyển")]
        public string IdNgheTrungTuyen { get; set; }

        public override string CheckError()
        {
            throw new NotImplementedException();
        }

        public override bool Delete() => _LiteDb.Delete<HoSoTrungTuyenGDTX>(Id, TuDien.CategoryName.HoSoTrungTuyenGDTX);

        public override bool Save() => _LiteDb.Upsert(this, TuDien.CategoryName.HoSoTrungTuyenGDTX);

        public override double TinhDiemXetTuyen()
        {
            throw new NotImplementedException();
        }
    }

}
