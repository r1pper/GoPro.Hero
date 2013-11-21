using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public class Video:Media<VideoParameters>
    {
        private const string EXTENSION_LOW = "LRV";
        private const string EXTENSION_HIGH = "MP4";

        public long LowResolutionSize { get; private set; }

        public async Task<WebResponse> DownloadLowResolutionAsync()
        {
            var lowResName = Name.ToUpper().Replace(EXTENSION_HIGH, EXTENSION_LOW);
            return await base.DownloadAsync(lowResName);
        }

        protected sealed override void Initiaize(VideoParameters token, IGeneralBrowser browser)
        {
            base.Initiaize(token, browser);
        }

        public override string ToString()
        {
            return string.Format("video:{0}", base.Name);
        }
    }
}
