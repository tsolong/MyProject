using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City.Shop
{
    /// <summary>
    /// ������Ƭ
    /// </summary>
    public class PhotoInfo
    {
        private int _Id;
        /// <summary>
        /// ��Ƭ���
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Url;
        /// <summary>
        /// ��Ƭ��ַ
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private string _Ext;
        /// <summary>
        /// ��Ƭ�ļ���չ��
        /// </summary>
        public string Ext
        {
            get { return _Ext; }
            set { _Ext = value; }
        }

        private string _Description;
        /// <summary>
        /// ��Ƭ����
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private DateTime _AddDate;
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime AddDate
        {
            get { return _AddDate; }
            set { _AddDate = value; }
        }

        private int _OrderNum;
        /// <summary>
        /// ��Ƭ����
        /// </summary>
        public int OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }

        private int _UserId;
        /// <summary>
        /// �����û����
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
    }
}
