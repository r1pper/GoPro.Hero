using System.Net;
using System.Threading.Tasks;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.Media
{
    public interface IMediaInitializer<TC,T>where T:MediaParameters where TC :ICamera<TC>, IFilterProvider<TC>
    {
        void Initialize(T token, IMediaBrowser<TC> browser);
    }

    public interface IMedia<T>where T :ICamera<T>, IFilterProvider<T>
    {
        IMediaBrowser<T> Browser { get; }
        string Name { get;  }
        long Size { get; }

        Task<WebResponse> DownloadAsync();
    }
}
