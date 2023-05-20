using System;

public class BangGia
{
	string Id { get; set; }	
	string Ten { get; set; }
	List<CTBangGia> Ds { get; set; }
}

public class CTBangGia
{
	int ApDung { get; set; }	// 0: Tất cả, 1:Nhóm sp, 2:Sản phẩm, 3:Biến thể sp
	string Ma { get; set; } // Dựa vào ApDung
	public DateTime NgayApDung { get; set; }
	public DateTime NgayKetThuc { get; set; }
    public double SLToiThieu { get; set; }
	public double GiaCoDinh { get; set; }
	public double ChietKhau { get; set; }
    public double PhuPhi { get; set; }
}

public class BangGiaApDung
{
	int ApDung { get; set; } // 0: Tất cả, 2: 1 khách, 2: Nhóm khách
	string Ma { get; set; } // Dựa vào ApDung
	string IdBangGia { get; set; }
}