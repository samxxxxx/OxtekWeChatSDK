using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Utils
{
    /// <summary>
    /// 时间戳和日期的转化
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStampSecond(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalSeconds;
        }
        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timeStamp)
        {
            var tmp = timeStamp.ToString();
            if (tmp.ToCharArray().Length < 13)
            {
                tmp = tmp.PadRight(13, '0');
            }

            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(Convert.ToUInt64(tmp)).AddHours(8);
        }

        /// <summary>
        /// GMT时间转成本地时间
        /// </summary>
        /// <param name="gmt">字符串形式的GMT时间</param>
        /// <returns></returns>
        public static DateTime GMT2Local(string gmt)
        {

            DateTime dt = DateTime.MinValue;
            string pattern = "";
            try
            {

                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd-MMM-yy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;

        }

        /// <summary>
        /// 将长度为14的数字字符串转换成日期时间
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string strTime)
        {
            try
            {
                strTime = strTime.Substring(0, 4) + "-" + strTime.Substring(4, 2) + "-" + strTime.Substring(6, 2) + " " + strTime.Substring(8, 2) + ":" + strTime.Substring(10, 2) + ":" + strTime.Substring(12, 2);
                DateTime dtTime = DateTime.Parse(strTime);
                DateTime.TryParse(strTime, out DateTime result);
                return result;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
