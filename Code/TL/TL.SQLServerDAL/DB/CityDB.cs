using System;
using System.Collections.Generic;
using System.Text;

namespace TL.SQLServerDAL
{
    /// <summary>
    /// �������ݿ�
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
