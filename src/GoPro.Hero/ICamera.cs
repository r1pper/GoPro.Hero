using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Browser.FileSystem;
using System;

namespace GoPro.Hero
{
    public interface ICamera
    {
        void Command(ICommandRequest command);
        Task CommandAsync(ICommandRequest command);
        CommandResponse Command(ICommandRequest command, bool checkStatus = true);
        Task<CommandResponse> CommandAsync(ICommandRequest command, bool checkStatus = true);

        TC PrepareCommand<TC>() where TC : CommandRequest;
        TC PrepareCommand<TC>(int port) where TC : CommandRequest;

        Node FileSystem<TF>(int port = 8080) where TF : IFileSystemBrowser;
    }

    public interface ICamera<T>:ICamera where T :ICamera<T>,IFilterProvider<T>
    {
        T SetFilter(IFilter<T> filter);

        T Shutter(bool open);
        Task ShutterAsync(bool open);
        T Command(CommandRequest<T> command);
        Task CommandAsync(CommandRequest<T> command);
        CommandResponse Command(CommandRequest<T> command, bool checkStatus = true);
        Task<CommandResponse> CommandAsync(CommandRequest<T> command, bool checkStatus = true);

        T Power(bool on);
        Task PowerAsync(bool on);
        new TC PrepareCommand<TC>() where TC : CommandRequest<T>;
        new TC PrepareCommand<TC>(int port) where TC : CommandRequest<T>;

        ICameraFacade<T> UnifiedApi();
    }
}