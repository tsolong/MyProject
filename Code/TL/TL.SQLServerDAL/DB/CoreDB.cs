using System;
using System.Collections.Generic;
using System.Text;

namespace TL.SQLServerDAL
{
    /// <summary>
    /// �����ݿ�
    /// </summary>
    public class CoreDB : BaseDB
    {
        public CoreDB()
        {
            ConnStr = TL.Config.SysConfig.GetConfigValue("CoreConnectionString");
        }
    }
}
