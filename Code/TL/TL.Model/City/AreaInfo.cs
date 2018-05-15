using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// 城市->地区
    /// </summary>
    public class AreaInfo
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
    }
}
