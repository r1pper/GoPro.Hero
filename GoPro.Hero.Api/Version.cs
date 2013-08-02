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

        protected bool Equals(Version other)
        {
            return Major == other.Major && Minor == other.Minor && Build == other.Build && Revision == other.Revision;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Major.GetHashCode();
                hashCode = (hashCode*397) ^ Minor.GetHashCode();
                hashCode = (hashCode*397) ^ Build.GetHashCode();
                hashCode = (hashCode*397) ^ Revision.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Version) obj);
        }
    }
}