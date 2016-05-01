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

        public static Node Browse(this ICamera camera)
        {
            return camera.FileSystem<AmbarellaBrowser>();
        }

        public static ICamera VideoResolution(this ICamera camera, VideoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task VideoResolutionAsync(this ICamera camera, VideoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync();
        }

        public static async Task<VideoResolution> VideoResolutionAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoResolution;
        }

        public static VideoResolution VideoResolution(this ICamera camera)
        {
            return camera.ExtendedSettings().VideoResolution;
        }

        public static IEnumerable<VideoResolution> ValidVideoResolution(this ICamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }


        public static ICamera Orientation(this ICamera camera, Orientation orientation)
        {
            camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute();
            return camera;
        }

        public static async Task OrientationAsync(this ICamera camera, Orientation orientation)
        {
            await camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync();
        }

        public static Orientation Orientation(this ICamera camera)
        {
            return camera.ExtendedSettings().Orientation;
        }

        public static async Task<Orientation> OrientationAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Orientation;
        }


        public static ICamera TimeLapse(this ICamera camera, TimeLapse timeLapse)
        {
            camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute();
            return camera;
        }

        public static async Task TimeLapseAsync(this ICamera camera, TimeLapse timeLapse)
        {
            await camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync();
        }

        public static TimeLapse TimeLapse(this ICamera camera)
        {
            return camera.ExtendedSettings().TimeLapse;
        }

        public static async Task<TimeLapse> TimeLapseAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).TimeLapse;
        }


        public static ICamera BeepSound(this ICamera camera, BeepSound beepSound)
        {
            camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute();
            return camera;
        }

        public static async Task BeepSoundAsync(this ICamera camera, BeepSound beepSound)
        {
            await camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync();
        }

        public static async Task<BeepSound> BeepSoundAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BeepSound;
        }

        public static BeepSound BeepSound(this ICamera camera)
        {
            return camera.ExtendedSettings().BeepSound;
        }


        public static ICamera Protune(this ICamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraProtune>().Set(state).Execute();
            return camera;
        }

        public static async Task ProtuneAsync(this ICamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync();
        }

        public static ICamera EnableProtune(this ICamera camera)
        {
            return camera.Protune(true);
        }

        public static async Task EnableProtuneAsync(this ICamera camera)
        {
            await camera.ProtuneAsync(true);
        }

        public static ICamera DisableProtune(this ICamera camera)
        {
            return camera.Protune(false);
        }

        public static async Task DisableProtuneAsync(this ICamera camera)
        {
            await camera.ProtuneAsync(false);
        }

        public static bool SupportsProtune(this ICamera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public static bool Protune(this ICamera camera)
        {
            return camera.ExtendedSettings().Protune;
        }

        public static async Task<bool> ProtuneAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Protune;
        }

        public static IEnumerable<bool> ValidProtune(this ICamera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates();
        }


        public static ICamera AutoLowLight(this ICamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).Execute();
            return camera;
        }

        public static async Task AutoLowLightAsync(this ICamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).ExecuteAsync();
        }

        public static ICamera EnableAutoLowLight(this ICamera camera)
        {
            return camera.AutoLowLight(true);
        }

        public static async Task EnableAutoLowLightAsync(this ICamera camera)
        {
            await camera.AutoLowLightAsync(true);
        }

        public static ICamera DisableAutoLowLight(this ICamera camera)
        {
            return camera.AutoLowLight(false);
        }

        public static async Task DisableAutoLowLightAsync(this ICamera camera)
        {
            await camera.AutoLowLightAsync(false);
        }

        public static bool SupportsAutoLowLight(this ICamera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates().Any();
        }

        public static bool AutoLowLight(this ICamera camera)
        {
            return camera.ExtendedSettings().AutoLowLight;
        }

        public static async Task<bool> AutoLowLightAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).AutoLowLight;
        }

        public static IEnumerable<bool> ValidAutoLowLight(this ICamera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates();
        }


        public static ICamera PhotoResolution(this ICamera camera, PhotoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task PhotoResolutionAsync(this ICamera camera, PhotoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync();
        }

        public static PhotoResolution PhotoResolution(this ICamera camera)
        {
            return camera.ExtendedSettings().PhotoResolution;
        }

        public static async Task<PhotoResolution> PhotoResolutionAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoResolution;
        }

        public static IEnumerable<PhotoResolution> ValidPhotoResolution(this ICamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }


        public static ICamera VideoStandard(this ICamera camera, VideoStandard videoStandard)
        {
            camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute();
            return camera;
        }

        public static async Task VideoStandardAsync(this ICamera camera, VideoStandard videoStandard)
        {
            await camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync();
        }

        public static VideoStandard VideoStandard(this ICamera camera)
        {
            return camera.ExtendedSettings().VideoStandard;
        }

        public static async Task<VideoStandard> VideoStandardAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoStandard;
        }


        public static ICamera Mode(this ICamera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task ModeAsync(this ICamera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync();
        }

        public static Mode Mode(this ICamera camera)
        {
            return camera.ExtendedSettings().Mode;
        }

        public static async Task<Mode> ModeAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Mode;
        }


        public static ICamera Locate(this ICamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraLocate>().Set(state).Execute();
            return camera;
        }

        public static async Task LocateAsync(this ICamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync();
        }

        public static ICamera EnableLocate(this ICamera camera)
        {
            return camera.Locate(true);
        }

        public static async Task EnableLocateAsync(this ICamera camera)
        {
            await camera.LocateAsync(true);
        }

        public static ICamera DisableLocate(this ICamera camera)
        {
            return camera.Locate(false);
        }

        public static async Task DisableLocateAsync(this ICamera camera)
        {
            await camera.LocateAsync(false);
        }

        public static bool Locate(this ICamera camera)
        {
            return camera.ExtendedSettings().LocateCamera;
        }

        public static async Task<bool> LocateAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LocateCamera;
        }


        public static ICamera LivePreview(this ICamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraPreview>().Set(state).Execute();
            return camera;
        }

        public static async Task LivePreviewAsync(this ICamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync();
        }

        public static ICamera EnableLivePreview(this ICamera camera)
        {
            return camera.LivePreview(true);
        }

        public static async Task EnableLivePreviewAsync(this ICamera camera)
        {
            await camera.LivePreviewAsync(true);
        }

        public static ICamera DisableLivePreview(this ICamera camera)
        {
            return camera.LivePreview(false);
        }

        public static async Task DisableLivePreviewAsync(this ICamera camera)
        {
            await camera.LivePreviewAsync(false);
        }

        public static bool LivePreview(this ICamera camera)
        {
            return camera.ExtendedSettings().PreviewActive;
        }

        public static async Task<bool> LivePreviewAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewActive;
        }


        public static ICamera LedBlink(this ICamera camera, LedBlink ledBlink)
        {
            camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute();
            return camera;
        }

        public static async Task LedBlinkAsync(this ICamera camera, LedBlink ledBlink)
        {
            await camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync();
        }

        public static LedBlink LedBlink(this ICamera camera)
        {
            return camera.ExtendedSettings().LedBlink;
        }

        public static async Task<LedBlink> LedBlinkAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LedBlink;
        }


        public static ICamera FieldOfView(this ICamera camera, FieldOfView fieldOfView)
        {
            camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute();
            return camera;
        }

        public static async Task FieldOfViewAsync(this ICamera camera, FieldOfView fieldOfView)
        {
            await camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync();
        }

        public static FieldOfView FieldOfView(this ICamera camera)
        {
            return camera.ExtendedSettings().FieldOfView;
        }

        public static async Task<FieldOfView> FieldOfViewAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FieldOfView;
        }

        public static IEnumerable<FieldOfView> ValidFieldOfView(this ICamera camera)
        {
            return camera.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }


        public static ICamera SpotMeter(this ICamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute();
            return camera;
        }

        public static async Task SpotMeterAsync(this ICamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync();
        }

        public static ICamera EnableSpotMeter(this ICamera camera)
        {
            return camera.SpotMeter(true);
        }

        public static async Task EnableSpotMeterAsync(this ICamera camera)
        {
            await camera.SpotMeterAsync(true);
        }

        public static ICamera DisableSpotMeter(this ICamera camera)
        {
            return camera.SpotMeter(false);
        }

        public static async Task DisableSpotMeterAsync(this ICamera camera)
        {
            await camera.SpotMeterAsync(false);
        }

        public static bool SpotMeter(this ICamera camera)
        {
            return camera.ExtendedSettings().SpotMeter;
        }

        public static async Task<bool> SpotMeterAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).SpotMeter;
        }


        public static ICamera DefaultModeOnPowerOn(this ICamera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task DefaultModeOnPowerOnAsync(this ICamera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync();
        }

        public static Mode DefaultModeOnPowerOn(this ICamera camera)
        {
            return camera.ExtendedSettings().OnDefault;
        }

        public static async Task<Mode> DefaultModeOnPowerOnAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).OnDefault;
        }


        public static ICamera DeleteLastFileOnSdCard(this ICamera camera)
        {
            var task = camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteLastFileOnSdCardAsync(this ICamera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();
        }

        public static ICamera DeleteAllFilesOnSdCard(this ICamera camera)
        {
            camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteAllFilesOnSdCardAsync(this ICamera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();
        }


        public static ICamera WhiteBalance(this ICamera camera, WhiteBalance whiteBalance)
        {
            camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute();
            return camera;
        }

        public static async Task WhiteBalanceAsync(this ICamera camera, WhiteBalance whiteBalance)
        {
            await camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync();
        }

        public static WhiteBalance WhiteBalance(this ICamera camera)
        {
            return camera.ExtendedSettings().WhiteBalance;
        }

        public static async Task<WhiteBalance> WhiteBalanceAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).WhiteBalance;
        }

        public static IEnumerable<WhiteBalance> ValidWhiteBalance(this ICamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }


        public static ICamera LoopingVideo(this ICamera camera, LoopingVideo loopingVideo)
        {
            camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute();
            return camera;
        }

        public static async Task LoopingVideoAsync(this ICamera camera, LoopingVideo loopingVideo)
        {
            await camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync();
        }

        public static LoopingVideo LoopingVideo(this ICamera camera)
        {
            return camera.ExtendedSettings().LoopingVideoMode;
        }

        public static async Task<LoopingVideo> LoopingVideoAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public static IEnumerable<LoopingVideo> ValidLoopingVideo(this ICamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }


        public static ICamera FrameRate(this ICamera camera, FrameRate frameRate)
        {
            camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute();
            return camera;
        }

        public static async Task FrameRateAsync(this ICamera camera, FrameRate frameRate)
        {
            await camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync();
        }

        public static FrameRate FrameRate(this ICamera camera)
        {
            return camera.ExtendedSettings().FrameRate;
        }

        public static async Task<FrameRate> FrameRateAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FrameRate;
        }

        public static IEnumerable<FrameRate> ValidFrameRate(this ICamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }


        public static ICamera BurstRate(this ICamera camera, BurstRate burstRate)
        {
            camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute();
            return camera;
        }

        public static async Task BurstRateAsync(this ICamera camera, BurstRate burstRate)
        {
            await camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync();
        }

        public static BurstRate BurstRate(this ICamera camera)
        {
            return camera.ExtendedSettings().BurstRate;
        }

        public static async Task<BurstRate> BurstRateAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BurstRate;
        }


        public static ICamera ContinuousShot(this ICamera camera, ContinuousShot continuousShot)
        {
            camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute();
            return camera;
        }

        public static async Task ContinuousShotAsync(this ICamera camera, ContinuousShot continuousShot)
        {
            await camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync();
        }

        public static ContinuousShot ContinuousShot(this ICamera camera)
        {
            return camera.ExtendedSettings().ContinuousShot;
        }

        public static async Task<ContinuousShot> ContinuousShotAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).ContinuousShot;
        }


        public static ICamera PhotoInVideo(this ICamera camera, PhotoInVideo photoInVideo)
        {
            camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).Execute();
            return camera;
        }

        public static async Task PhotoInVideoAsync(this ICamera camera, PhotoInVideo photoInVideo)
        {
            await camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).ExecuteAsync();
        }

        public static PhotoInVideo PhotoInVideo(this ICamera camera)
        {
            return camera.ExtendedSettings().PhotoInVideo;
        }

        public static async Task<PhotoInVideo> PhotoInVideoAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoInVideo;
        }


        public static ICamera OpenShutter(this ICamera camera)
        {
            return camera.Shutter(true);
        }

        public static async Task OpenShutterAsync(this ICamera camera)
        {
            await camera.ShutterAsync(true);
        }

        public static ICamera CloseShutter(this ICamera camera)
        {
            return camera.Shutter(false);
        }

        public static async Task CloseShutterAsync(this ICamera camera)
        {
            await camera.ShutterAsync(false);
        }

        public static bool Shutter(this ICamera camera)
        {
            return camera.ExtendedSettings().Shutter || camera.BacpacStatus().ShutterStatus > 0;
        }

        public static async Task<bool> ShutterAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Shutter || (await camera.BacpacStatusAsync()).ShutterStatus > 0;
        }


        public static ICamera PowerOn(this ICamera camera)
        {
            return camera.Power(true);
        }

        public static async Task PowerOnAsync(this ICamera camera)
        {
            await camera.PowerAsync(true);
        }

        public static ICamera PowerOff(this ICamera camera)
        {
            return camera.Power(false);
        }

        public static async Task PowerOffAsync(this ICamera camera)
        {
            await camera.PowerAsync(false);
        }

        public static bool Power(this ICamera camera)
        {
            return camera.BacpacStatus().CameraPower;
        }

        public static async Task<bool> PowerAsync(this ICamera camera)
        {
            return (await camera.BacpacStatusAsync()).CameraPower;
        }

        public static SignalStrength SignalStrength(this ICamera camera)
        {
            return camera.BacpacStatus().Rssi;
        }

        public static async Task<SignalStrength> SignalStrengthAsync(this ICamera camera)
        {
            return (await camera.BacpacStatusAsync()).Rssi;
        }

        public static byte BatteryStatus(this ICamera camera)
        {
            return camera.Settings().Battery;
        }

        public static async Task<byte> BatteryStatusAsync(this ICamera camera)
        {
            return (await camera.SettingsAsync()).Battery;
        }

        public static int PhotoCount(this ICamera camera)
        {
            return camera.ExtendedSettings().PhotosCount;
        }

        public static async Task<int> PhotoCountAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosCount;
        }

        public static int AvailablePhotoSpace(this ICamera camera)
        {
            return camera.ExtendedSettings().PhotosAvailableSpace;
        }

        public static async Task<int> AvailablePhotoSpaceAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public static int VideoCount(this ICamera camera)
        {
            return camera.ExtendedSettings().VideosCount;
        }

        public static async Task<int> VideoCountAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideosCount;
        }

        public static TimeSpan AvailableVideoSpace(this ICamera camera)
        {
            var seconds = camera.ExtendedSettings().VideosAvailableSpace;
            return new TimeSpan(0, 0, seconds);
        }

        public static async Task<TimeSpan> AvailableVideoSpaceAsync(this ICamera camera)
        {
            var seconds = (await camera.ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public static string FullName(this ICamera camera)
        {
            return camera.Information().Name;
        }

        public static async Task<string> FullNameAsync(this ICamera camera)
        {
            return (await camera.InformationAsync()).Name;
        }


        public static bool LivePreviewAvailable(this ICamera camera)
        {
            return camera.ExtendedSettings().PreviewAvailable;
        }

        public static async Task<bool> LivePreviewAvailableAsync(this ICamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewAvailable;
        }
    }
}
