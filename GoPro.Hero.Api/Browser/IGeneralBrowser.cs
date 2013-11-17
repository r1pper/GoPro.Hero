using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Browser
{
    public interface IGeneralBrowser
    {
        Uri Address { get; }
        ICamera Camera { get; }
        string Destination { get; }

        void Initialize(ICamera camera, Uri address);
    }
}
