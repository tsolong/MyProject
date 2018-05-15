using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using TL.Model.City;

namespace TL.Config
{
    /// <summary>
    /// ϵͳ����
    /// </summary>
    public class SysConfig
    {
        public static readonly string XmlPath = HttpContext.Current.Server.MapPath("/Config/Sys.config");

        /// <summary>
        /// ��ȡ�����ļ�ָ���ڵ��б�
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        /// <returns>�ڵ��б�</returns>
        public static XmlNodeList GetConfigList(string NodeName)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlPath);
            XmlElement Root = XmlDoc.DocumentElement;
            return Root.GetElementsByTagName(NodeName);
        }

        /// <summary>
        /// ��ȡ�����ļ�ָ���ڵ��б��еĵ�һ���ڵ�ֵ
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        /// <returns>�ڵ�ֵ</returns>
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
        /// ��ȡ���ж����б�
        /// </summary>
        /// <returns>���ж����б�</returns>
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
        /// ��ȡ�������ж���
        /// </summary>
        /// <param name="Domain">���з��ʵ�����</param>
        /// <returns>�������ж���</returns>
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
