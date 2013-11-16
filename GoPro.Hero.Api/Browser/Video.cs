using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class Video:Media
    {
        private const string EXTENSION_LOW = "LRV";
        private const string EXTENSION_HIGH = "MP4";

        public long LowResolutionSize { get; private set; }

        internal Video(JToken token, MediaBrowser browser):base(token,browser)
        {
            LowResolutionSize = token["ls"].Value<long>();
        }

        public WebResponse DownloadLowResolution()
        {
            var lowResName = Name.ToUpper().Replace(EXTENSION_HIGH, EXTENSION_LOW);
            return base.Download(lowResName);
        }
    }
}
