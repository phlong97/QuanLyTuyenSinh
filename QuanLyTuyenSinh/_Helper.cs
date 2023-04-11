using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace QuanLyTuyenSinh
{
    public static class _Helper
    {
        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        // Lưu List<T> vào tệp JSON
        public static void SaveToJson<T>(List<T> myList, string filePath)
        {
            string json = JsonConvert.SerializeObject(myList);
            File.WriteAllText(filePath, json);
        }

        // Đọc List<T> từ tệp JSON
        public static List<T> LoadFromJson<T>(string filePath)
        {
            List<T> myList = new List<T>();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                myList = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return myList;
        }


        #region Addresss
        public class Adress
        {
            public string AdressCode { get; set; }
            public string AdressName { get; set; }
                   
        }
        /// <summary>
        /// Lấy danh sách tỉnh
        /// từ tệp Catalogue_Dia_Ban_Tinh.xml
        /// </summary>
        /// <returns></returns>     
        public static List<Adress> getListProvince()
        {
            List<Adress> lstReturn = new List<Adress>();
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(QuanLyTuyenSinh.Properties.Resources.Catalogue_Dia_Ban_Tinh);
            var nodes = doc.GetElementsByTagName("Item");
            if (nodes is null)
            {
                return null;
            }
            foreach (System.Xml.XmlNode node in nodes)
            {
                if (node.Attributes != null)
                {
                    var attribute = node.Attributes["Value"];
                    if (attribute != null)
                    {
                        string[] result = attribute.Value.Split("###");
                        lstReturn.Add(new Adress { AdressCode = result[1], AdressName = result[2], });

                    }
                }

            }
            return lstReturn;
        }
        /// <summary>
        /// Lấy danh sách huyện theo mã tỉnh
        /// từ tệp Catalogue_Dia_Ban_Huyen
        /// </summary>
        /// <param name="ProvinceCode"></param>
        /// <returns></returns>
        public static List<Adress> getListDistrict(string? ProvinceCode = null)
        {
            List<Adress> lstReturn = new List<Adress>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(QuanLyTuyenSinh.Properties.Resources.Catalogue_Dia_Ban_Huyen);
            var nodes = doc.GetElementsByTagName("Item");
            if (nodes is null)
            {
                return null;
            }
            foreach (System.Xml.XmlNode node in nodes)
            {
                if (node.Attributes != null)
                {
                    var attribute = node.Attributes["Value"];
                    if (attribute != null)
                    {
                        string[] result = attribute.Value.Split("###");
                        if (ProvinceCode is not null && result[2].StartsWith(ProvinceCode))
                        {
                            lstReturn.Add(new Adress { AdressCode = result[2], AdressName = result[3], });
                        }
                        else
                        {
                            lstReturn.Add(new Adress { AdressCode = result[2], AdressName = result[3], });
                        }

                    }
                }

            }

            return lstReturn;
        }
        /// <summary>
        /// Lấy danh sách phường, xã theo mã huyện
        /// từ tệp Catalogue_Dia_Ban_Xa
        /// </summary>
        /// <param name="DistrictCode"></param>
        /// <returns></returns>
        public static List<Adress> getListWards(string? DistrictCode = null)
        {
            List<Adress> lstReturn = new List<Adress>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(QuanLyTuyenSinh.Properties.Resources.Catalogue_Dia_Ban_Xa);
            var nodes = doc.GetElementsByTagName("Item");
            if (nodes is null)
            {
                return null;
            }
            foreach (System.Xml.XmlNode node in nodes)
            {
                if (node.Attributes != null)
                {
                    var attribute = node.Attributes["Value"];
                    if (attribute != null)
                    {
                        string[] result = attribute.Value.Split("###");
                        if (DistrictCode is not null && result[3].StartsWith(DistrictCode))
                        {
                            lstReturn.Add(new Adress { AdressCode = result[3], AdressName = result[4], });
                        }
                        else
                        {
                            lstReturn.Add(new Adress { AdressCode = result[3], AdressName = result[4], });
                        }
                    }
                }

            }

            return lstReturn;
        }
        #endregion

        public static void ResetDSTonGiao()
        {
            string DsTG = "Không,Phật giáo,Công giáo,Bùi Sơn Kì hương,Cao đài,Chăm Bà la môn," +
                "Đạo tứ Ân Hiếu nghĩa,Giáo hội Các thành hữu Ngày sau của Chúa Giê su Ky tô (Mormon)," +
                "Giáo hội Cơ đốc Phục lâm Việt Nam,Giáo hội Phật đường Nam Tông Minh Sư đạo,Hồi giáo," +
                "Hội thánh Minh lý đạo - Tam Tông Miếu,Phật giáo Hiếu Nghĩa Tà Lơn (Cấp đăng ký hoạt động)," +
                "Phật giáo Hòa Hảo,Tin lành,Tịnh độ Cư sỹ Phật hội Việt Nam,Tôn giáo Baha'i";           
           
            var dstg = DsTG.Split(',');
            List<TonGiao> tonGiaos = new();
            for (int i = 0; i < dstg.Count(); i++)
            {                
               tonGiaos.Add(new TonGiao { Ma = i.ToString("d2"), Ten = dstg[i] });
            };
            SaveToJson(tonGiaos, Path.Combine(TuDien.JSON_FOLDER_PATH, TuDien.DbName.TonGiao));   
                
        }
    }

    
}
