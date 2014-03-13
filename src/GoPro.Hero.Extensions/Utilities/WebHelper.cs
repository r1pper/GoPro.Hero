using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Exceptions;

namespace GoPro.Hero.Utilities
{
    class WebHelper
    {
        public static async Task<byte[]> BufferRequestAsync(string uri)
        {
            return Configuration.CommandRequestMode == Configuration.HttpRequestMode.Async ?
                await BufferRequestAsynchronous(uri) :
                BufferRequestSynchronous(uri);
        }

        private static async Task<byte[]> BufferRequestAsynchronous(string uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (request != null)
            {
                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        var stream = response.GetResponseStream();

                        var memoryStream = new MemoryStream();
                        var buffer = new byte[4096];

                        while (true)
                        {
                            var readCount = stream.Read(buffer, 0, buffer.Length);
                            memoryStream.Write(buffer, 0, readCount);
                            if (readCount == 0) break;
                        }

                        stream.Dispose();
                        return memoryStream.ToArray();
                    }
                }
            }
            throw new GoProException();
        }

        private static byte[] BufferRequestSynchronous(string uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (request != null)
            {
                var asyncResponse = request.BeginGetResponse(null, null);
                asyncResponse.AsyncWaitHandle.WaitOne();

                using (var response = request.EndGetResponse(asyncResponse) as HttpWebResponse)
                {
                    if (response != null)
                    {
                        var stream = response.GetResponseStream();

                        var memoryStream = new MemoryStream();
                        var buffer = new byte[4096];

                        while (true)
                        {
                            var readCount = stream.Read(buffer, 0, buffer.Length);
                            memoryStream.Write(buffer, 0, readCount);
                            if (readCount == 0) break;
                        }

                        stream.Dispose();
                        return memoryStream.ToArray();
                    }
                }
            }
            throw new GoProException();
        }
    }
}
