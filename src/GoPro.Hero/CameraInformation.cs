using System;
using System.IO;
using System.Text;
using GoPro.Hero.Utilities;

namespace GoPro.Hero
{
    public class CameraInformation
    {
        public byte Protocol { get; private set; }
        public Model Model { get; private set; }
        public string Version { get; private set; }
        public string Name { get; private set; }

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                Protocol = binReader.ReadByte();
                Model = binReader.ReadEnum<Model>();
                var versionLength = binReader.ReadByte();
                Version = Encoding.UTF8.GetString(binReader.ReadBytes(versionLength), 0, versionLength);
                var nameLength = binReader.ReadByte();
                nameLength = (byte) Math.Min(nameLength, (int) (stream.Length - stream.Position));
                Name = Encoding.UTF8.GetString(binReader.ReadBytes(nameLength), 0, nameLength);
            }
        }
    }
}