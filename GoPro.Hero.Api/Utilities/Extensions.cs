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
            var b0=binReader.ReadByte();
            var b1=binReader.ReadByte();
            return BitConverter.ToInt16(new byte[] { b1, b0 }, 0);
        }

        public static ushort ReadUInt16BigEndian(this BinaryReader binReader)
        {
            var b0 = binReader.ReadByte();
            var b1 = binReader.ReadByte();
            return BitConverter.ToUInt16(new byte[] { b1, b0 }, 0);
        }
    }
}
