using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Common
{
    /// <summary>
    /// ��ҳ��
    /// </summary>
    public class PageBar
    {
        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        private int _PageIndex;
        public int PageIndex
        {
            get { return _PageIndex; }
            set { this._PageIndex = value; }
        }

        /// <summary>
        /// ÿҳ��������
        /// </summary>
        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set { this._PageSize = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private int _RecordTotal;
        public int RecordTotal
        {
            get { return _RecordTotal; }
            set { this._RecordTotal = value; }
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        private int _PageTotal;
        public int PageTotal
        {
            get { return _PageTotal; }
            set { this._PageTotal = value; }
        }

        /// <summary>
        /// ��ҳ��������
        /// </summary>
        private string _UrlName;
        public string UrlName
        {
            get { return _UrlName; }
            set { this._UrlName = value; }
        }

        /// <summary>
        /// �Ƿ�ƴ����ǰurl�еĲ�������ҳ��url��
        /// </summary>
        private bool _Flag = true;
        public bool Flag
        {
            get { return _Flag; }
            set { this._Flag = value; }
        }

        public PageBar()
        {
        }

        public PageBar(int PageIndex, int PageSize, int RecordTotal, string UrlName)
        {
            init(PageIndex, PageSize, RecordTotal, UrlName, true);
        }

        public PageBar(int PageIndex, int PageSize, int RecordTotal, string UrlName, bool Flag)
        {
            init(PageIndex, PageSize, RecordTotal, UrlName, Flag);
        }

        public void init(int PageIndex, int PageSize, int RecordTotal, string UrlName, bool Flag)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.RecordTotal = RecordTotal;
            this.UrlName = UrlName;
            this.Flag = Flag;
            this.PageTotal = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.RecordTotal) / this.PageSize));
        }

        /// <summary>
        /// ���ɷ�ҳHtml����
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            string html = "";
            if (this.RecordTotal > 0 && this.PageIndex > 0 && this.PageIndex <= this.PageTotal)
            {
                html += "<div class=\"pageBar\">\r";

                html += "\t<span>�� " + this.RecordTotal + " ������</span>\r";
                html += "\t<span>ÿҳ " + this.PageSize + " ��</span>\r";
                html += "\t<span>�� " + this.PageTotal + " ҳ</span>\r";

                if (this.PageIndex == 1)
                {
                    html += "\r";
                    html += "\t<a title=\"��ҳ�Ѿ�����ҳ\" class=\"disabled\">��ҳ</a>\r";
                    html += "\t<a title=\"��ҳ�Ѿ�����ҳ\" class=\"disabled\">��һҳ</a>\r";
                    html += GetNumHTML();
                    if (this.PageTotal > 1)
                    {
                        if (this.Flag)
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��һҳ\">��һҳ</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                        }
                        else
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"��һҳ\">��һҳ</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                        }
                    }
                    else
                    {
                        html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">��һҳ</a>\r";
                        html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">ĩҳ</a>\r";
                    }
                }
                else if (this.PageIndex == this.PageTotal)
                {
                    html += "\r";
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��һҳ\">��һҳ</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"��һҳ\">��һҳ</a>\r";
                    }
                    html += GetNumHTML();
                    html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">��һҳ</a>\r";
                    html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">ĩҳ</a>\r";
                }
                else
                {
                    html += "\r";

                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��һҳ\">��һҳ</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��һҳ\">��һҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"��һҳ\">��һҳ</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"��һҳ\">��һҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                    }
                }

                html += "</div>";
            }
            return html;
        }

        /// <summary>
        /// ��������ҳ�뵼��
        /// </summary>
        /// <returns></returns>
        private string GetNumHTML()
        {
            string html = "\r";

            for (int i = 1; i <= this.PageTotal; i++)
            {
                if (i == this.PageIndex)
                {
                    html += "\t<a class=\"currentPage\" title=\"�����������ҳ\">" + i + "</a>\r";
                }
                else
                {
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + i + Tools.GetUrlPar(this.UrlName) + "\" title=\"��" + i + "ҳ\">" + i + "</a>\r";
                    }
                }
            }
            html += "\r";
            return html;
        }
    }
}
