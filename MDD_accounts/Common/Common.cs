using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MDD_accounts.TOOL
{
    public class Common
    {
        #region 获取MAC地址
        public string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        #endregion

        #region 获取本机IP地址
        public string GetLocalIP()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            string ip = string.Empty;

            foreach (IPAddress ipa in localhost.AddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = ipa.ToString();
                    break;
                }
            }
            return ip;
        }

        #endregion

        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.CurrentCulture) + MakeRandomString("0123456789", 4));
        }
        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="pwdlen"></param>
        /// <returns></returns>
        public static string MakeRandomString(int pwdlen)
        {
            return MakeRandomString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_*", pwdlen);
        }
        /// <summary>
        /// 取得随机字符串
        /// </summary>
        /// <param name="pwdchars"></param>
        /// <param name="pwdlen"></param>
        /// <returns></returns>
        public static string MakeRandomString(string pwdchars, int pwdlen)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < pwdlen; i++)
            {
                int num = random.Next(pwdchars.Length);
                builder.Append(pwdchars[num]);
            }
            return builder.ToString();
        }

        #region gzip对字符串进行压缩和解压
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }
            memoryStream.Position = 0;
            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);
            return Convert.ToBase64String(compressedData);
            //return compressedData.ToString();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decompress(string text)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(text.ToString());
                // return (string)(System.Text.Encoding.UTF8.GetString(Decompress(buffer)));

                //byte[] buffer = Encoding.UTF8.GetBytes(text);
                using (var compressStream = new MemoryStream(buffer))
                {
                    using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                    {
                        using (var resultStream = new MemoryStream())
                        {
                            //zipStream.CopyTo(resultStream);
                            //return resultStream.ToArray();

                            byte[] block = new byte[1024];
                            while (true)
                            {
                                int bytesRead = zipStream.Read(block, 0, block.Length);
                                if (bytesRead <= 0)
                                    break;
                                else
                                    resultStream.Write(block, 0, bytesRead);
                            }

                            return (string)(System.Text.Encoding.UTF8.GetString(resultStream.ToArray()));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return text;
            }

        }

        #endregion
    }

    public class Converter
    {
        #region DateTimeToStr
        /// <summary>
        /// 默认时间格式
        /// </summary>
        /// <returns></returns> 
        public static string GetDateTimeDefaultFormat()
        {
            //需要更改时间默认显示格式  只改一下参数枚举值即可
            return GetDateTimeFormat(DateTimeFormat.AllFormat);
        }
        /// <summary>
        /// 将对象转换为指定格式的字符串（默认 yyyy-MM-dd HH:mm:ss）
        /// </summary>
        /// <param name="time">待转换的对象</param>
        /// <param name="sFormat">指定的字符串格式</param>
        /// <returns>转换后的字符串</returns>
        /// <remarks>如果为空值，或非日期时间对象，则返回""</remarks>
        public static string ToTimeStr(object time, DateTimeFormat dtFormat = DateTimeFormat.AllFormat)
        {
            if (time == null || time == DBNull.Value)
                return "";
            else
            {
                try
                {
                    return ((DateTime)time).ToString(GetDateTimeFormat(dtFormat));
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 根据枚举值获取时间格式
        /// </summary>
        /// <param name="dtFormat">枚举日期格式</param>
        /// <returns></returns> 
        public static string GetDateTimeFormat(DateTimeFormat dtFormat)
        {
            FieldInfo fi = dtFormat.GetType().GetField(dtFormat.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            return attributes[0].Description;
        }
        #endregion

        #region StrToDateTime
        /// <summary>
        /// 字符串转换为日期格式
        /// </summary>
        /// <param name="strDateTime">待转换的字符串</param>
        /// <returns>转换后的日期</returns>
        /// <remarks>如果转换失败，返回0000-00-00</remarks>
        public static DateTime StrToDateTime(string strDateTime)
        {
            DateTime now;
            Array format = Enum.GetValues(typeof(DateTimeFormat));
            List<string> litstr = new List<string>();
            foreach (var str in format)
            {
                litstr.Add(GetDateTimeFormat((DateTimeFormat)str));
            }
            if (litstr.Count > 0)
            {
                if (DateTime.TryParseExact(strDateTime, litstr.ToArray(), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out now))
                {
                    return now;
                }
            }
            return DateTime.MinValue;
        }
        #endregion

        #region ToInt

        /// <summary>
        /// 将对象转换为整型
        /// </summary>
        /// <param name="objInt">对象</param>
        /// <returns>得到的整数。缺省为0</returns>
        public static int ToInt(object objInt)
        {
            return ToInt(objInt, 0);
        }
        /// <summary>
        /// 将对象转换为整型
        /// </summary>
        /// <param name="objInt">对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的整数</returns>
        public static int ToInt(object objInt, int defaultValue)
        {
            try
            {
                if (objInt == null)
                {
                    return defaultValue;
                }
                else if (objInt is string)
                {
                    string sString = (string)objInt;
                    if (sString.Length > 10)
                    {
                        return defaultValue;
                    }
                    else
                    {
                        return StrToInt(sString, defaultValue);
                    }

                }
                else if (objInt is int)
                {
                    return (int)objInt;
                }
                else
                {
                    return Convert.ToInt32(objInt);
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转换为整数
        /// </summary>
        /// <param name="sToConvertString"></param>
        /// <param name="nDefaultValue"></param>
        /// <returns></returns>
        public static int StrToInt(string sToConvertString, int nDefaultValue)
        {
            Regex rex = new Regex(@"^[-\+]?\d+$");
            int nResult = nDefaultValue;
            if (rex.IsMatch(sToConvertString))
            {
                nResult = int.Parse(sToConvertString);
                return nResult;
            }
            else
                return nDefaultValue;
        }
        #endregion

        #region ToBool
        /// <summary>
        /// 将对象转换为布尔型(1,Y:true --- 0,N:false)
        /// </summary>
        /// <param name="objBool">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的布尔型</returns>
        public static bool ToBool(object objBool, bool defaultValue)
        {
            if (objBool == null || objBool == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    if (objBool.ToString() == "1" || objBool.ToString().ToUpper() == "Y")
                    {
                        return true;
                    }
                    else if (objBool.ToString() == "0" || objBool.ToString().ToUpper() == "N")
                    {
                        return false;
                    }
                    return Convert.ToBoolean(objBool);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为布尔型(1 :true --- 0 :false)
        /// </summary>
        /// <param name="objBool">待转换的对象</param> 
        /// <returns>得到的布尔型</returns>
        public static int ToBoolInt(bool objBool)
        {
            if (objBool)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 将对象转换为布尔型( Y:true ---  N:false)
        /// </summary>
        /// <param name="objBool">待转换的对象</param> 
        /// <returns>得到的布尔型</returns>
        public static string ToBoolString(bool objBool)
        {
            if (objBool)
            {
                return "Y";
            }
            return "N";
        }
        #endregion

        #region ToDecimal
        /// <summary>
        /// 将对象转换为Decimal类型
        /// </summary>
        /// <param name="objDecimal">待转换的对象</param>
        /// <returns>得到的Decimal类型。缺省为0</returns>
        public static decimal ToDecimal(object objDecimal)
        {
            return ToDecimal(objDecimal, 0);
        }
        /// <summary>
        /// 将对象转换为Decimal类型
        /// </summary>
        /// <param name="objDecimal">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的Decimal类型</returns>
        public static decimal ToDecimal(object objDecimal, decimal defaultValue)
        {
            if (objDecimal == null)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToDecimal(objDecimal);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        #endregion ToDecimal

        #region ToFloat

        /// <summary>
        /// 将对象转换为Float类型
        /// </summary>
        /// <param name="objFloat">待转换的对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的Float类型</returns>
        public static double ToFloat(object objFloat, double defaultValue)
        {
            if (objFloat == null)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(objFloat);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为Float类型
        /// </summary>
        /// <param name="objFloat">待转换的对象</param>
        /// <returns>得到的Float类型。缺省为0</returns>
        public static double ToFloat(object objFloat)
        {
            return ToFloat(objFloat, 0);
        }

        #endregion ToFloat

        #region ToString

        /// <summary>
        /// 将对象转换为字符串类型
        /// </summary>
        /// <param name="objString">待转换为对象</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>得到的字符串类型</returns>
        public static string ToString(object objString, string defaultValue)
        {
            if (objString == null || objString == DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return Convert.ToString(objString);
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 将对象转换为字符串类型
        /// </summary>
        /// <param name="objString"></param>
        /// <returns></returns>
        public static string ToString(object objString)
        {
            return ToString(objString, "");
        }

        #endregion

        #region ToByte
        /// <summary>
        /// 将对象转换为 byte
        /// </summary>
        /// <param name="objInt">对象</param> 
        /// <returns>得到的整数</returns>
        public static byte ToByte(object objByte)
        {
            try
            {
                return Convert.ToByte(objByte);
            }
            catch
            {
                return new byte();
            }
        }
        #endregion 
    }

    /// <summary>
    /// 时间格式化
    /// </summary>
    [Flags]
    public enum DateTimeFormat
    {
        [Description("yyyy-MM-dd HH:mm:ss")]
        AllFormat,
        [Description("yyyy-MM-dd HH:mm")]
        MinuteFormat,
        [Description("yyyy-MM-dd HH")]
        HourFormat,
        [Description("yyyy-MM-dd")]
        DateFormat,
        [Description("yyyyMMddHHmm")]
        DatemFormat,
        [Description("yyyyMMddHHmmss")]
        DatesFormat,
        [Description("yyyy年MM月dd日 HH时mm分ss秒")]
        DateCodeAllFormat,
        [Description("yyyy年MM月dd日 HH时mm分")]
        DateCodeMinuteFormat,
        [Description("yyyy年MM月dd日 HH时")]
        DateCodeHourFormat,
        [Description("yyyy年MM月dd日")]
        DateCodeDayFormat,
        [Description("HH:mm:ss")]
        ShortMinuteFormat,
        [Description("yyyy-MM-dd HH:mm:ss.000")]
        MillisecondFormat,
        [Description("yyyy/MM/dd HH:mm:ss")]
        DateBaseFormat,
        [Description("HH:mm")]
        HourMinuteFormat,
        [Description("MM月dd日")]
        YearMonthFormat,
    }
}