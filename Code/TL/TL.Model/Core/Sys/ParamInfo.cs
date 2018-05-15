using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.Core.Sys
{
    /// <summary>
    /// ϵͳ����
    /// </summary>
    public class ParamInfo
    {
        private int _Id;
        /// <summary>
        /// ���
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _SiteName;
        /// <summary>
        ///վ������
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _SiteSubName;
        /// <summary>
        /// վ���ӱ���
        /// </summary>
        public string SiteSubName
        {
            get { return _SiteSubName; }
            set { _SiteSubName = value; }
        }

        private string _SiteDomain;
        /// <summary>
        /// վ������
        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        private string _SiteEmail;
        /// <summary>
        /// վ������
        /// </summary>
        public string SiteEmail
        {
            get { return _SiteEmail; }
            set { _SiteEmail = value; }
        }

        private string _Keywords;
        /// <summary>
        /// �ؼ���
        /// </summary>
        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }

        private string _Description;
        /// <summary>
        /// ��ҳ����
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Copyright;
        /// <summary>
        /// ��Ȩ��Ϣ
        /// </summary>
        public string Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; }
        }
    }
}
