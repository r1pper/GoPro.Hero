using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using GoPro.Hero.Api.Exceptions;
using HtmlTidy = Tidy.Core.Tidy;

namespace GoPro.Hero.Api.Browser.FileSystem
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

        public IEnumerable<Node> Nodes(Node node)
        {
            var page = LoadPage(node.Path);
            return Parse(page, node);
        }

        protected abstract IEnumerable<Node> Parse(XElement page, Node parent);


        public WebResponse DownloadContent(Node node)
        {
            var webRequest = WebRequest.CreateHttp(node.Path);

            var res = webRequest.BeginGetResponse(null, null);
            res.AsyncWaitHandle.WaitOne();
            if (!res.IsCompleted)
                throw new GoProException();

            return webRequest.EndGetResponse(res);
        }

        private static XElement LoadPage(Uri address)
        {
            var webRequest = WebRequest.CreateHttp(address);

            var res = webRequest.BeginGetResponse(null, null);
            res.AsyncWaitHandle.WaitOne();
            if (!res.IsCompleted)
                throw new GoProException();

            using (var response = webRequest.EndGetResponse(res))
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