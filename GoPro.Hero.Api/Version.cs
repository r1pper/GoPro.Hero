namespace GoPro.Hero.Api
{
    public class Version
    {
        public Version(byte major, byte minor, byte build, byte revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public byte Major { get; private set; }
        public byte Minor { get; private set; }
        public byte Build { get; private set; }
        public byte Revision { get; private set; }
    }
}