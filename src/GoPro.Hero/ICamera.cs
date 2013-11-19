using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;

namespace GoPro.Hero
{
    public interface ICamera : IFilterProvider
    {
        CameraSettings Settings();
        Task<CameraSettings> SettingsAsync();

        CameraExtendedSettings ExtendedSettings();
        Task<CameraExtendedSettings> ExtendedSettingsAsync();

        CameraInformation Information();
        Task<CameraInformation> InformationAsync();

        BacpacStatus BacpacStatus();
        Task<BacpacStatus> BacpacStatusAsync();
        BacpacInformation BacpacInformation();
        Task<BacpacInformation> BacpacInformationAsync();

        ICamera SetFilter(IFilter<ICamera> filter);

        ICamera Shutter(bool open, bool nonBlocking);
        ICamera Command(CommandRequest<ICamera> command);
        ICamera Command(CommandRequest<ICamera> command, out CommandResponse commandResponse, bool checkStatus = true);
        ICamera PrepareCommand<T>(out T command) where T : CommandRequest<ICamera>;
        ICamera PrepareCommand<T>(int port,out T command) where T : CommandRequest<ICamera>;
        ICamera Power(bool on, bool nonBlocking);
        T PrepareCommand<T>() where T : CommandRequest<ICamera>;
        T PrepareCommand<T>(int port) where T : CommandRequest<ICamera>;
        CommandResponse Command(CommandRequest<ICamera> command, bool checkStatus = true);

        ICamera SetName(string name, bool nonBlocking);
        ICamera GetName(out string name);
        string GetName();
    }
}