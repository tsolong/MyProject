using System;
using System.Text.RegularExpressions;
namespace Jumbot.Common
{
    /// <summary>
    /// �ṩ������Ҫʹ�õ�һЩ��֤�߼���
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// ���һ���ַ����Ƿ����ת��Ϊ���ڣ�һ��������֤�û��������ڵĺϷ��ԡ�
        /// </summary>
        /// <param name="_value">����֤���ַ�����</param>
        /// <returns>�Ƿ����ת��Ϊ���ڵ�boolֵ��</returns>
        public static bool IsStringDate(string _value)
        {
            DateTime dTime;
            try
            {
                dTime = DateTime.Parse(_value);
            }
            catch (FormatException e)
            {
                //���ڸ�ʽ����ȷʱ
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ���һ���ַ����Ƿ��Ǵ����ֹ��ɵģ�һ�����ڲ�ѯ�ַ�����������Ч����֤��
        /// </summary>
        /// <param name="_value">����֤���ַ�������</param>
        /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
        public static bool IsNumeric(string _value)
        {
            return Validator.QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// ���һ���ַ����Ƿ��Ǵ���ĸ�����ֹ��ɵģ�һ�����ڲ�ѯ�ַ�����������Ч����֤��
        /// </summary>
        /// <param name="_value">����֤���ַ�����</param>
        /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return Validator.QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// �ж��Ƿ������֣�����С����������
        /// </summary>
        /// <param name="_value">����֤���ַ�����</param>
        /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
        public static bool IsNumber(string _value)
        {
            return Validator.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }

        /// <summary>
        /// ������֤һ���ַ����Ƿ����ָ����������ʽ��
        /// </summary>
        /// <param name="_express">������ʽ�����ݡ�</param>
        /// <param name="_value">����֤���ַ�����</param>
        /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            System.Text.RegularExpressions.Regex myRegex = new System.Text.RegularExpressions.Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ�ʼ�
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�ΪID��ʽ
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsIDCard(string _value)
        {
            Regex regex;
            string[] strArray;
            DateTime time;
            if ((_value.Length != 15) && (_value.Length != 0x12))
            {
                return false;
            }
            if (_value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$");
                if (!regex.Match(_value).Success)
                {
                    return false;
                }
                strArray = regex.Split(_value);
                try
                {
                    time = new DateTime(int.Parse("19" + strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(_value).Success)
            {
                return false;
            }
            strArray = regex.Split(_value);
            try
            {
                time = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�ΪInt
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsInt(string _value)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(_value).Success)
            {
                if ((long.Parse(_value) > 0x7fffffffL) || (long.Parse(_value) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsLengthStr(string _value, int _begin, int _end)
        {
            int length = _value.Length;
            if ((length < _begin) && (length > _end))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ�ֻ�����
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex(@"^13\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ�绰����
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ��ַ
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string _value)
        {
            Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ��ĸ������
        /// Regex("[a-zA-Z0-9]?"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }
        /// <summary>
        /// ���ַ���ת������
        /// </summary>
        /// <param name="_value">�ַ���</param>
        /// <param name="_defaultValue">Ĭ��ֵ</param>
        /// <returns></returns>
        public static DateTime StrToDate(string _value, DateTime _defaultValue)
        {
            if (IsStringDate(_value))
                return Convert.ToDateTime(_value);
            else
                return _defaultValue;
        }
        /// <summary>
        /// ���ַ���ת������
        /// </summary>
        /// <param name="_value">�ַ���</param>
        /// <param name="_defaultValue">Ĭ��ֵ</param>
        /// <returns></returns>
        public static int StrToInt(string _value, int _defaultValue)
        {
            if (IsNumber(_value))
                return int.Parse(_value);
            else
                return _defaultValue;
        }
    }
}
