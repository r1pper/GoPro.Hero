using System.Linq;
using System.Reflection;
using GoPro.Hero.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoPro.Hero.Tests
{
    [TestClass]
    public class M3U8Tests
    {
        private readonly string[] _expected = new[]
            {
                "amba_hls-7.ts",
                "amba_hls-8.ts",
                "amba_hls-9.ts",
                "amba_hls-10.ts",
                "amba_hls-11.ts",
                "amba_hls-12.ts",
                "amba_hls-13.ts",
                "amba_hls-14.ts"
            };

        private M3U8Parser GetParser()
        {
            var sample =
                Assembly.GetExecutingAssembly().GetManifestResourceStream("GoPro.Hero.Tests.Resources.amba.m3u8");
            return new M3U8Parser(sample);
        }

        [TestMethod]
        public void CheckParameters()
        {
            var parser = GetParser();

            Assert.AreEqual(false, parser.AllowCache);
            Assert.AreEqual("3", parser.Version);
            Assert.AreEqual(2.0, parser.TargetDuration);
            Assert.AreEqual(591, parser.Sequence);
        }

        [TestMethod]
        public void CheckFiles()
        {
            var parser = GetParser();

            var files = parser.All().ToArray();

            Assert.AreEqual(8, files.Length);

            for (var i = 0; i < files.Length; i++)
                Assert.AreEqual(_expected[i], files[i]);
        }

        [TestMethod]
        public void CheckQueue()
        {
            var parser = GetParser();

            for (var i = 0; i < _expected.Length; i++)
                Assert.AreEqual(_expected[i], parser.Dequeue());
        }
    }
}