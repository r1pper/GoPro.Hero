using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser;
using GoPro.Hero.Browser.Media;
using GoPro.Hero.Hero3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Tests
{
    [TestClass]
    public class MediaBrowserTests
    {
        private delegate IEnumerable<Media> ParseDelegate(Stream json);

        private MediaBrowser _instance;
        private ParseDelegate _parseDelegate;

        [TestInitialize]
        public void Initialize()
        {
            _instance = Activator.CreateInstance<MediaBrowser>();

            (_instance as IGeneralBrowser).Initialize(null, null);

            var browserType = _instance.GetType();
            var method=browserType.GetMethod("Parse", BindingFlags.NonPublic | BindingFlags.Instance);
            _parseDelegate= (ParseDelegate)method.CreateDelegate(typeof(ParseDelegate), _instance);
        }

        [TestMethod]
        public void CheckContentsFromSample()
        {
            var sample = GetSample();
            var contents = _parseDelegate(sample);
            Assert.IsNull(contents);
        }


        [TestMethod]
        public void CheckContentsFromCamera()
        {
            var camera = GetCamera();
            var contents=camera.Browse<MediaBrowser>().Contents();
            Assert.IsNull(contents);
        }

        [TestMethod]
        public void CheckImagesFromCamera()
        {
            var camera = GetCamera();
            var first = camera.Contents().Contents<Image>().ToList();
            var second = camera.Browse<MediaBrowser>().Images().ToList();
            var third = camera.Contents().Images().ToList();
            var forth = camera.Browse<MediaBrowser>().Contents<Image>().ToList();

            
            CollectionAssert.AreEquivalent(first, second);
            CollectionAssert.AreEquivalent(third, forth);
            CollectionAssert.AreEquivalent(forth, first);

            if (first == null)
                Assert.Inconclusive("no image found");
        }

        [TestMethod]
        public void CheckTimeLapsesFromCamera()
        {
            var camera = GetCamera();
            var first = camera.Contents().Contents<TimeLapsedImage>().ToList();
            var second = camera.Browse<MediaBrowser>().TimeLapses().ToList();
            var third = camera.Contents().TimeLapses().ToList();
            var forth = camera.Browse<MediaBrowser>().Contents<TimeLapsedImage>().ToList();


            CollectionAssert.AreEquivalent(first, second);
            CollectionAssert.AreEquivalent(third, forth);
            CollectionAssert.AreEquivalent(forth, first);

            if (first == null)
                Assert.Inconclusive("no timelapsed image found");
        }

        [TestMethod]
        public void CheckDownloadImage()
        {
            var camera = GetCamera();
            var image=camera.Contents().Images().FirstOrDefault();
            if (image == null)
                Assert.Inconclusive("no image found");

            var response=image.Download().GetResponseStream();

            var memory = ReadToMemory(response);

            Assert.AreEqual(memory.Length, response.Length);
        }

        [TestMethod]
        public void CheckDownloadVideo()
        {
            var camera = GetCamera();
            var video = camera.Contents().Videos().FirstOrDefault();
            if (video == null)
                Assert.Inconclusive("no video found");

            var response = video.Download().GetResponseStream();

            var memory = ReadToMemory(response);

            Assert.AreEqual(memory.Length, response.Length);
        }

        [TestMethod]
        public void CheckDownloadTimeLapsedImage()
        {
            var camera = GetCamera();
            var timeLapsed = camera.Contents().TimeLapses().FirstOrDefault();
            if (timeLapsed == null)
                Assert.Inconclusive("no timelapsed image found");

            foreach (var item in timeLapsed)
            {
                var stream=item.GetResponseStream();
                var memory = ReadToMemory(stream);
                Assert.AreEqual(memory.Length, stream.Length);
            }
        }

        [TestMethod]
        public void CheckDownloadImageThumbnail()
        {
            var camera = GetCamera();
            var image = camera.Contents().Images().FirstOrDefault();
            var thumbnail=image.Thumbnail();
        }

        [TestMethod]
        public void CheckDownloadImageBigThumbnail()
        {
            var camera = GetCamera();
            var image = camera.Contents().Images().FirstOrDefault();
            var thumbnail = image.BigThumbnail();
        }

        [TestMethod]
        public void CheckDownloadTimeLapsedThumbnail()
        {
            var camera = GetCamera();
            var timeLapse = camera.Contents().TimeLapses().FirstOrDefault();
            var thumbnail = timeLapse.Thumbnail();
        }

        [TestMethod]
        public void CheckDownloadTimeLapsedBigThumbnail()
        {
            var camera = GetCamera();
            var timeLapse = camera.Contents().TimeLapses().FirstOrDefault();
            var thumbnail = timeLapse.BigThumbnail();
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

        private Hero3Camera GetCamera()
        {
            return Camera.Create<Hero3Camera>(ExpectedParameters.IP_ADDRESS);
        }

        private Stream GetSample()
        {
            var sample =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Tests.Resources.MediaBrowserSample.json");

            return sample;
        }
    }
}
