using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class BacpacInformation
    {
        public short Version { get; private set; }
        public short Model { get; private set; }
        public string Id { get; private set; } //fixed 2 char
        public Version BootloaderVersion { get; private set; }
        public Version FirmwareVersion { get; private set; }
        public string MacAddress { get; private set; }
        public string Ssid { get; private set; }

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                this.Version = binReader.ReadByte();
                this.Model = binReader.ReadByte();
                this.Id = Encoding.UTF8.GetString(binReader.ReadBytes(2), 0, 2);
                this.BootloaderVersion = new Version(
                    binReader.ReadByte(),
                    binReader.ReadByte(),
                    binReader.ReadByte(),
                    binReader.ReadByte()
                    );
                this.FirmwareVersion = new Version(
                    binReader.ReadByte(),
                    binReader.ReadByte(),
                    binReader.ReadByte(),
                    default(byte)
                    );

                var macBuilder = new StringBuilder();
                macBuilder.Append(Convert.ToString(binReader.ReadByte(), 16));
                for (int i = 0; i < 5; i++)
                    macBuilder.Append("-").Append(Convert.ToString(binReader.ReadByte(), 16));
                this.MacAddress = macBuilder.ToString();

                var ssidLength = binReader.ReadByte();
                this.Ssid = Encoding.UTF8.GetString(binReader.ReadBytes(ssidLength), 0, ssidLength);
            }
        }
    }
}
