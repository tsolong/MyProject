using System;
namespace Jumbot.Common
{
    /// <summary>
    /// 一些常用的字符串函数
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// 倒序加1加密
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string EncryptStr(string rs) //倒序加1加密 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] + 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        /// <summary>
        /// 顺序减1解码 
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string DecryptStr(string rs) //顺序减1解码 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] - 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        /// <summary>
        /// 左截取
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(0, len);
        }
        /// <summary>
        /// 右截取
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(inputString.Length - len, len);
        }
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutTitle(string inputString, int len)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (tempLen > len)
                    break;
            }
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";
            //return tempString;
            return HtmlEncode(tempString);
        }

        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }
        /// <summary>
        /// 过滤特殊特号
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string FilterSymbol(string theString)
        {
            string[] aryReg ={ "'", "\"", "<", ">", "%", "?", ",", ".", "=", "-", "_", ";", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
    }
}
