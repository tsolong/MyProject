using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City.Shop
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public class ShopInfo
    {
        private int _UserId;
        /// <summary>
        /// �û����
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private bool _IsOnline;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsOnline
        {
            get { return _IsOnline; }
            set { _IsOnline = value; }
        }

        private Nullable<int> _Area;
        /// <summary>
        /// ����
        /// </summary>
        public Nullable<int> Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        private Nullable<int> _AreaSub;
        /// <summary>
        /// ����->�ص�
        /// </summary>
        public Nullable<int> AreaSub
        {
            get { return _AreaSub; }
            set { _AreaSub = value; }
        }

        private string _FoodSeries;
        /// <summary>
        /// ��ϵ
        /// </summary>
        public string FoodSeries
        {
            get { return _FoodSeries; }
            set { _FoodSeries = value; }
        }

        private string _FoodSeriesSub;
        /// <summary>
        /// �Ӳ�ϵ
        /// </summary>
        public string FoodSeriesSub
        {
            get { return _FoodSeriesSub; }
            set { _FoodSeriesSub = value; }
        }

        private string _ShopName;
        /// <summary>
        /// ��������
        /// </summary>
        public string ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }

        private string _Address;
        /// <summary>
        /// ��ϸ��ַ
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _MarkAddress;
        /// <summary>
        /// ��ע��ַ
        /// </summary>
        public string MarkAddress
        {
            get { return _MarkAddress; }
            set { _MarkAddress = value; }
        }

        private string _Route;
        /// <summary>
        /// ��ͨ·��
        /// </summary>
        public string Route
        {
            get { return _Route; }
            set { _Route = value; }
        }

        private string _Phone;
        /// <summary>
        /// �绰
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _MobilePhone;
        /// <summary>
        /// �ֻ�
        /// </summary>
        public string MobilePhone
        {
            get { return _MobilePhone; }
            set { _MobilePhone = value; }
        }

        private string _Email;
        /// <summary>
        /// ����
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private Nullable<int> _Consume;
        /// <summary>
        /// �˾�����
        /// </summary>
        public Nullable<int> Consume
        {
            get { return _Consume; }
            set { _Consume = value; }
        }

        private Nullable<int> _Level;
        /// <summary>
        /// �����Ǽ�
        /// </summary>
        public Nullable<int> Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private Nullable<int> _Balcony;
        /// <summary>
        /// ����
        /// </summary>
        public Nullable<int> Balcony
        {
            get { return _Balcony; }
            set { _Balcony = value; }
        }

        private Nullable<int> _Takeaway;
        /// <summary>
        /// ����
        /// </summary>
        public Nullable<int> Takeaway
        {
            get { return _Takeaway; }
            set { _Takeaway = value; }
        }

        private Nullable<int> _Card;
        /// <summary>
        /// ˢ��
        /// </summary>
        public Nullable<int> Card
        {
            get { return _Card; }
            set { _Card = value; }
        }

        private Nullable<int> _Park;
        /// <summary>
        /// ͣ����
        /// </summary>
        public Nullable<int> Park
        {
            get { return _Park; }
            set { _Park = value; }
        }

        private string _ShopHours;
        /// <summary>
        /// Ӫҵʱ��
        /// </summary>
        public string ShopHours
        {
            get { return _ShopHours; }
            set { _ShopHours = value; }
        }

        private string _TotalSeat;
        /// <summary>
        /// ����λ��
        /// </summary>
        public string TotalSeat
        {
            get { return _TotalSeat; }
            set { _TotalSeat = value; }
        }

        private string _WebSite;
        /// <summary>
        /// ������ַ
        /// </summary>
        public string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
        }

        private string _Equipment;
        /// <summary>
        /// �豸����
        /// </summary>
        public string Equipment
        {
            get { return _Equipment; }
            set { _Equipment = value; }
        }

        private string _Intro;
        /// <summary>
        /// ���̽���
        /// </summary>
        public string Intro
        {
            get { return _Intro; }
            set { _Intro = value; }
        }

        private string _Remark;
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
    }
}
