using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class MediaBrowser:IGeneralBrowser
    {
        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }

        public string Id { get; private set; }
        public string Destination { get; private set; }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        public IEnumerable<Media> Contents()
        {
            var response = Camera.PrepareCommand<CommandGoProMediaList>(8080).Send();
            var jsonStream = response.GetResponseStream();

            return Parse(jsonStream);
        }

        private IEnumerable<Media> Parse(Stream jsonStream)
        {
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var mediaList = JObject.Load(jsonReader);

                Id = mediaList["id"].Value<string>();

                var media = mediaList["media"].Values().Select(j =>
                {
                    if (j["d"] != null)//unique to media info
                    {
                        Destination = j["d"].Value<string>();
                        return default(Media);
                    }

                    if (j["ls"] != null)//unique to video media
                        return new Video(j, this);

                    if (j["b"] != null)//unique to burst mode
                        return new TimeLapse(j, this);

                    if (j["n"] != null)
                        return new Image(j, this);

                    return default(Media);
                }
                ).Except(null);

                return media.ToList();
            }
        }
    }
}
