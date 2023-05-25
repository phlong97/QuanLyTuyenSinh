using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            [Display(Name = "Mã địa chỉ")]
            public string AdressCode { get; set; }

            [Display(Name = "Tên địa chỉ")]
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
        public static List<Adress> getListDistrict(object ProvinceCode)
        {
            List<Adress> lstReturn = new List<Adress>();
            if (ProvinceCode == null) return lstReturn;
            string code = ProvinceCode.ToString();
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
                        if (!string.IsNullOrEmpty(code) && result[2].StartsWith(code))
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
        public static List<Adress> getListWards(object DistrictCode)
        {
            List<Adress> lstReturn = new List<Adress>();
            if (DistrictCode == null) return lstReturn;
            string code = DistrictCode.ToString();
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
                        if (!string.IsNullOrEmpty(code) && result[3].StartsWith(code))
                        {
                            lstReturn.Add(new Adress { AdressCode = result[3], AdressName = result[4], });
                        }
                    }
                }
            }

            return lstReturn;
        }

        #endregion Addresss

        public static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }

        /// <summary>
        /// Extension method to convert dynamic data to a DataTable. Useful for databinding.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>A DataTable with the copied dynamic data.</returns>
        public static DataTable ToDataTable(this IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }

        public static string ToIIVX(int n)
        {
            string s = string.Empty;
            while (n >= 1000) { s += "M"; n -= 1000; }
            if (n >= 900) { s += "CM"; n -= 900; }
            if (n >= 500) { s += "D"; n -= 500; }
            if (n >= 400) { s += "CD"; n -= 400; }
            while (n >= 100) { s += "C"; n -= 100; }
            if (n >= 90) { s += "XC"; n -= 90; }
            if (n >= 50) { s += "L"; n -= 50; }
            if (n >= 40) { s += "XL"; n -= 40; }
            while (n >= 10) { s += "X"; n -= 10; }
            if (n >= 9) { s += "IX"; n -= 9; }
            if (n >= 5) { s += "V"; n -= 5; }
            if (n >= 4) { s += "IV"; n -= 4; }
            while (n >= 1) { s += "I"; n -= 1; }
            return s;
        }
    }

    public static class DevForm
    {
        public static void CreateSearchLookupEdit(SearchLookUpEdit lookUp, string display, string member, IList source = null, string nullText = "(Trống)")
        {
            if (lookUp == null)
            {
                return;
            }
            if (source is not null) lookUp.Properties.DataSource = source;
            lookUp.Properties.DisplayMember = display;
            lookUp.Properties.ValueMember = member;
            lookUp.Properties.BestFitMode = BestFitMode.BestFit;
            lookUp.Properties.AutoHeight = true;
            lookUp.Properties.NullText = nullText;
        }

        public static void CreateComboboxEdit(ComboBoxEdit cbb, string[] lst, string nullText = "(Trống)", bool allowEdit = false, bool autoComplete = false)
        {
            cbb.Properties.Items.Clear();
            cbb.Properties.NullText = nullText;
            cbb.Properties.TextEditStyle = allowEdit ? TextEditStyles.Standard : TextEditStyles.DisableTextEditor;
            cbb.Properties.AutoComplete = autoComplete;

            cbb.Properties.Items.AddRange(lst);
        }

        public static void CreateRepositoryItemLookUpEdit(GridView gridView, IList lst, string fieldName, string display, string value, string nullText = "(Trống)")
        {
            var col = gridView.Columns.ColumnByFieldName(fieldName);
            if (col != null)
            {
                col.ColumnEdit = new RepositoryItemLookUpEdit()
                {
                    DataSource = lst,
                    DisplayMember = display,
                    ValueMember = value,
                    NullText = nullText,
                };
            }
        }

        public static int GetVisibleToUsersColumnCount(GridView view)
        {
            GridViewInfo info = view.GetViewInfo() as GridViewInfo;
            int result = 0;
            for (int i = 0; i < view.VisibleColumns.Count; i++)
                if (info.GetColumnLeftCoord(view.GetVisibleColumn(i)) < view.ViewRect.Width - info.ViewRects.IndicatorWidth)
                    result++;
            return result;
        }
    }

    public static class PasswordHasher
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var hasher = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = hasher.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string newHash = HashPassword(enteredPassword, storedSalt);
            return newHash == storedHash;
        }
    }
}