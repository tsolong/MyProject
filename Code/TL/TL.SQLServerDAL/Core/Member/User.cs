using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.Core.Member;

namespace TL.SQLServerDAL.Core.Member
{
    /// <summary>
    /// ��Ա�û�
    /// </summary>
    public class User : CoreDB
    {
        /// <summary>
        /// ����û����Ƿ��Ѵ���
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <returns>�Ѵ��ڷ���true �����ڷ���false</returns>
        public bool CheckUserNameIsExist(string UserName)
        {
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = UserName;

            string sql = "select count(*) from [" + Pre + "_Member_User] where [UserName]=@UserName";
            if ((int)(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar)) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ����û������Ƿ��Ѵ���
        /// </summary>
        /// <param name="Email">�����ַ</param>
        /// <returns>�Ѵ��ڷ���true �����ڷ���false</returns>
        public bool CheckEmailIsExist(string Email)
        {
            SqlParameter MyPar = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar.Value = Email;

            string sql = "select count(*) from [" + Pre + "_Member_User] where [Email]=@Email";
            if ((int)(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar)) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="ShopUser">�û�����</param>
        /// <returns>������Ӱ�������</returns>
        public int Add(UserInfo MemberUser)
        {
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar[0].Value = MemberUser.UserName;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = MemberUser.Password;
            MyPar[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[2].Value = MemberUser.Email;

            string sql = "insert into [" + Pre + "_Member_User]([UserName],[Password],[Email]) values(@UserName,@Password,@Email)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }
    }
}
