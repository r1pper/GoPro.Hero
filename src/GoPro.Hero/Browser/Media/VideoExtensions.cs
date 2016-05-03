using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser.Media;
using GoPro.Hero.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public static class VideoExtensions
    {
        public class VideoInfo
        {
            public int Duration { get; set; }
            public int Profile { get; set; }
        }


        public static async Task<IMediaBrowser> DeleteAsync(this Video video)
        {
            await video.DeleteFile(video.Name);
            return video.Browser;
        }

        public static async Task<int> DurationAsync(this Video video)
        {
            return (await video.InfoAsync()).Duration;
        }

        public static async Task<int> ProfileAsync(this Video video)
        {
            return (await video.InfoAsync()).Profile;
        }

        public static async Task<VideoInfo> InfoAsync(this Video video)
        {
            var jsonStream = await ReadInfoAsync(video);
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var info = new VideoInfo();

                var token = JObject.Load(jsonReader);
                info.Duration = token["dur"].Value<int>();
                info.Profile = token["profile"].Value<int>();

                return info;
            }
        }

        private static async Task<Stream> ReadInfoAsync(Video video)
        {
            string path = string.Format("{0}/{1}", video.Browser.Destination, video.Name);
            var response = await video.Browser.Camera.PrepareCommand<CommandGoProVideoInfo>(video.Browser.Address.Port).Set(path).SendAsync(checkStatus:false);

            return response.GetResponseStream();
        }
    }
}
