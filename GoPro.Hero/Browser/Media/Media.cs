using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Exceptions;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser.Media
{
    public abstract class Media:IMedia
    {
        protected const string ABSOLUTE_PATH = "http://{0}/videos/DCIM/{1}";
 
        public IGeneralBrowser Browser { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; }

        public virtual Stream Thumbnail()
        {
            return Thumbnail(Name);
        }

        public virtual WebResponse Download()
        {
            return Download(Name);
        }

        protected virtual WebResponse Download(string name)
        {
            var path = string.Format(ABSOLUTE_PATH, Browser.Address, name);
            var webRequest = WebRequest.CreateHttp(path);

            var res = webRequest.BeginGetResponse(null, null);
            res.AsyncWaitHandle.WaitOne();
            if (!res.IsCompleted)
                throw new GoProException();

            return webRequest.EndGetResponse(res);
        }

        protected Stream Thumbnail(string name)
        {
            string path = string.Format("{0}/{1}", Browser.Destination, name);
            var request = Browser.Camera.PrepareCommand<CommandGoProThumbnail>(Browser.Address.Port).Set(path).Send();
            return request.GetResponseStream();
        }

        protected Stream BigThumbnail(string name)
        {
            string path = string.Format("{0}/{1}", Browser.Destination, name);
            var request = Browser.Camera.PrepareCommand<CommandGoProBigThumbnail>(Browser.Address.Port).Set(path).Send();
            return request.GetResponseStream();
        }
     
        protected virtual void Initiaize(JToken token, IGeneralBrowser browser)
        {
            Name = token["n"].Value<string>();
            Size = token["s"].Value<long>();

            Browser = browser;
        }

        public override bool Equals(object obj)
        {
            var media = obj as Media;
            if (media == null) return false;
            return Name == media.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 19;
        }

        void IMedia.Initialize(JToken token, IGeneralBrowser browser)
        {
            Initiaize(token, browser);
        }

        internal static T Create<T>(JToken token, IGeneralBrowser browser) where T:IMedia
        {
            var instance = Activator.CreateInstance<T>();
            instance.Initialize(token, browser);

            return instance;
        }
    }
}
