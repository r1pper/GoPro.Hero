using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPro.Hero.Hero3
{
    public static class Hero3Extensions
    {

        public static Node<Camera> Browse(this Camera camera)
        {
            return camera.FileSystem<AmbarellaBrowser<Camera>>();
        }

        public static Camera VideoResolution(this Camera camera, VideoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task VideoResolutionAsync(this Camera camera, VideoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync();
        }

        public static async Task<VideoResolution> VideoResolutionAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoResolution;
        }

        public static VideoResolution VideoResolution(this Camera camera)
        {
            return camera.ExtendedSettings().VideoResolution;
        }

        public static IEnumerable<VideoResolution> ValidVideoResolution(this Camera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }


        public static Camera Orientation(this Camera camera, Orientation orientation)
        {
            camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute();
            return camera;
        }

        public static async Task OrientationAsync(this Camera camera, Orientation orientation)
        {
            await camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync();
        }

        public static Orientation Orientation(this Camera camera)
        {
            return camera.ExtendedSettings().Orientation;
        }

        public static async Task<Orientation> OrientationAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Orientation;
        }


        public static Camera TimeLapse(this Camera camera, TimeLapse timeLapse)
        {
            camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute();
            return camera;
        }

        public static async Task TimeLapseAsync(this Camera camera, TimeLapse timeLapse)
        {
            await camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync();
        }

        public static TimeLapse TimeLapse(this Camera camera)
        {
            return camera.ExtendedSettings().TimeLapse;
        }

        public static async Task<TimeLapse> TimeLapseAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).TimeLapse;
        }


        public static Camera BeepSound(this Camera camera, BeepSound beepSound)
        {
            camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute();
            return camera;
        }

        public static async Task BeepSoundAsync(this Camera camera, BeepSound beepSound)
        {
            await camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync();
        }

        public static async Task<BeepSound> BeepSoundAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BeepSound;
        }

        public static BeepSound BeepSound(this Camera camera)
        {
            return camera.ExtendedSettings().BeepSound;
        }


        public static Camera Protune(this Camera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraProtune>().Set(state).Execute();
            return camera;
        }

        public static async Task ProtuneAsync(this Camera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync();
        }

        public static Camera EnableProtune(this Camera camera)
        {
            return camera.Protune(true);
        }

        public static async Task EnableProtuneAsync(this Camera camera)
        {
            await camera.ProtuneAsync(true);
        }

        public static Camera DisableProtune(this Camera camera)
        {
            return camera.Protune(false);
        }

        public static async Task DisableProtuneAsync(this Camera camera)
        {
            await camera.ProtuneAsync(false);
        }

        public static bool SupportsProtune(this Camera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public static bool Protune(this Camera camera)
        {
            return camera.ExtendedSettings().Protune;
        }

        public static async Task<bool> ProtuneAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Protune;
        }

        public static IEnumerable<bool> ValidProtune(this Camera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates();
        }


        public static Camera AutoLowLight(this Camera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).Execute();
            return camera;
        }

        public static async Task AutoLowLightAsync(this Camera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).ExecuteAsync();
        }

        public static Camera EnableAutoLowLight(this Camera camera)
        {
            return camera.AutoLowLight(true);
        }

        public static async Task EnableAutoLowLightAsync(this Camera camera)
        {
            await camera.AutoLowLightAsync(true);
        }

        public static Camera DisableAutoLowLight(this Camera camera)
        {
            return camera.AutoLowLight(false);
        }

        public static async Task DisableAutoLowLightAsync(this Camera camera)
        {
            await camera.AutoLowLightAsync(false);
        }

        public static bool SupportsAutoLowLight(this Camera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates().Any();
        }

        public static bool AutoLowLight(this Camera camera)
        {
            return camera.ExtendedSettings().AutoLowLight;
        }

        public static async Task<bool> AutoLowLightAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).AutoLowLight;
        }

        public static IEnumerable<bool> ValidAutoLowLight(this Camera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates();
        }


        public static Camera PhotoResolution(this Camera camera, PhotoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task PhotoResolutionAsync(this Camera camera, PhotoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync();
        }

        public static PhotoResolution PhotoResolution(this Camera camera)
        {
            return camera.ExtendedSettings().PhotoResolution;
        }

        public static async Task<PhotoResolution> PhotoResolutionAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoResolution;
        }

        public static IEnumerable<PhotoResolution> ValidPhotoResolution(this Camera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }


        public static Camera VideoStandard(this Camera camera, VideoStandard videoStandard)
        {
            camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute();
            return camera;
        }

        public static async Task VideoStandardAsync(this Camera camera, VideoStandard videoStandard)
        {
            await camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync();
        }

        public static VideoStandard VideoStandard(this Camera camera)
        {
            return camera.ExtendedSettings().VideoStandard;
        }

        public static async Task<VideoStandard> VideoStandardAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoStandard;
        }


        public static Camera Mode(this Camera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task ModeAsync(this Camera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync();
        }

        public static Mode Mode(this Camera camera)
        {
            return camera.ExtendedSettings().Mode;
        }

        public static async Task<Mode> ModeAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Mode;
        }


        public static Camera Locate(this Camera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraLocate>().Set(state).Execute();
            return camera;
        }

        public static async Task LocateAsync(this Camera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync();
        }

        public static Camera EnableLocate(this Camera camera)
        {
            return camera.Locate(true);
        }

        public static async Task EnableLocateAsync(this Camera camera)
        {
            await camera.LocateAsync(true);
        }

        public static Camera DisableLocate(this Camera camera)
        {
            return camera.Locate(false);
        }

        public static async Task DisableLocateAsync(this Camera camera)
        {
            await camera.LocateAsync(false);
        }

        public static bool Locate(this Camera camera)
        {
            return camera.ExtendedSettings().LocateCamera;
        }

        public static async Task<bool> LocateAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LocateCamera;
        }


        public static Camera LivePreview(this Camera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraPreview>().Set(state).Execute();
            return camera;
        }

        public static async Task LivePreviewAsync(this Camera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync();
        }

        public static Camera EnableLivePreview(this Camera camera)
        {
            return camera.LivePreview(true);
        }

        public static async Task EnableLivePreviewAsync(this Camera camera)
        {
            await camera.LivePreviewAsync(true);
        }

        public static Camera DisableLivePreview(this Camera camera)
        {
            return camera.LivePreview(false);
        }

        public static async Task DisableLivePreviewAsync(this Camera camera)
        {
            await camera.LivePreviewAsync(false);
        }

        public static bool LivePreview(this Camera camera)
        {
            return camera.ExtendedSettings().PreviewActive;
        }

        public static async Task<bool> LivePreviewAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewActive;
        }


        public static Camera LedBlink(this Camera camera, LedBlink ledBlink)
        {
            camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute();
            return camera;
        }

        public static async Task LedBlinkAsync(this Camera camera, LedBlink ledBlink)
        {
            await camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync();
        }

        public static LedBlink LedBlink(this Camera camera)
        {
            return camera.ExtendedSettings().LedBlink;
        }

        public static async Task<LedBlink> LedBlinkAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LedBlink;
        }


        public static Camera FieldOfView(this Camera camera, FieldOfView fieldOfView)
        {
            camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute();
            return camera;
        }

        public static async Task FieldOfViewAsync(this Camera camera, FieldOfView fieldOfView)
        {
            await camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync();
        }

        public static FieldOfView FieldOfView(this Camera camera)
        {
            return camera.ExtendedSettings().FieldOfView;
        }

        public static async Task<FieldOfView> FieldOfViewAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FieldOfView;
        }

        public static IEnumerable<FieldOfView> ValidFieldOfView(this Camera camera)
        {
            return camera.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }


        public static Camera SpotMeter(this Camera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute();
            return camera;
        }

        public static async Task SpotMeterAsync(this Camera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync();
        }

        public static Camera EnableSpotMeter(this Camera camera)
        {
            return camera.SpotMeter(true);
        }

        public static async Task EnableSpotMeterAsync(this Camera camera)
        {
            await camera.SpotMeterAsync(true);
        }

        public static Camera DisableSpotMeter(this Camera camera)
        {
            return camera.SpotMeter(false);
        }

        public static async Task DisableSpotMeterAsync(this Camera camera)
        {
            await camera.SpotMeterAsync(false);
        }

        public static bool SpotMeter(this Camera camera)
        {
            return camera.ExtendedSettings().SpotMeter;
        }

        public static async Task<bool> SpotMeterAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).SpotMeter;
        }


        public static Camera DefaultModeOnPowerOn(this Camera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task DefaultModeOnPowerOnAsync(this Camera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync();
        }

        public static Mode DefaultModeOnPowerOn(this Camera camera)
        {
            return camera.ExtendedSettings().OnDefault;
        }

        public static async Task<Mode> DefaultModeOnPowerOnAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).OnDefault;
        }


        public static Camera DeleteLastFileOnSdCard(this Camera camera)
        {
            var task = camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteLastFileOnSdCardAsync(this Camera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();
        }

        public static Camera DeleteAllFilesOnSdCard(this Camera camera)
        {
            camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteAllFilesOnSdCardAsync(this Camera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();
        }


        public static Camera WhiteBalance(this Camera camera, WhiteBalance whiteBalance)
        {
            camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute();
            return camera;
        }

        public static async Task WhiteBalanceAsync(this Camera camera, WhiteBalance whiteBalance)
        {
            await camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync();
        }

        public static WhiteBalance WhiteBalance(this Camera camera)
        {
            return camera.ExtendedSettings().WhiteBalance;
        }

        public static async Task<WhiteBalance> WhiteBalanceAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).WhiteBalance;
        }

        public static IEnumerable<WhiteBalance> ValidWhiteBalance(this Camera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }


        public static Camera LoopingVideo(this Camera camera, LoopingVideo loopingVideo)
        {
            camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute();
            return camera;
        }

        public static async Task LoopingVideoAsync(this Camera camera, LoopingVideo loopingVideo)
        {
            await camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync();
        }

        public static LoopingVideo LoopingVideo(this Camera camera)
        {
            return camera.ExtendedSettings().LoopingVideoMode;
        }

        public static async Task<LoopingVideo> LoopingVideoAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public static IEnumerable<LoopingVideo> ValidLoopingVideo(this Camera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }


        public static Camera FrameRate(this Camera camera, FrameRate frameRate)
        {
            camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute();
            return camera;
        }

        public static async Task FrameRateAsync(this Camera camera, FrameRate frameRate)
        {
            await camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync();
        }

        public static FrameRate FrameRate(this Camera camera)
        {
            return camera.ExtendedSettings().FrameRate;
        }

        public static async Task<FrameRate> FrameRateAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FrameRate;
        }

        public static IEnumerable<FrameRate> ValidFrameRate(this Camera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }


        public static Camera BurstRate(this Camera camera, BurstRate burstRate)
        {
            camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute();
            return camera;
        }

        public static async Task BurstRateAsync(this Camera camera, BurstRate burstRate)
        {
            await camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync();
        }

        public static BurstRate BurstRate(this Camera camera)
        {
            return camera.ExtendedSettings().BurstRate;
        }

        public static async Task<BurstRate> BurstRateAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BurstRate;
        }


        public static Camera ContinuousShot(this Camera camera, ContinuousShot continuousShot)
        {
            camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute();
            return camera;
        }

        public static async Task ContinuousShotAsync(this Camera camera, ContinuousShot continuousShot)
        {
            await camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync();
        }

        public static ContinuousShot ContinuousShot(this Camera camera)
        {
            return camera.ExtendedSettings().ContinuousShot;
        }

        public static async Task<ContinuousShot> ContinuousShotAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).ContinuousShot;
        }


        public static Camera PhotoInVideo(this Camera camera, PhotoInVideo photoInVideo)
        {
            camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).Execute();
            return camera;
        }

        public static async Task PhotoInVideoAsync(this Camera camera, PhotoInVideo photoInVideo)
        {
            await camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).ExecuteAsync();
        }

        public static PhotoInVideo PhotoInVideo(this Camera camera)
        {
            return camera.ExtendedSettings().PhotoInVideo;
        }

        public static async Task<PhotoInVideo> PhotoInVideoAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoInVideo;
        }


        public static Camera OpenShutter(this Camera camera)
        {
            return camera.Shutter(true);
        }

        public static async Task OpenShutterAsync(this Camera camera)
        {
            await camera.ShutterAsync(true);
        }

        public static Camera CloseShutter(this Camera camera)
        {
            return camera.Shutter(false);
        }

        public static async Task CloseShutterAsync(this Camera camera)
        {
            await camera.ShutterAsync(false);
        }

        public static bool Shutter(this Camera camera)
        {
            return camera.ExtendedSettings().Shutter || camera.BacpacStatus().ShutterStatus > 0;
        }

        public static async Task<bool> ShutterAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Shutter || (await camera.BacpacStatusAsync()).ShutterStatus > 0;
        }


        public static Camera PowerOn(this Camera camera)
        {
            return camera.Power(true);
        }

        public static async Task PowerOnAsync(this Camera camera)
        {
            await camera.PowerAsync(true);
        }

        public static Camera PowerOff(this Camera camera)
        {
            return camera.Power(false);
        }

        public static async Task PowerOffAsync(this Camera camera)
        {
            await camera.PowerAsync(false);
        }

        public static bool Power(this Camera camera)
        {
            return camera.BacpacStatus().CameraPower;
        }

        public static async Task<bool> PowerAsync(this Camera camera)
        {
            return (await camera.BacpacStatusAsync()).CameraPower;
        }

        public static SignalStrength SignalStrength(this Camera camera)
        {
            return camera.BacpacStatus().Rssi;
        }

        public static async Task<SignalStrength> SignalStrengthAsync(this Camera camera)
        {
            return (await camera.BacpacStatusAsync()).Rssi;
        }

        public static byte BatteryStatus(this Camera camera)
        {
            return camera.Settings().Battery;
        }

        public static async Task<byte> BatteryStatusAsync(this Camera camera)
        {
            return (await camera.SettingsAsync()).Battery;
        }

        public static int PhotoCount(this Camera camera)
        {
            return camera.ExtendedSettings().PhotosCount;
        }

        public static async Task<int> PhotoCountAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosCount;
        }

        public static int AvailablePhotoSpace(this Camera camera)
        {
            return camera.ExtendedSettings().PhotosAvailableSpace;
        }

        public static async Task<int> AvailablePhotoSpaceAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public static int VideoCount(this Camera camera)
        {
            return camera.ExtendedSettings().VideosCount;
        }

        public static async Task<int> VideoCountAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideosCount;
        }

        public static TimeSpan AvailableVideoSpace(this Camera camera)
        {
            var seconds = camera.ExtendedSettings().VideosAvailableSpace;
            return new TimeSpan(0, 0, seconds);
        }

        public static async Task<TimeSpan> AvailableVideoSpaceAsync(this Camera camera)
        {
            var seconds = (await camera.ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public static string FullName(this Camera camera)
        {
            return camera.Information().Name;
        }

        public static async Task<string> FullNameAsync(this Camera camera)
        {
            return (await camera.InformationAsync()).Name;
        }


        public static bool LivePreviewAvailable(this Camera camera)
        {
            return camera.ExtendedSettings().PreviewAvailable;
        }

        public static async Task<bool> LivePreviewAvailableAsync(this Camera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewAvailable;
        }
    }
}
