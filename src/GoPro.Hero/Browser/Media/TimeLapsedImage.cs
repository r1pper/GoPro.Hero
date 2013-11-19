using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public class TimeLapsedImage:Media,IEnumerable<WebResponse>
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

        public override string ToString()
        {
            return string.Format("timelapsed:{0}, count:{1}", base.Name,Count);
        }

        public IEnumerator<WebResponse> GetEnumerator()
        {
            return new TimeLapsedImageEnumerator(this);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class TimeLapsedImageEnumerator : IEnumerator<WebResponse>
        {
            public int CurrentIndex { get; private set; }
            public TimeLapsedImage Owner { get; private set; }
            public WebResponse Current{get;private set;}
            object IEnumerator.Current{get { return Current; }}

            public bool MoveNext()
            {
                CurrentIndex++;
                return CurrentIndex<=Owner.End;
            }

            public void Reset()
            {
                CurrentIndex = Owner.Start;
            }

            public void Dispose()
            {

            }

            public TimeLapsedImageEnumerator(TimeLapsedImage owner)
            {
                Owner = owner;
                CurrentIndex = Owner.Start;
            }
        }
    }
}
