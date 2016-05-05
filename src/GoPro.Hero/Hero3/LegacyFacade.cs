using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoPro.Hero.Hero3
{
    public  class LegacyFacade :ICameraFacade
    {
        private readonly LegacyCamera _camera;

        public ICamera Camera()
        {
            return _camera;
        }

        public T Camera<T>() where T : class, ICamera
        {
            return _camera as T;
        }

        public  Node Browse()
        {  
            return _camera.FileSystem<AmbarellaBrowser>();
        }

        public ICameraFacade VideoResolution( VideoResolution resolution)
        {
            _camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute();
            return this;
        }

        public  async Task VideoResolutionAsync( VideoResolution resolution)
        {
            await _camera.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).ExecuteAsync();
        }

        public  async Task<VideoResolution> VideoResolutionAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).VideoResolution;
        }

        public  VideoResolution VideoResolution()
        {
            return _camera.ExtendedSettings().VideoResolution;
        }

        public  IEnumerable<VideoResolution> ValidVideoResolution()
        {
            var valid = _camera.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }


        public  ICameraFacade Orientation( Orientation orientation)
        {
            _camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute();
            return this;
        }

        public  async Task OrientationAsync( Orientation orientation)
        {
            await _camera.PrepareCommand<CommandCameraOrientation>().Select(orientation).ExecuteAsync();
        }

        public  Orientation Orientation()
        {
            return _camera.ExtendedSettings().Orientation;
        }

        public  async Task<Orientation> OrientationAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).Orientation;
        }


        public  ICameraFacade TimeLapse( TimeLapse timeLapse)
        {
            _camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute();
            return this;
        }

        public  async Task TimeLapseAsync( TimeLapse timeLapse)
        {
            await _camera.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).ExecuteAsync();
        }

        public  TimeLapse TimeLapse()
        {
            return _camera.ExtendedSettings().TimeLapse;
        }

        public  async Task<TimeLapse> TimeLapseAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).TimeLapse;
        }


        public  ICameraFacade BeepSound( BeepSound beepSound)
        {
            _camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute();
            return this;
        }

        public  async Task BeepSoundAsync( BeepSound beepSound)
        {
            await _camera.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).ExecuteAsync();
        }

        public  async Task<BeepSound> BeepSoundAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).BeepSound;
        }

        public  BeepSound BeepSound()
        {
            return _camera.ExtendedSettings().BeepSound;
        }


        public  ICameraFacade Protune( bool state)
        {
            _camera.PrepareCommand<CommandCameraProtune>().Set(state).Execute();
            return this;
        }

        public  async Task ProtuneAsync( bool state)
        {
            await _camera.PrepareCommand<CommandCameraProtune>().Set(state).ExecuteAsync();
        }

        public  bool SupportsProtune()
        {
            return _camera.PrepareCommand<CommandCameraProtune>().ValidStates().Any();
        }

        public  bool Protune()
        {
            return _camera.ExtendedSettings().Protune;
        }

        public  async Task<bool> ProtuneAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).Protune;
        }

        public  IEnumerable<bool> ValidProtune()
        {
            return _camera.PrepareCommand<CommandCameraProtune>().ValidStates();
        }


        public  ICameraFacade AutoLowLight( bool state)
        {
            _camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).Execute();
            return this;
        }

        public  async Task AutoLowLightAsync( bool state)
        {
            await _camera.PrepareCommand<CommandCameraAutoLowLight>().Set(state).ExecuteAsync();
        }    

        public  bool SupportsAutoLowLight()
        {
            return _camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates().Any();
        }

        public  bool AutoLowLight()
        {
            return _camera.ExtendedSettings().AutoLowLight;
        }

        public  async Task<bool> AutoLowLightAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).AutoLowLight;
        }

        public  IEnumerable<bool> ValidAutoLowLight()
        {
            return _camera.PrepareCommand<CommandCameraAutoLowLight>().ValidStates();
        }


        public  ICameraFacade PhotoResolution( PhotoResolution resolution)
        {
            _camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute();
            return this;
        }

        public  async Task PhotoResolutionAsync( PhotoResolution resolution)
        {
            await _camera.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).ExecuteAsync();
        }

        public  PhotoResolution PhotoResolution()
        {
            return _camera.ExtendedSettings().PhotoResolution;
        }

        public  async Task<PhotoResolution> PhotoResolutionAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PhotoResolution;
        }

        public  IEnumerable<PhotoResolution> ValidPhotoResolution()
        {
            var valid = _camera.PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }


        public  ICameraFacade VideoStandard( VideoStandard videoStandard)
        {
            _camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute();
            return this;
        }

        public  async Task VideoStandardAsync( VideoStandard videoStandard)
        {
            await _camera.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).ExecuteAsync();
        }

        public  VideoStandard VideoStandard()
        {
            return _camera.ExtendedSettings().VideoStandard;
        }

        public  async Task<VideoStandard> VideoStandardAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).VideoStandard;
        }


        public  ICameraFacade Mode( Mode mode)
        {
            _camera.PrepareCommand<CommandCameraMode>().Select(mode).Execute();
            return this;
        }

        public  async Task ModeAsync( Mode mode)
        {
            await _camera.PrepareCommand<CommandCameraMode>().Select(mode).ExecuteAsync();
        }

        public  Mode Mode()
        {
            return _camera.ExtendedSettings().Mode;
        }

        public  async Task<Mode> ModeAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).Mode;
        }


        public  ICameraFacade Locate( bool state)
        {
            _camera.PrepareCommand<CommandCameraLocate>().Set(state).Execute();
            return this;
        }

        public  async Task LocateAsync( bool state)
        {
            await _camera.PrepareCommand<CommandCameraLocate>().Set(state).ExecuteAsync();
        }

        public  bool Locate()
        {
            return _camera.ExtendedSettings().LocateCamera;
        }

        public  async Task<bool> LocateAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).LocateCamera;
        }


        public  ICameraFacade LivePreview( bool state)
        {
            _camera.PrepareCommand<CommandCameraPreview>().Set(state).Execute();
            return this;
        }

        public  async Task LivePreviewAsync( bool state)
        {
            await _camera.PrepareCommand<CommandCameraPreview>().Set(state).ExecuteAsync();
        }

        public  bool LivePreview()
        {
            return _camera.ExtendedSettings().PreviewActive;
        }

        public  async Task<bool> LivePreviewAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PreviewActive;
        }


        public  ICameraFacade LedBlink( LedBlink ledBlink)
        {
            _camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute();
            return this;
        }

        public  async Task LedBlinkAsync( LedBlink ledBlink)
        {
            await _camera.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).ExecuteAsync();
        }

        public  LedBlink LedBlink()
        {
            return _camera.ExtendedSettings().LedBlink;
        }

        public  async Task<LedBlink> LedBlinkAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).LedBlink;
        }


        public  ICameraFacade FieldOfView( FieldOfView fieldOfView)
        {
            _camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute();
            return this;
        }

        public  async Task FieldOfViewAsync( FieldOfView fieldOfView)
        {
            await _camera.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).ExecuteAsync();
        }

        public  FieldOfView FieldOfView()
        {
            return _camera.ExtendedSettings().FieldOfView;
        }

        public  async Task<FieldOfView> FieldOfViewAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).FieldOfView;
        }

        public  IEnumerable<FieldOfView> ValidFieldOfView()
        {
            return _camera.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }


        public  ICameraFacade SpotMeter( bool state)
        {
            _camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute();
            return this;
        }

        public  async Task SpotMeterAsync( bool state)
        {
            await _camera.PrepareCommand<CommandCameraSpotMeter>().Set(state).ExecuteAsync();
        }

        public  bool SpotMeter()
        {
            return _camera.ExtendedSettings().SpotMeter;
        }

        public  async Task<bool> SpotMeterAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).SpotMeter;
        }


        public  ICameraFacade DefaultModeOnPowerOn( Mode mode)
        {
            _camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute();
            return this;
        }

        public  async Task DefaultModeOnPowerOnAsync( Mode mode)
        {
            await _camera.PrepareCommand<CommandCameraDefaultMode>().Select(mode).ExecuteAsync();
        }

        public  Mode DefaultModeOnPowerOn()
        {
            return _camera.ExtendedSettings().OnDefault;
        }

        public  async Task<Mode> DefaultModeOnPowerOnAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).OnDefault;
        }


        public  ICameraFacade DeleteLastFileOnSdCard()
        {
            var task = _camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute();
            return this;
        }

        public  async Task DeleteLastFileOnSdCardAsync()
        {
            await _camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().ExecuteAsync();
        }

        public  ICameraFacade DeleteAllFilesOnSdCard()
        {
            _camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            return this;
        }

        public  async Task DeleteAllFilesOnSdCardAsync()
        {
            await _camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().ExecuteAsync();
        }


        public  ICameraFacade WhiteBalance( WhiteBalance whiteBalance)
        {
            _camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute();
            return this;
        }

        public  async Task WhiteBalanceAsync( WhiteBalance whiteBalance)
        {
            await _camera.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).ExecuteAsync();
        }

        public  WhiteBalance WhiteBalance()
        {
            return _camera.ExtendedSettings().WhiteBalance;
        }

        public  async Task<WhiteBalance> WhiteBalanceAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).WhiteBalance;
        }

        public  IEnumerable<WhiteBalance> ValidWhiteBalance()
        {
            var valid = _camera.PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }


        public  ICameraFacade LoopingVideo( LoopingVideo loopingVideo)
        {
            _camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute();
            return this;
        }

        public  async Task LoopingVideoAsync( LoopingVideo loopingVideo)
        {
            await _camera.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).ExecuteAsync();
        }

        public  LoopingVideo LoopingVideo()
        {
            return _camera.ExtendedSettings().LoopingVideoMode;
        }

        public  async Task<LoopingVideo> LoopingVideoAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).LoopingVideoMode;
        }

        public  IEnumerable<LoopingVideo> ValidLoopingVideo()
        {
            var valid = _camera.PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }


        public  ICameraFacade FrameRate( FrameRate frameRate)
        {
            _camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute();
            return this;
        }

        public  async Task FrameRateAsync( FrameRate frameRate)
        {
            await _camera.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).ExecuteAsync();
        }

        public  FrameRate FrameRate()
        {
            return _camera.ExtendedSettings().FrameRate;
        }

        public  async Task<FrameRate> FrameRateAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).FrameRate;
        }

        public  IEnumerable<FrameRate> ValidFrameRate()
        {
            var valid = _camera.PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }


        public  ICameraFacade BurstRate( BurstRate burstRate)
        {
            _camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute();
            return this;
        }

        public  async Task BurstRateAsync( BurstRate burstRate)
        {
            await _camera.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).ExecuteAsync();
        }

        public  BurstRate BurstRate()
        {
            return _camera.ExtendedSettings().BurstRate;
        }

        public  async Task<BurstRate> BurstRateAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).BurstRate;
        }


        public  ICameraFacade ContinuousShot( ContinuousShot continuousShot)
        {
            _camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute();
            return this;
        }

        public  async Task ContinuousShotAsync( ContinuousShot continuousShot)
        {
            await _camera.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).ExecuteAsync();
        }

        public  ContinuousShot ContinuousShot()
        {
            return _camera.ExtendedSettings().ContinuousShot;
        }

        public  async Task<ContinuousShot> ContinuousShotAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).ContinuousShot;
        }


        public  ICameraFacade PhotoInVideo( PhotoInVideo photoInVideo)
        {
            _camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).Execute();
            return this;
        }

        public  async Task PhotoInVideoAsync( PhotoInVideo photoInVideo)
        {
            await _camera.PrepareCommand<CommandCameraPhotoInVideo>().Select(photoInVideo).ExecuteAsync();
        }

        public  PhotoInVideo PhotoInVideo()
        {
            return _camera.ExtendedSettings().PhotoInVideo;
        }

        public  async Task<PhotoInVideo> PhotoInVideoAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PhotoInVideo;
        }

        public  bool Shutter()
        {
            return _camera.ExtendedSettings().Shutter || _camera.BacpacStatus().ShutterStatus > 0;
        }

        public  async Task<bool> ShutterAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).Shutter || (await _camera.BacpacStatusAsync()).ShutterStatus > 0;
        }

        public ICameraFacade Shutter(bool state)
        {
            _camera.Shutter(state);
            return this;
        }

        public async Task ShutterAsync(bool state)
        {
            await _camera.ShutterAsync(state);
        }

        public ICameraFacade Power(bool power)
        {
            _camera.Power(power);
            return this;
        }

        public async Task PowerAsync(bool power)
        {
            await _camera.PowerAsync(true);
        }

        public  bool Power()
        {
            return _camera.BacpacStatus().CameraPower;
        }

        public  async Task<bool> PowerAsync()
        {
            return (await _camera.BacpacStatusAsync()).CameraPower;
        }

        public  SignalStrength SignalStrength()
        {
            return _camera.BacpacStatus().Rssi;
        }

        public  async Task<SignalStrength> SignalStrengthAsync()
        {
            return (await _camera.BacpacStatusAsync()).Rssi;
        }

        public  byte BatteryStatus()
        {
            return _camera.Settings().Battery;
        }

        public  async Task<byte> BatteryStatusAsync()
        {
            return (await _camera.SettingsAsync()).Battery;
        }

        public  int PhotoCount()
        {
            return _camera.ExtendedSettings().PhotosCount;
        }

        public  async Task<int> PhotoCountAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PhotosCount;
        }

        public  int AvailablePhotoSpace()
        {
            return _camera.ExtendedSettings().PhotosAvailableSpace;
        }

        public  async Task<int> AvailablePhotoSpaceAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PhotosAvailableSpace;
        }

        public  int VideoCount()
        {
            return _camera.ExtendedSettings().VideosCount;
        }

        public  async Task<int> VideoCountAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).VideosCount;
        }

        public  TimeSpan AvailableVideoSpace()
        {
            var seconds = _camera.ExtendedSettings().VideosAvailableSpace;
            return new TimeSpan(0, 0, seconds);
        }

        public  async Task<TimeSpan> AvailableVideoSpaceAsync()
        {
            var seconds = (await _camera.ExtendedSettingsAsync()).VideosAvailableSpace;
            var availableSpace = new TimeSpan(0, 0, seconds);
            return availableSpace;
        }

        public  string FullName()
        {
            return _camera.Information().Name;
        }

        public  async Task<string> FullNameAsync()
        {
            return (await _camera.InformationAsync()).Name;
        }


        public  bool LivePreviewAvailable()
        {
            return _camera.ExtendedSettings().PreviewAvailable;
        }

        public  async Task<bool> LivePreviewAvailableAsync()
        {
            return (await _camera.ExtendedSettingsAsync()).PreviewAvailable;
        }


        public ICameraFacade Chain(params Func<ICameraFacade, Task>[] fs)
        {
            foreach (var f in fs)
                AsyncHelpers.RunSync(() => f(this));

            return this;
        }

        public async Task ChainAsync(params Func<ICameraFacade, Task>[] fs)
        {
            foreach (var f in fs)
                await f(this);
        }

        public ICameraFacade Chain<T>(Func<ICameraFacade, T> f, out T output)
        {
            output = f(this);
            return this;
        }

        public ICameraFacade Chain<T>(Func<ICameraFacade, Task<T>> f, out T output)
        {
            output = AsyncHelpers.RunSync(() => f(this));
            return this;
        }

        public ICameraFacade Chain<T>(Func<ICameraFacade, T> f, Action<T> output)
        {
            output(f(this));
            return this;
        }

        public async Task ChainAsync<T>(Func<ICameraFacade, Task<T>> f, Action<T> output)
        {
            output(await f(this));
        }

        public ICameraFacade Chain<T>(Func<ICameraFacade, Task<T>> f, Action<T> output)
        {
            output(AsyncHelpers.RunSync(() => f(this)));
            return this;
        }

        public LegacyFacade(LegacyCamera camera)
        {
            _camera = camera;
        }
    }
}
