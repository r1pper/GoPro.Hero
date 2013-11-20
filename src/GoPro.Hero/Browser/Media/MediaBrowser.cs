using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Browser.Media
{
    public class MediaBrowser:IMediaBrowser
    {
        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }

        public string Id { get; private set; }
        public string Destination { get; private set; }

        public Media this[string name]
        {
            get { return ContentAsync(name).Await(); }
        }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        public Media Content(string name)
        {
            return ContentAsync(name).Await();
        }

        public IEnumerable<Media> Contents()
        {
            return ContentsAsync().Await();
        }

        public async Task<Media> ContentAsync(string name)
        {
            return (await ContentsAsync()).Where(c => c.Name == name).FirstOrDefault();
        }

        public async Task<T> ContentAsync<T>(string name)where T:Media
        {
            return (await ContentsAsync<T>()).Where(c => c.Name == name).FirstOrDefault();
        }

        public async Task<IEnumerable<Media>> ContentsAsync()
        {
            var response = await Camera.PrepareCommand<CommandGoProMediaList>(8080).SendAsync();
            var jsonStream = response.GetResponseStream();

            return Parse(jsonStream);
        }

        public async Task<IEnumerable<T>> ContentsAsync<T>() where T : Media
        {
            return  (await ContentsAsync()).Where(c => c.GetType() == typeof(T)).Cast<T>();
        }

        public async Task<IEnumerable<TimeLapsedImage>> TimeLapsesAsync()
        {
            return await ContentsAsync<TimeLapsedImage>();
        }

        public async Task<IEnumerable<Video>> VideosAsync()
        {
            return await ContentsAsync<Video>();
        }

        public async Task<IEnumerable<Image>> ImagesAsync()
        {
            return await ContentsAsync<Image>();
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
