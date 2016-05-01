using System.Net;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public class Video:Media<VideoParameters>
    {
        private const string EXTENSION_LOW = "LRV";
        private const string EXTENSION_HIGH = "MP4";

        public long LowResolutionSize { get; private set; }

        public async Task<WebResponse> DownloadLowResolutionAsync()
        {
            var lowResName = Name.ToUpper().Replace(EXTENSION_HIGH, EXTENSION_LOW);
            return await base.DownloadAsync(lowResName);
        }

        protected sealed override void Initiaize(VideoParameters token, IMediaBrowser browser)
        {
            base.Initiaize(token, browser);
            LowResolutionSize = token.LowResolutionSize;
        }

        public override string ToString()
        {
            return string.Format("video:{0}", base.Name);
        }
    }
}
