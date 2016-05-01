using GoPro.Hero.Filtering;

namespace GoPro.Hero.Hero3
{
    public class Hero3Camera : Camera
    {
        public Hero3Camera(Bacpac bacpac) : base(bacpac)
        {
            var filter = FilterGeneric.Create("GoPro.Hero.Hero3.Hero3FilterScheme.xml");
            SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address))
        {
        }

    }
}