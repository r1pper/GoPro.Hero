using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Browser.FileSystem;
using System;

namespace GoPro.Hero
{
    public interface ICamera<T> where T :ICamera<T>,IFilterProvider<T>
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
        TC PrepareCommand<TC>() where TC : CommandRequest<T>;
        TC PrepareCommand<TC>(int port) where TC : CommandRequest<T>;

        Node<T> FileSystem<TF>(int port = 8080) where TF : IFileSystemBrowser<T>;

        T Chain(params Func<T, Task>[] fs);   
        T Chain<TD>(Func<T, TD> f, out TD output);
        T Chain<TD>(Func<T, Task<TD>> f, out TD output);
        T Chain<TD>(Func<T, TD> f, Action<TD> output);
        T Chain<TD>(Func<T, Task<TD>> f, Action<TD> output);
        Task ChainAsync(params Func<T, Task>[] fs);
        Task ChainAsync<TD>(Func<T, Task<TD>> f, Action<TD> output);
    }
}