using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Exceptions;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public abstract class Media<TM>:IMedia,IMediaInitializer<TM> where TM:MediaParameters
    {
        protected const string ABSOLUTE_PATH = "{0}videos/DCIM/100GOPRO/{1}";
 
        public IGeneralBrowser Browser { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; }

        public virtual async Task<Stream> ThumbnailAsync()
        {
            return await ThumbnailAsync(Name);
        }

        public virtual async Task<WebResponse> DownloadAsync()
        {
            return await DownloadAsync(Name);
        }

        protected virtual async Task<WebResponse> DownloadAsync(string name)
        {
            var path = string.Format(ABSOLUTE_PATH, Browser.Address, name);
            var webRequest = WebRequest.CreateHttp(path);

            return await webRequest.GetResponseAsync();
        }

        protected async Task<Stream> ThumbnailAsync(string name)
        {
            string path = string.Format("{0}/{1}", Browser.Destination, name);
            var request = await Browser.Camera.PrepareCommand<CommandGoProThumbnail>(Browser.Address.Port).Set(path).SendAsync();
            return request.GetResponseStream();
        }

        protected async Task<Stream> BigThumbnailAsync(string name)
        {
            string path = string.Format("{0}/{1}", Browser.Destination, name);
            var request =await Browser.Camera.PrepareCommand<CommandGoProBigThumbnail>(Browser.Address.Port).Set(path).SendAsync();
            return request.GetResponseStream();
        }
     
        protected virtual void Initiaize(TM token, IGeneralBrowser browser)
        {
            //Name = token["n"].Value<string>();
            //Size = token["s"].Value<long>();

            Name = token.Name;
            Size = token.Size;

            Browser = browser;
        }

        public override bool Equals(object obj)
        {
            var media = obj as Media<TM>;
            if (media == null) return false;
            return Name == media.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 19;
        }

        void IMediaInitializer<TM>.Initialize(TM token, IGeneralBrowser browser)
        {
            Initiaize(token, browser);
        }

        internal protected static T Create<T>(TM token, IGeneralBrowser browser) where T:Media<TM>,IMediaInitializer<TM>
        {
            var instance = Activator.CreateInstance<T>();
            instance.Initialize(token, browser);

            return instance;
        }
    }
}
