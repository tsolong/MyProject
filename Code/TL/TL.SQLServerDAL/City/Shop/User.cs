using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model;
using TL.Model.City.Shop;

namespace TL.SQLServerDAL.City.Shop
{
    /// <summary>
    /// ���е����û�
    /// </summary>
    public class User : CityDB
    {
        /// <summary>
        /// ����¼
        /// </summary>
        /// <param name="ShopUser">��¼����</param>
        /// <returns>��¼״̬</returns>
        public LoginState CheckLogin(UserInfo ShopUser)
        {
            //��ѯ
            string sql = "select top 1 * from [" + Pre + "_Shop_User] where [UserName]=@UserName";
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = ShopUser.UserName;
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);
            bool isExist = false;
            bool Locked = true;
            bool Password = false;
            while (dr.Read())
            {
                if (!Convert.ToBoolean(dr["Locked"])) Locked = false;
                if (Convert.ToString(dr["Password"]) == ShopUser.Password) Password = true;
                isExist = true;
                ShopUser.UserId = Convert.ToInt32(dr["UserId"]);
            }
            dr.Close();
            if (!isExist) return LoginState.Err_UserNameOrPassword;
            if (Locked) return LoginState.Err_Locked;
            if (!Password) return LoginState.Err_UserNameOrPassword;

            //��ȡ�ϴε�¼��Ϣ
            UserInfo ShopUserInfo = GetById(ShopUser.UserId);
            //������ε�¼��Ϣ
            UpdateUserLoginInfo(ShopUser);
            //�����ϴε�¼��Ϣ
            ShopUser.Password = "";
            ShopUser.Email = ShopUserInfo.Email;
            ShopUser.LastLoginTime = ShopUserInfo.LastLoginTime;
            ShopUser.LastLoginIP = ShopUserInfo.LastLoginIP;

            //���ص�¼״̬
            return LoginState.Succeed;
        }

        /// <summary>
        /// ���µ�¼��Ϣ
        /// </summary>
        /// <param name="SysUser">Ҫ���µ��û�����</param>
        /// <returns>���¼�¼������</returns>
        public int UpdateUserLoginInfo(UserInfo ShopUser)
        {
            String sql = "update [" + Pre + "_Shop_User] set [LastLoginTime]=@LastLoginTime,[LastLoginIP]=@LastLoginIP where [UserId]=@UserId";
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar[0].Value = ShopUser.UserId;
            MyPar[1] = new SqlParameter("@LastLoginTime", SqlDbType.DateTime, 8);
            MyPar[1].Value = ShopUser.LastLoginTime;
            MyPar[2] = new SqlParameter("@LastLoginIP", SqlDbType.NVarChar, 50);
            MyPar[2].Value = ShopUser.LastLoginIP;
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ����û����Ƿ��Ѵ���
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <returns>�Ѵ��ڷ���true �����ڷ���false</returns>
        public bool CheckUserNameIsExist(string UserName)
        {
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = UserName;

            string sql = "select count(*) from [" + Pre + "_Shop_User] where [UserName]=@UserName";
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

            string sql = "select count(*) from [" + Pre + "_Shop_User] where [Email]=@Email";
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
        public int Add(UserInfo ShopUser)
        {
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar[0].Value = ShopUser.UserName;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = ShopUser.Password;
            MyPar[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[2].Value = ShopUser.Email;

            string sql = "insert into [" + Pre + "_Shop_User]([UserName],[Password],[Email]) values(@UserName,@Password,@Email)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="UserId">�û���UserId</param>
        /// <param name="Password">������</param>
        /// <returns>������Ӱ������</returns>
        public int ChangePassword(int UserId, string Password)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@UserId", SqlDbType.Int, 8);
            MyPar[0].Value = UserId;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = Password;

            string sql = "update [" + Pre + "_Shop_User] set [Password]=@Password where [UserId]=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ��DataReader�е�����ת��Ϊ���󼯺�
        /// </summary>
        /// <param name="dr">DataReader����</param>
        /// <returns>���󼯺�</returns>
        public IList<UserInfo> DrRead(SqlDataReader dr)
        {
            IList<UserInfo> ShopUserList = new List<UserInfo>();
            while (dr.Read())
            {
                UserInfo ShopUser = new UserInfo();
                ShopUser.UserId = Convert.ToInt32(dr["UserId"]);
                ShopUser.UserName = Convert.ToString(dr["UserName"]);
                ShopUser.Password = Convert.ToString(dr["Password"]);
                ShopUser.Email = Convert.ToString(dr["Email"]);
                //if (dr["LastLoginTime"].Equals(DBNull.Value))
                //if (dr["LastLoginTime"] == DBNull.Value)
                if (Convert.IsDBNull(dr["LastLoginTime"])) ShopUser.LastLoginTime = null; else ShopUser.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                if (dr["LastLoginIP"] == DBNull.Value) ShopUser.LastLoginIP = ""; else ShopUser.LastLoginIP = dr["LastLoginIP"].ToString();
                ShopUser.Locded = Convert.ToBoolean(dr["Locked"]);
                ShopUser.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                ShopUserList.Add(ShopUser);
            }
            dr.Close();
            return ShopUserList;
        }

        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <returns>�û�����</returns>
        public UserInfo GetById(int UserId)
        {
            string sql = "select top 1 * from [" + Pre + "_Shop_User] where [UserId]=@UserId";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            IList<UserInfo> ShopUserList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (ShopUserList.Count > 0)
                return ShopUserList[0];
            else
                return null;
        }
    }
}
