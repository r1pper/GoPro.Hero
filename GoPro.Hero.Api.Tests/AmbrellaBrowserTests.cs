using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using GoPro.Hero.Api.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class AmbrellaBrowserTests
    {
        private AmbrellaBrowser _instance;
        private ParseDelegate _parseDelegate;

        [TestInitialize]
        public void Initialize()
        {
            var ambrellaType = typeof (AmbrellaBrowser);
            _instance = Activator.CreateInstance<AmbrellaBrowser>();
            _instance.Initialize(null, null);

            var method = ambrellaType.GetMethod("Parse", BindingFlags.Instance | BindingFlags.NonPublic);
            _parseDelegate = (ParseDelegate) method.CreateDelegate(typeof (ParseDelegate), _instance);
        }

        [TestMethod]
        public void CheckMainPage()
        {
            var mainPageStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Api.Tests.Resources.main.htm");
            var element = XElement.Load(mainPageStream);

            var res = _parseDelegate(element);
        }

        private delegate IEnumerable<Node> ParseDelegate(XElement element);
    }
}