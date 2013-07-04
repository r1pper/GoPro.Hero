using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Commands.CameraCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class CameraTests
    {
        [TestMethod]
        public void Initialize()
        {
            var bacpac=Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create<Camera>(bacpac);

            camera.Power(true);
            Thread.Sleep(2000);
            var res = camera.BacpacStatus.CameraPower;
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void LocateCamera()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create<Camera>(bacpac);

            var command = camera.PrepareCommand<CommandCameraLocate>();

            command.Enable = true; 
            var res=camera.Command(command).ExtendedSettings.LocateCamera;
            Assert.AreEqual(true, res);

            command.Enable = false;
            res = camera.Command(command).ExtendedSettings.LocateCamera;
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void ChangeMode()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create<Camera>(bacpac);

            ChangeMode(camera, Mode.Video);
            ChangeMode(camera, Mode.TimeLapse);
            ChangeMode(camera, Mode.Photo);
            ChangeMode(camera, Mode.Burst);
        }

        private static void ChangeMode(Camera camera, Mode mode)
        {
            var command = camera.PrepareCommand<CommandCameraMode>();
            command.Select = mode;

            var res = camera.Command(command).ExtendedSettings.Mode;
            Assert.AreEqual(mode, res);
        }
    }
}
