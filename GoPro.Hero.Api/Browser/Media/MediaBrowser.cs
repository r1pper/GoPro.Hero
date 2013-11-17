using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser.Media
{
    public class MediaBrowser:IGeneralBrowser
    {
        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }

        public string Id { get; private set; }
        public string Destination { get; private set; }

        public Media this[string name]
        {
            get { return Content(name); }
        }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        public Media Content(string name)
        {
            return Contents().Where(c => c.Name == name).FirstOrDefault();
        }

        public T Content<T>(string name)where T:Media
        {
            return Contents<T>().Where(c => c.Name == name).FirstOrDefault();
        }

        public IEnumerable<Media> Contents()
        {
            var response = Camera.PrepareCommand<CommandGoProMediaList>(8080).Send();
            var jsonStream = response.GetResponseStream();

            return Parse(jsonStream);
        }

        public IEnumerable<T> Contents<T>() where T : Media
        {
            return Contents().Where(c => c.GetType() == typeof(T)).Cast<T>();
        }

        public IEnumerable<TimeLapsedImage> TimeLapses()
        {
            return Contents<TimeLapsedImage>();
        }

        public IEnumerable<Video> Videos()
        {
            return Contents<Video>();
        }

        public IEnumerable<Image> Images()
        {
            return Contents<Image>();
        }

        private IEnumerable<Media> Parse(Stream jsonStream)
        {
            using (var jsonReader = new JsonTextReader(new StreamReader(jsonStream)))
            {
                var mediaList = JObject.Load(jsonReader);

                Id = mediaList["id"].Value<string>();

                var media = mediaList["media"].ElementAt(0);

                Destination = media["d"].Value<string>();

                var fs = media["fs"].Select(j =>
                {
                    if (j["ls"] != null)//unique to video media
                        return Media.Create<Video>(j, this);

                    if (j["b"] != null)//unique to burst mode
                        return Media.Create<TimeLapsedImage>(j, this);

                    if (j["n"] != null)
                        return Media.Create<Image>(j, this);

                    return default(Media);
                }
                );

                return fs.ToList();
            }
        }
    }
}
