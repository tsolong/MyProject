using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Web.UploadServices
{
    public class Config
    {
        /// <summary>
        /// 获取Web.config文件中节点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        public static string Get(string NodeName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[NodeName];
        }
    }
}
