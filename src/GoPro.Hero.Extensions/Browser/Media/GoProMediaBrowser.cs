using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.Media
{
    public class GoProMediaBrowser:MediaBrowser
    {
        static GoProMediaBrowser()
        {
#if WINDOWS
            WindowsSpecific.UseUnsafeHeaderParsing();
#endif
        }

        public override async Task<IEnumerable<IMedia>> ContentsAsync()
        {
            var response = await Camera.PrepareCommand<CommandGoProMediaList>(8080).SendAsync(checkStatus:false);
            var jsonStream = response.GetResponseStream();

            return Parse(jsonStream);
        }

        protected virtual IEnumerable<IMedia> Parse(Stream jsonStream)
        {
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var mediaList = JObject.Load(jsonReader);

                Id = mediaList["id"].Value<string>();

                var rootMedia = mediaList["media"];
                if (rootMedia.Count() == 0)
                    return new IMedia[] { };

                var media = rootMedia.ElementAt(0);

                Destination = media["d"].Value<string>();

                var fs = media["fs"].Select<JToken,IMedia>(j =>
                {
                    if (j["ls"] != null)//unique to video media
                        return MediaBuilder<VideoParameters>.Create<Video>(VideoParameters(j), this);
                    
                    if (j["b"] != null)//unique to burst mode
                        return MediaBuilder<TimeLapsedImageParameters>.Create<TimeLapsedImage>(TimeLapsedParameters(j), this);

                    if (j["n"] != null)
                        return MediaBuilder<ImageParameters>.Create<Image>(ImageParameters(j), this);

                    return null;
                }
                );

                return fs.ToList();
            }
        }

        private VideoParameters VideoParameters(JToken token)
        {
            var video = new VideoParameters
            {
                Name = token["n"].Value<string>(),
                Size = token["s"].Value<long>(),
                LowResolutionSize = token["ls"].Value<long>()
            };

            return video;
        }

        private ImageParameters ImageParameters(JToken token)
        {
            var image = new ImageParameters
            {
                Name = token["n"].Value<string>(),
                Size = token["s"].Value<long>()
            };

            return image;
        }

        private TimeLapsedImageParameters TimeLapsedParameters(JToken token)
        {
            var timeLapsed = new TimeLapsedImageParameters
            {
                Name = token["n"].Value<string>(),
                Size = token["s"].Value<long>(),
                Group = token["g"].Value<int>(),
                Start = token["b"].Value<int>(),
                End = token["l"].Value<int>()
            };

            return timeLapsed;
        }
    }
}
