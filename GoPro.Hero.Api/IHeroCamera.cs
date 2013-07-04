using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;

namespace GoPro.Hero.Api
{
    public interface IHeroCamera
    {
        IHeroCamera Shutter(bool open);
        IHeroCamera Command(CommandRequest command);
        IHeroCamera Command(CommandRequest command,out CommandResponse commandResponse,bool checkStatus=true);
        IHeroCamera PrepareCommand<T>(out T command) where T : CommandRequest;
        IHeroCamera Power(bool on);
        T PrepareCommand<T>() where T : CommandRequest;
        CommandResponse Command(CommandRequest command,bool checkStatus=true);

        CameraSettings Settings { get; }
        CameraExtendedSettings ExtendedSettings {get;}
  
        BacpacStatus BacpacStatus { get; }
        BacpacInformation BacpacInformation { get; }
        CameraInformation Information { get; }

    }
}
