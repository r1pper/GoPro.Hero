using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using GoPro.Hero.Exceptions;
using HtmlTidy = Tidy.Core.Tidy;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Browser.FileSystem
{
    public abstract class FileSystemBrowser : IFileSystemBrowser
    {
        private const string DESTINATION = "videos/DCIM/100GOPRO";

        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }
        public string Destination { get; set; }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Destination = DESTINATION;
            Camera = camera;
            Address = address;
        }


        public bool IsFile(Uri address)
        {
            return address.ToString().EndsWith("/");
        }

        public async Task<IEnumerable<Node>> NodesAsync(Node node)
        {
            var page = await LoadPageAsync(node.Path);
            return Parse(page, node);
        }

        public IEnumerable<Node> Nodes(Node node)
        {
            return NodesAsync(node).Result;
        }

        protected abstract IEnumerable<Node> Parse(XElement page, Node parent);


        public async Task<WebResponse> DownloadContentAsync(Node node)
        {
            var webRequest = WebRequest.CreateHttp(node.Path);
            return await webRequest.GetResponseAsync();
        }

        public WebResponse DownloadContent(Node node)
        {
            return DownloadContentAsync(node).Result;
        }

        private static async Task<XElement> LoadPageAsync(Uri address)
        {
            var webRequest = WebRequest.CreateHttp(address);

            using (var response = await webRequest.GetResponseAsync())
            {
                var stream = response.GetResponseStream();

                var tidy = new HtmlTidy();
                var page = tidy.ParseXml(stream);

                stream.Dispose();
                return page;
            }
        }
    }
}