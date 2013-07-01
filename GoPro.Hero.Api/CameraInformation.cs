using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class CameraInformation
    {
        public byte Protocol { get; private set; }
        public byte Model { get; private set; }
        public string Version { get; private set; }
        public string Name { get; private set; }

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                this.Protocol = binReader.ReadByte();
                this.Model = binReader.ReadByte();
                var versionLength=binReader.ReadByte();
                this.Version = Encoding.UTF8.GetString(binReader.ReadBytes(versionLength), 0, versionLength);
                var nameLength = binReader.ReadByte();
                this.Name = Encoding.UTF8.GetString(binReader.ReadBytes(nameLength), 0, nameLength);
            }
        }
    }
}
