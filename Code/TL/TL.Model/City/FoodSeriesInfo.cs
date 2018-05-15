using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// Ö÷²ËÏµ
    /// </summary>
    public class FoodSeriesInfo
    {
        private int _Id;
        /// <summary>
        /// ±àºÅ
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;
        /// <summary>
        /// Ãû³Æ
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _OrderNum;
        /// <summary>
        /// ÅÅÐò
        /// </summary>
        public int OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }
    }
}
