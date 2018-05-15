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
    /// 系统用户
    /// </summary>
    public class User : CoreDB
    {
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="SysUser">登录对象</param>
        /// <returns>登录状态</returns>
        public LoginState CheckLogin(UserInfo SysUser)
        {
            //查询
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

            //获取上次登录信息
            UserInfo SysUserInfo = GetById(SysUser.UserId);
            //更新这次登录信息
            UpdateUserLoginInfo(SysUser);
            //更新上次登录信息
            SysUser.Password = "";
            SysUser.LastLoginTime = SysUserInfo.LastLoginTime;
            SysUser.LastLoginIP = SysUserInfo.LastLoginIP;

            //返回登录状态
            return LoginState.Succeed;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="SysUser">要更新的用户对象</param>
        /// <returns>更新记录的条数</returns>
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
        /// 检查用户名是否已存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>已存在返回true 不存在返回false</returns>
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
        /// 添加用户
        /// </summary>
        /// <param name="SysUser">用户对象</param>
        /// <returns>操作所影响的行数</returns>
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
        /// 删除用户 可删除单个或多个
        /// </summary>
        /// <param name="UserId">用户的UserId</param>
        /// <returns>操作所影响行数</returns>
        public int Del(string UserId)
        {
            /*SqlParameter MyPar = new SqlParameter("@UserId", SqlDbType.NVarChar, 1000);
            MyPar.Value = UserId;*/
            string sql = "delete from [" + Pre + "_Sys_User] where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserId">用户的UserId</param>
        /// <param name="Password">新的密码</param>
        /// <returns>操作所影响行数</returns>
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
        /// 锁定用户 可锁定单个或多个
        /// </summary>
        /// <param name="UserId">用户的UserId</param>
        /// <returns>操作所影响行数</returns>
        public int Locked(string UserId)
        {
            string sql = "update [" + Pre + "_Sys_User] set [Locked]=1 where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 解锁用户 可解锁单个或多个
        /// </summary>
        /// <param name="UserId">用户的UserId</param>
        /// <returns>操作所影响行数</returns>
        public int UnLocked(string UserId)
        {
            string sql = "update [" + Pre + "_Sys_User] set [Locked]=0 where UserId in(" + UserId + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 将DataReader中的数据转换为对象集合
        /// </summary>
        /// <param name="dr">DataReader对象</param>
        /// <returns>对象集合</returns>
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
        /// 获取单个用户对象
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns>用户对象</returns>
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
        /// 获取用户对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>用户对象列表</returns>
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
