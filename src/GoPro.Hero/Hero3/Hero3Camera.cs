using System;
using System.Collections.Generic;
using GoPro.Hero.Browser;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using System.Linq;
using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Browser.Media;
using System.Threading.Tasks;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Hero3
{
    public class Hero3Camera : Camera
    {
        public Hero3Camera(Bacpac bacpac) : base(bacpac)
        {
            var filter = FilterGeneric.Create("GoPro.Hero.Hero3.Hero3FilterScheme.xml");
            base.SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address))
        {
        }

        public Node Browse()
        {
            return base.FileSystem<AmbrellaBrowser>();
        }

        public MediaBrowser Contents()
        {
            return base.Browse<MediaBrowser>();
        }

        public Hero3Camera VideoResolution(VideoResolution resolution, bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraVideoResolution,VideoResolution>(resolution, nonBlocking);
        }

        public async Task<Hero3Camera> VideoResolutionAsync(VideoResolution resolution)
        {
            return await base.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync() as Hero3Camera;
        }

        public async Task<VideoResolution> VideoResolutionAsync()
        {
            return (await base.ExtendedSettingsAsync()).VideoResolution;
        }

        public Hero3Camera VideoResolution(out VideoResolution resolution)
        {
            resolution = base.ExtendedSettings().VideoResolution;
            return this;
        }

        public IEnumerable<VideoResolution> ValidVideoResolution()
        {
            var valid = base.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidVideoResolution(out IEnumerable<VideoResolution> validVideoResolution)
        {
            validVideoResolution = ValidVideoResolution();
            return this;
        }

        public Hero3Camera Orientation(Orientation orientation ,bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraOrientation, Orientation>(orientation, nonBlocking);
        }

        public async Task<Hero3Camera> OrientationAsync(Orientation orientation)
        {
            return await base.PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera Orientation(out Orientation orientation)
        {
            orientation = base.ExtendedSettings().Orientation;
            return this;
        }

        public async Task<Orientation> OrientationAsync()
        {
            return (await base.ExtendedSettingsAsync()).Orientation;
        }

        public Hero3Camera TimeLapse(TimeLapse timeLapse,bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraTimeLapse, TimeLapse>(timeLapse, nonBlocking);
        }

        public async Task<Hero3Camera> TimeLapseAsync(TimeLapse timeLapse)
        {
            return await base.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera TimeLapse(out TimeLapse timeLapse)
        {
            timeLapse = base.ExtendedSettings().TimeLapse;
            return this;
        }

        public async Task<TimeLapse> TimeLapseAsync()
        {
            return (await base.ExtendedSettingsAsync()).TimeLapse;
        }

        public Hero3Camera BeepSound(BeepSound beepSound , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraBeepSound, BeepSound>(beepSound, nonBlocking);
        }

        public async Task<Hero3Camera> BeepSoundAsync(BeepSound beepSound)
        {
            return await base.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync() as Hero3Camera;
        }

        public async Task<BeepSound> BeepSoundAsync()
        {
            return (await base.ExtendedSettingsAsync()).BeepSound;
        }

        public Hero3Camera BeepSound(out BeepSound beepSound)
        {
            beepSound = base.ExtendedSettings().BeepSound;
            return this;
        }

        public Hero3Camera Protune(bool state, bool nonBlocing = false)
        {
            return ExecuteBooleanCommand<CommandCameraProtune>(state, nonBlocing);
        }

        public async Task<Hero3Camera> ProtuneAsync(bool state)
        {
            return await base.PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera EnableProtune()
        {
            return Protune(true);
        }

        public async Task<Hero3Camera> EnableProtuneAsync()
        {
            return await ProtuneAsync(true);
        }

        public Hero3Camera DisableProtune()
        {
            return Protune(false);
        }

        public async Task<Hero3Camera> DisableProtuneAsync()
        {
            return await ProtuneAsync(false);
        }

        public bool SupportsProtune()
        {
            return base.PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public Hero3Camera Protune(out bool state)
        {
            state = base.ExtendedSettings().Protune;
            return this;
        }

        public async Task<bool> ProtuneAsync()
        {
            return (await base.ExtendedSettingsAsync()).Protune;
        }

        public IEnumerable<bool> ValidProtune()
        {
            return base.PrepareCommand<CommandCameraProtune>().ValidStates();
        }

        public Hero3Camera ValidProtune(out IEnumerable<bool> validProtune)
        {
            validProtune = ValidProtune();
            return this;
        }

        public Hero3Camera PhotoResolution(PhotoResolution resolution , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraPhotoResolution, PhotoResolution>(resolution, nonBlocking);
        }

        public async Task<Hero3Camera> PhotoResolutionAsync(PhotoResolution resolution)
        {
            return await base.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera PhotoResolution(out PhotoResolution resolution)
        {
            resolution = base.ExtendedSettings().PhotoResolution;
            return this;
        }

        public async Task<PhotoResolution> PhotoResolutionAsync()
        {
            return (await base.ExtendedSettingsAsync()).PhotoResolution;
        }

        public IEnumerable<PhotoResolution> ValidPhotoResolution()
        {
            var valid = PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidPhotoResolution(out IEnumerable<PhotoResolution> validPhotoResolution)
        {
            validPhotoResolution = ValidPhotoResolution();
            return this;
        }

        public Hero3Camera VideoStandard(VideoStandard videoStandard , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraVideoStandard, VideoStandard>(videoStandard, nonBlocking);
        }

        public async Task<Hero3Camera> VideoStandardAsync(VideoStandard videoStandard)
        {
            return await base.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera VideoStandard(out VideoStandard videoStandard)
        {
            videoStandard = base.ExtendedSettings().VideoStandard;
            return this;
        }

        public async Task<VideoStandard> VideoStandardAsync()
        {
            return (await base.ExtendedSettingsAsync()).VideoStandard;
        }

        public Hero3Camera Mode(Mode mode , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraMode, Mode>(mode, nonBlocking);
        }

        public async Task<Hero3Camera> ModeAsync(Mode mode)
        {
            return await base.PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera Mode(out Mode mode)
        {
            mode = base.ExtendedSettings().Mode;
            return this;
        }

        public async Task<Mode> ModeAsync()
        {
            return (await base.ExtendedSettingsAsync()).Mode;
        }

        public Hero3Camera Locate(bool state , bool nonBlocking = false)
        {
            return ExecuteBooleanCommand<CommandCameraLocate>(state, nonBlocking);
        }

        public async Task<Hero3Camera> LocateAsync(bool state)
        {
            return await base.PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera EnableLocate()
        {
            return Locate(true);
        }

        public async Task<Hero3Camera> EnableLocateAsync()
        {
            return await LocateAsync(true);
        }

        public Hero3Camera DisableLocate()
        {
            return Locate(false);
        }

        public async Task<Hero3Camera> DisableLocateAsync()
        {
            return await LocateAsync(false);
        }

        public Hero3Camera Locate(out bool state)
        {
            state = base.ExtendedSettings().LocateCamera;
            return this;
        }

        public async Task<bool> LocateAsync()
        {
            return (await base.ExtendedSettingsAsync()).LocateCamera;
        }

        public Hero3Camera LivePreview(bool state , bool nonBlocking = false)
        {
            return ExecuteBooleanCommand<CommandCameraPreview>(state, nonBlocking);
        }

        public async Task<Hero3Camera> LivePreviewAsync(bool state)
        {
            return await base.PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera EnableLivePreview()
        {
            return LivePreview(true);
        }

        public async Task<Hero3Camera> EnableLivePreviewAsync()
        {
            return await LivePreviewAsync(true);
        }

        public Hero3Camera DisableLivePreview()
        {
            return LivePreview(false);
        }

        public async Task<Hero3Camera> DisableLivePreviewAsync()
        {
            return await LivePreviewAsync(false);
        }

        public Hero3Camera LivePreview(out bool state)
        {
            state = base.ExtendedSettings().PreviewActive;
            return this;
        }

        public async Task<bool> LivePreviewAsync()
        {
            return (await base.ExtendedSettingsAsync()).PreviewActive;
        }

        public Hero3Camera LedBlink(LedBlink ledBlink , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraLedBlink, LedBlink>(ledBlink, nonBlocking);
        }

        public async Task<Hero3Camera> LedBlinkAsync(LedBlink ledBlink)
        {
            return await base.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera LedBlink(out LedBlink ledBlink)
        {
            ledBlink = base.ExtendedSettings().LedBlink;
            return this;
        }

        public async Task<LedBlink> LedBlinkAsync()
        {
            return (await base.ExtendedSettingsAsync()).LedBlink;
        }

        public Hero3Camera FieldOfView(FieldOfView fieldOfView , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraFieldOfView, FieldOfView>(fieldOfView, nonBlocking);
        }

        public async Task<Hero3Camera> FieldOfViewAsync(FieldOfView fieldOfView)
        {
            return await base.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera FieldOfView(out FieldOfView fieldOfView)
        {
            fieldOfView = base.ExtendedSettings().FieldOfView;
            return this;
        }

        public async Task<FieldOfView> FieldOfViewAsync()
        {
            return (await base.ExtendedSettingsAsync()).FieldOfView;
        }

        public IEnumerable<FieldOfView> ValidFieldOfView()
        {
            return base.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }

        public Hero3Camera ValidFieldOfView(out IEnumerable<FieldOfView> validFieldOfView)
        {
            validFieldOfView = ValidFieldOfView();
            return this;
        }

        public Hero3Camera SpotMeter(bool state , bool nonBlocking = false)
        {
            return ExecuteBooleanCommand<CommandCameraSpotMeter>(state, nonBlocking);
        }

        public async  Task<Hero3Camera> SpotMeterAsync(bool state)
        {
            return await  base.PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera EnableSpotMeter()
        {
            return SpotMeter(true);
        }

        public async Task<Hero3Camera> EnableSpotMeterAsync()
        {
            return await SpotMeterAsync(true);
        }

        public Hero3Camera DisableSpotMeter()
        {
            return SpotMeter(false);
        }

        public async Task<Hero3Camera> DisableSpotMeterAsync()
        {
            return await SpotMeterAsync(false);
        }

        public Hero3Camera SpotMeter(out bool state)
        {
            state = base.ExtendedSettings().SpotMeter;
            return this;
        }

        public async Task<bool> SpotMeterAsync()
        {
            return (await base.ExtendedSettingsAsync()).SpotMeter;
        }

        public Hero3Camera DefaultModeOnPowerOn(Mode mode , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraDefaultMode, Mode>(mode, nonBlocking);
        }

        public async Task<Hero3Camera> DefaultModeOnPowerOnAsync(Mode mode)
        {
            return await base.PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera DefaultModeOnPowerOn(out Mode mode)
        {
            mode = base.ExtendedSettings().OnDefault;
            return this;
        }

        public async Task<Mode> DefaultModeOnPowerOnAsync()
        {
            return (await base.ExtendedSettingsAsync()).OnDefault;
        }

        public Hero3Camera DeleteLastFileOnSdCard(bool nonBlocking = false)
        {
            var task= base.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public async Task<Hero3Camera> DeleteLastFileOnSdCardAsync()
        {
            return await base.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera DeleteAllFilesOnSdCard(bool nonBlocking = false)
        {
            var task=base.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public async Task<Hero3Camera> DeleteAllFilesOnSdCardAsync()
        {
            return await base.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera WhiteBalance(WhiteBalance whiteBalance , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraWhiteBalance, WhiteBalance>(whiteBalance, nonBlocking);
        }

        public async Task<Hero3Camera> WhiteBalanceAsync(WhiteBalance whiteBalance)
        {
            return await base.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera WhiteBalance(out WhiteBalance whiteBalance)
        {
            whiteBalance = base.ExtendedSettings().WhiteBalance;
            return this;
        }

        public async Task<WhiteBalance> WhiteBalanceAsync()
        {
            return (await base.ExtendedSettingsAsync()).WhiteBalance;
        }

        public IEnumerable<WhiteBalance> ValidWhiteBalance()
        {
            var valid = PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidWhiteBalance(out IEnumerable<WhiteBalance> validWhiteBalance)
        {
            validWhiteBalance = ValidWhiteBalance();
            return this;
        }

        public Hero3Camera LoopingVideo(LoopingVideo loopingVideo , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraLoopingVideo, LoopingVideo>(loopingVideo, nonBlocking);
        }

        public async Task<Hero3Camera> LoopingVideoAsync(LoopingVideo loopingVideo)
        {
            return await base.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera LoopingVideo(out LoopingVideo loopingVideo)
        {
            loopingVideo = base.ExtendedSettings().LoopingVideoMode;
            return this;
        }

        public async Task<LoopingVideo> LoopingVideoAsync()
        {
            return (await this.ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public IEnumerable<LoopingVideo> ValidLoopingVideo()
        {
            var valid = PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidLoopingVideo(out IEnumerable<LoopingVideo> validLoopingVideo)
        {
            validLoopingVideo = ValidLoopingVideo();
            return this;
        }

        public Hero3Camera FrameRate(FrameRate frameRate , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraFrameRate, FrameRate>(frameRate, nonBlocking);
        }

        public async Task<Hero3Camera> FrameRateAsync(FrameRate frameRate)
        {
            return await base.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera FrameRate(out FrameRate frameRate)
        {
            frameRate = base.ExtendedSettings().FrameRate;
            return this;
        }

        public async Task<FrameRate> FrameRateAsync()
        {
            return (await base.ExtendedSettingsAsync()).FrameRate;
        }

        public IEnumerable<FrameRate> ValidFrameRate()
        {
            var valid = PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidFrameRate(out IEnumerable<FrameRate> validFrameRate)
        {
            validFrameRate = ValidFrameRate();
            return this;
        }

        public Hero3Camera BurstRate(BurstRate burstRate, bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraBurstRate, BurstRate>(burstRate, nonBlocking);
        }

        public async Task<Hero3Camera> BurstRateAsync(BurstRate burstRate)
        {
            return await base.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera BurstRate(out BurstRate burstRate)
        {
            burstRate = base.ExtendedSettings().BurstRate;
            return this;
        }

        public async Task<BurstRate> BurstRate()
        {
            return (await base.ExtendedSettingsAsync()).BurstRate;
        }

        public Hero3Camera ContinuousShot(ContinuousShot continuousShot , bool nonBlocking = false)
        {
            return ExecuteMultiChoiceCommand<CommandCameraContinuousShot, ContinuousShot>(continuousShot, nonBlocking);
        }

        public async Task<Hero3Camera> ContinuousShotAsync(ContinuousShot continuousShot)
        {
            return await base.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync() as Hero3Camera;
        }

        public Hero3Camera ContinuousShot(out ContinuousShot continuousShot)
        {
            continuousShot = base.ExtendedSettings().ContinuousShot;
            return this;
        }

        public async Task<ContinuousShot> ContinuousShotAsync()
        {
            return (await base.ExtendedSettingsAsync()).ContinuousShot;
        }

        public new Hero3Camera Shutter(bool state, bool nonBlocking = false)
        {
            return base.Shutter(state, nonBlocking) as Hero3Camera;
        }

        public new async Task<Hero3Camera> ShutterAsync(bool state)
        {
            return await base.ShutterAsync(state) as Hero3Camera;
        }

        public Hero3Camera OpenShutter()
        {
            return Shutter(true);
        }

        public async Task<Hero3Camera> OpenShutterAsync()
        {
            return await ShutterAsync(true);
        }

        public Hero3Camera CloseShutter()
        {
            return Shutter(false);
        }

        public async Task<Hero3Camera> CloseShutterAsync()
        {
            return await ShutterAsync(false);
        }

        public Hero3Camera Shutter(out bool state)
        {
            state = base.ExtendedSettings().Shutter || base.BacpacStatus().ShutterStatus > 0;
            return this;
        }

        public async Task<bool> ShutterAsync()
        {
            return (await base.ExtendedSettingsAsync()).Shutter || (await base.BacpacStatusAsync()).ShutterStatus > 0;
        }

        public new Hero3Camera Power(bool state, bool nonBlocking = false)
        {
            return base.Power(state, nonBlocking) as Hero3Camera;
        }

        public new async Task<Hero3Camera> PowerAsync(bool state)
        {
            return await base.PowerAsync(state) as Hero3Camera;
        }

        public Hero3Camera PowerOn()
        {
            return Power(true);
        }

        public async Task<Hero3Camera> PowerOnAsync()
        {
            return await PowerAsync(true);
        }

        public Hero3Camera PowerOff()
        {
            return Power(false);
        }

        public async Task<Hero3Camera> PowerOffAsync()
        {
            return await PowerAsync(false);
        }

        public Hero3Camera Power(out bool state)
        {
            state = base.BacpacStatus().CameraPower;
            return this;
        }

        public async Task<bool> PowerAsync()
        {
            return (await base.BacpacStatusAsync()).CameraPower;
        }

        public Hero3Camera Model(out Model model)
        {
            model = base.BacpacStatusCache().CameraModel;
            return this;
        }

        public Hero3Camera SignalStrength(out SignalStrength signalStrength)
        {
            signalStrength = base.BacpacStatus().Rssi;
            return this;
        }

        public async Task<SignalStrength> SignalStrengthAsync()
        {
            return (await base.BacpacStatusAsync()).Rssi;
        }

        public Hero3Camera BatteryStatus(out byte batteryStatus)
        {
            batteryStatus = base.Settings().Battery;
            return this;
        }

        public async Task<byte> BatteryStatusAsync()
        {
            return (await base.SettingsAsync()).Battery;
        }

        public Hero3Camera PhotoCount(out int photoCount)
        {
            photoCount = base.ExtendedSettings().PhotosCount;
            return this;
        }

        public async Task<int> PhotoCountAsync()
        {
            return (await base.ExtendedSettingsAsync()).PhotosCount;
        }

        public Hero3Camera AvailablePhotoSpace(out int availableSpace)
        {
            availableSpace = base.ExtendedSettings().PhotosAvailableSpace;
            return this;
        }

        public async Task<int> AvailablePhotoSpaceAsync()
        {
            return (await base.ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public Hero3Camera VideoCount(out int videoCount)
        {
            videoCount = base.ExtendedSettings().VideosCount;
            return this;
        }

        public async Task<int> VideoCountAsync()
        {
            return (await base.ExtendedSettingsAsync()).VideosCount;
        }

        public Hero3Camera AvailableVideoSpace(out TimeSpan availableSpace)
        {
            var seconds = base.ExtendedSettings().VideosAvailableSpace;
            availableSpace = new TimeSpan(0, 0, seconds);
            return this;
        }

        public async Task<TimeSpan> AvailableVideoSpaceAsync()
        {
            var seconds = (await base.ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public Hero3Camera MacAddress(out string macAddress)
        {
            macAddress = base.BacpacInformationCache().MacAddress;
            return this;
        }

        public Hero3Camera IpAddress(out string ipAddress)
        {
            ipAddress = base.Bacpac.Address;
            return this;
        }

        public Hero3Camera Password(out string password)
        {
            password = base.Bacpac.Password;
            return this;
        }

        public Hero3Camera BootLoader(out Version version)
        {
            version = base.BacpacInformationCache().BootloaderVersion;
            return this;
        }

        public Hero3Camera Firmware(out Version version)
        {
            version = base.BacpacInformationCache().FirmwareVersion;
            return this;
        }

        public Hero3Camera Ssid(out string ssid)
        {
            ssid = base.BacpacInformationCache().Ssid;
            return this;
        }

        public Hero3Camera FullName(out string fullName)
        {
            fullName = base.Information().Name;
            return this;
        }

        public async Task<string> FullNameAsync()
        {
            return (await base.InformationAsync()).Name;
        }

        public Hero3Camera Version(out string version)
        {
            version = base.InformationCache().Version;
            return this;
        }

        public Hero3Camera LivePreviewAvailable(out bool state)
        {
            state = base.ExtendedSettings().PreviewAvailable;
            return this;
        }

        public async Task<bool> LivePreviewAvailableAsync()
        {
            return (await base.ExtendedSettingsAsync()).PreviewAvailable;
        }

        private Hero3Camera ExecuteMultiChoiceCommand<T, TO>(TO selection, bool nonBlocking) where T : CommandMultiChoice<TO, ICamera>
        {
            var task = base.PrepareCommand<T>().Select(selection).ExecuteAsync();

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        private Hero3Camera ExecuteBooleanCommand<T>(bool value, bool nonBlocking) where T : CommandBoolean<ICamera>
        {
            var task = base.PrepareCommand<T>().Set(value).ExecuteAsync();

            if (!nonBlocking)
                task.Wait();

            return this;
        }
    }
}