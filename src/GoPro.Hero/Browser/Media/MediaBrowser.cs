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
    public abstract class MediaBrowser:IMediaBrowser
    {
        public Uri Address { get; private set; }
        public ICamera Camera { get; private set; }

        public string Id { get; protected set; }
        public string Destination { get; protected set; }

        public IMedia this[string name]
        {
            get { return ContentAsync(name).Result; }
        }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        public IMedia Content(string name)
        {
            return ContentAsync(name).Result;
        }

        public IEnumerable<IMedia> Contents()
        {
            return ContentsAsync().Result;
        }

        public async Task<IMedia> ContentAsync(string name)
        {
            return (await ContentsAsync()).Where(c => c.Name == name).FirstOrDefault();
        }

        public async Task<T> ContentAsync<T>(string name)where T:IMedia
        {
            return (await ContentsAsync<T>()).Where(c => c.Name == name).FirstOrDefault();
        }

        public abstract Task<IEnumerable<IMedia>> ContentsAsync();

        public async Task<IEnumerable<T>> ContentsAsync<T>() where T : IMedia
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
    }
}
