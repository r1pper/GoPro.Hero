using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoPro.Hero.Api.Commands.CameraCommands;
using GoPro.Hero.Api.Hero3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class Hero3CameraTests
    {
        private Hero3Camera GetCamera()
        {
            var camera = Camera.Create<Hero3Camera>(ExpectedParameters.IP_ADDRESS);
            return camera;
        }

        private void CheckMultipleChoice<T,C>(Func<T,T> checkDelegate, Func<T> getInit)where C:Commands.CommandMultiChoice<T,ICamera>
        {
            var availableValues = GetCamera().PrepareCommand<C>().ValidStates(); //Enum.GetValues(typeof(T));
            var init = getInit();

            foreach (T value in availableValues)
                Assert.AreEqual(value, checkDelegate(value));

            Assert.AreEqual(init, checkDelegate(init));
        }

        [TestInitialize]
        public void InitializeCamera()
        {
            var camera = GetCamera();

            var init = camera.BacpacStatus.CameraPower;
            if (init) return;

            camera.Power(true);
            Thread.Sleep(5000);
            var res = camera.BacpacStatus.CameraPower;
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void CheckHero3CameraInitialize()
        {
            var camera = GetCamera();
        }

        [TestMethod]
        public void CheckAvailablePhotoSpace()
        {
            int photoSpaceAvailable;

            var photoSpace = GetCamera().AvailablePhotoSpace(out photoSpaceAvailable).ExtendedSettings.PhotosAvailableSpace;
            Assert.AreEqual(photoSpace, photoSpaceAvailable);
        }

        [TestMethod]
        public void CheckAvailableVideoSpace()
        {
            TimeSpan videoSpaceAvailable;

            var videoSpace = GetCamera().AvailableVideoSpace(out videoSpaceAvailable).ExtendedSettings.VideosAvailableSpace;
            Assert.AreEqual(videoSpace, videoSpaceAvailable);
        }

        [TestMethod]
        public void CheckBattery()
        {
            byte battery;
            byte batteryState=GetCamera().BatteryStatus(out battery).Settings.Battery;

            Assert.AreEqual(batteryState, battery);
            Assert.IsTrue(battery > 0 && battery <= 100);
        }

        [TestMethod]
        public void CheckName()
        {
            string name;
            GetCamera().GetName(out name);

            var name2 = GetCamera().GetName();

            Assert.AreEqual(name, name2);
            Assert.AreEqual(ExpectedParameters.REAL_NAME, name);
        }

        [TestMethod]
        public void CheckIpAddress()
        {
            string ipAddress;
            GetCamera().IpAddress(out ipAddress);

            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, ipAddress);
        }

        [TestMethod]
        public void CheckBeep()
        {
            CheckMultipleChoice<BeepSound,CommandCameraBeepSound>(
                (v) =>
                {
                    BeepSound beepSound;
                    GetCamera().BeepSound(v).BeepSound(out beepSound);
                    return beepSound;
                }
            ,
                () =>
                {
                    BeepSound beepSound;
                    GetCamera().BeepSound(out beepSound);
                    return beepSound;
                }
            );
        }

        [TestMethod]
        public void CheckBootloaderVersion()
        {
            Version version;
            GetCamera().BootLoader(out version);
            Assert.AreEqual(ExpectedParameters.VERSION, version.ToString());
        }

        [TestMethod]
        public void CheckBurstRate()
        {
            CheckMultipleChoice<BurstRate,CommandCameraBurstRate>(
                (v) =>
                {
                    BurstRate burstRate;
                    GetCamera().BurstRate(v).BurstRate(out burstRate);
                    return burstRate;
                }
            ,
                () =>
                {
                    BurstRate burstRate;
                    GetCamera().BurstRate(out burstRate);
                    return burstRate;
                }
            );
        }

        [TestMethod]
        public void CheckContinuousShot()
        {
            CheckMultipleChoice<ContinuousShot,CommandCameraContinuousShot>(
                (v) =>
                {
                    ContinuousShot continuousShot;
                    GetCamera().ContinuousShot(v).ContinuousShot(out continuousShot);
                    return continuousShot;
                }
            ,
                () =>
                {
                    ContinuousShot continuousShot;
                    GetCamera().ContinuousShot(out continuousShot);
                    return continuousShot;
                }
            );
        }

        [TestMethod]
        public void CheckDefaultMode()
        {
            CheckMultipleChoice<Mode,CommandCameraDefaultMode>(
                (v) =>
                {
                    Mode mode;
                    GetCamera().DefaultModeOnPowerOn(v).DefaultModeOnPowerOn(out mode);
                    return mode;
                }
            ,
                () =>
                {
                    Mode mode;
                    GetCamera().DefaultModeOnPowerOn(out mode);
                    return mode;
                }
            );
        }

        [TestMethod]
        public void CheckShutter()
        {
            bool shutterInit;
            bool shutterState;

            GetCamera().Shutter(out shutterInit).OpenShutter().Shutter(out shutterState);
            Assert.IsTrue(shutterState);

            GetCamera().CloseShutter().Shutter(out shutterState);
            Assert.IsFalse(shutterState);

            shutterState = !shutterInit;
            GetCamera().Shutter(shutterInit).Shutter(out shutterState);
            Assert.AreEqual(shutterInit, shutterState);
        }

        [TestMethod]
        public void CheckDeleteLastOnSdCard()
        {
            int videoCount;
            int photoCount;
            int afterDeleteVideoCount;
            int afterDeletePhotoCount;

            GetCamera().VideoCount(out videoCount).PhotoCount(out photoCount)
                .DeleteLastFileOnSdCard()
                .VideoCount(out afterDeleteVideoCount).PhotoCount(out afterDeletePhotoCount);

            Assert.IsTrue(afterDeletePhotoCount <= photoCount);
            Assert.IsTrue(afterDeleteVideoCount <= videoCount);
        }

        [TestMethod]
        public void CheckDeleteAllOnSdCard()
        {
            int videoCount;
            int photoCount;

            GetCamera().DeleteLastFileOnSdCard()
                .VideoCount(out videoCount).PhotoCount(out photoCount);

            Assert.IsTrue(photoCount == 0);
            Assert.IsTrue(videoCount == 0);
        }

        [TestMethod]
        public void CheckLivePreview()
        {
            bool livePreviewInit;
            bool livePreviewState;

            GetCamera().LivePreview(out livePreviewInit).DisableLivePreview().LivePreview(out livePreviewState);
            Assert.IsFalse(livePreviewState);

            GetCamera().EnableLivePreview().LivePreview(out livePreviewState);
            Assert.IsTrue(livePreviewState);

            livePreviewState = !livePreviewInit;
            GetCamera().LivePreview(livePreviewInit).LivePreview(out livePreviewState);
            Assert.AreEqual(livePreviewInit, livePreviewState);
        }

        [TestMethod]
        public void CheckLocate()
        {
            bool locateInit;
            bool locateState;

            GetCamera().Locate(out locateInit).DisableLocate().Locate(out locateState);
            Assert.IsFalse(locateState);

            GetCamera().EnableLocate().Locate(out locateState);
            Assert.IsTrue(locateState);

            locateState = !locateInit;
            GetCamera().Locate(locateInit).Locate(out locateState);
            Assert.AreEqual(locateInit, locateState);
        }

        [TestMethod]
        [Ignore]
        public void CheckLoopingVideo()
        {
            CheckMultipleChoice<LoopingVideo,CommandCameraLoopingVideo>(
                (v) =>
                {
                    LoopingVideo loopingVideo;
                    GetCamera().LoopingVideo(v).LoopingVideo(out loopingVideo);
                    return loopingVideo;
                }
            ,
                () =>
                {
                    LoopingVideo loopingVideo;
                    GetCamera().LoopingVideo(out loopingVideo);
                    return loopingVideo;
                }
            );
        }

        [TestMethod]
        public void CheckProtune()
        {
            bool protuneInit;
            bool protuneState;

            GetCamera().Protune(out protuneInit).DisableProtune().Protune(out protuneState);
            Assert.IsFalse(protuneState);

            GetCamera().EnableProtune().Protune(out protuneState);
            Assert.IsTrue(protuneState);

            protuneState = !protuneInit;
            GetCamera().Protune(protuneInit).Protune(out protuneState);
            Assert.AreEqual(protuneInit, protuneState);
        }


        [TestMethod]
        public void CheckSpotMeter()
        {
            bool spotMeterInit;
            bool spotMeterState;

            GetCamera().SpotMeter(out spotMeterInit).DisableSpotMeter().SpotMeter(out spotMeterState);
            Assert.IsFalse(spotMeterState);

            GetCamera().EnableSpotMeter().SpotMeter(out spotMeterState);
            Assert.IsTrue(spotMeterState);

            spotMeterState = !spotMeterInit;
            GetCamera().SpotMeter(spotMeterInit).SpotMeter(out spotMeterState);
            Assert.AreEqual(spotMeterInit, spotMeterState);
        }

        [TestMethod]
        public void CheckMacAddress()
        {
            string macAddress;

            var bacPacMacAddress=GetCamera().MacAddress(out macAddress).BacpacInformation.MacAddress;
            Assert.AreEqual(macAddress, bacPacMacAddress);
        }

        [TestMethod]
        public void CheckLivePreviewAvailable()
        {
            bool livePreviewAvailable;

            var livePreview = GetCamera().LivePreviewAvailable(out livePreviewAvailable).ExtendedSettings.PreviewAvailable;
            Assert.AreEqual(livePreview, livePreviewAvailable);
        }
    }
}
