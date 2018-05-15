using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City.Search
{
    /// <summary>
    /// ����
    /// </summary>
    public class SearchInfo
    {
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
    }
}
