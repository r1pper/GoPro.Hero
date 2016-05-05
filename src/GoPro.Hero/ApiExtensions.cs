using GoPro.Hero.Browser.Media;

using System.Threading.Tasks;

namespace GoPro.Hero
{
    public static  class ApiExtensions
    {
        public static GoProMediaBrowser Contents(this ICamera camera)
        {
            return camera.Browse<GoProMediaBrowser>();
        }

        public static  ICameraFacade EnableProtune(this ICameraFacade camera) 
        {
            return camera.Protune(true);
        }

        public static  async Task EnableProtuneAsync(this ICameraFacade camera) 
        {
            await camera.ProtuneAsync(true);
        }

        public static  ICameraFacade DisableProtune(this ICameraFacade camera) 
        {
            return camera.Protune(false);
        }

        public static  async Task DisableProtuneAsync(this ICameraFacade camera) 
        {
            await camera.ProtuneAsync(false);
        }

        public static  ICameraFacade EnableAutoLowLight(this ICameraFacade camera) 
        {
            return camera.AutoLowLight(true);
        }

        public static  async Task EnableAutoLowLightAsync(this ICameraFacade camera) 
        {
            await camera.AutoLowLightAsync(true);
        }

        public static  ICameraFacade DisableAutoLowLight(this ICameraFacade camera) 
        {
            return camera.AutoLowLight(false);
        }

        public static  async Task DisableAutoLowLightAsync(this ICameraFacade camera) 
        {
            await camera.AutoLowLightAsync(false);
        }


        public static  ICameraFacade EnableLocate(this ICameraFacade camera) 
        {
            return camera.Locate(true);
        }

        public static  async Task EnableLocateAsync(this ICameraFacade camera) 
        {
            await camera.LocateAsync(true);
        }

        public static  ICameraFacade DisableLocate(this ICameraFacade camera) 
        {
            return camera.Locate(false);
        }

        public static  async Task DisableLocateAsync(this ICameraFacade camera) 
        {
            await camera.LocateAsync(false);
        }

        public static  ICameraFacade EnableLivePreview(this ICameraFacade camera) 
        {
            return camera.LivePreview(true);
        }

        public static  async Task EnableLivePreviewAsync(this ICameraFacade camera) 
        {
            await camera.LivePreviewAsync(true);
        }

        public static  ICameraFacade DisableLivePreview(this ICameraFacade camera) 
        {
            return camera.LivePreview(false);
        }

        public static  async Task DisableLivePreviewAsync(this ICameraFacade camera) 
        {
            await camera.LivePreviewAsync(false);
        }

        public static  ICameraFacade EnableSpotMeter(this ICameraFacade camera) 
        {
            return camera.SpotMeter(true);
        }

        public static  async Task EnableSpotMeterAsync(this ICameraFacade camera) 
        {
            await camera.SpotMeterAsync(true);
        }

        public static  ICameraFacade DisableSpotMeter(this ICameraFacade camera) 
        {
            return camera.SpotMeter(false);
        }

        public static  async Task DisableSpotMeterAsync(this ICameraFacade camera) 
        {
            await camera.SpotMeterAsync(false);
        }

        public static  ICameraFacade OpenShutter(this ICameraFacade camera) 
        {
            return camera.Shutter(true);
        }

        public static  async Task OpenShutterAsync(this ICameraFacade camera) 
        {
            await camera.ShutterAsync(true);
        }

        public static  ICameraFacade CloseShutter(this ICameraFacade camera) 
        {
            return camera.Shutter(false);
        }

        public static  async Task CloseShutterAsync(this ICameraFacade camera) 
        {
            await camera.ShutterAsync(false);
        }

        public static  ICameraFacade PowerOn(this ICameraFacade camera) 
        {
            camera.Power(true);
            return camera;
        }

        public static  async Task PowerOnAsync(this ICameraFacade camera) 
        {
            await camera.PowerAsync(true);
        }

        public static  ICameraFacade PowerOff(this ICameraFacade camera) 
        {
            camera.Power(false);
            return camera;
        }

        public static  async Task PowerOffAsync(this ICameraFacade camera) 
        {
            await camera.PowerAsync(false);
        }

    }
}
