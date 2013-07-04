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

    }
}

