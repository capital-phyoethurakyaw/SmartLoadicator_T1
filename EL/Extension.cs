using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL
{
    public static class Extension
    {  
    }
    public static class DateTimeFormat
    {
        /// <summary>
        /// yyyy年MM月dd日 HH時mm分ss秒
        /// </summary>
        public const string yyyyMdHmsJP = "yyyy年M月d日 H時m分s秒";
        /// <summary>
        /// yyyy/MM/dd HH:mm:ss
        /// </summary>
        public const string yyyyMMddHHmmss = "yyyy/MM/dd HH:mm:ss";
        /// <summary>
        /// yyyy/MM/dd
        /// </summary>
        public const string yyyyMMdd = "yyyy/MM/dd";
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public const string yyyy_MM_dd = "yyyy-MM-dd";
        /// <summary>
        /// HH:mm:ss
        /// </summary>
        public const string HHmmss = "HH:mm:ss";
        /// <summary>
        /// yyyy/MM/dd HH:mm:ss
        /// </summary>
        public const string yyyyMMddHHmmssNumber = "yyyyMMdd HHmmss";
        /// <summary>
        /// yyyy/MM/dd
        /// </summary>
        public const string yyyyMMddNumber = "yyyyMMdd";
        /// <summary>
        /// HH:mm:ss
        /// </summary>
        public const string HHmmssNumber = "HHmmss";
        /// <summary>
        /// dddd
        /// </summary>
        public const string DayOfWeekLong = "dddd";
        /// <summary>
        /// ddd
        /// </summary>
        public const string DayOfWeekShort = "ddd";
    }
    public static class Extentions
    {
        public static Int16 ToInt16(this object o, Int16 defaultValue = 0)
        {
            if (o == null)
            {
                return defaultValue;
            }
            else
            {
                return ToInt16(o.ToString());
            }
        }
        public static Int16 ToInt16(this string s, Int16 defaultValue = 0)
        {
            if (Int16.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        public static Int32 ToInt32(this object o, Int32 defaultValue = 0)
        {
            if (o == null)
            {
                return defaultValue;
            }
            else
            {
                return ToInt32(o.ToString());
            }
        }
        public static Int32 ToInt32(this string s, Int32 defaultValue = 0)
        {
            if (Int32.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        public static decimal ToDecimal(this object o, decimal defaultValue = 0)
        {
            if (o == null)
            {
                return defaultValue;
            }
            else
            {
                return ToDecimal(o.ToString());
            }
        }
        public static decimal ToDecimal(this string s, decimal defaultValue = 0)
        {
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        public static byte ToByte(this object o, byte defaultValue = 0)
        {
            if (o == null)
            {
                return defaultValue;
            }
            else
            {
                return ToByte(o.ToString());
            }
        }
        public static byte ToByte(this string s, byte defaultValue = 0)
        {
            if (byte.TryParse(s, out var result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
        public static string ToStringOrNull(this object o)
        {
            if (o == null || o.ToString() == string.Empty)
            {
                return null;
            }
            else
            {
                return o.ToString();
            }
        }
        public static string ToStringOrNull(this string s)
        {
            if (s == null || s == string.Empty)
            {
                return null;
            }
            else
            {
                return s;
            }
        }
        public static string ToStringOrEmpty(this object o)
        {
            if (o == null)
            {
                return string.Empty;
            }
            else
            {
                return o.ToString();
            }
        }
        public static string ToStringOrEmpty(this string s)
        {
            if (s == null)
            {
                return string.Empty;
            }
            else
            {
                return s;
            }
        }
        public static string ToStringOrEmpty(this object o, string format = "")
        {
            if (o == null)
            {
                return string.Empty;
            }
            else
            {
                return string.Format("{0:" + format + "}", o);
            }
        }
        public static string ToDateString(this DateTime? d, string format = "")
        {
            if (d == null)
            {
                return "";
            }
            else
            {
                return ((DateTime)d).ToString(format);
            }
        }
        public static DateTime ToDateTime(this object o, DateTime defaultValue)
        {
            if (o == null)
            {
                return defaultValue;
            }
            else
            {
                return ToDateTime(o.ToString()) ?? defaultValue;
            }
        }
        public static DateTime ToDateTime(this string s, DateTime defaultValue)
        {
            return ToDateTime(s) ?? defaultValue;
        }
        public static DateTime? ToDateTime(this string s)
        {
            if (s == null)
            {
                return null as DateTime?;
            }
            if (s.Contains("/") || s.Contains("-"))
            {
                return DateTime.TryParse(s, out var dt) ? dt : null as DateTime?;
            }
            else
            {
                string format = s.Length > 8 ? DateTimeFormat.yyyyMMddHHmmssNumber : DateTimeFormat.yyyyMMddNumber;
                return DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out var dt) ? dt : null as DateTime?;
            }
        }
        public static Boolean ToBoolean(this byte b)
        {
            return Convert.ToBoolean(b);
        }
        /// <summary>
        /// Convert.ToBoolean(int)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this int i)
        {
            return Convert.ToBoolean(i);
        }
        /// <summary>
        /// Convert string to Boolean. "false"->false, "true"->true, Other characters->false, "0"->false, Other numbers->true
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(this string s)
        {
            if (Boolean.TryParse(s, out var result))
            {
                return result;
            }
            else
            {
                return Convert.ToBoolean(s.ToInt32(0));
            }
        }
        /// <summary>
        /// Cuts the string to the specified number of bytes.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetByteString(this string s, int length)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            else
            {
                Encoding e = System.Text.Encoding.GetEncoding("Shift_JIS");
                return new String(s.TakeWhile((c, i) => e.GetByteCount(s.Substring(0, i + 1)) <= length).ToArray());
            }
        }

        public static string GetByteString(this string s, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            else
            {
                Encoding e = System.Text.Encoding.GetEncoding("Shift_JIS");
                var cutChars = s
                    .SkipWhile((x, i) => e.GetByteCount(s.Substring(0, i + 1)) <= startIndex)
                    .TakeWhile((x, i) => e.GetByteCount(s.Substring(0, i + 1)) <= length)
                    .ToArray();

                return new string(cutChars);
            }
        }

        public static TEnum ToEnum<TEnum>(this string s) where TEnum : struct
        {
            TEnum ret;
            Enum.TryParse(s, out ret);
            return ret;
        }
    }
}
