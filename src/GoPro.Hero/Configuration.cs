using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero
{
    public static class Configuration
    {
        public enum HttpRequestMode { Sync, Async };

        public static HttpRequestMode CommandRequestMode { get; set; }

        static Configuration()
        {
            CommandRequestMode = HttpRequestMode.Async;
        }
    }
}
