using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    interface IHeroCamera
    {
        IHeroCamera Shutter(bool open);
        IHeroCamera Mode(Mode mode);
        IHeroCamera LocateCamera(bool locate);
        IHeroCamera Update();

        CameraSettings GetRawSettings();
        CameraExtendedSettings GetRawExtendedSettings();

        BacpacStatus BacpacStatus { get; }
        BacpacInformation BacpacInformation { get; }
        CameraInformation Information { get; }

    }
}
