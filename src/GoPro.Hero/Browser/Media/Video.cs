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

        public long LowResolutionSize { get; private set; }

        public int Duration { get; private set; }
        public int Profile { get; private set; }

        public async Task<WebResponse> DownloadLowResolutionAsync()
        {
            var lowResName = Name.ToUpper().Replace(EXTENSION_HIGH, EXTENSION_LOW);
            return await base.DownloadAsync(lowResName);
        }

        protected sealed override void Initiaize(JToken token, IGeneralBrowser browser)
        {
            base.Initiaize(token, browser);

            LowResolutionSize = token["ls"].Value<long>();

            ParseInfo();
        }

        private void ParseInfo()
        {
            var jsonStream = ReadInfo();
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var token = JObject.Load(jsonReader);
                Duration=token["dur"].Value<int>();
                Profile = token["profile"].Value<int>();
            }
        }

        private Stream ReadInfo()
        {
            string path = string.Format("{0}/{1}", Browser.Destination, base.Name);
            var response=Browser.Camera.PrepareCommand<CommandGoProVideoInfo>(Browser.Address.Port).Set(path).Send();
            
            return response.GetResponseStream();
        }

        public override string ToString()
        {
            return string.Format("video:{0}", base.Name);
        }
    }
}
