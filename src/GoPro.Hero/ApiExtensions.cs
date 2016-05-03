using GoPro.Hero.Browser.Media;
using GoPro.Hero.Filtering;

using System.Threading.Tasks;

namespace GoPro.Hero
{
    public static  class ApiExtensions
    {
        public static MediaBrowser Contents(this ICamera camera)
        {
            return camera.Browse<GoProMediaBrowser>();
        }

        public static  ICameraFacade<T> EnableProtune<T>(this ICameraFacade<T> camera) where T: ICamera<T>,IFilterProvider<T>
        {
            return camera.Protune(true);
        }

        public static  async Task EnableProtuneAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.ProtuneAsync(true);
        }

        public static  ICameraFacade<T> DisableProtune<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.Protune(false);
        }

        public static  async Task DisableProtuneAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.ProtuneAsync(false);
        }

        public static  ICameraFacade<T> EnableAutoLowLight<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.AutoLowLight(true);
        }

        public static  async Task EnableAutoLowLightAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.AutoLowLightAsync(true);
        }

        public static  ICameraFacade<T> DisableAutoLowLight<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.AutoLowLight(false);
        }

        public static  async Task DisableAutoLowLightAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.AutoLowLightAsync(false);
        }


        public static  ICameraFacade<T> EnableLocate<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.Locate(true);
        }

        public static  async Task EnableLocateAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.LocateAsync(true);
        }

        public static  ICameraFacade<T> DisableLocate<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.Locate(false);
        }

        public static  async Task DisableLocateAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.LocateAsync(false);
        }

        public static  ICameraFacade<T> EnableLivePreview<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.LivePreview(true);
        }

        public static  async Task EnableLivePreviewAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.LivePreviewAsync(true);
        }

        public static  ICameraFacade<T> DisableLivePreview<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.LivePreview(false);
        }

        public static  async Task DisableLivePreviewAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.LivePreviewAsync(false);
        }

        public static  ICameraFacade<T> EnableSpotMeter<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.SpotMeter(true);
        }

        public static  async Task EnableSpotMeterAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.SpotMeterAsync(true);
        }

        public static  ICameraFacade<T> DisableSpotMeter<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.SpotMeter(false);
        }

        public static  async Task DisableSpotMeterAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.SpotMeterAsync(false);
        }

        public static  ICameraFacade<T> OpenShutter<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.Shutter(true);
        }

        public static  async Task OpenShutterAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.ShutterAsync(true);
        }

        public static  ICameraFacade<T> CloseShutter<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            return camera.Shutter(false);
        }

        public static  async Task CloseShutterAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.ShutterAsync(false);
        }

        public static  ICameraFacade<T> PowerOn<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            camera.Power(true);
            return camera;
        }

        public static  async Task PowerOnAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.PowerAsync(true);
        }

        public static  ICameraFacade<T> PowerOff<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            camera.Power(false);
            return camera;
        }

        public static  async Task PowerOffAsync<T>(this ICameraFacade<T> camera) where T : ICamera<T>, IFilterProvider<T>
        {
            await camera.PowerAsync(false);
        }

    }
}
