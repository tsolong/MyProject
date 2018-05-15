using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// ����
    /// </summary>
    public class CityInfo
    {
        private string _Name;
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get{return _Name;}
            set{_Name =value;}
        }

        private string _EName;
        /// <summary>
        /// ����Ӣ������
        /// </summary>
        public string EName
        {
            get { return _EName; }
            set { _EName = value; }
        }

        private string _Code;
        /// <summary>
        /// ���е绰����
        /// </summary>
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Domain;
        /// <summary>
        /// ���з�������
        /// </summary>
        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        private string _PicturesUrl;
        /// <summary>
        /// ����ͼƬ���ʵ�ַ
        /// </summary>
        public string PicturesUrl
        {
            get { return _PicturesUrl; }
            set { _PicturesUrl = value; }
        }

        private string _UploadServicesUrl;
        /// <summary>
        /// �����ϴ�ͼƬ��Web�����ַ
        /// </summary>
        public string UploadServicesUrl
        {
            get { return _UploadServicesUrl; }
            set { _UploadServicesUrl = value; }
        }

        private string _UploadServicesNamespace;
        /// <summary>
        /// Web���������ռ�
        /// </summary>
        public string UploadServicesNamespace
        {
            get { return _UploadServicesNamespace; }
            set { _UploadServicesNamespace = value; }
        }

        private string _UploadServicesPassword;
        /// <summary>
        /// Web�����������
        /// </summary>
        public string UploadServicesPassword
        {
            get { return _UploadServicesPassword; }
            set { _UploadServicesPassword = value; }
        }

        private string _ConnectionString;
        /// <summary>
        /// �������ݿ�����������ַ���
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
    }
}
