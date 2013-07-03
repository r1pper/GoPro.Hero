using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var camera = Camera.Create(bacpac);
        }

        [TestMethod]
        public void LocateCamera()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create(bacpac);
            camera.LocateCamera(true);
            var res = camera.UpdateExtendedSettings().ExtendedSettings.LocateCamera;
            //Assert.AreEqual(true, res);
            camera.LocateCamera(false);
            res = camera.UpdateExtendedSettings().ExtendedSettings.LocateCamera;
            //Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void ChangeMode()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = Camera.Create(bacpac);
            camera.SetMode(Mode.Video);
        }
    }
}
