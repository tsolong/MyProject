using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// 城市
    /// </summary>
    public class CityInfo
    {
        private string _Name;
        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name
        {
            get{return _Name;}
            set{_Name =value;}
        }

        private string _EName;
        /// <summary>
        /// 城市英文名称
        /// </summary>
        public string EName
        {
            get { return _EName; }
            set { _EName = value; }
        }

        private string _Code;
        /// <summary>
        /// 城市电话区号
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Domain;
        /// <summary>
        /// 城市访问域名
        /// </summary>
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        private string _PicturesUrl;
        /// <summary>
        /// 城市图片访问地址
        /// </summary>
        public string PicturesUrl
        {
            get { return _PicturesUrl; }
            set { _PicturesUrl = value; }
        }

        private string _UploadServicesUrl;
        /// <summary>
        /// 城市上传图片的Web服务地址
        /// </summary>
        public string UploadServicesUrl
        {
            get { return _UploadServicesUrl; }
            set { _UploadServicesUrl = value; }
        }

        private string _UploadServicesNamespace;
        /// <summary>
        /// Web服务命名空间
        /// </summary>
        public string UploadServicesNamespace
        {
            get { return _UploadServicesNamespace; }
            set { _UploadServicesNamespace = value; }
        }

        private string _UploadServicesPassword;
        /// <summary>
        /// Web服务调用密码
        /// </summary>
        public string UploadServicesPassword
        {
            get { return _UploadServicesPassword; }
            set { _UploadServicesPassword = value; }
        }

        private string _ConnectionString;
        /// <summary>
        /// 城市数据库服务器连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
    }
}
