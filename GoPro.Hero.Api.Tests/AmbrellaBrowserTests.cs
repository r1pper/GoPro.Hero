using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using GoPro.Hero.Api.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HtmlTidy = Tidy.Core.Tidy;
using GoPro.Hero.Api.Hero3;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api.Tests
{
    [TestClass]
    public class AmbrellaBrowserTests
    {
        private delegate IEnumerable<Node> ParseDelegate(XElement element, Node node);

        private AmbrellaBrowser _instance;
        private ParseDelegate _parseDelegate;

        private  Node GetMainNode()
        {
            var camera = Camera.Create<Hero3Camera>(ExpectedParameters.IP_ADDRESS);
            var mainNode = camera.Browse();
            return mainNode;
        }

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

            var nodes = _parseDelegate(element,null).ToArray();
            
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

            var nodes = _parseDelegate(element,null).ToArray();

            Assert.AreEqual(nodes.Length, 0);
        }

        [TestMethod]
        public void CheckLivePage()
        {
            var mainPageStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Api.Tests.Resources.live.htm");
            var tidy = new HtmlTidy();
            var element = tidy.ParseXml(mainPageStream);

            var nodes = _parseDelegate(element,null).ToArray();

            Assert.AreEqual(nodes[0].Name, "aaba.m3u8");
            Assert.AreEqual(nodes[1].Name, "amba.mp4");
            Assert.AreEqual(nodes[2].Name, "amba.m3u8");
            Assert.AreEqual(nodes[3].Name, "amba_hls-1.ts");
            Assert.AreEqual(nodes[4].Name, "amba_hls-2.ts");
            Assert.AreEqual(nodes[5].Name, "amba_hls-3.ts");
            Assert.AreEqual(nodes[6].Name, "amba_hls-4.ts");
            Assert.AreEqual(nodes[7].Name, "amba_hls-5.ts");
            Assert.AreEqual(nodes[8].Name, "amba_hls-6.ts");
            Assert.AreEqual(nodes[9].Name, "amba_hls-7.ts");
            Assert.AreEqual(nodes[10].Name, "amba_hls-8.ts");
            Assert.AreEqual(nodes[11].Name, "amba_hls-9.ts");
            Assert.AreEqual(nodes[12].Name, "amba_hls-10.ts");
            Assert.AreEqual(nodes[13].Name, "amba_hls-11.ts");
            Assert.AreEqual(nodes[14].Name, "amba_hls-12.ts");
            Assert.AreEqual(nodes[15].Name, "amba_hls-13.ts");
            Assert.AreEqual(nodes[16].Name, "amba_hls-14.ts");
            Assert.AreEqual(nodes[17].Name, "amba_hls-15.ts");
            Assert.AreEqual(nodes[18].Name, "amba_hls-16.ts");
            Assert.AreEqual(nodes[19].Name, "precap-1.ts");
            Assert.AreEqual(nodes[20].Name, "precap-2.ts");
            Assert.AreEqual(nodes[21].Name, "precap-3.ts");
            Assert.AreEqual(nodes[22].Name, "precap-4.ts");
            Assert.AreEqual(nodes[23].Name, "precap-5.ts");
            Assert.AreEqual(nodes[24].Name, "precap-6.ts");
            Assert.AreEqual(nodes[25].Name, "precap-7.ts");
            Assert.AreEqual(nodes[26].Name, "precap-8.ts");

        }

        [TestMethod]
        public void CheckCameraMainPage()
        {
            var mainNode = GetMainNode();

            var nodes=mainNode.Nodes().ToArray();

            Assert.AreEqual(nodes.Length, 5);
            Assert.AreEqual(nodes[0].Name, "DCIM");
            Assert.AreEqual(nodes[1].Name, "live");
            Assert.AreEqual(nodes[2].Name, "mjpeg");
            Assert.AreEqual(nodes[3].Name, "pref");
            Assert.AreEqual(nodes[4].Name, "shutter");
        }

        [TestMethod]
        public void CheckCameraDownload()
        {
            var mainNode = GetMainNode();

            var m3U8 = mainNode["live"]["amba.m3u8"];
            var webResponse=m3U8.DownloadContent();
            var parser = new M3U8Parser(webResponse.GetResponseStream());
            webResponse.Dispose();
            Assert.AreEqual("3",parser.Version);
        }
    }
}