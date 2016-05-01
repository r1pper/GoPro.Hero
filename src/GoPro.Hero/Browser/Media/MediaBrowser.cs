using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        protected virtual void Initialize(ICamera camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        void IGeneralBrowser.Initialize(ICamera camera, Uri address)
        {
            Initialize(camera, address);
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

        public async Task<TC> ContentAsync<TC>(string name)where TC: IMedia
        {
            return (await ContentsAsync<TC>()).Where(c => c.Name == name).FirstOrDefault();
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
