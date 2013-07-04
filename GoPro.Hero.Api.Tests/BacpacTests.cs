using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class BacpacTests
    {
        [TestMethod]
        public void Initialize()
        {
            var bacpac=Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            
            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, bacpac.Address);
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }

        [TestMethod]
        public void RetrievePassword()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);

            bacpac.UpdatePassword();
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }

        [TestMethod]
        public void PowerUp()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);

            bacpac.Power(true);
            Thread.Sleep(2000);
            var res = bacpac.Status.CameraPower;
            Assert.AreEqual(true, res);

            bacpac.Power(false);
            Thread.Sleep(2000);
            res = bacpac.Status.CameraPower;
            Assert.AreEqual(false, res);
        }
    }
}
