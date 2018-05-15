using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City.Shop
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public class ShopInfo
    {
        private int _UserId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private bool _IsOnline;
        /// <summary>
        /// 是否上线
        /// </summary>
        public bool IsOnline
        {
            get { return _IsOnline; }
            set { _IsOnline = value; }
        }

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

        private string _ShopName;
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }

        private string _Address;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _MarkAddress;
        /// <summary>
        /// 标注地址
        /// </summary>
        public string MarkAddress
        {
            get { return _MarkAddress; }
            set { _MarkAddress = value; }
        }

        private string _Route;
        /// <summary>
        /// 交通路线
        /// </summary>
        public string Route
        {
            get { return _Route; }
            set { _Route = value; }
        }

        private string _Phone;
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _MobilePhone;
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone
        {
            get { return _MobilePhone; }
            set { _MobilePhone = value; }
        }

        private string _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
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

        private string _ShopHours;
        /// <summary>
        /// 营业时间
        /// </summary>
        public string ShopHours
        {
            get { return _ShopHours; }
            set { _ShopHours = value; }
        }

        private string _TotalSeat;
        /// <summary>
        /// 总座位数
        /// </summary>
        public string TotalSeat
        {
            get { return _TotalSeat; }
            set { _TotalSeat = value; }
        }

        private string _WebSite;
        /// <summary>
        /// 店铺网址
        /// </summary>
        public string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
        }

        private string _Equipment;
        /// <summary>
        /// 设备服务
        /// </summary>
        public string Equipment
        {
            get { return _Equipment; }
            set { _Equipment = value; }
        }

        private string _Intro;
        /// <summary>
        /// 店铺介绍
        /// </summary>
        public string Intro
        {
            get { return _Intro; }
            set { _Intro = value; }
        }

        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
    }
}
