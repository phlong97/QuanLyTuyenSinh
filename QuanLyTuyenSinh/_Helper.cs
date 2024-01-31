using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Application = Microsoft.Office.Interop.Word.Application;

namespace QuanLyTuyenSinh
{
    public static class _Helper
    {
        public static void FindAndReplace(Application app, string findText, string replaceWithText)
        {
            Find findObject = app.Selection.Find;
            findObject.ClearFormatting();
            findObject.Text = findText;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = replaceWithText;
            object missing = System.Reflection.Missing.Value;
            object replaceAll = WdReplace.wdReplaceAll;
            findObject.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        }
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                myList = JsonConvert.DeserializeObject<List<T>>(json);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            return myList;
        }

        #region Addresss

        public class Address
        {
            [Display(Name = "Mã địa chỉ")]
            public string AddressCode { get; set; }

            [Display(Name = "Tên địa chỉ")]
            public string AddressName { get; set; }
        }
        public class Address2
        {
            [Display(Name = "Mã địa chỉ cha")]
            public string AddressCodeParent { get; set; }
            [Display(Name = "Mã địa chỉ")]
            public string AddressCode { get; set; }

            [Display(Name = "Tên địa chỉ")]
            public string AddressName { get; set; }
        }
        /// <summary>
        /// Lấy danh sách tỉnh
        /// từ tệp Catalogue_Dia_Ban_Tinh.xml
        /// </summary>
        /// <returns></returns>
        public static List<Address> getListProvince()
        {
            List<Address> lstReturn = new List<Address>();
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
                        lstReturn.Add(new Address { AddressCode = result[1], AddressName = result[2], });
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
        public static List<Address> getListDistrict(object ProvinceCode)
        {
            List<Address> lstReturn = new List<Address>();
            if (ProvinceCode == null) return lstReturn;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string code = ProvinceCode.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
                            lstReturn.Add(new Address { AddressCode = result[2], AddressName = result[3], });
                        }
                    }
                }
            }

            return lstReturn;
        }
        /// <summary>
        /// Lấy danh sách huyện
        /// từ tệp Catalogue_Dia_Ban_Huyen
        /// </summary>
        /// <param name="ProvinceCode"></param>
        /// <returns></returns>
        public static List<Address> getListDistrict()
        {
            List<Address> lstReturn = new List<Address>();
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
                        lstReturn.Add(new Address { AddressCode = result[2], AddressName = result[3], });
                    }
                }
            }

            return lstReturn;
        }
        public static List<Address2> getListDistrict2()
        {
            List<Address2> lstReturn = new();
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
                        lstReturn.Add(new Address2 { AddressCodeParent = result[1], AddressCode = result[2], AddressName = result[3], });
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
        public static List<Address> getListWards(object DistrictCode)
        {
            List<Address> lstReturn = new List<Address>();
            if (DistrictCode == null) return lstReturn;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string code = DistrictCode.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
                            lstReturn.Add(new Address { AddressCode = result[3], AddressName = result[4], });
                        }
                    }
                }
            }

            return lstReturn;
        }
        /// <summary>
        /// Lấy danh sách phường, xã
        /// từ tệp Catalogue_Dia_Ban_Xa
        /// </summary>
        /// <param name="DistrictCode"></param>
        /// <returns></returns>
        public static List<Address> getListWards()
        {
            List<Address> lstReturn = new List<Address>();
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
                        lstReturn.Add(new Address { AddressCode = result[3], AddressName = result[4], });
                    }
                }
            }

            return lstReturn;
        }
        public static List<Address2> getListWards2()
        {
            List<Address2> lstReturn = new();
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
                        lstReturn.Add(new Address2 { AddressCodeParent = result[2], AddressCode = result[3], AddressName = result[4], });
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
        public static System.Data.DataTable ToDataTable(this IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new System.Data.DataTable();
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
        public static void CreateSearchLookupEdit(SearchLookUpEdit lookUp, string display, string member, IList? source = null, string nullText = "(Trống)")
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            GridViewInfo info = view.GetViewInfo() as GridViewInfo;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            int result = 0;
            for (int i = 0; i < view.VisibleColumns.Count; i++)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (info.GetColumnLeftCoord(view.GetVisibleColumn(i)) < view.ViewRect.Width - info.ViewRects.IndicatorWidth)
                    result++;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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