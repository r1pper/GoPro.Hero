namespace GoPro.Hero.Hero3
{
    public class Hero3Camera : LegacyCamera
    {
        public Hero3Camera(Bacpac bacpac) : base(bacpac)
        {
            var filter = LegacyFilter.Hero3Profile();
            SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address))
        {
        }

    }
}