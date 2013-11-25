using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
