using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser.Media;

namespace GoPro.Hero.Browser.Media
{
    public static class TimeLapsedImageExtensions
    {
        public static async Task<Stream> ThumbnailAsync(this TimeLapsedImage image,  int index)
        {
            var name = image.IndexName(index);
            return await image.ThumbnailAsync(name);
        }

        public static async Task<Stream> BigThumbnailAsync(this TimeLapsedImage image,int index)
        {
            var name = image.IndexName(index);
            return await image.BigThumbnailAsync(name);
        }

        public static async Task<Stream> BigThumbnailAsync(this TimeLapsedImage image)
        {
            return await image.BigThumbnailAsync(image.Name);
        }
    }
}
