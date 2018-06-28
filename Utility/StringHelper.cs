using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Utility
{
    public class StringHelper
    {
        /// <summary>
        /// 数据格式化,将长字符数据格式化为(原数据前n位+...)
        /// </summary>
        /// <param name="arg">原数据</param>
        /// <returns>格式化后的字符串</returns>
        public static string ConvertLongString(string arg, int n)
        {
            string result = "";
            if (arg != null)
            {
                if (arg.Length > n)//大于n个字符
                {
                    result = arg.Substring(0, n) + "……";
                }
                else
                {
                    result = arg;
                }
            }
            return result;
        }
        /// <summary>
        /// 办公管理有效期转换
        /// </summary>
        /// <param name="intstr"></param>
        /// <returns></returns>
        public static string ConvertValidTime(string intstr)
        {
            string result = "";
            switch (intstr)
            {
                case "1":
                    result = "一天";
                    break;
                case "2":
                    result = "二天";
                    break;
                case "3":
                    result = "三天";
                    break;
                case "7":
                    result = "一周";
                    break;
                case "15":
                    result = "半个有";
                    break;
                case "30":
                    result = "一个月";
                    break;
                case "90":
                    result = "三个月";
                    break;
                case "180":
                    result = "半年";
                    break;
                case "365":
                    result = "一年";
                    break;
                case "999999":
                    result = "无限制";
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 取日期所在周的周一日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetMondayDate(DateTime date)
        {
            DayOfWeek dow = date.DayOfWeek;
            DateTime mondayDate = date.AddDays(DayOfWeek.Monday - dow);
            return mondayDate;
        }


        public static string FormatDateToString(object date, string dateFormat)
        {
            if (date is DBNull)
                return "";

            if (date == null || date.ToString().Trim() == "")
                return "";

            if (String.IsNullOrEmpty(dateFormat))
                return "";

            if (dateFormat == "//") dateFormat = "yyyy/MM/dd";      // For compatibility.
            if (dateFormat == "--") dateFormat = "yyyy-MM-dd";
            try
            {
                dateFormat = dateFormat.Replace("/", "\'/\'");
                DateTime dtTemp = (DateTime)date;
                return dtTemp.ToString(dateFormat);
            }
            catch
            {
                return date.ToString();
            }
        }
    }
}
