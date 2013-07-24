using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using GoPro.Hero.Api.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HtmlTidy = Tidy.Core.Tidy;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class AmbrellaBrowserTests
    {
        private delegate IEnumerable<Node> ParseDelegate(XElement element);

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
            var tidy = new HtmlTidy();
            var element = tidy.ParseXml(mainPageStream);

            var nodes = _parseDelegate(element).ToArray();
            
            Assert.AreEqual(nodes.Length,5);
            Assert.AreEqual(nodes[0].Name,"DCIM");
            Assert.AreEqual(nodes[1].Name,"live");
            Assert.AreEqual(nodes[2].Name,"mjpeg");
            Assert.AreEqual(nodes[3].Name,"pref");
            Assert.AreEqual(nodes[4].Name,"shutter");
            
        }

        [TestMethod]
        public void CheckEmptyDcimPage()
        {
            var mainPageStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Api.Tests.Resources.dcim.htm");
            var tidy = new HtmlTidy();
            var element = tidy.ParseXml(mainPageStream);

            var nodes = _parseDelegate(element).ToArray();

            Assert.AreEqual(nodes.Length, 0);
        }

        [TestMethod]
        public void CheckLivePage()
        {
            var mainPageStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Api.Tests.Resources.live.htm");
            var tidy = new HtmlTidy();
            var element = tidy.ParseXml(mainPageStream);

            var nodes = _parseDelegate(element).ToArray();

            Assert.AreEqual(nodes.Length, 0);
        }
    }
}