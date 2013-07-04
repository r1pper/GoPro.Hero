using System;
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

            var res = bacpac.Power(true).Status.CameraPower;
            Assert.AreEqual(true, res);

            res = bacpac.Power(false).Status.CameraPower;
            Assert.AreEqual(false, res);
        }
    }
}
