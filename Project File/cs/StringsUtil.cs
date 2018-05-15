using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Web.Security;


public class StringsUtil
{
    private static string passWord;	//�����ַ���

    /// <summary>
    /// �ж������Ƿ�����
    /// </summary>
    /// <param name="num">Ҫ�жϵ��ַ���</param>
    /// <returns></returns>
    static public bool VldInt(string num)
    {
        #region
        try
        {
            Convert.ToInt32(num);
            return true;
        }
        catch { return false; }
        #endregion
    }

    /// <summary>
    /// �����ı��༭���滻����ַ���
    /// </summary>
    /// <param name="str">Ҫ�滻���ַ���</param>
    /// <returns></returns>
    static public string GetHtmlEditReplace(string str)
    {
        #region
        return str.Replace("'", "''").Replace(",", "��").Replace("%", "��").Replace("script", "").Replace(".js", "");
        #endregion
    }

    /// <summary>
    /// ��ȡ�ַ�������
    /// </summary>
    /// <param name="str">��Ҫ��ȡ���ַ���</param>
    /// <param name="num">��ȡ�ַ����ĳ���</param>
    /// <returns></returns>
    static public string GetSubString(string str, int num)
    {
        #region
        return (str.Length > num) ? str.Substring(0, num) + "..." : str;
        #endregion
    }

    /// <summary>
    /// ����������Ϣ
    /// </summary>
    /// <param name="text">����</param>
    /// <param name="maxLength">��󳤶�</param>
    /// <returns></returns>
    public static string InputText(string text, int maxLength)
    {
        #region
        text = text.Trim();
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        if (text.Length > maxLength)
            text = text.Substring(0, maxLength);
        text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br/>
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
        text = text.Replace("'", "''");
        return text;
        #endregion
    }
   /**/
    ///��ȡHTML����������
    ///   <summary>   
    ///   ȥ��HTML���   
    ///   </summary>   
    ///   <param   name="strHtml">����HTML��Դ��   </param>   
    ///   <returns>�Ѿ�ȥ���������</returns>   

    public static string StripHTML(string strHtml)
    {
        #region
        string[] aryReg ={   
                      @"<script[^>]*?>.*?</script>",   
    
                      @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",   
                      @"([\r\n])[\s]+",   
                      @"&(quot|#34);",   
                      @"&(amp|#38);",   
                      @"&(lt|#60);",   
                      @"&(gt|#62);",     
                      @"&(nbsp|#160);",     
                      @"&(iexcl|#161);",   
                      @"&(cent|#162);",   
                      @"&(pound|#163);",   
                      @"&(copy|#169);",   
                      @"&#(\d+);",   
                      @"-->",   
                      @"<!--.*\n"   
                    };

        string[] aryRep =   {   
                        "",   
                        "",   
                        "",   
                        "\"",   
                        "&",   
                        "<",   
                        ">",   
                        "   ",   
                        "\xa1",//chr(161),   
                        "\xa2",//chr(162),   
                        "\xa3",//chr(163),   
                        "\xa9",//chr(169),   
                        "",   
                        "\r\n",   
                        ""   
                      };

        string newReg = aryReg[0];
        string strOutput = strHtml;
        for (int i = 0; i < aryReg.Length; i++)
        {
            Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
            strOutput = regex.Replace(strOutput, aryRep[i]);
        }
        strOutput.Replace("<", "");
        strOutput.Replace(">", "");
        strOutput.Replace("\r\n", "");
        return strOutput;
        #endregion
    }

    /// <summary>
    /// ���������
    /// </summary>
    /// <returns></returns>
    private string GenerateCheckCode()
    {
        #region
        int number;
        char code;
        string checkCode = String.Empty;

        System.Random random = new Random();

        for (int i = 0; i < 5; i++)
        {
            number = random.Next();

            if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
            else
                code = (char)('A' + (char)(number % 26));

            checkCode += code.ToString();
        }

        HttpContext.Current.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));

        return checkCode;
        #endregion
    }


    /// <summary>
    /// ������֤��ͼƬ
    /// </summary>
    public void CreateCheckCodeImage()
    {
        #region
        string checkCode = GenerateCheckCode();
        if (checkCode == null || checkCode.Trim() == String.Empty)
            return;

        System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
        Graphics g = Graphics.FromImage(image);

        try
        {
            //�������������
            Random random = new Random();

            //���ͼƬ����ɫ
            g.Clear(Color.White);

            //��ͼƬ�ı���������
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(checkCode, font, brush, 2, 2);

            //��ͼƬ��ǰ��������
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //��ͼƬ�ı߿���
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Gif";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
        #endregion
    }


    /// <summary>
    /// ��ȡ���ֵ�һ��ƴ��
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    static public string getSpells(string input)
    {
        #region
        int len = input.Length;
        string reVal = "";
        for (int i = 0; i < len; i++)
        {
            reVal += getSpell(input.Substring(i, 1));
        }
        return reVal;
        #endregion
    }

    static public string getSpell(string cn)
    {
        #region
        byte[] arrCN = Encoding.Default.GetBytes(cn);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return "?";
        }
        else return cn;
        #endregion
    }


    /// <summary>
    /// ���תȫ��
    /// </summary>
    /// <param name="BJstr"></param>
    /// <returns></returns>
    static public string GetQuanJiao(string BJstr)
    {
        #region
        char[] c = BJstr.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
            if (b.Length == 2)
            {
                if (b[1] == 0)
                {
                    b[0] = (byte)(b[0] - 32);
                    b[1] = 255;
                    c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                }
            }
        }

        string strNew = new string(c);
        return strNew;

        #endregion
    }

    /// <summary>
    /// ȫ��ת���
    /// </summary>
    /// <param name="QJstr"></param>
    /// <returns></returns>
    static public string GetBanJiao(string QJstr)
    {
        #region
        char[] c = QJstr.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
            if (b.Length == 2)
            {
                if (b[1] == 255)
                {
                    b[0] = (byte)(b[0] + 32);
                    b[1] = 0;
                    c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                }
            }
        }
        string strNew = new string(c);
        return strNew;
        #endregion
    }

    #region ���ܵ�����
    /// <summary>
    /// ���ܵ�����
    /// </summary>
    public enum PasswordType
    {
        SHA1,
        MD5
    }
    #endregion


    /// <summary>
    /// �ַ�������
    /// </summary>
    /// <param name="PasswordString">Ҫ���ܵ��ַ���</param>
    /// <param name="PasswordFormat">Ҫ���ܵ����</param>
    /// <returns></returns>
    static public string EncryptPassword(string PasswordString, string PasswordFormat)
    {
        #region
        switch (PasswordFormat)
        {
            case "SHA1":
                {
                    passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
                    break;
                }
            case "MD5":
                {
                    passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
                    break;
                }
            default:
                {
                    passWord = string.Empty;
                    break;
                }
        }
        return passWord;
        #endregion
    }

    /// <summary>
    /// �ַ�������
    /// </summary>
    /// <param name="str">Ҫ���˵��ַ���</param>
    /// <returns></returns>
    public static string Encode(string str)
    {
        str = str.Replace("&", "&amp;");
        str = str.Replace("'", "''");
        str = str.Replace("\"", "&quot;");
        str = str.Replace(" ", "&nbsp;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt;");
        str = str.Replace("\n", "<br/>");
        return str;
    }

    /// <summary>
    /// ��ȡ��ȫ�ַ�
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetSafeStr(string str)
    {
        str = str.Replace("'", "");
        str = str.Replace(";", "");
        str = str.Replace("--", "");
        return str;
    }
}