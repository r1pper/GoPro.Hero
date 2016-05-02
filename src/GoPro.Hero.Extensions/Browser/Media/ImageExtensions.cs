using System.IO;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public static class ImageExtensions
    {
        public static async Task<Stream> BigThumbnailAsync(this Image image)
        {
            return await image.BigThumbnailAsync(image.Name);
        }

        public static async Task<IMediaBrowser> DeleteAsync(this Image image)
        {
            await image.DeleteFile(image.Name);
            return image.Browser;
        }
    }
}
