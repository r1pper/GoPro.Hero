using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.Media
{
    public abstract class Media<TC,TM>:IMedia<TC>,IMediaInitializer<TC,TM> where TM:MediaParameters where TC :ICamera<TC>,IFilterProvider<TC>
    {
        protected const string ABSOLUTE_PATH = "{0}videos/DCIM/100GOPRO/{1}";
 
        public IMediaBrowser<TC> Browser { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; }

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
     
        protected virtual void Initiaize(TM token, IMediaBrowser<TC> browser)
        {
            //Name = token["n"].Value<string>();
            //Size = token["s"].Value<long>();

            Name = token.Name;
            Size = token.Size;

            Browser = browser;
        }

        public override bool Equals(object obj)
        {
            var media = obj as Media<TC,TM>;
            if (media == null) return false;
            return Name == media.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 19;
        }

        void IMediaInitializer<TC,TM>.Initialize(TM token, IMediaBrowser<TC> browser)
        {
            Initiaize(token, browser);
        }

        internal protected static T Create<T>(TM token, IMediaBrowser<TC> browser) where T:Media<TC,TM>,IMediaInitializer<TC,TM>
        {
            var instance = Activator.CreateInstance<T>();
            instance.Initialize(token, browser);

            return instance;
        }
    }
}
