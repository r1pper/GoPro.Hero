using System;
using System.Collections.Generic;
using System.Threading;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using GoPro.Hero.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Tests
{
    [TestClass]
    public class CameraTests
    {
        private Camera GetCamera()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create<Camera>(bacpac);

            return camera;
        }

        private void ChangeSelection<T, TS>(Camera camera, TS select, Func<ICamera, TS> valueRetriever)
            where T : CommandMultiChoice<TS, ICamera>
        {
            var command = camera.PrepareCommand<T>();
            command.Selection = select;

            var res = valueRetriever(camera.Command(command));
            Assert.AreEqual(select, res);
        }

        public void CheckMultiChoiceCommand<T, TS>(Func<ICamera, TS> valueRetriever)
            where T : CommandMultiChoice<TS, ICamera>
        {
            var camera = GetCamera();
            var init = valueRetriever(camera);

            var selection = Enum.GetValues(typeof (TS));

            foreach (var selected in selection)
            {
                var value = (TS) Convert.ChangeType(selected, typeof (TS));
                ChangeSelection<T, TS>(camera, value, valueRetriever);
            }

            ChangeSelection<T, TS>(camera, init, valueRetriever);
        }

        private void CheckBooleanCommand<T>(Func<ICamera, bool> valueRetriever) where T : CommandBoolean<ICamera>
        {
            var camera = GetCamera();
            var init = valueRetriever(camera);

            var command = camera.PrepareCommand<T>();

            command.State = true;
            var res = valueRetriever(camera.Command(command));
            Assert.AreEqual(true, res);

            command.State = false;
            res = valueRetriever(camera.Command(command));
            Assert.AreEqual(false, res);

            command.State = init;
            camera.Command(command);
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
        public void CheckSetDateTime()
        {
            Assert.Inconclusive("cannot check it");
            var camera = GetCamera();
            camera.PrepareCommand<CommandCameraSetTime>().Set(DateTime.Now).Execute();
        }

        [TestMethod]
        public void CheckGetName()
        {
            var camera = GetCamera();

            var name = camera.GetName();
            Assert.AreEqual(ExpectedParameters.REAL_NAME, name);
        }

        [TestMethod]
        public void CheckSetName()
        {
            var camera = GetCamera();
            var init = camera.GetName();

            camera.SetName(ExpectedParameters.TEMP_NAME);
            var tempName = camera.GetName();
            Assert.AreEqual(ExpectedParameters.TEMP_NAME, tempName);

            camera.SetName(init);
            Assert.AreEqual(init, camera.GetName());
        }

        [TestMethod]
        public void CheckVideoResolution()
        {
            CheckMultiChoiceCommand<CommandCameraVideoResolution, VideoResolution>(
                c => c.ExtendedSettings().VideoResolution);
        }

        [TestMethod]
        public void CheckOrientation()
        {
            CheckMultiChoiceCommand<CommandCameraOrientation, Orientation>(c => c.ExtendedSettings().Orientation);
        }

        [TestMethod]
        public void CheckTimeLapse()
        {
            CheckMultiChoiceCommand<CommandCameraTimeLapse, TimeLapse>(c => c.ExtendedSettings().TimeLapse);
        }

        [TestMethod]
        public void CheckBeepSound()
        {
            CheckMultiChoiceCommand<CommandCameraBeepSound, BeepSound>(c => c.ExtendedSettings().BeepSound);
        }

        [TestMethod]
        public void CheckProtune()
        {
            CheckBooleanCommand<CommandCameraProtune>(c => c.ExtendedSettings().Protune);
        }

        [TestMethod]
        public void CheckPhotoResolution()
        {
            Assert.Inconclusive("needs specific camera filtering");

            CheckMultiChoiceCommand<CommandCameraPhotoResolution, PhotoResolution>(
                c => c.ExtendedSettings().PhotoResolution);
        }

        [TestMethod]
        public void CheckVideoStandard()
        {
            CheckMultiChoiceCommand<CommandCameraVideoStandard, VideoStandard>(c => c.ExtendedSettings().VideoStandard);
        }

        [TestMethod]
        public void CheckModes()
        {
            CheckMultiChoiceCommand<CommandCameraMode, Mode>(c => c.ExtendedSettings().Mode);
        }

        [TestMethod]
        public void LocateCamera()
        {
            CheckBooleanCommand<CommandCameraLocate>(c => c.ExtendedSettings().LocateCamera);
        }

        [TestMethod]
        public void CheckPreview()
        {
            var camera = GetCamera();
            var available = camera.ExtendedSettings().PreviewAvailable;
            Assert.AreEqual(true, available);

            CheckBooleanCommand<CommandCameraPreview>(c => c.ExtendedSettings().PreviewActive);
        }

        [TestMethod]
        public void CheckLedBlinks()
        {
            CheckMultiChoiceCommand<CommandCameraLedBlink, LedBlink>(c => c.ExtendedSettings().LedBlink);
        }

        [TestMethod]
        public void CheckFieldOfView()
        {
            var camera = GetCamera();
            var initResolution = camera.ExtendedSettings().VideoResolution;
            var currentResolution =
                camera.PrepareCommand<CommandCameraVideoResolution>()
                      .Select(VideoResolution.Vr1080)
                      .Execute()
                      .ExtendedSettings().VideoResolution;
            Assert.AreEqual(VideoResolution.Vr1080, currentResolution);

            CheckMultiChoiceCommand<CommandCameraFieldOfView, FieldOfView>(c => c.ExtendedSettings().FieldOfView);

            camera.PrepareCommand<CommandCameraVideoResolution>().Select(initResolution).Execute();
        }

        [TestMethod]
        public void CheckSpotMeter()
        {
            CheckBooleanCommand<CommandCameraSpotMeter>(c => c.ExtendedSettings().SpotMeter);
        }

        [TestMethod]
        public void CheckOnDefaultMode()
        {
            CheckMultiChoiceCommand<CommandCameraDefaultMode, Mode>(c => c.ExtendedSettings().OnDefault);
        }

        [TestMethod]
        public void CheckDeleteAllOnSdCard()
        {
            var camera = GetCamera();

            camera.PrepareCommand<CommandCameraDeleteAllFilesOnSd>().Execute();
            Thread.Sleep(5000);
            var info = camera.ExtendedSettings();
            Assert.AreEqual(0, info.PhotosCount);
            Assert.AreEqual(0, info.VideosCount);
        }

        [TestMethod]
        public void CheckDeleteLastOnSdCard()
        {
            var camera = GetCamera();
            var initPhoto = camera.ExtendedSettings().PhotosCount;
            var initVideo = camera.ExtendedSettings().VideosCount;

            var photo =
                camera.PrepareCommand<CommandCameraDeleteLastFileOnSd>().Execute().ExtendedSettings().PhotosCount;
            var video = camera.ExtendedSettings().VideosCount;

            Assert.IsTrue(photo <= initPhoto || video <= initVideo);
        }

        [TestMethod]
        public void CheckCameraInformation()
        {
            var camera = GetCamera();
            var info = camera.Information();

            var trimmedName = info.Name.Fix();
            Assert.AreEqual(ExpectedParameters.REAL_NAME, trimmedName);
        }

        [TestMethod]
        public void CheckCameraSettings()
        {
            var camera = GetCamera();
            var settings = camera.Settings();

            Assert.AreEqual(ExpectedParameters.DEFAULT_VIDEO, settings.VideoStandard);
        }

        [TestMethod]
        public void CheckCameraExtendedSettings()
        {
            var camera = GetCamera();
            var extendedSettings = camera.ExtendedSettings();

            Assert.AreEqual(ExpectedParameters.DEFAULT_VIDEO, extendedSettings.VideoStandard);
        }

        [TestMethod]
        public void CheckWhiteBalance()
        {
            var camera = GetCamera();

            var protuneInit = camera.ExtendedSettings().Protune;
            camera.PrepareCommand<CommandCameraProtune>().Set(true).Execute();

            var protune = camera.ExtendedSettings().Protune;
            Assert.AreEqual(protune, true);

            CheckMultiChoiceCommand<CommandCameraWhiteBalance, WhiteBalance>(c => c.ExtendedSettings().WhiteBalance);

            camera.PrepareCommand<CommandCameraProtune>().Set(protuneInit).Execute();
        }

        [TestMethod]
        public void CheckLoopingVideo()
        {
            var camera = GetCamera();

            var protuneInit = camera.ExtendedSettings().Protune;
            camera.PrepareCommand<CommandCameraProtune>().Set(false).Execute();

            var protune = camera.ExtendedSettings().Protune;
            Assert.AreEqual(protune, false);

            CheckMultiChoiceCommand<CommandCameraLoopingVideo, LoopingVideo>(c => c.ExtendedSettings().LoopingVideoMode);

            camera.PrepareCommand<CommandCameraProtune>().Set(protuneInit).Execute();
        }


        [TestMethod]
        public void CheckFrameRate()
        {
            var camera = GetCamera();
            var initResolution = camera.ExtendedSettings().VideoResolution;
            var initVideoStandard = camera.ExtendedSettings().VideoStandard;

            var currentResolution =
                camera.PrepareCommand<CommandCameraVideoResolution>()
                      .Select(VideoResolution.Vr1080)
                      .Execute()
                      .ExtendedSettings().VideoResolution;
            Assert.AreEqual(VideoResolution.Vr1080, currentResolution);

            var currentStandard =
                camera.PrepareCommand<CommandCameraVideoStandard>()
                      .Select(VideoStandard.Ntsc)
                      .Execute()
                      .ExtendedSettings().VideoStandard;
            Assert.AreEqual(VideoStandard.Ntsc, currentStandard);

            var availableFrameRates = new[] {FrameRate.Fps24, FrameRate.Fps30, FrameRate.Fps48, FrameRate.Fps60};
            var command = camera.PrepareCommand<CommandCameraFrameRate>();
            foreach (var frameRate in availableFrameRates)
            {
                var currentFrameRate = command.Select(frameRate).Execute().ExtendedSettings().FrameRate;
                Assert.AreEqual(frameRate, currentFrameRate);
            }

            camera
                .PrepareCommand<CommandCameraVideoStandard>().Select(initVideoStandard).Execute()
                .PrepareCommand<CommandCameraVideoResolution>().Select(initResolution).Execute();
        }

        [TestMethod]
        public void CheckBurstRate()
        {
            CheckMultiChoiceCommand<CommandCameraBurstRate, BurstRate>(c => c.ExtendedSettings().BurstRate);
        }

        [TestMethod]
        public void CheckContinuousShot()
        {
            CheckMultiChoiceCommand<CommandCameraContinuousShot, ContinuousShot>(
                c => c.ExtendedSettings().ContinuousShot);
        }

        [TestMethod]
        public void CheckSetFilter()
        {
            var camera = GetCamera();
            var testFilter = new FilterTest();
            camera.SetFilter(testFilter);
        }

        [TestMethod]
        public void CheckBattery()
        {
            var camera = GetCamera();
            var batteryCamera = camera.Settings().Battery;
            Assert.IsTrue(batteryCamera>0);
            Assert.IsTrue(batteryCamera<=100);
        }

        private class FilterTest : IFilter<ICamera>
        {
            public void Initialize(ICamera owner)
            {
                if (owner == null)
                    Assert.Fail("Owner is Null");
            }

            public IEnumerable<T> GetValidStates<T, TC>(string command) where TC : CommandMultiChoice<T, ICamera>
            {
                throw new NotImplementedException();
            }

            public IEnumerable<bool> GetValidStates<TC>(string command) where TC : CommandBoolean<ICamera>
            {
                throw new NotImplementedException();
            }
        }
    }
}