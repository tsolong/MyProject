using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// 子菜系
    /// </summary>
    public class FoodSeriesSubInfo
    {
        private int _Id;
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _OrderNum;
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }

        private int _FoodSeriesId;
        /// <summary>
        /// 所属主菜系Id
        /// </summary>
        public int FoodSeriesId
        {
            get { return _FoodSeriesId; }
            set { _FoodSeriesId = value; }
        }
    }
}
