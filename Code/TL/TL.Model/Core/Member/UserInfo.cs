using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.Core.Member
{
    /// <summary>
    /// ��Ա�û�
    /// </summary>
    public class UserInfo
    {
        private int _UserId;
        /// <summary>
        /// �û����
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password;
        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _Email;
        /// <summary>
        /// ����
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private Nullable<DateTime> _LastLoginTime;
        /// <summary>
        /// ���һ�ε�¼ʱ��
        /// </summary>
        public Nullable<DateTime> LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; }
        }

        private string _LastLoginIP;
        /// <summary>
        /// ���һ�ε�¼IP
        /// </summary>
        public string LastLoginIP
        {
            get { return _LastLoginIP; }
            set { _LastLoginIP = value; }
        }

        private bool _Locked;
        /// <summary>
        /// �ʻ��Ƿ�����
        /// </summary>
        public bool Locded
        {
            get { return _Locked; }
            set { _Locked = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// �ʻ�����ʱ��
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
