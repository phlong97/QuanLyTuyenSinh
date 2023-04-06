using DevExpress.CodeParser;
using DevExpress.XtraReports.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string[] result = node.OuterXml.Replace("\" />","").Split("###");
                lstReturn.Add(new Adress { AdressCode = result[1], AdressName = result[2], });
            }
            return lstReturn;
        }
        /// <summary>
        /// Lấy danh sách huyện theo mã tỉnh
        /// từ tệp Catalogue_Dia_Ban_Huyen
        /// </summary>
        /// <param name="ProvinceCode"></param>
        /// <returns></returns>
        public static List<Adress> getListDistrict(string ProvinceCode)
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
                string[] result = node.OuterXml.Replace("\" />", "").Split("###");
                if (result[2].StartsWith(ProvinceCode))
                {
                    lstReturn.Add(new Adress { AdressCode = result[2], AdressName = result[3], });
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
        public static List<Adress> getListWards(string DistrictCode)
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
                string[] result = node.OuterXml.Replace("\" />", "").Split("###");
                if (result[3].StartsWith(DistrictCode))
                {
                    lstReturn.Add(new Adress { AdressCode = result[3], AdressName = result[4], });
                }                
            }

            return lstReturn;
        }
    }

    public class Adress
    {
        public string AdressCode { get; set; }
        public string AdressName { get; set; }
    }
}
