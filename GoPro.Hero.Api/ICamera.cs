using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api
{
    public interface ICamera:IFilterProvider
    {
        ICamera SetFilter(IFilter<ICamera> filter);

        ICamera Shutter(bool open);
        ICamera Command(CommandRequest<ICamera> command);
        ICamera Command(CommandRequest<ICamera> command,out CommandResponse commandResponse,bool checkStatus=true);
        ICamera PrepareCommand<T>(out T command) where T : CommandRequest<ICamera>;
        ICamera Power(bool on);
        T PrepareCommand<T>() where T : CommandRequest<ICamera>;
        CommandResponse Command(CommandRequest<ICamera> command,bool checkStatus=true);

        ICamera SetName(string name);
        ICamera GetName(out string name);
        string GetName();

        CameraSettings Settings { get; }
        CameraExtendedSettings ExtendedSettings {get;}
  
        BacpacStatus BacpacStatus { get; }
        BacpacInformation BacpacInformation { get; }
        CameraInformation Information { get; }

    }
}
