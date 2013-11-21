using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser.Media;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Browser.Media
{
    public static class MediaExtensions
    {
        public static async Task<Stream> ThumbnailAsync(this IMedia media)
        {
            return await media.ThumbnailAsync(media.Name);
        }

        internal static async Task<Stream> ThumbnailAsync(this IMedia media, string name)
        {
            string path = string.Format("{0}/{1}", media.Browser.Destination, name);
            var request = await media.Browser.Camera.PrepareCommand<CommandGoProThumbnail>(media.Browser.Address.Port).Set(path).SendAsync(checkStatus:false);
            return request.GetResponseStream();
        }

        internal static async Task<Stream> BigThumbnailAsync(this IMedia media, string name)
        {
            string path = string.Format("{0}/{1}", media.Browser.Destination, name);
            var request = await media.Browser.Camera.PrepareCommand<CommandGoProBigThumbnail>(media.Browser.Address.Port).Set(path).SendAsync(checkStatus:false);
            return request.GetResponseStream();
        }
    }
}
