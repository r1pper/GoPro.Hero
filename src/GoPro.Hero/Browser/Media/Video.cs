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
    public class Video:Media
    {
        private const string EXTENSION_LOW = "LRV";
        private const string EXTENSION_HIGH = "MP4";

        private int? _duration;
        private int? _profile;

        public long LowResolutionSize { get; private set; }

        public async Task<int> DurationAsync()
        {
            if (_duration == null)
               await ParseInfoAsync();

            return _duration.Value;
        }

        public async Task<int> ProfileAsync()
        {
            if (_profile == null)
                await ParseInfoAsync();

            return _profile.Value;
        }

        public async Task<WebResponse> DownloadLowResolutionAsync()
        {
            var lowResName = Name.ToUpper().Replace(EXTENSION_HIGH, EXTENSION_LOW);
            return await base.DownloadAsync(lowResName);
        }

        protected sealed override void Initiaize(JToken token, IGeneralBrowser browser)
        {
            base.Initiaize(token, browser);

            LowResolutionSize = token["ls"].Value<long>();
        }

        private async Task ParseInfoAsync()
        {
            var jsonStream =await ReadInfoAsync();
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var token = JObject.Load(jsonReader);
                _duration=token["dur"].Value<int>();
                _profile = token["profile"].Value<int>();
            }
        }

        private async Task<Stream> ReadInfoAsync()
        {
            string path = string.Format("{0}/{1}", Browser.Destination, base.Name);
            var response= await Browser.Camera.PrepareCommand<CommandGoProVideoInfo>(Browser.Address.Port).Set(path).SendAsync();
            
            return response.GetResponseStream();
        }

        public override string ToString()
        {
            return string.Format("video:{0}", base.Name);
        }
    }
}
