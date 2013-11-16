using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class Image : Media
    {
        internal Image(JToken token, MediaBrowser browser) : base(token, browser) { }

        public Stream BigThumbnail()
        {
            return base.BigThumbnail(base.Name);
        }
    }
}
