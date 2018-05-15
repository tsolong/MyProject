using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.Core.Member
{
    /// <summary>
    /// 会员用户
    /// </summary>
    public class UserInfo
    {
        private int _UserId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private Nullable<DateTime> _LastLoginTime;
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public Nullable<DateTime> LastLoginTime
        {
            get { return _LastLoginTime; }
            set { _LastLoginTime = value; }
        }

        private string _LastLoginIP;
        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        public string LastLoginIP
        {
            get { return _LastLoginIP; }
            set { _LastLoginIP = value; }
        }

        private bool _Locked;
        /// <summary>
        /// 帐户是否被锁定
        /// </summary>
        public bool Locded
        {
            get { return _Locked; }
            set { _Locked = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 帐户创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
