using System.IO;
using System.Linq;
using System.Threading;
using GoPro.Hero.Browser.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoPro.Hero.Hero3;

namespace GoPro.Hero.Tests
{
    [TestClass]
    public class AmbarellaMediaBrowserTests
    {
        private LegacyCamera GetCamera()
        {
            var bacpac = Bacpac.Create(ExpectedParameters.IP_ADDRESS);
            var camera = LegacyCamera.Create<LegacyCamera>(bacpac);

            return camera;
        }

        [TestInitialize]
        public void InitializeCamera()
        {
            var camera = GetCamera();

            var init = camera.BacpacStatus().CameraPower;
            if (init) return;

            camera.Power(true);
            Thread.Sleep(5000);
            var res = camera.BacpacStatus().CameraPower;
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void CheckContents()
        {
            var camera = GetCamera();
            var browser = camera.Browse<AmbarellaMediaBrowser>();
           
            var contents=browser.Contents();
        }

        [TestMethod]
        public void CheckTimeLapses()
        {
            var camera = GetCamera();
            var browser = camera.Browse<AmbarellaMediaBrowser>();

            var contents = browser.TimeLapsesAsync().Result;

            var timeLapse = contents.FirstOrDefault();

            if (timeLapse == null)
                Assert.Inconclusive("no timelapsed image found");
        }

        [TestMethod]
        public void CheckDownloadImage()
        {
            var camera = GetCamera();
            var image = camera.Browse<AmbarellaMediaBrowser>().ImagesAsync().Result.FirstOrDefault();
            if (image == null)
                Assert.Inconclusive("no image found");

            var response = image.DownloadAsync().Result.GetResponseStream();

            var memory = ReadToMemory(response);

            Assert.AreEqual(AsMegabytes(memory.Length),AsMegabytes(image.Size));
        }

        [TestMethod]
        public void CheckImageThumbnail()
        {
            var camera = GetCamera();
            var image = camera.Browse<AmbarellaMediaBrowser>().ImagesAsync().Result.FirstOrDefault();
            if (image == null)
            {
                Assert.Inconclusive("no image found");
                return;
            }

            var thumbnail = image.ThumbnailAsync().Result;
            var memory = ReadToMemory(thumbnail);

            Assert.IsTrue(memory.Length>1024);
        }

        [TestMethod]
        public void CheckImageBigThumbnail()
        {
            var camera = GetCamera();
            var image = camera.Browse<AmbarellaMediaBrowser>().ImagesAsync().Result.FirstOrDefault();
            if (image == null)
            {
                Assert.Inconclusive("no image found");
                return;
            }

            var thumbnail = image.ThumbnailAsync().Result;
            var memory = ReadToMemory(thumbnail);

            Assert.IsTrue(memory.Length > 1024);
        }

        private MemoryStream ReadToMemory(Stream response)
        {
            var memory = new MemoryStream();
            var buffer = new byte[4096];
            while (true)
            {
                var readCount = response.Read(buffer, 0, buffer.Length);
                memory.Write(buffer, 0, readCount);
                if (readCount == 0) break;
            }
            return memory;
        }

        private long AsMegabytes(long size)
        {
            return size / (1024 * 1024);
        }
    }

}
