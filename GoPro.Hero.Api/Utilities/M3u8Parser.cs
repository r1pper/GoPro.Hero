using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Utilities
{
    public class M3u8Parser
    {
        private const string TARGET_DURATION="#EXT-X-TARGETDURATION";
        private const string VERSION="#EXT-X-VERSION";
        private const string ALLOW_CACHE="#EXT-X-ALLOW-CACHE";
        private const string SEQUENCE="#EXT-X-MEDIA-SEQUENCE";
        private const string INFO = "#EXTINF";

        private Dictionary<string, string> _content = new Dictionary<string, string>();
        private Queue<string> _files = new Queue<string>();

        public string this[string key]
        {
            get
            {
                return _content.ContainsKey(key) ? _content[key] : null;
            }
        }
        public float TargetDuration { get { return this.GetValue<float>(TARGET_DURATION); } }
        public string Version { get { return this.GetValue<string>(VERSION); } }
        public bool AllowCache
        {
            get
            {
                var allowCache = this[ALLOW_CACHE];
                return allowCache == null ? false : allowCache == "YES" ? true : false;
            }
        }
        public int Sequence { get { return this.GetValue<int>(SEQUENCE); } }

        public string Dequeue()
        {
            return _files.Dequeue().Split(',').Last();
        }

        public IEnumerable<string> All()
        {
            return _files.Select(x => x.Split(',').Last());
        }

        private T GetValue<T>(string key)
        {
            return _content.ContainsKey(key) ? (T)Convert.ChangeType(_content[key], typeof(T),null) : default(T);
        }

        public M3u8Parser(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                var line = string.Empty;
                do
                {
                    if (string.IsNullOrEmpty(line) || line.EndsWith(","))
                    {
                        line += sr.ReadLine();
                        continue;
                    }

                    if (line.StartsWith(INFO))
                        _files.Enqueue(line);
                    else
                    {
                        var split = line.Split(':');
                        _content.Add(split.First(), split.Last());
                    }

                    line = string.Empty;
                } while (!sr.EndOfStream || !string.IsNullOrEmpty(line));
            }
        }
    }
}
