using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Web.UploadServices
{
    public class Config
    {
        /// <summary>
        /// ��ȡWeb.config�ļ��нڵ��ֵ
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        public static string Get(string NodeName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[NodeName];
        }
    }
}
