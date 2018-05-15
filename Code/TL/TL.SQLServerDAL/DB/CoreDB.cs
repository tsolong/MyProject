using System;
using System.Collections.Generic;
using System.Text;

namespace TL.SQLServerDAL
{
    /// <summary>
    /// Ö÷Êý¾Ý¿â
    /// </summary>
    public class CoreDB : BaseDB
    {
        public CoreDB()
        {
            ConnStr = TL.Config.SysConfig.GetConfigValue("CoreConnectionString");
        }
    }
}
