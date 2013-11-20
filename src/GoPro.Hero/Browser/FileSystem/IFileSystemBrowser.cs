using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.FileSystem
{
    public interface IFileSystemBrowser:IGeneralBrowser
    {
        bool IsFile(Uri address);
        Task<IEnumerable<Node>> NodesAsync(Node node);
        IEnumerable<Node> Nodes(Node node);
        Task<WebResponse> DownloadContentAsync(Node node);
        WebResponse DownloadContent(Node node);
    }
}