using GoPro.Hero.Browser.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoPro.Hero.Hero3
{
    public static class ApiExtensions
    {

        public static Node Browse(this LegacyCamera camera)
        {
            return camera.FileSystem<AmbarellaBrowser>();
        }

        public static LegacyCamera VideoResolution(this LegacyCamera camera, VideoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task VideoResolutionAsync(this LegacyCamera camera, VideoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync();
        }

        public static async Task<VideoResolution> VideoResolutionAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoResolution;
        }

        public static VideoResolution VideoResolution(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().VideoResolution;
        }

        public static IEnumerable<VideoResolution> ValidVideoResolution(this LegacyCamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }


        public static LegacyCamera Orientation(this LegacyCamera camera, Orientation orientation)
        {
            camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute();
            return camera;
        }

        public static async Task OrientationAsync(this LegacyCamera camera, Orientation orientation)
        {
            await camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync();
        }

        public static Orientation Orientation(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().Orientation;
        }

        public static async Task<Orientation> OrientationAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Orientation;
        }


        public static LegacyCamera TimeLapse(this LegacyCamera camera, TimeLapse timeLapse)
        {
            camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute();
            return camera;
        }

        public static async Task TimeLapseAsync(this LegacyCamera camera, TimeLapse timeLapse)
        {
            await camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync();
        }

        public static TimeLapse TimeLapse(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().TimeLapse;
        }

        public static async Task<TimeLapse> TimeLapseAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).TimeLapse;
        }


        public static LegacyCamera BeepSound(this LegacyCamera camera, BeepSound beepSound)
        {
            camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute();
            return camera;
        }

        public static async Task BeepSoundAsync(this LegacyCamera camera, BeepSound beepSound)
        {
            await camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync();
        }

        public static async Task<BeepSound> BeepSoundAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BeepSound;
        }

        public static BeepSound BeepSound(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().BeepSound;
        }


        public static LegacyCamera Protune(this LegacyCamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraProtune>().Set(state).Execute();
            return camera;
        }

        public static async Task ProtuneAsync(this LegacyCamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync();
        }

        public static LegacyCamera EnableProtune(this LegacyCamera camera)
        {
            return camera.Protune(true);
        }

        public static async Task EnableProtuneAsync(this LegacyCamera camera)
        {
            await camera.ProtuneAsync(true);
        }

        public static LegacyCamera DisableProtune(this LegacyCamera camera)
        {
            return camera.Protune(false);
        }

        public static async Task DisableProtuneAsync(this LegacyCamera camera)
        {
            await camera.ProtuneAsync(false);
        }

        public static bool SupportsProtune(this LegacyCamera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public static bool Protune(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().Protune;
        }

        public static async Task<bool> ProtuneAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Protune;
        }

        public static IEnumerable<bool> ValidProtune(this LegacyCamera camera)
        {
            return camera.PrepareCommand<CommandCameraProtune>().ValidStates();
        }


        public static LegacyCamera AutoLowLight(this LegacyCamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).Execute();
            return camera;
        }

        public static async Task AutoLowLightAsync(this LegacyCamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).ExecuteAsync();
        }

        public static LegacyCamera EnableAutoLowLight(this LegacyCamera camera)
        {
            return camera.AutoLowLight(true);
        }

        public static async Task EnableAutoLowLightAsync(this LegacyCamera camera)
        {
            await camera.AutoLowLightAsync(true);
        }

        public static LegacyCamera DisableAutoLowLight(this LegacyCamera camera)
        {
            return camera.AutoLowLight(false);
        }

        public static async Task DisableAutoLowLightAsync(this LegacyCamera camera)
        {
            await camera.AutoLowLightAsync(false);
        }

        public static bool SupportsAutoLowLight(this LegacyCamera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates().Any();
        }

        public static bool AutoLowLight(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().AutoLowLight;
        }

        public static async Task<bool> AutoLowLightAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).AutoLowLight;
        }

        public static IEnumerable<bool> ValidAutoLowLight(this LegacyCamera camera)
        {
            return camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates();
        }


        public static LegacyCamera PhotoResolution(this LegacyCamera camera, PhotoResolution resolution)
        {
            camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute();
            return camera;
        }

        public static async Task PhotoResolutionAsync(this LegacyCamera camera, PhotoResolution resolution)
        {
            await camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync();
        }

        public static PhotoResolution PhotoResolution(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PhotoResolution;
        }

        public static async Task<PhotoResolution> PhotoResolutionAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoResolution;
        }

        public static IEnumerable<PhotoResolution> ValidPhotoResolution(this LegacyCamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }


        public static LegacyCamera VideoStandard(this LegacyCamera camera, VideoStandard videoStandard)
        {
            camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute();
            return camera;
        }

        public static async Task VideoStandardAsync(this LegacyCamera camera, VideoStandard videoStandard)
        {
            await camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync();
        }

        public static VideoStandard VideoStandard(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().VideoStandard;
        }

        public static async Task<VideoStandard> VideoStandardAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideoStandard;
        }


        public static LegacyCamera Mode(this LegacyCamera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task ModeAsync(this LegacyCamera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync();
        }

        public static Mode Mode(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().Mode;
        }

        public static async Task<Mode> ModeAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Mode;
        }


        public static LegacyCamera Locate(this LegacyCamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraLocate>().Set(state).Execute();
            return camera;
        }

        public static async Task LocateAsync(this LegacyCamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync();
        }

        public static LegacyCamera EnableLocate(this LegacyCamera camera)
        {
            return camera.Locate(true);
        }

        public static async Task EnableLocateAsync(this LegacyCamera camera)
        {
            await camera.LocateAsync(true);
        }

        public static LegacyCamera DisableLocate(this LegacyCamera camera)
        {
            return camera.Locate(false);
        }

        public static async Task DisableLocateAsync(this LegacyCamera camera)
        {
            await camera.LocateAsync(false);
        }

        public static bool Locate(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().LocateCamera;
        }

        public static async Task<bool> LocateAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LocateCamera;
        }


        public static LegacyCamera LivePreview(this LegacyCamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraPreview>().Set(state).Execute();
            return camera;
        }

        public static async Task LivePreviewAsync(this LegacyCamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync();
        }

        public static LegacyCamera EnableLivePreview(this LegacyCamera camera)
        {
            return camera.LivePreview(true);
        }

        public static async Task EnableLivePreviewAsync(this LegacyCamera camera)
        {
            await camera.LivePreviewAsync(true);
        }

        public static LegacyCamera DisableLivePreview(this LegacyCamera camera)
        {
            return camera.LivePreview(false);
        }

        public static async Task DisableLivePreviewAsync(this LegacyCamera camera)
        {
            await camera.LivePreviewAsync(false);
        }

        public static bool LivePreview(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PreviewActive;
        }

        public static async Task<bool> LivePreviewAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewActive;
        }


        public static LegacyCamera LedBlink(this LegacyCamera camera, LedBlink ledBlink)
        {
            camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute();
            return camera;
        }

        public static async Task LedBlinkAsync(this LegacyCamera camera, LedBlink ledBlink)
        {
            await camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync();
        }

        public static LedBlink LedBlink(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().LedBlink;
        }

        public static async Task<LedBlink> LedBlinkAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LedBlink;
        }


        public static LegacyCamera FieldOfView(this LegacyCamera camera, FieldOfView fieldOfView)
        {
            camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute();
            return camera;
        }

        public static async Task FieldOfViewAsync(this LegacyCamera camera, FieldOfView fieldOfView)
        {
            await camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync();
        }

        public static FieldOfView FieldOfView(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().FieldOfView;
        }

        public static async Task<FieldOfView> FieldOfViewAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FieldOfView;
        }

        public static IEnumerable<FieldOfView> ValidFieldOfView(this LegacyCamera camera)
        {
            return camera.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }


        public static LegacyCamera SpotMeter(this LegacyCamera camera, bool state)
        {
            camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute();
            return camera;
        }

        public static async Task SpotMeterAsync(this LegacyCamera camera, bool state)
        {
            await camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync();
        }

        public static LegacyCamera EnableSpotMeter(this LegacyCamera camera)
        {
            return camera.SpotMeter(true);
        }

        public static async Task EnableSpotMeterAsync(this LegacyCamera camera)
        {
            await camera.SpotMeterAsync(true);
        }

        public static LegacyCamera DisableSpotMeter(this LegacyCamera camera)
        {
            return camera.SpotMeter(false);
        }

        public static async Task DisableSpotMeterAsync(this LegacyCamera camera)
        {
            await camera.SpotMeterAsync(false);
        }

        public static bool SpotMeter(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().SpotMeter;
        }

        public static async Task<bool> SpotMeterAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).SpotMeter;
        }


        public static LegacyCamera DefaultModeOnPowerOn(this LegacyCamera camera, Mode mode)
        {
            camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute();
            return camera;
        }

        public static async Task DefaultModeOnPowerOnAsync(this LegacyCamera camera, Mode mode)
        {
            await camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync();
        }

        public static Mode DefaultModeOnPowerOn(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().OnDefault;
        }

        public static async Task<Mode> DefaultModeOnPowerOnAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).OnDefault;
        }


        public static LegacyCamera DeleteLastFileOnSdCard(this LegacyCamera camera)
        {
            var task = camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteLastFileOnSdCardAsync(this LegacyCamera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();
        }

        public static LegacyCamera DeleteAllFilesOnSdCard(this LegacyCamera camera)
        {
            camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            return camera;
        }

        public static async Task DeleteAllFilesOnSdCardAsync(this LegacyCamera camera)
        {
            await camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();
        }


        public static LegacyCamera WhiteBalance(this LegacyCamera camera, WhiteBalance whiteBalance)
        {
            camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute();
            return camera;
        }

        public static async Task WhiteBalanceAsync(this LegacyCamera camera, WhiteBalance whiteBalance)
        {
            await camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync();
        }

        public static WhiteBalance WhiteBalance(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().WhiteBalance;
        }

        public static async Task<WhiteBalance> WhiteBalanceAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).WhiteBalance;
        }

        public static IEnumerable<WhiteBalance> ValidWhiteBalance(this LegacyCamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }


        public static LegacyCamera LoopingVideo(this LegacyCamera camera, LoopingVideo loopingVideo)
        {
            camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute();
            return camera;
        }

        public static async Task LoopingVideoAsync(this LegacyCamera camera, LoopingVideo loopingVideo)
        {
            await camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync();
        }

        public static LoopingVideo LoopingVideo(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().LoopingVideoMode;
        }

        public static async Task<LoopingVideo> LoopingVideoAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public static IEnumerable<LoopingVideo> ValidLoopingVideo(this LegacyCamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }


        public static LegacyCamera FrameRate(this LegacyCamera camera, FrameRate frameRate)
        {
            camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute();
            return camera;
        }

        public static async Task FrameRateAsync(this LegacyCamera camera, FrameRate frameRate)
        {
            await camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync();
        }

        public static FrameRate FrameRate(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().FrameRate;
        }

        public static async Task<FrameRate> FrameRateAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).FrameRate;
        }

        public static IEnumerable<FrameRate> ValidFrameRate(this LegacyCamera camera)
        {
            var valid = camera.PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }


        public static LegacyCamera BurstRate(this LegacyCamera camera, BurstRate burstRate)
        {
            camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute();
            return camera;
        }

        public static async Task BurstRateAsync(this LegacyCamera camera, BurstRate burstRate)
        {
            await camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync();
        }

        public static BurstRate BurstRate(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().BurstRate;
        }

        public static async Task<BurstRate> BurstRateAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).BurstRate;
        }


        public static LegacyCamera ContinuousShot(this LegacyCamera camera, ContinuousShot continuousShot)
        {
            camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute();
            return camera;
        }

        public static async Task ContinuousShotAsync(this LegacyCamera camera, ContinuousShot continuousShot)
        {
            await camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync();
        }

        public static ContinuousShot ContinuousShot(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().ContinuousShot;
        }

        public static async Task<ContinuousShot> ContinuousShotAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).ContinuousShot;
        }


        public static LegacyCamera PhotoInVideo(this LegacyCamera camera, PhotoInVideo photoInVideo)
        {
            camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).Execute();
            return camera;
        }

        public static async Task PhotoInVideoAsync(this LegacyCamera camera, PhotoInVideo photoInVideo)
        {
            await camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).ExecuteAsync();
        }

        public static PhotoInVideo PhotoInVideo(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PhotoInVideo;
        }

        public static async Task<PhotoInVideo> PhotoInVideoAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotoInVideo;
        }


        public static LegacyCamera OpenShutter(this LegacyCamera camera)
        {
            return camera.Shutter(true);
        }

        public static async Task OpenShutterAsync(this LegacyCamera camera)
        {
            await camera.ShutterAsync(true);
        }

        public static LegacyCamera CloseShutter(this LegacyCamera camera)
        {
            return camera.Shutter(false);
        }

        public static async Task CloseShutterAsync(this LegacyCamera camera)
        {
            await camera.ShutterAsync(false);
        }

        public static bool Shutter(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().Shutter || camera.BacpacStatus().ShutterStatus > 0;
        }

        public static async Task<bool> ShutterAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).Shutter || (await camera.BacpacStatusAsync()).ShutterStatus > 0;
        }


        public static LegacyCamera PowerOn(this LegacyCamera camera)
        {
            return camera.Power(true);
        }

        public static async Task PowerOnAsync(this LegacyCamera camera)
        {
            await camera.PowerAsync(true);
        }

        public static LegacyCamera PowerOff(this LegacyCamera camera)
        {
            return camera.Power(false);
        }

        public static async Task PowerOffAsync(this LegacyCamera camera)
        {
            await camera.PowerAsync(false);
        }

        public static bool Power(this LegacyCamera camera)
        {
            return camera.BacpacStatus().CameraPower;
        }

        public static async Task<bool> PowerAsync(this LegacyCamera camera)
        {
            return (await camera.BacpacStatusAsync()).CameraPower;
        }

        public static SignalStrength SignalStrength(this LegacyCamera camera)
        {
            return camera.BacpacStatus().Rssi;
        }

        public static async Task<SignalStrength> SignalStrengthAsync(this LegacyCamera camera)
        {
            return (await camera.BacpacStatusAsync()).Rssi;
        }

        public static byte BatteryStatus(this LegacyCamera camera)
        {
            return camera.Settings().Battery;
        }

        public static async Task<byte> BatteryStatusAsync(this LegacyCamera camera)
        {
            return (await camera.SettingsAsync()).Battery;
        }

        public static int PhotoCount(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PhotosCount;
        }

        public static async Task<int> PhotoCountAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosCount;
        }

        public static int AvailablePhotoSpace(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PhotosAvailableSpace;
        }

        public static async Task<int> AvailablePhotoSpaceAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public static int VideoCount(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().VideosCount;
        }

        public static async Task<int> VideoCountAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).VideosCount;
        }

        public static TimeSpan AvailableVideoSpace(this LegacyCamera camera)
        {
            var seconds = camera.ExtendedSettings().VideosAvailableSpace;
            return new TimeSpan(0, 0, seconds);
        }

        public static async Task<TimeSpan> AvailableVideoSpaceAsync(this LegacyCamera camera)
        {
            var seconds = (await camera.ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public static string FullName(this LegacyCamera camera)
        {
            return camera.Information().Name;
        }

        public static async Task<string> FullNameAsync(this LegacyCamera camera)
        {
            return (await camera.InformationAsync()).Name;
        }


        public static bool LivePreviewAvailable(this LegacyCamera camera)
        {
            return camera.ExtendedSettings().PreviewAvailable;
        }

        public static async Task<bool> LivePreviewAvailableAsync(this LegacyCamera camera)
        {
            return (await camera.ExtendedSettingsAsync()).PreviewAvailable;
        }
    }
}
