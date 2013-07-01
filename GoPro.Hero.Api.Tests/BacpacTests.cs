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
            var bacpac=Bacpac.Create("10.5.5.9");

            Assert.AreEqual(ExpectedParameters.IP_ADDRESS, bacpac.Address);
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }

        [TestMethod]
        public void RetrievePassword()
        {
            var bacpac=Bacpac.Create("10.5.5.9");

            bacpac.UpdatePassword();
            Assert.AreEqual(ExpectedParameters.PASSWORD, bacpac.Password);
        }
    }
}
