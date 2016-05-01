using GoPro.Hero.Filtering;
using System;

namespace GoPro.Hero.Browser
{
    public interface IGeneralBrowser<T> where T :ICamera<T>, IFilterProvider<T>
    {
        Uri Address { get; }
        T Camera { get; }
        string Destination { get; }

        void Initialize(T camera, Uri address);
    }
}
