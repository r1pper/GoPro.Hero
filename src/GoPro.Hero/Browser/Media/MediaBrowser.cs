using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.Media
{
    public abstract class MediaBrowser<T>:IMediaBrowser<T>where T :ICamera<T>,IFilterProvider<T>
    {
        public Uri Address { get; private set; }
        public T Camera { get; private set; }

        public string Id { get; protected set; }
        public string Destination { get; protected set; }

        public IMedia this[string name]
        {
            get { return ContentAsync(name).Result; }
        }

        protected virtual void Initialize(T camera, Uri address)
        {
            Address = address;
            Camera = camera;
        }

        void IGeneralBrowser<T>.Initialize(T camera, Uri address)
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

        public async Task<IEnumerable<TC>> ContentsAsync<TC>() where TC : IMedia
        {
            return  (await ContentsAsync()).Where(c => c.GetType() == typeof(T)).Cast<TC>();
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
