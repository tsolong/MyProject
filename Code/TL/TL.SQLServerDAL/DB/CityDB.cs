using System;
using System.Collections.Generic;
using System.Text;

namespace TL.SQLServerDAL
{
    /// <summary>
    /// 城市数据库
    /// </summary>
    public class CityDB : BaseDB
    {
        public CityDB()
        {
            TL.Model.City.CityInfo CurrentCity = TL.Config.SysConfig.GetCityByDomain(TL.Common.Tools.GetHost());
            if (CurrentCity != null)
                ConnStr = CurrentCity.ConnectionString;
        }
    }
}
