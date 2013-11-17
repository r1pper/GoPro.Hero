using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser.Media
{
    public class TimeLapse:Media
    {
        public int Group { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }
        public int Count { get { return this.End + 1 - this.Start; } }

        public Stream Thumbnail(int index)
        {
            var name = IndexName(index);
            return base.Thumbnail(name);
        }

        private string IndexName(int index)
        {
            var name = string.Format("G{0:000}{1:0000}", Group, index);
            return name;
        }

        public Stream BigThumbnail(int index)
        {
            var name = IndexName(index);
            return base.BigThumbnail(name);
        }

        public Stream BigThumbnail()
        {
            return base.BigThumbnail(base.Name);
        }

        protected sealed override void Initiaize(JToken token, IGeneralBrowser browser)
        {
            base.Initiaize(token, browser);

            Group = token["g"].Value<int>();
            Start = token["b"].Value<int>();
            End = token["l"].Value<int>();
        }

        private TimeLapse() { }
    }
}
