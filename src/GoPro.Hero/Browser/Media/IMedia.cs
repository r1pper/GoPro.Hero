using System.Net;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public interface IMediaInitializer<T>where T:MediaParameters
    {
        void Initialize(T token, IMediaBrowser browser);
    }

    public interface IMedia
    {
        IMediaBrowser Browser { get; }
        string Name { get;  }
        long Size { get; }

        Task<WebResponse> DownloadAsync();
    }
}
