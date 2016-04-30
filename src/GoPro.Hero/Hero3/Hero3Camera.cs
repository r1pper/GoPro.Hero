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
            SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address))
        {
        }

        public Node Browse()
        {
            return FileSystem<AmbarellaBrowser>();
        }

        public Hero3Camera VideoResolution(VideoResolution resolution)
        {
            PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute();
            return this;
        }

        public async Task VideoResolutionAsync(VideoResolution resolution)
        {
            await PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync();
        }

        public async Task<VideoResolution> VideoResolutionAsync()
        {
            return (await ExtendedSettingsAsync()).VideoResolution;
        }

        public VideoResolution VideoResolution()
        {
            return ExtendedSettings().VideoResolution;
        }

        public IEnumerable<VideoResolution> ValidVideoResolution()
        {
            var valid = PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }


        public Hero3Camera Orientation(Orientation orientation)
        {
            PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute();
            return this;
        }

        public async Task OrientationAsync(Orientation orientation)
        {
            await PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync();
        }

        public Orientation Orientation()
        {
            return ExtendedSettings().Orientation;
        }

        public async Task<Orientation> OrientationAsync()
        {
            return (await ExtendedSettingsAsync()).Orientation;
        }


        public Hero3Camera TimeLapse(TimeLapse timeLapse)
        {
            PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute();
            return this;
        }

        public async Task TimeLapseAsync(TimeLapse timeLapse)
        {
            await PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync();
        }

        public TimeLapse TimeLapse()
        {
            return ExtendedSettings().TimeLapse;
        }

        public async Task<TimeLapse> TimeLapseAsync()
        {
            return (await ExtendedSettingsAsync()).TimeLapse;
        }


        public Hero3Camera BeepSound(BeepSound beepSound)
        {
            PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute();
            return this;
        }

        public async Task BeepSoundAsync(BeepSound beepSound)
        {
            await PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync();
        }

        public async Task<BeepSound> BeepSoundAsync()
        {
            return (await ExtendedSettingsAsync()).BeepSound;
        }

        public BeepSound BeepSound()
        {
            return ExtendedSettings().BeepSound;
        }


        public Hero3Camera Protune(bool state)
        {
            PrepareCommand<CommandCameraProtune>().Set(state).Execute();
            return this;
        }

        public async Task ProtuneAsync(bool state)
        {
            await PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync();
        }

        public Hero3Camera EnableProtune()
        {
            return Protune(true);
        }

        public async Task EnableProtuneAsync()
        {
            await ProtuneAsync(true);
        }

        public Hero3Camera DisableProtune()
        {
            return Protune(false);
        }

        public async Task DisableProtuneAsync()
        {
            await ProtuneAsync(false);
        }

        public bool SupportsProtune()
        {
            return PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public bool Protune()
        {
            return ExtendedSettings().Protune;
        }

        public async Task<bool> ProtuneAsync()
        {
            return (await ExtendedSettingsAsync()).Protune;
        }

        public IEnumerable<bool> ValidProtune()
        {
            return PrepareCommand<CommandCameraProtune>().ValidStates();
        }


        public Hero3Camera AutoLowLight(bool state)
        {
            PrepareCommand<CommandCameraAutoLowLight>().Set(state).Execute();
            return this;
        }

        public async Task AutoLowLightAsync(bool state)
        {
            await PrepareCommand<CommandCameraAutoLowLight>().Set(state).ExecuteAsync();
        }

        public Hero3Camera EnableAutoLowLight()
        {
            return AutoLowLight(true);
        }

        public async Task EnableAutoLowLightAsync()
        {
            await AutoLowLightAsync(true);
        }

        public Hero3Camera DisableAutoLowLight()
        {
            return AutoLowLight(false);
        }

        public async Task DisableAutoLowLightAsync()
        {
            await AutoLowLightAsync(false);
        }

        public bool SupportsAutoLowLight()
        {
            return PrepareCommand<CommandCameraAutoLowLight>().ValidStates().Any();
        }

        public bool AutoLowLight()
        {
            return ExtendedSettings().AutoLowLight;
        }

        public async Task<bool> AutoLowLightAsync()
        {
            return (await ExtendedSettingsAsync()).AutoLowLight;
        }

        public IEnumerable<bool> ValidAutoLowLight()
        {
            return PrepareCommand<CommandCameraAutoLowLight>().ValidStates();
        }


        public Hero3Camera PhotoResolution(PhotoResolution resolution)
        {
            PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute();
            return this;
        }

        public async Task PhotoResolutionAsync(PhotoResolution resolution)
        {
            await PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync();
        }

        public PhotoResolution PhotoResolution()
        {
            return ExtendedSettings().PhotoResolution;
        }

        public async Task<PhotoResolution> PhotoResolutionAsync()
        {
            return (await ExtendedSettingsAsync()).PhotoResolution;
        }

        public IEnumerable<PhotoResolution> ValidPhotoResolution()
        {
            var valid = PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }


        public Hero3Camera VideoStandard(VideoStandard videoStandard)
        {
            PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute();
            return this;
        }

        public async Task VideoStandardAsync(VideoStandard videoStandard)
        {
             await PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync();
        }

        public VideoStandard VideoStandard()
        {
            return ExtendedSettings().VideoStandard;
        }

        public async Task<VideoStandard> VideoStandardAsync()
        {
            return (await ExtendedSettingsAsync()).VideoStandard;
        }


        public Hero3Camera Mode(Mode mode)
        {
            PrepareCommand<CommandCameraMode>().Select(mode).Execute();
            return this;
        }

        public async Task ModeAsync(Mode mode)
        {
             await PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync();
        }

        public Mode Mode()
        {
            return ExtendedSettings().Mode;
        }

        public async Task<Mode> ModeAsync()
        {
            return (await ExtendedSettingsAsync()).Mode;
        }


        public Hero3Camera Locate(bool state)
        {
            PrepareCommand<CommandCameraLocate>().Set(state).Execute();
            return this;
        }

        public async Task LocateAsync(bool state)
        {
            await PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync();
        }

        public Hero3Camera EnableLocate()
        {
            return Locate(true);
        }

        public async Task EnableLocateAsync()
        {
            await LocateAsync(true);
        }

        public Hero3Camera DisableLocate()
        {
            return Locate(false);
        }

        public async Task DisableLocateAsync()
        {
            await LocateAsync(false);
        }

        public bool Locate()
        {
            return ExtendedSettings().LocateCamera;
        }

        public async Task<bool> LocateAsync()
        {
            return (await ExtendedSettingsAsync()).LocateCamera;
        }


        public Hero3Camera LivePreview(bool state)
        {
            PrepareCommand<CommandCameraPreview>().Set(state).Execute();
            return this;
        }

        public async Task LivePreviewAsync(bool state)
        {
             await PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync();
        }

        public Hero3Camera EnableLivePreview()
        {
            return LivePreview(true);
        }

        public async Task EnableLivePreviewAsync()
        {
             await LivePreviewAsync(true);
        }

        public Hero3Camera DisableLivePreview()
        {
            return LivePreview(false);
        }

        public async Task DisableLivePreviewAsync()
        {
            await LivePreviewAsync(false);
        }

        public bool LivePreview()
        {
            return ExtendedSettings().PreviewActive;
        }

        public async Task<bool> LivePreviewAsync()
        {
            return (await ExtendedSettingsAsync()).PreviewActive;
        }


        public Hero3Camera LedBlink(LedBlink ledBlink)
        {
            PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute();
            return this;
        }

        public async Task LedBlinkAsync(LedBlink ledBlink)
        {
             await PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync();
        }

        public LedBlink LedBlink()
        {
            return ExtendedSettings().LedBlink;
        }

        public async Task<LedBlink> LedBlinkAsync()
        {
            return (await ExtendedSettingsAsync()).LedBlink;
        }


        public Hero3Camera FieldOfView(FieldOfView fieldOfView)
        {
            PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute();
            return this;
        }

        public async Task FieldOfViewAsync(FieldOfView fieldOfView)
        {
             await PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync();
        }

        public FieldOfView FieldOfView()
        {
            return ExtendedSettings().FieldOfView;
        }

        public async Task<FieldOfView> FieldOfViewAsync()
        {
            return (await ExtendedSettingsAsync()).FieldOfView;
        }

        public IEnumerable<FieldOfView> ValidFieldOfView()
        {
            return PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }


        public Hero3Camera SpotMeter(bool state)
        {
            PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute();
            return this;
        }

        public async  Task SpotMeterAsync(bool state)
        {
             await  PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync();
        }

        public Hero3Camera EnableSpotMeter()
        {
            return SpotMeter(true);
        }

        public async Task EnableSpotMeterAsync()
        {
             await SpotMeterAsync(true);
        }

        public Hero3Camera DisableSpotMeter()
        {
            return SpotMeter(false);
        }

        public async Task DisableSpotMeterAsync()
        {
             await SpotMeterAsync(false);
        }

        public bool SpotMeter()
        {
            return ExtendedSettings().SpotMeter;
        }

        public async Task<bool> SpotMeterAsync()
        {
            return (await ExtendedSettingsAsync()).SpotMeter;
        }


        public Hero3Camera DefaultModeOnPowerOn(Mode mode)
        {
            PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute();
            return this;
        }

        public async Task DefaultModeOnPowerOnAsync(Mode mode)
        {
             await PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync();
        }

        public Mode DefaultModeOnPowerOn()
        {
            return ExtendedSettings().OnDefault;
        }

        public async Task<Mode> DefaultModeOnPowerOnAsync()
        {
            return (await ExtendedSettingsAsync()).OnDefault;
        }


        public Hero3Camera DeleteLastFileOnSdCard()
        {
            var task= PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute();
            return this;
        }

        public async Task DeleteLastFileOnSdCardAsync()
        {
            await PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();
        }

        public Hero3Camera DeleteAllFilesOnSdCard()
        {
            PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            return this;
        }

        public async Task DeleteAllFilesOnSdCardAsync()
        {
            await PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();
        }


        public Hero3Camera WhiteBalance(WhiteBalance whiteBalance)
        {
            PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute();
            return this;
        }

        public async Task WhiteBalanceAsync(WhiteBalance whiteBalance)
        {
             await PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync();
        }

        public WhiteBalance WhiteBalance()
        {
            return ExtendedSettings().WhiteBalance;
        }

        public async Task<WhiteBalance> WhiteBalanceAsync()
        {
            return (await ExtendedSettingsAsync()).WhiteBalance;
        }

        public IEnumerable<WhiteBalance> ValidWhiteBalance()
        {
            var valid = PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }


        public Hero3Camera LoopingVideo(LoopingVideo loopingVideo)
        {
            PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute();
            return this;
        }

        public async Task LoopingVideoAsync(LoopingVideo loopingVideo)
        {
            await PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync();
        }

        public LoopingVideo LoopingVideo()
        {
            return ExtendedSettings().LoopingVideoMode;
        }

        public async Task<LoopingVideo> LoopingVideoAsync()
        {
            return (await ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public IEnumerable<LoopingVideo> ValidLoopingVideo()
        {
            var valid = PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }


        public Hero3Camera FrameRate(FrameRate frameRate)
        {
            PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute();
            return this;
        }

        public async Task FrameRateAsync(FrameRate frameRate)
        {
             await PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync();
        }

        public FrameRate FrameRate()
        {
            return ExtendedSettings().FrameRate;
        }

        public async Task<FrameRate> FrameRateAsync()
        {
            return (await ExtendedSettingsAsync()).FrameRate;
        }

        public IEnumerable<FrameRate> ValidFrameRate()
        {
            var valid = PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }


        public Hero3Camera BurstRate(BurstRate burstRate)
        {
            PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute();
            return this;
        }

        public async Task BurstRateAsync(BurstRate burstRate)
        {
            await PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync();
        }

        public BurstRate BurstRate()
        {
            return ExtendedSettings().BurstRate;
        }

        public async Task<BurstRate> BurstRateAsync()
        {
            return (await ExtendedSettingsAsync()).BurstRate;
        }


        public Hero3Camera ContinuousShot(ContinuousShot continuousShot)
        {
            PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute();
            return this;
        }

        public async Task ContinuousShotAsync(ContinuousShot continuousShot)
        {
             await PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync();
        }

        public ContinuousShot ContinuousShot()
        {
            return ExtendedSettings().ContinuousShot;
        }

        public async Task<ContinuousShot> ContinuousShotAsync()
        {
            return (await ExtendedSettingsAsync()).ContinuousShot;
        }


        public Hero3Camera PhotoInVideo(PhotoInVideo photoInVideo)
        {
            PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).Execute();
            return this;
        }

        public async Task PhotoInVideoAsync(PhotoInVideo photoInVideo)
        {
             await PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).ExecuteAsync();
        }

        public PhotoInVideo PhotoInVideo()
        {
            return ExtendedSettings().PhotoInVideo;
        }

        public async Task<PhotoInVideo> PhotoInVideoAsync()
        {
            return (await ExtendedSettingsAsync()).PhotoInVideo;
        }


        public new Hero3Camera Shutter(bool state)
        {
            return base.Shutter(state) as Hero3Camera;
        }

        public Hero3Camera OpenShutter()
        {
            return Shutter(true);
        }

        public async Task OpenShutterAsync()
        {
             await ShutterAsync(true);
        }

        public Hero3Camera CloseShutter()
        {
            return Shutter(false);
        }

        public async Task CloseShutterAsync()
        {
             await ShutterAsync(false);
        }

        public bool Shutter()
        {
            return ExtendedSettings().Shutter || BacpacStatus().ShutterStatus > 0;
        }

        public async Task<bool> ShutterAsync()
        {
            return (await ExtendedSettingsAsync()).Shutter || (await BacpacStatusAsync()).ShutterStatus > 0;
        }


        public new Hero3Camera Power(bool state)
        {
            return base.Power(state) as Hero3Camera;
        }

        public Hero3Camera PowerOn()
        {
            return Power(true);
        }

        public async Task PowerOnAsync()
        {
             await PowerAsync(true);
        }

        public Hero3Camera PowerOff()
        {
            return Power(false);
        }

        public async Task PowerOffAsync()
        {
             await PowerAsync(false);
        }

        public bool Power()
        {
           return BacpacStatus().CameraPower;
        }

        public async Task<bool> PowerAsync()
        {
            return (await BacpacStatusAsync()).CameraPower;
        }

        public Model Model()
        {
            return BacpacStatusCache().CameraModel;
        }

        public SignalStrength SignalStrength()
        {
            return  BacpacStatus().Rssi;
        }

        public async Task<SignalStrength> SignalStrengthAsync()
        {
            return (await BacpacStatusAsync()).Rssi;
        }

        public byte BatteryStatus()
        {
            return Settings().Battery;
        }

        public async Task<byte> BatteryStatusAsync()
        {
            return (await SettingsAsync()).Battery;
        }

        public int PhotoCount()
        {
            return ExtendedSettings().PhotosCount;
        }

        public async Task<int> PhotoCountAsync()
        {
            return (await ExtendedSettingsAsync()).PhotosCount;
        }

        public int AvailablePhotoSpace()
        {
            return ExtendedSettings().PhotosAvailableSpace;
        }

        public async Task<int> AvailablePhotoSpaceAsync()
        {
            return (await ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public int VideoCount()
        {
            return ExtendedSettings().VideosCount;
        }

        public async Task<int> VideoCountAsync()
        {
            return (await ExtendedSettingsAsync()).VideosCount;
        }

        public TimeSpan AvailableVideoSpace()
        {
            var seconds = ExtendedSettings().VideosAvailableSpace;
            return new TimeSpan(0, 0, seconds);
        }

        public async Task<TimeSpan> AvailableVideoSpaceAsync()
        {
            var seconds = (await ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public string MacAddress()
        {
            return BacpacInformationCache().MacAddress;
        }

        public string IpAddress()
        {
            return Bacpac.Address;
        }

        public string Password()
        {
            return Bacpac.Password;
        }

        public Version BootLoader()
        {
            return BacpacInformationCache().BootloaderVersion;
        }

        public Version Firmware()
        {
            return BacpacInformationCache().FirmwareVersion;
        }

        public string Ssid()
        {
            return BacpacInformationCache().Ssid;
        }

        public string FullName()
        {
            return Information().Name;
        }

        public async Task<string> FullNameAsync()
        {
            return (await InformationAsync()).Name;
        }

        public string Version()
        {
            return InformationCache().Version;
        }

        public bool LivePreviewAvailable()
        {
            return ExtendedSettings().PreviewAvailable;
        }

        public async Task<bool> LivePreviewAvailableAsync()
        {
            return (await ExtendedSettingsAsync()).PreviewAvailable;
        }


        public Hero3Camera Chain(params Func<Hero3Camera, object>[] actions)
        {
            foreach (var a in actions)
            {
                var res = a(this);
                if (res is ICamera)
                    continue;

                AsyncHelpers.RunSync(() => (Task)res);
            }
            return this;
        }

        public async Task ChainAsync(params Func<Hero3Camera, object>[] actions)
        {
            foreach (var a in actions)
            {
                var res = a(this);
                if (res is ICamera)
                    continue;

                await (Task)res;
            }
        }

        public async Task ChainAsync(params Func<Hero3Camera, Task>[] actions)
        {
            foreach (var a in actions)
                await a(this);
        }

        public Hero3Camera Chain<T>(Func<Hero3Camera, T> f, out T output)
        {
            output = f(this);
            return this;
        }
    }
}