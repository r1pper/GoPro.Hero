using System;

namespace GoPro.Hero.Browser
{
    public interface IGeneralBrowser
    {
        Uri Address { get; }
        ICamera Camera { get; }
        string Destination { get; }

        void Initialize(ICamera camera, Uri address);
    }
}
