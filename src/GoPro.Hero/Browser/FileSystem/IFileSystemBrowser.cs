using GoPro.Hero.Filtering;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.FileSystem
{
    public interface IFileSystemBrowser<T>:IGeneralBrowser<T> where T :ICamera<T>, IFilterProvider<T>
    {
        bool IsFile(Uri address);
        Task<IEnumerable<Node<T>>> NodesAsync(Node<T> node);
        IEnumerable<Node<T>> Nodes(Node<T> node);
        Task<WebResponse> DownloadContentAsync(Node<T> node);
        WebResponse DownloadContent(Node<T> node);
    }
}