using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [TestMethod]
        public void CheckHero3CameraInitialize()
        {
            var camera = GetCamera();
        }

        [TestMethod]
        public void CheckBattery()
        {
            byte battery;
            GetCamera().BatteryStatus(out battery);

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
        public void Check()
        {
            string ipAddress;
            GetCamera().IpAddress(out ipAddress);

            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, ipAddress);
        }
    }
}
