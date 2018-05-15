using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TL.Model.Core.Sys;

namespace TL.SQLServerDAL.Core.Sys
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class Param : CoreDB
    {
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns>系统参数对象</returns>
        public ParamInfo Get()
        {
            ParamInfo SysParam = null;
            string sql = "select top 1 * from [" + Pre + "_Sys_Param]";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);
            while (dr.Read())
            {
                SysParam = new ParamInfo();
                /*SysParam.Id = Convert.ToInt32(dr["Id"]);*/
                SysParam.SiteName = Convert.ToString(dr["SiteName"]);
                SysParam.SiteSubName = Convert.ToString(dr["SiteSubName"]);
                SysParam.SiteDomain = Convert.ToString(dr["SiteDomain"]);
                SysParam.SiteEmail = Convert.ToString(dr["SiteEmail"]);
                SysParam.Keywords = Convert.ToString(dr["Keywords"]);
                SysParam.Description = Convert.ToString(dr["Description"]);
                SysParam.Copyright = Convert.ToString(dr["Copyright"]);
            }
            dr.Close();
            return SysParam;
        }

        /// <summary>
        /// 保存系统参数
        /// </summary>
        /// <param name="SysParam">系统参数对象</param>
        /// <returns>操作所影响行数</returns>
        public int Save(ParamInfo SysParam)
        {
            String sql = "update [" + Pre + "_Sys_Param] set [SiteName]=@SiteName,[SiteSubName]=@SiteSubName,[SiteDomain]=@SiteDomain,[SiteEmail]=@SiteEmail,[Keywords]=@Keywords,[Description]=@Description,[Copyright]=@Copyright";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, GetParameters(SysParam));
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="SysParam">系统参数对象</param>
        /// <returns>参数集合</returns>
        private SqlParameter[] GetParameters(ParamInfo SysParam)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
            MyPar[0].Value = SysParam.SiteName;
            MyPar[1] = new SqlParameter("@SiteSubName", SqlDbType.NVarChar, 50);
            MyPar[1].Value = SysParam.SiteSubName;
            MyPar[2] = new SqlParameter("@SiteDomain", SqlDbType.NVarChar, 50);
            MyPar[2].Value = SysParam.SiteDomain;
            MyPar[3] = new SqlParameter("@SiteEmail", SqlDbType.NVarChar, 50);
            MyPar[3].Value = SysParam.SiteEmail;
            MyPar[4] = new SqlParameter("@Keywords", SqlDbType.NVarChar, 50);
            MyPar[4].Value = SysParam.Keywords;
            MyPar[5] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
            MyPar[5].Value = SysParam.Description;
            MyPar[6] = new SqlParameter("@Copyright", SqlDbType.NText);
            MyPar[6].Value = SysParam.Copyright;
            return MyPar;
        }
    }
}
