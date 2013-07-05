using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Utilities
{
    public static class Extensions
    {
        public static short ReadInt16BigEndian(this BinaryReader binReader)
        {
            var b0 = binReader.ReadByte();
            var b1 = binReader.ReadByte();
            return BitConverter.ToInt16(new byte[] { b1, b0 }, 0);
        }

        public static ushort ReadUInt16BigEndian(this BinaryReader binReader)
        {
            var b0 = binReader.ReadByte();
            var b1 = binReader.ReadByte();
            return BitConverter.ToUInt16(new byte[] { b1, b0 }, 0);
        }

        public static T ReadEnum<T>(this BinaryReader binReader)
        {
            var val = binReader.ReadByte();
            return (T)Enum.ToObject(typeof(T), val);
        }

        public static string UrlEncode(this string s)
        {
            s = s.Replace(" ", "%20");
            s = s.Replace("!", "%21");
            s = s.Replace("*", "%2A");
            s = s.Replace("(", "%28");
            s = s.Replace(")", "%29");
            s = s.Replace("-", "%2D");
            s = s.Replace(".", "%2E");
            s = s.Replace("_", "%5F");
            s = s.Replace(@"\", "%5C");
            return s;
        }

        public static string Fix(this string s)
        {
            return s.Trim('\0');
        }

        public static string ToHeroDateTime(this DateTime dateTime)
        {
            var yearOld = dateTime.Year % 100;
            var year = yearOld.ToString("x2");
            var month = dateTime.Month.ToString("x2");
            var day = dateTime.Day.ToString("x2");
            var hour = dateTime.Hour.ToString("x2");
            var minute = dateTime.Minute.ToString("x2");
            var second = dateTime.Second.ToString("x2");

            var heroDateTime = string.Format("%{0}%{1}%{2}%{3}%{4}%{5}", year, month, day, hour, minute, second);
            return heroDateTime;
        }

        public static DateTime ToDateTimeHeroString(this string heroDateTime)
        {
            var split = heroDateTime.Split('%');

            var year = Convert.ToInt32(split[0], 16) + 2000;
            var month = Convert.ToInt32(split[1], 16);
            var day = Convert.ToInt32(split[2], 16);
            var hour = Convert.ToInt32(split[3], 16);
            var minute = Convert.ToInt32(split[4], 16);
            var second = Convert.ToInt32(split[5], 16);

            var time = new DateTime(year, month, day, hour, minute, second);
            return time;
        }

    }
}

