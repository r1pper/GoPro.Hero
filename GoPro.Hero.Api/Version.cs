using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class Version
    {
        public byte Major { get; private set; }
        public byte Minor { get; private set; }
        public byte Build { get; private set; }
        public byte Revision { get; private set; }

        public Version(byte major, byte minor, byte build, byte revision)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
        }
    }
}
