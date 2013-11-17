using System;
using System.Collections.Generic;
using System.Net;

namespace GoPro.Hero.Api.Browser.FileSystem
{
    public interface IFileSystemBrowser:IGeneralBrowser
    {
        bool IsFile(Uri address);
        IEnumerable<Node> Nodes(Node node);
        WebResponse DownloadContent(Node node);
    }
}