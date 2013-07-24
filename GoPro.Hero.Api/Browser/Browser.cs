using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using GoPro.Hero.Api.Exceptions;

namespace GoPro.Hero.Api.Browser
{
    public abstract class Browser : IBrowser
    {
        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }

        public void Initialize(ICamera camera, Uri address)
        {
            Camera = camera;
            Address = address;
        }


        public bool IsFile(Uri address)
        {
            try
            {
                var page = LoadPage(address);
                Parse(page);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Node> Nodes()
        {
            var page = LoadPage(Address);
            return Parse(page);
        }

        public IEnumerable<Node> Nodes(Node node)
        {
            var page = LoadPage(node.Path);
            return Parse(page);
        }

        private XElement LoadPage(Uri address)
        {
            var webRequest = WebRequest.CreateHttp(address);

            var res = webRequest.BeginGetResponse(null, null);
            res.AsyncWaitHandle.WaitOne();
            if (!res.IsCompleted)
                throw new GoProException();

            using (var response = webRequest.EndGetResponse(res))
            {
                var stream = response.GetResponseStream();
                var page = XElement.Load(stream);
                stream.Dispose();
                return page;
            }
        }

        protected abstract IEnumerable<Node> Parse(XElement page);
    }
}