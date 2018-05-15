using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model;
using TL.Model.Core.Sys;

namespace TL.SQLServerDAL.Core.Sys
{
    /// <summary>
    /// ϵͳ�û�
    /// </summary>
    public class User : CoreDB
    {
        /// <summary>
        /// ����¼
        /// </summary>
        /// <param name="SysUser">��¼����</param>
        /// <returns>��¼״̬</returns>
        public LoginState CheckLogin(UserInfo SysUser)
        {
            //��ѯ
            string sql = "select top 1 * from [" + Pre + "_Sys_User] where [UserName]=@UserName";
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = SysUser.UserName;
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);
            bool isExist = false;
            bool Locked = true;
            bool Password = false;
            while (dr.Read())
            {
                if (!Convert.ToBoolean(dr["Locked"])) Locked = false;
                if (Convert.ToString(dr["Password"]) == SysUser.Password) Password = true;
                isExist = true;
                SysUser.UserId = Convert.ToInt32(dr["UserId"]);
            }
            dr.Close();
            if (!isExist) return LoginState.Err_UserNameOrPassword;
            if (Locked) return LoginState.Err_Locked;
            if (!Password) return LoginState.Err_UserNameOrPassword;

            //��ȡ�ϴε�¼��Ϣ
            UserInfo SysUserInfo = GetById(SysUser.UserId);
            //������ε�¼��Ϣ
            UpdateUserLoginInfo(SysUser);
            //�����ϴε�¼��Ϣ
            SysUser.Password = "";
            SysUser.LastLoginTime = SysUserInfo.LastLoginTime;
            SysUser.LastLoginIP = SysUserInfo.LastLoginIP;

            //���ص�¼״̬
            return LoginState.Succeed;
        }

        /// <summary>
        /// ���µ�¼��Ϣ
        /// </summary>
        /// <param name="SysUser">Ҫ���µ��û�����</param>
        /// <returns>���¼�¼������</returns>
        public int UpdateUserLoginInfo(UserInfo SysUser)
        {
            String sql = "update [" + Pre + "_Sys_User] set [LastLoginTime]=@LastLoginTime,[LastLoginIP]=@LastLoginIP where [UserId]=@UserId";
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar[0].Value = SysUser.UserId;
            MyPar[1] = new SqlParameter("@LastLoginTime", SqlDbType.DateTime, 8);
            MyPar[1].Value = SysUser.LastLoginTime;
            MyPar[2] = new SqlParameter("@LastLoginIP", SqlDbType.NVarChar, 50);
            MyPar[2].Value = SysUser.LastLoginIP;
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

            string sql = "select count(*) from [" + Pre + "_Sys_User] where [UserName]=@UserName";
            if ((int)(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar)) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="SysUser">�û�����</param>
        /// <returns>������Ӱ�������</returns>
        public int Add(UserInfo SysUser)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar[0].Value = SysUser.UserName;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = SysUser.Password;

            string sql = "insert into [" + Pre + "_Sys_User]([UserName],[Password]) values(@UserName,@Password)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ɾ���û� ��ɾ����������
        /// </summary>
        /// <param name="UserId">�û���UserId</param>
        /// <returns>������Ӱ������</returns>
        public int Del(string UserId)
        {
            /*SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.NVarChar, 1000);
            MyPar.Value = UserId;*/
            string sql = "delete from [" + Pre + "_Sys_User] where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="UserId">�û���UserId</param>
        /// <param name="Password">�µ�����</param>
        /// <returns>������Ӱ������</returns>
        public int ChangePassword(int UserId, string Password)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@UserId", SqlDbType.Int, 8);
            MyPar[0].Value = UserId;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = Password;

            string sql = "update [" + Pre + "_Sys_User] set [Password]=@Password where [UserId]=@UserId";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// �����û� ��������������
        /// </summary>
        /// <param name="UserId">�û���UserId</param>
        /// <returns>������Ӱ������</returns>
        public int Locked(string UserId)
        {
            string sql = "update [" + Pre + "_Sys_User] set [Locked]=1 where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// �����û� �ɽ�����������
        /// </summary>
        /// <param name="UserId">�û���UserId</param>
        /// <returns>������Ӱ������</returns>
        public int UnLocked(string UserId)
        {
            string sql = "update [" + Pre + "_Sys_User] set [Locked]=0 where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// ��DataReader�е�����ת��Ϊ���󼯺�
        /// </summary>
        /// <param name="dr">DataReader����</param>
        /// <returns>���󼯺�</returns>
        public IList<UserInfo> DrRead(SqlDataReader dr)
        {
            IList<UserInfo> SysUserList = new List<UserInfo>();
            while (dr.Read())
            {
                UserInfo SysUser = new UserInfo();
                SysUser.UserId = Convert.ToInt32(dr["UserId"]);
                SysUser.UserName = Convert.ToString(dr["UserName"]);
                SysUser.Password = Convert.ToString(dr["Password"]);
                //if (dr["LastLoginTime"].Equals(DBNull.Value))
                //if (dr["LastLoginTime"] == DBNull.Value)
                if (Convert.IsDBNull(dr["LastLoginTime"])) SysUser.LastLoginTime = null; else SysUser.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                if (dr["LastLoginIP"] == DBNull.Value) SysUser.LastLoginIP = ""; else SysUser.LastLoginIP = dr["LastLoginIP"].ToString();
                SysUser.Locded = Convert.ToBoolean(dr["Locked"]);
                SysUser.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                SysUserList.Add(SysUser);
            }
            dr.Close();
            return SysUserList;
        }

        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <returns>�û�����</returns>
        public UserInfo GetById(int UserId)
        {
            string sql = "select top 1 * from [" + Pre + "_Sys_User] where [UserId]=@UserId";
            SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.BigInt, 8);
            MyPar.Value = UserId;

            IList<UserInfo> SysUserList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (SysUserList.Count > 0)
                return SysUserList[0];
            else
                return null;
        }

        /// <summary>
        /// ��ȡ�û������б�
        /// </summary>
        /// <param name="PageIndex">ҳ��</param>
        /// <param name="PageSize">ÿҳ��������</param>
        /// <param name="RecordTotal">�洢���̷��ؼ�¼����</param>
        /// <returns>�û������б�</returns>
        public IList<UserInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[6];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Sys_User]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 100);
            MyPar[1].Value = "*";
            /*MyPar[2] = new SqlParameter("@SelectWhere", SqlDbType.VarChar, 100);
            MyPar[2].Value = "[locked]=0";*/
            MyPar[2] = new SqlParameter("@OrderColumnName", SqlDbType.VarChar, 100);
            MyPar[2].Value = "UserId";
            MyPar[3] = new SqlParameter("@PageSize", SqlDbType.VarChar, 100);
            MyPar[3].Value = PageSize;
            MyPar[4] = new SqlParameter("@PageIndex", SqlDbType.VarChar, 100);
            MyPar[4].Value = PageIndex;
            MyPar[5] = new SqlParameter("@RecordTotal", SqlDbType.Int, 8);
            MyPar[5].Direction = ParameterDirection.Output;

            SqlDataReader dr = SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar);
            IList<UserInfo> SysUserList = DrRead(dr);
            RecordTotal = Convert.ToInt32(MyPar[5].Value);
            return SysUserList;
        }
    }
}
