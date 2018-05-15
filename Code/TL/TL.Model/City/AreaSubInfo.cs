using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// ����->����->�ص�
    /// </summary>
    public class AreaSubInfo
    {
        private int _Id;
        /// <summary>
        /// ���
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _OrderNum;
        /// <summary>
        /// ����
        /// </summary>
        public int OrderNum
        {
            get { return _OrderNum; }
            set { _OrderNum = value; }
        }

        private int _AreaId;
        /// <summary>
        /// �����������
        /// </summary>
        public int AreaId
        {
            get { return _AreaId; }
            set { _AreaId = value; }
        }
    }
}
