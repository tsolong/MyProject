using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Model.City
{
    /// <summary>
    /// ����->����
    /// </summary>
    public class AreaInfo
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
    }
}
