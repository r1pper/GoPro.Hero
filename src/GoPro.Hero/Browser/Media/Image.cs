using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Browser.Media
{
    public class Image : Media<ImageParameters>
    {
        public async Task<Stream> BigThumbnailAsync()
        {
            return await base.BigThumbnailAsync(base.Name);
        }

        public override string ToString()
        {
            return string.Format("image:{0}", base.Name);
        }
    }
}
