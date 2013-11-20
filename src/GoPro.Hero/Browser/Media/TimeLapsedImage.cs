using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public class TimeLapsedImage:Media<TimeLapsedImageParameters>,IEnumerable<WebResponse>
    {
        public int Group { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }
        public int Count { get { return this.End + 1 - this.Start; } }

        public async Task<Stream> ThumbnailAsync(int index)
        {
            var name = IndexName(index);
            return await base.ThumbnailAsync(name);
        }

        private string IndexName(int index)
        {
            var name = string.Format("G{0:000}{1:0000}", Group, index);
            return name;
        }

        public async Task<Stream> BigThumbnailAsync(int index)
        {
            var name = IndexName(index);
            return await base.BigThumbnailAsync(name);
        }

        public async Task<Stream> BigThumbnailAsync()
        {
            return await base.BigThumbnailAsync(base.Name);
        }

        protected sealed override void Initiaize(TimeLapsedImageParameters token, IGeneralBrowser browser)
        {
            base.Initiaize(token, browser);
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
