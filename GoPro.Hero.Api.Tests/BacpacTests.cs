using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class BacpacTests
    {
        private Bacpac GetBacpac()
        {
            return Bacpac.Create(ExpectedParameters.IP_ADDRESS);
        }

        [TestInitialize]
        public void BacpacInitialize()
        {
            var bacpac= GetBacpac();
            
            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, bacpac.Address);
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }

        [TestMethod]
        public void CheckPassword()
        {
            var bacpac = GetBacpac();

            bacpac.UpdatePassword();
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }

        [TestMethod]
        public void CheckPowerUp()
        {
            var bacpac = GetBacpac();

            bacpac.Power(true);
            Thread.Sleep(2000);
            var res = bacpac.Status.CameraPower;
            Assert.AreEqual(true, res);

            bacpac.Power(false);
            Thread.Sleep(2000);
            res = bacpac.Status.CameraPower;
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void CheckInformation()
        {
            var bacpac = GetBacpac();
            //bacpac.Information.MacAddress;
        }

        [TestMethod]
        public void CheckStatus()
        {
            var bacpac = GetBacpac();
            var status = bacpac.Status;

            Assert.IsTrue(status.CameraAttached);
        }
    }
}
