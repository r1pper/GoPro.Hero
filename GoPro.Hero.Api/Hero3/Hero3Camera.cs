using System;
using System.Collections.Generic;
using GoPro.Hero.Api.Commands.CameraCommands;

namespace GoPro.Hero.Api.Hero3
{
    public class Hero3Camera:Camera
    {
        public Hero3Camera(Bacpac bacpac) : base(bacpac) {
            var filter = Filtering.FilterGeneric.Create("GoPro.Hero.Api.Hero3.Hero3FilterScheme.xml");
            base.SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address)) { }

        public Hero3Camera VideoResolution(VideoResolution resolution)
        {
            return base.PrepareCommand<CommandCameraVideoResolution>().Select(resolution).Execute() as Hero3Camera;
        }

        public Hero3Camera VideoResolution(out VideoResolution resolution)
        {
            resolution = base.ExtendedSettings.VideoResolution;
            return this;
        }

        public IEnumerable<VideoResolution> ValidVideoResolution()
        {
            var valid = base.PrepareCommand<CommandCameraVideoResolution>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidVideoResolution(out IEnumerable<VideoResolution> validVideoResolution)
        {
            validVideoResolution = this.ValidVideoResolution();
            return this;
        }

        public Hero3Camera Orientation(Orientation orientation)
        {
            return base.PrepareCommand<CommandCameraOrientation>().Select(orientation).Execute() as Hero3Camera;
        }

        public Hero3Camera Orientation(out Orientation orientation)
        {
            orientation = base.ExtendedSettings.Orientation;
            return this;
        }

        public Hero3Camera TimeLapse(TimeLapse timeLapse)
        {
            return base.PrepareCommand<CommandCameraTimeLapse>().Select(timeLapse).Execute() as Hero3Camera;
        }

        public Hero3Camera TimeLapse(out TimeLapse timeLapse)
        {
            timeLapse = base.ExtendedSettings.TimeLapse;
            return this;
        }

        public Hero3Camera BeepSound(BeepSound beepSound)
        {
            return base.PrepareCommand<CommandCameraBeepSound>().Select(beepSound).Execute() as Hero3Camera;
        }

        public Hero3Camera BeepSound(out BeepSound beepSound)
        {
            beepSound = base.ExtendedSettings.BeepSound;
            return this;
        }

        public Hero3Camera Protune(bool state)
        {
            return base.PrepareCommand<CommandCameraProtune>().Set(state).Execute() as Hero3Camera;
        }

        public Hero3Camera EnableProtune()
        {
            return this.Protune(true);
        }

        public Hero3Camera DisableProtune()
        {
            return this.Protune(false);
        }

        public Hero3Camera Protune(out bool state)
        {
            state = base.ExtendedSettings.Protune;
            return this;
        }

        public IEnumerable<bool> ValidProtune()
        {
            return base.PrepareCommand<CommandCameraProtune>().ValidStates();
        }

        public Hero3Camera ValidProtune(out IEnumerable<bool> validProtune)
        {
            validProtune = this.ValidProtune();
            return this;
        }

        public Hero3Camera PhotoResolution(PhotoResolution resolution)
        {
            return base.PrepareCommand<CommandCameraPhotoResolution>().Select(resolution).Execute() as Hero3Camera;
        }

        public Hero3Camera PhotoResolution(out PhotoResolution resolution)
        {
            resolution = base.ExtendedSettings.PhotoResolution;
            return this;
        }

        public IEnumerable<PhotoResolution> ValidPhotoResolution()
        {
            var valid = this.PrepareCommand<CommandCameraPhotoResolution>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidPhotoResolution(out IEnumerable<PhotoResolution> validPhotoResolution)
        {
            validPhotoResolution = this.ValidPhotoResolution();
            return this;
        }

        public Hero3Camera VideoStandard(VideoStandard videoStandard)
        {
            return base.PrepareCommand<CommandCameraVideoStandard>().Select(videoStandard).Execute() as Hero3Camera;
        }

        public Hero3Camera VideoStandard(out VideoStandard videoStandard)
        {
            videoStandard = base.ExtendedSettings.VideoStandard;
            return this;
        }

        public Hero3Camera Mode(Mode mode)
        {
            return base.PrepareCommand<CommandCameraMode>().Select(mode).Execute() as Hero3Camera;
        }

        public Hero3Camera Mode(out Mode mode)
        {
            mode = base.ExtendedSettings.Mode;
            return this;
        }

        public Hero3Camera Locate(bool state)
        {
            return base.PrepareCommand<CommandCameraLocate>().Set(state).Execute() as Hero3Camera;
        }

        public Hero3Camera EnableLocate()
        {
            return this.Locate(true);
        }

        public Hero3Camera DisableLocate()
        {
            return this.Locate(false);
        }

        public Hero3Camera Locate(out bool state)
        {
            state = base.ExtendedSettings.LocateCamera;
            return this;
        }

        public Hero3Camera LivePreview(bool state)
        {
            return base.PrepareCommand<CommandCameraPreview>().Set(state).Execute() as Hero3Camera;
        }

        public Hero3Camera EnableLivePreview()
        {
            return this.LivePreview(true);
        }

        public Hero3Camera DisableLivePreview()
        {
            return this.LivePreview(false);
        }

        public Hero3Camera LivePreview(out bool state)
        {
            state = base.ExtendedSettings.PreviewActive;
            return this;
        }

        public Hero3Camera LedBlink(LedBlink ledBlink)
        {
            return base.PrepareCommand<CommandCameraLedBlink>().Select(ledBlink).Execute() as Hero3Camera;
        }

        public Hero3Camera LedBlink(out LedBlink ledBlink)
        {
            ledBlink = base.ExtendedSettings.LedBlink;
            return this;
        }

        public Hero3Camera FieldOfView(FieldOfView fieldOfView)
        {
            return base.PrepareCommand<CommandCameraFieldOfView>().Select(fieldOfView).Execute() as Hero3Camera;
        }

        public Hero3Camera FieldOfView(out FieldOfView fieldOfView)
        {
            fieldOfView = base.ExtendedSettings.FieldOfView;
            return this;
        }

        public IEnumerable<FieldOfView> ValidFieldOfView()
        {
            return base.PrepareCommand<CommandCameraFieldOfView>().ValidStates();
        }

        public Hero3Camera ValidFieldOfView(out IEnumerable<FieldOfView> validFieldOfView)
        {
            validFieldOfView = this.ValidFieldOfView();
            return this;
        }

        public Hero3Camera SpotMeter(bool state)
        {
            return base.PrepareCommand<CommandCameraSpotMeter>().Set(state).Execute() as Hero3Camera;
        }

        public Hero3Camera EnableSpotMeter()
        {
            return this.SpotMeter(true);
        }

        public Hero3Camera DisableSpotMeter()
        {
            return this.SpotMeter(false);
        }

        public Hero3Camera SpotMeter(out bool state)
        {
            state = base.ExtendedSettings.SpotMeter;
            return this;
        }

        public Hero3Camera DefaultModeOnPowerOn(Mode mode)
        {
            return base.PrepareCommand<CommandCameraDefaultMode>().Select(mode).Execute() as Hero3Camera;
        }

        public Hero3Camera DefaultModeOnPowerOn(out Mode mode)
        {
            mode = base.ExtendedSettings.Mode;
            return this;
        }

        public Hero3Camera DeleteLastFileOnSdCard()
        {
            return base.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute() as Hero3Camera;
        }

        public Hero3Camera DeleteAllFilesOnSdCard()
        {
            return base.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute() as Hero3Camera;
        }

        public Hero3Camera WhiteBalance(WhiteBalance whiteBalance)
        {
            return base.PrepareCommand<CommandCameraWhiteBalance>().Select(whiteBalance).Execute() as Hero3Camera;
        }

        public Hero3Camera WhiteBalance(out WhiteBalance whiteBalance)
        {
            whiteBalance = base.ExtendedSettings.WhiteBalance;
            return this;
        }

        public IEnumerable<WhiteBalance> ValidWhiteBalance()
        {
            var valid = this.PrepareCommand<CommandCameraWhiteBalance>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidWhiteBalance(out IEnumerable<WhiteBalance> validWhiteBalance)
        {
            validWhiteBalance = this.ValidWhiteBalance();
            return this;
        }

        public Hero3Camera LoopingVideo(LoopingVideo loopingVideo)
        {
            return base.PrepareCommand<CommandCameraLoopingVideo>().Select(loopingVideo).Execute() as Hero3Camera;
        }

        public Hero3Camera LoopingVideo(out LoopingVideo loopingVideo)
        {
            loopingVideo = base.ExtendedSettings.LoopingVideoMode;
            return this;
        }

        public IEnumerable<LoopingVideo> ValidLoopingVideo()
        {
            var valid = this.PrepareCommand<CommandCameraLoopingVideo>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidLoopingVideo(out IEnumerable<LoopingVideo> validLoopingVideo)
        {
            validLoopingVideo = this.ValidLoopingVideo();
            return this;
        }

        public Hero3Camera FrameRate(FrameRate frameRate)
        {
            return base.PrepareCommand<CommandCameraFrameRate>().Select(frameRate).Execute() as Hero3Camera;
        }

        public Hero3Camera FrameRate(out FrameRate frameRate)
        {
            frameRate = base.ExtendedSettings.FrameRate;
            return this;
        }

        public IEnumerable<FrameRate> ValidFrameRate()
        {
            var valid = this.PrepareCommand<CommandCameraFrameRate>().ValidStates();
            return valid;
        }

        public Hero3Camera ValidFrameRate(out IEnumerable<FrameRate> validFrameRate)
        {
            validFrameRate = this.ValidFrameRate();
            return this;
        }

        public Hero3Camera BurstRate(BurstRate burstRate)
        {
            return base.PrepareCommand<CommandCameraBurstRate>().Select(burstRate).Execute() as Hero3Camera;
        }

        public Hero3Camera BurstRate(out BurstRate burstRate)
        {
            burstRate = base.ExtendedSettings.BurstRate;
            return this;
        }

        public Hero3Camera ContinuousShot(ContinuousShot continuousShot)
        {
            return base.PrepareCommand<CommandCameraContinuousShot>().Select(continuousShot).Execute() as Hero3Camera;
        }

        public Hero3Camera ContinuousShot(out ContinuousShot continuousShot)
        {
            continuousShot = base.ExtendedSettings.ContinuousShot;
            return this;
        }

        public new Hero3Camera Shutter(bool state)
        {
            return base.Shutter(state) as Hero3Camera;
        }

        public Hero3Camera OpenShutter()
        {
            return this.Shutter(true);
        }

        public Hero3Camera CloseShutter()
        {
            return this.Shutter(false);
        }

        public Hero3Camera Shutter(out bool state)
        {
            state = base.ExtendedSettings.Shutter|| base.BacpacStatus.ShutterStatus>0;
            return this;
        }

        public new Hero3Camera Power(bool state)
        {
            return base.Power(state) as Hero3Camera;
        }

        public Hero3Camera PowerOn()
        {
            return this.Power(true);
        }

        public Hero3Camera PowerOff()
        {
            return this.Power(false);
        }

        public Hero3Camera Power(out bool state)
        {
            state = base.BacpacStatus.CameraPower;
            return this;
        }

        public Hero3Camera Model(out Model model)
        {
            model = base.BacpacStatus.CameraModel;
            return this;
        }

        public Hero3Camera SignalStrength(out SignalStrength signalStrength)
        {
            signalStrength = base.BacpacStatus.Rssi;
            return this;
        }

        public Hero3Camera BatteryStatus(out byte batteryStatus)
        {
            batteryStatus = base.Settings.Battery;
            return this;
        }

        public Hero3Camera PhotoCount(out int photoCount)
        {
            photoCount = base.ExtendedSettings.PhotosCount;
            return this;
        }

        public Hero3Camera AvailablePhotoSpace(out int availableSpace)
        {
            availableSpace = base.ExtendedSettings.PhotosAvailableSpace;
            return this;
        }

        public Hero3Camera VideoCount(out int videoCount)
        {
            videoCount = base.ExtendedSettings.VideosCount;
            return this;
        }

        public Hero3Camera AvailableVideoSpace(out TimeSpan availableSpace)
        {
            var seconds = base.ExtendedSettings.VideosAvailableSpace;
            availableSpace = new TimeSpan(0, 0, seconds);
            return this;
        }

        public Hero3Camera MacAddress(out string macAddress)
        {
            macAddress = base.BacpacInformation.MacAddress;
            return this;
        }

        public Hero3Camera IpAddress(out string ipAddress)
        {
            ipAddress = base.bacpac.Address;
            return this;
        }

        public Hero3Camera Password(out string password)
        {
            password = base.bacpac.Password;
            return this;
        }

        public Hero3Camera BootLoader(out Version version)
        {
            version = base.BacpacInformation.BootloaderVersion;
            return this;
        }

        public Hero3Camera Firmware(out Version version)
        {
            version = base.BacpacInformation.FirmwareVersion;
            return this;
        }

        public Hero3Camera Ssid(out string ssid)
        {
            ssid = base.BacpacInformation.Ssid;
            return this;
        }

        public Hero3Camera FullName(out string fullName)
        {
            fullName = base.Information.Name;
            return this;
        }

        public Hero3Camera Version(out string version)
        {
            version = base.Information.Version;
            return this;
        }

        public Hero3Camera LivePreviewAvailable(out bool state)
        {
            state = base.ExtendedSettings.PreviewAvailable;
            return this;
        }
    }
}
