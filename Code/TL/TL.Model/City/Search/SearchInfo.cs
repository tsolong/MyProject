using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City.Search
{
    /// <summary>
    /// 搜索
    /// </summary>
    public class SearchInfo
    {
        private Nullable<int> _Area;
        /// <summary>
        /// 地区
        /// </summary>
        public Nullable<int> Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        private Nullable<int> _AreaSub;
        /// <summary>
        /// 地区->地点
        /// </summary>
        public Nullable<int> AreaSub
        {
            get { return _AreaSub; }
            set { _AreaSub = value; }
        }

        private string _FoodSeries;
        /// <summary>
        /// 菜系
        /// </summary>
        public string FoodSeries
        {
            get { return _FoodSeries; }
            set { _FoodSeries = value; }
        }

        private string _FoodSeriesSub;
        /// <summary>
        /// 子菜系
        /// </summary>
        public string FoodSeriesSub
        {
            get { return _FoodSeriesSub; }
            set { _FoodSeriesSub = value; }
        }

        private Nullable<int> _Consume;
        /// <summary>
        /// 人均消费
        /// </summary>
        public Nullable<int> Consume
        {
            get { return _Consume; }
            set { _Consume = value; }
        }

        private Nullable<int> _Level;
        /// <summary>
        /// 店铺星级
        /// </summary>
        public Nullable<int> Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private Nullable<int> _Balcony;
        /// <summary>
        /// 包厢
        /// </summary>
        public Nullable<int> Balcony
        {
            get { return _Balcony; }
            set { _Balcony = value; }
        }

        private Nullable<int> _Takeaway;
        /// <summary>
        /// 外卖
        /// </summary>
        public Nullable<int> Takeaway
        {
            get { return _Takeaway; }
            set { _Takeaway = value; }
        }

        private Nullable<int> _Card;
        /// <summary>
        /// 刷卡
        /// </summary>
        public Nullable<int> Card
        {
            get { return _Card; }
            set { _Card = value; }
        }

        private Nullable<int> _Park;
        /// <summary>
        /// 停车场
        /// </summary>
        public Nullable<int> Park
        {
            get { return _Park; }
            set { _Park = value; }
        }
    }
}
