using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using TL.Model.City;

namespace TL.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SysConfig
    {
        public static readonly string XmlPath = HttpContext.Current.Server.MapPath("/Config/Sys.config");

        /// <summary>
        /// 获取配置文件指定节点列表
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <returns>节点列表</returns>
        public static XmlNodeList GetConfigList(string NodeName)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlPath);
            XmlElement Root = XmlDoc.DocumentElement;
            return Root.GetElementsByTagName(NodeName);
        }

        /// <summary>
        /// 获取配置文件指定节点列表中的第一个节点值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <returns>节点值</returns>
        public static string GetConfigValue(string NodeName)
        {
            try
            {
                return GetConfigList(NodeName)[0].InnerText;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取城市对象列表
        /// </summary>
        /// <returns>城市对象列表</returns>
        public static IList<CityInfo> GetCityList()
        {
            IList<CityInfo> CityList = new List<CityInfo>();
            XmlNodeList NodeList = GetConfigList("City");
            for (int i = 0; i < NodeList.Count; i++)
            {
                CityInfo NewCity = new CityInfo();
                NewCity.Name = NodeList[i].Attributes["Name"].Value;
                NewCity.EName = NodeList[i].Attributes["EName"].Value;
                NewCity.Code = NodeList[i].Attributes["Code"].Value;
                NewCity.Domain = NodeList[i].Attributes["Domain"].Value;
                NewCity.PicturesUrl = NodeList[i].Attributes["PicturesUrl"].Value;
                NewCity.UploadServicesUrl = NodeList[i].Attributes["UploadServicesUrl"].Value;
                NewCity.UploadServicesNamespace = NodeList[i].Attributes["UploadServicesNamespace"].Value;
                NewCity.UploadServicesPassword = NodeList[i].Attributes["UploadServicesPassword"].Value;
                NewCity.ConnectionString = NodeList[i].Attributes["ConnectionString"].Value;
                CityList.Add(NewCity);
            }
            return CityList;
        }
        
        /// <summary>
        /// 获取单个城市对象
        /// </summary>
        /// <param name="Domain">城市访问的域名</param>
        /// <returns>单个城市对象</returns>
        public static CityInfo GetCityByDomain(string Domain)
        {
            CityInfo NewCity = null;
            XmlNodeList NodeList = GetConfigList("City");
            for (int i = 0; i < NodeList.Count; i++)
            {
                if (Domain == NodeList[i].Attributes["Domain"].Value)
                {
                    NewCity = new CityInfo();
                    NewCity.Name = NodeList[i].Attributes["Name"].Value;
                    NewCity.EName = NodeList[i].Attributes["EName"].Value;
                    NewCity.Code = NodeList[i].Attributes["Code"].Value;
                    NewCity.Domain = NodeList[i].Attributes["Domain"].Value;
                    NewCity.PicturesUrl = NodeList[i].Attributes["PicturesUrl"].Value;
                    NewCity.UploadServicesUrl = NodeList[i].Attributes["UploadServicesUrl"].Value;
                    NewCity.UploadServicesNamespace = NodeList[i].Attributes["UploadServicesNamespace"].Value;
                    NewCity.UploadServicesPassword = NodeList[i].Attributes["UploadServicesPassword"].Value;
                    NewCity.ConnectionString = NodeList[i].Attributes["ConnectionString"].Value;
                    break;
                }
            }
            return NewCity;
        }
    }
}
