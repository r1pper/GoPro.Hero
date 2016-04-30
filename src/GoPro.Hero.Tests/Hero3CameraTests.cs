using System;
using System.Threading;
using GoPro.Hero.Commands;
using GoPro.Hero.Hero3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Tests
{
    [TestClass]
    public class Hero3CameraTests
    {
        private Hero3Camera GetCamera()
        {
            var camera = Camera.Create<Hero3Camera>(ExpectedParameters.IP_ADDRESS);
            return camera;
        }

        private void CheckMultipleChoice<T, TC>(Func<T, T> checkDelegate, Func<T> getInit)
            where TC : CommandMultiChoice<T, ICamera>
        {
            var availableValues = GetCamera().PrepareCommand<TC>().ValidStates(); //Enum.GetValues(typeof(T));
            var init = getInit();

            foreach (var value in availableValues)
                Assert.AreEqual(value, checkDelegate(value));

            Assert.AreEqual(init, checkDelegate(init));
        }

        [TestInitialize]
        public void InitializeCamera()
        {
            var camera = GetCamera();

            var init = camera.BacpacStatus().CameraPower;
            if (init) return;

            camera.Power(true);
            Thread.Sleep(5000);
            var res = camera.BacpacStatus().CameraPower;
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void CheckHero3CameraInitialize()
        {
            var camera = GetCamera();
            Assert.IsNotNull(camera);
        }

        [TestMethod]
        public void CheckAvailablePhotoSpace()
        {
            var photoSpaceAvailable = GetCamera().AvailablePhotoSpace();
            var photoSpace =GetCamera().ExtendedSettings().PhotosAvailableSpace;
            Assert.AreEqual(photoSpace, photoSpaceAvailable);
        }

        [TestMethod]
        public void CheckAvailableVideoSpace()
        {
            var videoSpaceAvailable = GetCamera().AvailableVideoSpace();
            var videoSpace = GetCamera().ExtendedSettings().VideosAvailableSpace;
            Assert.AreEqual(videoSpace, videoSpaceAvailable.TotalSeconds);
        }

        [TestMethod]
        public void CheckBattery()
        {
            var battery=GetCamera().BatteryStatus();
            var batteryState = GetCamera().Settings().Battery;

            Assert.AreEqual(batteryState, battery);
            Assert.IsTrue(battery > 0 && battery <= 100);
        }

        [TestMethod]
        public void CheckName()
        {
            var name = GetCamera().GetName();
            Assert.AreEqual(ExpectedParameters.REAL_NAME, name);
        }

        [TestMethod]
        public void CheckBeep()
        {
            CheckMultipleChoice<BeepSound, CommandCameraBeepSound>(
                v => GetCamera().BeepSound(v).BeepSound()
                ,
                () => GetCamera().BeepSound()
                );
        }

        [TestMethod]
        public void CheckBootloaderVersion()
        {
            var version = GetCamera().BootLoader();
            var versionState = GetCamera().BacpacInformation().BootloaderVersion;
            Assert.AreEqual(versionState, version);
        }

        [TestMethod]
        public void CheckBurstRate()
        {
            CheckMultipleChoice<BurstRate, CommandCameraBurstRate>(
                v => GetCamera().BurstRate(v).BurstRate()
                ,
                () => GetCamera().BurstRate()
                );
        }

        [TestMethod]
        public void CheckContinuousShot()
        {
            CheckMultipleChoice<ContinuousShot, CommandCameraContinuousShot>(
                v => GetCamera().ContinuousShot(v).ContinuousShot()
                ,
                () => GetCamera().ContinuousShot()
                );
        }

        [TestMethod]
        public void CheckDefaultMode()
        {
            CheckMultipleChoice<Mode, CommandCameraDefaultMode>(
                v => GetCamera().DefaultModeOnPowerOn(v).DefaultModeOnPowerOn()
                ,
                () => GetCamera().DefaultModeOnPowerOn()
                );
        }

        [TestMethod]
        public void CheckShutter()
        {
            Mode initMode;
            Thread.Sleep(5000); //camera powerup cool down;

            bool shutterInit;
            bool shutterState;

            GetCamera().Chain(c => c.Mode(), out initMode).Mode(Mode.Video).Chain(c => c.Shutter(), out shutterInit).OpenShutter();
            Thread.Sleep(5000);

            GetCamera().Chain(c=>c.Shutter(),out shutterState);
            Assert.IsTrue(shutterState);

            GetCamera().CloseShutter();
            Thread.Sleep(5000);
            GetCamera().Chain(c => c.Shutter(), out shutterState);
            Assert.IsFalse(shutterState);

            shutterState = !shutterInit;
            GetCamera().Mode(initMode).Shutter(shutterInit).Chain(c => c.Shutter(), out shutterState);
            Assert.AreEqual(shutterInit, shutterState);
        }

        [TestMethod]
        public void CheckDeleteLastOnSdCard()
        {
            int videoCount;
            int photoCount;
            int afterDeleteVideoCount;
            int afterDeletePhotoCount;

            GetCamera().Chain(c => c.VideoCount(), out videoCount).Chain(c => c.PhotoCount(), out photoCount).DeleteLastFileOnSdCard();

            Thread.Sleep(5000);
            GetCamera().Chain(c => c.VideoCount(), out afterDeleteVideoCount).Chain(c => c.PhotoCount(), out afterDeletePhotoCount);

            Assert.IsTrue((afterDeletePhotoCount < photoCount || photoCount == 0) ||
                          (afterDeleteVideoCount < videoCount || videoCount == 0));
        }

        [TestMethod]
        public void CheckDeleteAllOnSdCard()
        {
            int videoCount;
            int photoCount;

            GetCamera().DeleteAllFilesOnSdCard();

            Thread.Sleep(5000);
            GetCamera().Chain(c=>c.VideoCount(),out videoCount).Chain(c=>c.PhotoCount(),out photoCount);

            Assert.IsTrue(photoCount == 0);
            Assert.IsTrue(videoCount == 0);
        }

        [TestMethod]
        public void CheckLivePreview()
        {
            bool livePreviewInit;
            bool livePreviewState;

            GetCamera().Chain(c=>c.LivePreview(),out livePreviewInit).DisableLivePreview().Chain(c=>c.LivePreview(),out livePreviewState);
            Assert.IsFalse(livePreviewState);

            GetCamera().EnableLivePreview().Chain(c=>c.LivePreview(),out livePreviewState);
            Assert.IsTrue(livePreviewState);

            livePreviewState = !livePreviewInit;
            GetCamera().LivePreview(livePreviewInit).Chain(c=>c.LivePreview(),out livePreviewState);
            Assert.AreEqual(livePreviewInit, livePreviewState);
        }

        [TestMethod]
        public void CheckLocate()
        {
            bool locateInit;
            bool locateState;

            GetCamera().Chain(c => c.Locate(), out locateInit).DisableLocate().Chain(c => c.Locate(), out locateState);
            Assert.IsFalse(locateState);

            GetCamera().EnableLocate().Chain(c=>c.Locate(),out locateState);
            Assert.IsTrue(locateState);

            locateState = !locateInit;
            GetCamera().Locate(locateInit).Chain(c=>c.Locate(),out locateState);
            Assert.AreEqual(locateInit, locateState);
        }

        [TestMethod]
        [Ignore]
        public void CheckLoopingVideo()
        {//only works for 1080 and 1440
            CheckMultipleChoice<LoopingVideo, CommandCameraLoopingVideo>(
                v =>
                    {
                        LoopingVideo loopingVideo;
                        GetCamera().LoopingVideo(v).Chain(c=>c.LoopingVideo(),out loopingVideo);
                        return loopingVideo;
                    }
                ,
                () =>
                    {
                        LoopingVideo loopingVideo;
                        GetCamera().Chain(c=>c.LoopingVideo(),out loopingVideo);
                        return loopingVideo;
                    }
                );
        }

        [TestMethod]
        public void CheckProtune()
        {
            bool protuneInit;
            bool protuneState;

            GetCamera().Chain(c=>c.Protune(),out protuneInit).DisableProtune().Chain(c => c.Protune(),out protuneState);
            Assert.IsFalse(protuneState);

            GetCamera().EnableProtune().Chain(c => c.Protune(),out protuneState);
            Assert.IsTrue(protuneState);

            protuneState = !protuneInit;
            GetCamera().Protune(protuneInit).Chain(c => c.Protune(),out protuneState);
            Assert.AreEqual(protuneInit, protuneState);
        }

        [TestMethod]
        public void CheckProtuneSupport()
        { 
            var model = GetCamera().BacpacStatus().CameraModel;
            var supports = GetCamera().SupportsProtune();

            Assert.IsTrue((model == Model.Hero3Black && supports) || (model == Model.Hero3Silver && supports) ||
                          (model == Model.Hero3White && !supports));
        }

        [TestMethod]
        public void CheckAutoLowLight()
        {
            bool autoLowLightInit;
            bool autoLowLightState;

            GetCamera().Chain(c => c.AutoLowLight(),out autoLowLightInit).DisableAutoLowLight().Chain(c => c.AutoLowLight(),out autoLowLightState);
            Assert.IsFalse(autoLowLightState);

            GetCamera().EnableAutoLowLight().Chain(c => c.AutoLowLight(),out autoLowLightState);
            Assert.IsTrue(autoLowLightState);

            autoLowLightState = !autoLowLightInit;
            GetCamera().AutoLowLight(autoLowLightInit).Chain(c => c.AutoLowLight(),out autoLowLightState);
            Assert.AreEqual(autoLowLightInit, autoLowLightState);
        }

        [TestMethod]
        public void CheckAutoLowLightSupport()
        {
            var model = GetCamera().BacpacStatus().CameraModel;
            var supports = GetCamera().SupportsAutoLowLight();

            Assert.IsTrue((model == Model.Hero3PlusBlack && supports) || (model == Model.Hero3PlusSilver && supports) ||
                          (model == Model.Hero3White && !supports) || (model == Model.Hero3Silver && !supports) || (model == Model.Hero3Black && !supports));
        }

        [TestMethod]
        public void CheckSpotMeter()
        {
            bool spotMeterInit;
            bool spotMeterState;

            GetCamera().Chain(c => c.SpotMeter(),out spotMeterInit).DisableSpotMeter().Chain(c => c.SpotMeter(),out spotMeterState);
            Assert.IsFalse(spotMeterState);

            GetCamera().EnableSpotMeter().Chain(c => c.SpotMeter(),out spotMeterState);
            Assert.IsTrue(spotMeterState);

            spotMeterState = !spotMeterInit;
            GetCamera().SpotMeter(spotMeterInit).Chain(c => c.SpotMeter(),out spotMeterState);
            Assert.AreEqual(spotMeterInit, spotMeterState);
        }

        [TestMethod]
        public void CheckFieldOfView()
        {
            FieldOfView fieldOfViewInit;
            FieldOfView fieldOfViewState;

            var validStates = GetCamera().Chain(c => c.FieldOfView(),out fieldOfViewInit).ValidFieldOfView();

            foreach (var fieldOfView in validStates)
            {
                GetCamera().FieldOfView(fieldOfView).Chain(c => c.FieldOfView(),out fieldOfViewState);
                Assert.AreEqual(fieldOfView, fieldOfViewState);
            }

            GetCamera().FieldOfView(fieldOfViewInit).Chain(c => c.FieldOfView(),out fieldOfViewState);
            Assert.AreEqual(fieldOfViewInit, fieldOfViewState);
        }

        [TestMethod]
        public void CheckFrameRate()
        {
            FrameRate frameRateInit;
            FrameRate frameRateState;

            var validStates = GetCamera().Chain(c=>c.FrameRate(),out frameRateInit).ValidFrameRate();

            foreach (var frameRate in validStates)
            {
                GetCamera().FrameRate(frameRate).Chain(c=>c.FrameRate(),out frameRateState);
                Assert.AreEqual(frameRate, frameRateState);
            }

            GetCamera().FrameRate(frameRateInit).Chain(c=>c.FrameRate(),out frameRateState);
            Assert.AreEqual(frameRateInit, frameRateState);
        }

        [TestMethod]
        public void CheckFullName()
        {
            string fullName;

            var fullnameState = GetCamera().Chain(c=>c.FullName(),out fullName).Information().Name;
            Assert.AreEqual(fullnameState, fullName);
        }

        [TestMethod]
        public void CheckIpAddress()
        {
            string ipAddress;

            GetCamera().Chain(c=>c.IpAddress(),out ipAddress);
            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, ipAddress);
        }

        [TestMethod]
        public void CheckLedBlink()
        {
            CheckMultipleChoice<LedBlink, CommandCameraLedBlink>(
                v =>
                    {
                        LedBlink ledBlink;
                        GetCamera().LedBlink(v).Chain(c=>c.LedBlink(),out ledBlink);
                        return ledBlink;
                    }
                ,
                () =>
                    {
                        LedBlink ledBlink;
                        GetCamera().Chain(c => c.LedBlink(),out ledBlink);
                        return ledBlink;
                    }
                );
        }

        [TestMethod]
        public void CheckModes()
        {
            CheckMultipleChoice<Mode, CommandCameraMode>(
                v =>
                    {
                        Mode mode;
                        GetCamera().Mode(v).Chain(c => c.Mode(),out mode);
                        return mode;
                    }
                ,
                () =>
                    {
                        Mode mode;
                        GetCamera().Chain(c => c.Mode(),out mode);
                        return mode;
                    }
                );
        }

        [TestMethod]
        public void CheckModel()
        {
            Model model;

            var modelState = GetCamera().Chain(c => c.Model(),out model).BacpacStatus().CameraModel;
            Assert.AreEqual(modelState, model);
        }

        [TestMethod]
        public void CheckOrientation()
        {
            CheckMultipleChoice<Orientation, CommandCameraOrientation>(
                v =>
                    {
                        Orientation orientation;
                        GetCamera().Orientation(v).Chain(c => c.Orientation(),out orientation);
                        return orientation;
                    }
                ,
                () =>
                    {
                        Orientation orientation;
                        GetCamera().Chain(c => c.Orientation(),out orientation);
                        return orientation;
                    }
                );
        }

        [TestMethod]
        public void CheckPassword()
        {
            string password;

            GetCamera().Chain(c => c.Password(),out password);
            Assert.AreEqual(ExpectedParameters.PASSWORD, password);
        }

        [TestMethod]
        public void CheckPhotoCount()
        {
            int count;

            var countState = GetCamera().Chain(c => c.PhotoCount(),out count).ExtendedSettings().PhotosCount;
            Assert.AreEqual(countState, count);
        }

        [TestMethod]
        public void CheckVideoCount()
        {
            int count;

            var countState = GetCamera().Chain(c => c.VideoCount(),out count).ExtendedSettings().VideosCount;
            Assert.AreEqual(countState, count);
        }


        [TestMethod]
        public void CheckVideoResolution()
        {
            VideoResolution videoResolutionInit;
            VideoResolution videoResolutionState;

            var validStates =
                GetCamera().Chain(c => c.VideoResolution(),out videoResolutionInit).ValidVideoResolution();

            foreach (var videoResolution in validStates)
            {
                GetCamera().VideoResolution(videoResolution).Chain(c => c.VideoResolution(),out videoResolutionState);
                Assert.AreEqual(videoResolution, videoResolutionState);
            }

            GetCamera().VideoResolution(videoResolutionInit).Chain(c => c.VideoResolution(),out videoResolutionState);
            Assert.AreEqual(videoResolutionInit, videoResolutionState);
        }

        [TestMethod]
        public void CheckPhotoResolution()
        {
            PhotoResolution photoResolutionInit;
            PhotoResolution photoResolutionState;

            var validStates =
                GetCamera().Chain(c => c.PhotoResolution(),out photoResolutionInit).ValidPhotoResolution();

            foreach (var photoResolution in validStates)
            {
                GetCamera().PhotoResolution(photoResolution).Chain(c => c.PhotoResolution(),out photoResolutionState);
                Assert.AreEqual(photoResolution, photoResolutionState);
            }

            GetCamera().PhotoResolution(photoResolutionInit).Chain(c => c.PhotoResolution(),out photoResolutionState);
            Assert.AreEqual(photoResolutionInit, photoResolutionState);
        }

        [TestMethod]
        public void CheckPhotoInVideo()
        {//only works for 1080 and 1440 30fps
            CheckMultipleChoice<PhotoInVideo, CommandCameraPhotoInVideo>(
                v =>
                {
                    PhotoInVideo photoInVideo;
                    GetCamera().PhotoInVideo(v).Chain(c => c.PhotoInVideo(),out photoInVideo);
                    return photoInVideo;
                }
                ,
                () =>
                {
                    PhotoInVideo photoInVideo;
                    GetCamera().Chain(c => c.PhotoInVideo(),out photoInVideo);
                    return photoInVideo;
                }
                );
        }

        [TestMethod]
        public void CheckPower()
        {
            bool powerInit;
            bool powerState;

            GetCamera().Chain(c => c.Power(),out powerInit).PowerOff();

            Thread.Sleep(5000);
            GetCamera().Chain(c => c.Power(),out powerState);
            Assert.IsFalse(powerState);

            GetCamera().PowerOn();
            Thread.Sleep(5000);
            GetCamera().Chain(c => c.Power(),out powerState);
            Assert.IsTrue(powerState);

            GetCamera().Power(powerInit);
            Thread.Sleep(5000);
            GetCamera().Chain(c => c.Power(),out powerState);
            Assert.AreEqual(powerInit, powerState);
        }

        [TestMethod]
        public void CheckSignalStrength()
        {
            SignalStrength signalStrength;

            var signalState = GetCamera().Chain(c => c.SignalStrength(),out signalStrength).BacpacStatus().Rssi;
            Assert.AreEqual(signalState, signalStrength);
        }

        [TestMethod]
        public void CheckSsid()
        {
            string ssid;

            var ssidState = GetCamera().Chain(c => c.Ssid(),out ssid).BacpacInformation().Ssid;
            Assert.AreEqual(ssidState, ssid);
        }

        [TestMethod]
        public void CheckTimeLapse()
        {
            CheckMultipleChoice<TimeLapse, CommandCameraTimeLapse>(
                v =>
                    {
                        TimeLapse timeLapse;
                        GetCamera().TimeLapse(v).Chain(c => c.TimeLapse(),out timeLapse);
                        return timeLapse;
                    }
                ,
                () =>
                    {
                        TimeLapse timeLapse;
                        GetCamera().Chain(c => c.TimeLapse(),out timeLapse);
                        return timeLapse;
                    }
                );
        }

        [TestMethod]
        public void CheckVersion()
        {
            string version;

            var versionState = GetCamera().Chain(c => c.Version(),out version).Information().Version;
            Assert.AreEqual(versionState, version);
        }

        [TestMethod]
        public void CheckWhiteBalance()
        {
            WhiteBalance whiteBalanceInit;
            WhiteBalance whiteBalanceState;

            var validStates = GetCamera().Chain(c => c.WhiteBalance(),out whiteBalanceInit).ValidWhiteBalance();

            foreach (var whiteBalance in validStates)
            {
                GetCamera().WhiteBalance(whiteBalance).Chain(c => c.WhiteBalance(),out whiteBalanceState);
                Assert.AreEqual(whiteBalance, whiteBalanceState);
            }

            GetCamera().WhiteBalance(whiteBalanceInit).Chain(c => c.WhiteBalance(),out whiteBalanceState);
            Assert.AreEqual(whiteBalanceInit, whiteBalanceState);
        }

        [TestMethod]
        public void CheckVideoStandard()
        {
            CheckMultipleChoice<VideoStandard, CommandCameraVideoStandard>(
                v =>
                    {
                        VideoStandard videoStandard;
                        GetCamera().VideoStandard(v).Chain(c => c.VideoStandard(),out videoStandard);
                        return videoStandard;
                    }
                ,
                () =>
                    {
                        VideoStandard videoStandard;
                        GetCamera().Chain(c => c.VideoStandard(),out videoStandard);
                        return videoStandard;
                    }
                );
        }

        [TestMethod]
        public void CheckFirmware()
        {
            Version version;
            var versionState = GetCamera().Chain(c => c.Firmware(),out version).BacpacInformation().FirmwareVersion;

            Assert.AreEqual(versionState, version);
        }

        [TestMethod]
        public void CheckMacAddress()
        {
            string macAddress;

            var bacPacMacAddress = GetCamera().Chain(c => c.MacAddress(),out macAddress).BacpacInformation().MacAddress;
            Assert.AreEqual(macAddress, bacPacMacAddress);
        }

        [TestMethod]
        public void CheckLivePreviewAvailable()
        {
            bool livePreviewAvailable;

            var livePreview =
                GetCamera().Chain(c => c.LivePreviewAvailable(),out livePreviewAvailable).ExtendedSettings().PreviewAvailable;
            Assert.AreEqual(livePreview, livePreviewAvailable);
        }
    }
}