using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;

namespace GoPro.Hero
{
    public interface ICamera : IFilterProvider
    {
        CameraSettings Settings { get; }
        CameraExtendedSettings ExtendedSettings { get; }

        BacpacStatus BacpacStatus { get; }
        BacpacInformation BacpacInformation { get; }
        CameraInformation Information { get; }
        ICamera SetFilter(IFilter<ICamera> filter);

        ICamera Shutter(bool open);
        ICamera Command(CommandRequest<ICamera> command);
        ICamera Command(CommandRequest<ICamera> command, out CommandResponse commandResponse, bool checkStatus = true);
        ICamera PrepareCommand<T>(out T command) where T : CommandRequest<ICamera>;
        ICamera PrepareCommand<T>(int port,out T command) where T : CommandRequest<ICamera>;
        ICamera Power(bool on);
        T PrepareCommand<T>() where T : CommandRequest<ICamera>;
        T PrepareCommand<T>(int port) where T : CommandRequest<ICamera>;
        CommandResponse Command(CommandRequest<ICamera> command, bool checkStatus = true);

        ICamera SetName(string name);
        ICamera GetName(out string name);
        string GetName();
    }
}