using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Chinahoo.Extensions.Context
{
    /// <summary>
    /// 常用函数
    /// </summary>
    public class CommonHelper
    {
        private const string EMAIL_EXPRESSION = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private static readonly Regex _emailRegex;
        static CommonHelper()
        {
            _emailRegex = new Regex(EMAIL_EXPRESSION, RegexOptions.IgnoreCase);
        }
        public static string Trim(string html)
        {
            return Regex.Replace(html, @"\s+", " ").Trim();

        }
        /// <summary>
        /// 将字符转换成日期类型的数据
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回1900-1-1表示字符串非时间类型</returns>
        public static DateTime ToDateTime(string str)
        {
            DateTime i = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
            if (string.IsNullOrEmpty(str))
            {
                return i;
            }
            else
            {
                DateTime.TryParse(str, out i);
                return i;
            }

        }
        /// <summary>
        /// 将字符串类型数据转换成int类型数据
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回0表示字符串非int类型</returns>
        public static int ToInt(object str)
        {
            int i = 0;
            try
            {
                if (str != null)
                {

                    int.TryParse(str.ToString(), out i);
                }
            }
            catch
            {
            }
            finally { }
            return i;
        }
        /// <summary>
        /// 转换为64位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToInt64(object str)
        {
            try
            {
                long i = 0;
                long.TryParse(str.ToString(), out i);
                return i;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 将字符串类型数据转换成decimal类型数据
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回0表示字符串非decimal类型</returns>
        public static decimal ToDecimal(object str)
        {
            decimal temp = 0;
            try
            {
                if (str != null)
                {
                    decimal.TryParse(str.ToString(), out temp);
                }

            }
            catch { }
            return temp;
        }
        /// <summary>
        /// left截取字符串
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>

        public static string Left(string sSource, int iLength)
        {
            return sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
        }
        /// <summary>
        /// Right截取字符串
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Right(string sSource, int iLength)
        {
            return sSource.Substring(iLength > sSource.Length ? 0 : sSource.Length - iLength);
        }
        /// <summary>
        /// mid截取字符串
        /// </summary>
        /// <param name="sSource"></param>
        /// <param name="iStart"></param>
        /// <param name="iLength"></param>
        /// <returns></returns>
        public static string Mid(string sSource, int iStart, int iLength)
        {
            int iStartPoint = iStart > sSource.Length ? sSource.Length : iStart;
            return sSource.Substring(iStartPoint, iStartPoint + iLength > sSource.Length ? sSource.Length - iStartPoint : iLength);
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
        /// <summary>
        /// 判断是否是base64
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();

            return _emailRegex.IsMatch(email);
        }

        /// <summary>
        /// Verifies that string is an valid IP-Address
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out var _);
        }
    }
}
