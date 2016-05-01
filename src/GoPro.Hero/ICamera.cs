using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;

namespace GoPro.Hero
{
    public interface ICamera : IFilterProvider<ICamera>
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

        ICamera Shutter(bool open);
        ICamera Command(CommandRequest<ICamera> command);
        Task CommandAsync(CommandRequest<ICamera> command);
        CommandResponse Command(CommandRequest<ICamera> command, bool checkStatus = true);
        Task<CommandResponse> CommandAsync(CommandRequest<ICamera> command, bool checkStatus = true);

        ICamera Power(bool on);
        T PrepareCommand<T>() where T : CommandRequest<ICamera>;
        T PrepareCommand<T>(int port) where T : CommandRequest<ICamera>;


        ICamera SetName(string name);
        Task SetNameAsync(string name);
        Task<string> GetNameAsync();
    }
}