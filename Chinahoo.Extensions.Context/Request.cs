using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chinahoo.Extensions.Context
{
    /// <summary>
    /// 客户端请求
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 获取当前访问域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            return HttpContext.Current.Request.Host.Value.ToLower();
        }
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {

            try
            {
                string result = (HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault() != null
           && HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault() != String.Empty)
           ? HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault()
           : HttpContext.Current.Request.Headers["REMOTE_ADDR"].FirstOrDefault();
                if (string.IsNullOrEmpty(result))//
                    result = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault(); 

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.Headers["X-Forwarded-For"].FirstOrDefault(); 

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.Headers["Proxy-Client-IP"].FirstOrDefault(); 
                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.Headers["WL-Proxy-Client-IP"].FirstOrDefault(); 
                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.Headers["HTTP_CLIENT_IP"].FirstOrDefault();
                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Connection.RemoteIpAddress.ToString();

                if (string.IsNullOrEmpty(result))
                    if (string.IsNullOrEmpty(result))
                        return "127.0.0.1";
                    else
                    {
                        result = result.Split(',')[0].Split(' ')[0];

                    }
                return result.Split(',')[0].Split(' ')[0];
            }
            catch
            {
                return "127.0.0.1";
            }
        }
        /// <summary>
        /// 获取user-agent
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            try
            {

                return HttpContext.Current.Request.Headers["user-agent"].FirstOrDefault();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取页面来源Referer
        /// </summary>
        /// <returns></returns>
        public static string GetReferer()
        {

            return HttpContext.Current.Request.Headers["Referer"].FirstOrDefault();
        }
        #region 从页面获取值
        /// <summary>
        /// 从页面取相关int值(自动判断是表单还是地址栏)
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns>0表示非整数</returns>
        public static int GetInt(string para)
        {
            int queryInt = 0;
            string tempString = string.Empty;
            //判断是Post还是get提交
            if (HttpContext.Current.Request.Method.ToLower().Equals("get"))
            {
                tempString = GetQueryString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetFormString(para.ToLower());
                }
            }
            else
            {
                tempString = GetFormString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetQueryString(para.ToLower());
                }

            }
            //通过方法进行数据类型转换
            queryInt = CommonHelper.ToInt(tempString);

            return queryInt;
        }
      /// <summary>
      /// 获取数值转换int64
      /// </summary>
      /// <param name="para"></param>
      /// <returns></returns>

        public static Int64 GetInt64(string para)
        {
            Int64 queryInt = 0;
            string tempString = string.Empty;

            //判断是Post还是get提交
            if (HttpContext.Current.Request.Method.ToLower().Equals("get"))
            {
                tempString = GetQueryString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetFormString(para.ToLower());
                }
            }
            else
            {
                tempString = GetFormString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetQueryString(para.ToLower());
                }

            }
            //通过方法进行数据类型转换
            queryInt = CommonHelper.ToInt64(tempString);

            return queryInt;
        }
        /// <summary>
        /// 从页面取相关浮点数值(自动判断是表单还是地址栏)
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns>0表示浮非点数</returns>
        public static decimal GetDecimal(string para)
        {
            decimal queryDecimal = 0;
            string tempString = string.Empty;

            //判断是Post还是get提交
            if (HttpContext.Current.Request.Method.ToLower().Equals("get"))
            {
                tempString = GetQueryString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetFormString(para.ToLower());
                }
            }
            else
            {
                tempString = GetFormString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetQueryString(para);
                }
            }

            //通过方法进行数据类型转换
            queryDecimal = CommonHelper.ToDecimal(tempString);
            return queryDecimal;
        }


        /// <summary>
        /// 从页面中获取string类型值(自动判断是表单还是地址栏)
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns>字符串</returns>
        public static string GetString(string para)
        {
            string tempString = string.Empty;
            //判断是Post还是get提交
            if (HttpContext.Current.Request.Method.ToLower().Equals("get"))
            {
                tempString = GetQueryString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetFormString(para.ToLower());
                }
            }
            else
            {
                tempString = GetFormString(para.ToLower());
                if (string.IsNullOrEmpty(tempString))
                {
                    tempString = GetQueryString(para.ToLower());
                }
            }
            // return System.Net.WebUtility.HtmlDecode(tempString);
            return tempString;
        }
        #endregion


        #region 私有方法
        /// <summary>
        /// 从地址栏获取相关值
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns>字符串</returns>
        protected static string GetQueryString(string para)
        {

            //判断是否是空对象
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Query[para].ToString()))
            {
                //得到参数值
                string queryString = HttpContext.Current.Request.Query[para].ToString();

                //过滤非法字符
                return queryString.Trim();

            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 从表单项获取相关值
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns>字符串</returns>
        protected static string GetFormString(string para)
        {
            try
            {
                //判断是否是空对象
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[para]))
                {
                    //得到参数值
                    string formString = HttpContext.Current.Request.Form[para].ToString();

                    //过滤非法字符
                    return formString.Trim();

                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }

        }
        
        #endregion

    }
}
