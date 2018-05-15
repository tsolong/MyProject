using System;
using System.Collections.Generic;
using System.Text;

namespace TL.Common
{
    /// <summary>
    /// 分页条
    /// </summary>
    public class PageBar
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        private int _PageIndex;
        public int PageIndex
        {
            get { return _PageIndex; }
            set { this._PageIndex = value; }
        }

        /// <summary>
        /// 每页数据条数
        /// </summary>
        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set { this._PageSize = value; }
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        private int _RecordTotal;
        public int RecordTotal
        {
            get { return _RecordTotal; }
            set { this._RecordTotal = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        private int _PageTotal;
        public int PageTotal
        {
            get { return _PageTotal; }
            set { this._PageTotal = value; }
        }

        /// <summary>
        /// 分页参数名称
        /// </summary>
        private string _UrlName;
        public string UrlName
        {
            get { return _UrlName; }
            set { this._UrlName = value; }
        }

        /// <summary>
        /// 是否拼连当前url中的参数到分页的url中
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
        /// 生成分页Html代码
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            string html = "";
            if (this.RecordTotal > 0 && this.PageIndex > 0 && this.PageIndex <= this.PageTotal)
            {
                html += "<div class=\"pageBar\">\r";

                html += "\t<span>共 " + this.RecordTotal + " 条数据</span>\r";
                html += "\t<span>每页 " + this.PageSize + " 条</span>\r";
                html += "\t<span>共 " + this.PageTotal + " 页</span>\r";

                if (this.PageIndex == 1)
                {
                    html += "\r";
                    html += "\t<a title=\"本页已经是首页\" class=\"disabled\">首页</a>\r";
                    html += "\t<a title=\"本页已经是首页\" class=\"disabled\">上一页</a>\r";
                    html += GetNumHTML();
                    if (this.PageTotal > 1)
                    {
                        if (this.Flag)
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"下一页\">下一页</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"末页\">末页</a>\r";
                        }
                        else
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"下一页\">下一页</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"末页\">末页</a>\r";
                        }
                    }
                    else
                    {
                        html += "\t<a title=\"本页已经是末页\" class=\"disabled\">下一页</a>\r";
                        html += "\t<a title=\"本页已经是末页\" class=\"disabled\">末页</a>\r";
                    }
                }
                else if (this.PageIndex == this.PageTotal)
                {
                    html += "\r";
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"上一页\">上一页</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"上一页\">上一页</a>\r";
                    }
                    html += GetNumHTML();
                    html += "\t<a title=\"本页已经是末页\" class=\"disabled\">下一页</a>\r";
                    html += "\t<a title=\"本页已经是末页\" class=\"disabled\">末页</a>\r";
                }
                else
                {
                    html += "\r";

                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"上一页\">上一页</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"下一页\">下一页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"末页\">末页</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"上一页\">上一页</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"下一页\">下一页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"末页\">末页</a>\r";
                    }
                }

                html += "</div>";
            }
            return html;
        }

        /// <summary>
        /// 生成数字页码导航
        /// </summary>
        /// <returns></returns>
        private string GetNumHTML()
        {
            string html = "\r";

            for (int i = 1; i <= this.PageTotal; i++)
            {
                if (i == this.PageIndex)
                {
                    html += "\t<a class=\"currentPage\" title=\"你正在浏览本页\">" + i + "</a>\r";
                }
                else
                {
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + i + Tools.GetUrlPar(this.UrlName) + "\" title=\"第" + i + "页\">" + i + "</a>\r";
                    }
                }
            }
            html += "\r";
            return html;
        }
    }
}
