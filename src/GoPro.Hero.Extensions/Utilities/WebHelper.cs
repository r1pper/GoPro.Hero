using System.IO;
using System.Threading.Tasks;
using GoPro.Hero.Exceptions;
using System.Net.Http;

namespace GoPro.Hero.Utilities
{
    class WebHelper
    {
        public static async Task<byte[]> BufferRequestAsync(string uri)
        {
            return await BufferRequestAsynchronous(uri);
        }

        private static async Task<byte[]> BufferRequestAsynchronous(string uri)
        {
            using (var request = new HttpClient())
            using (var stream = await request.GetStreamAsync(uri))
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[4096];

                while (true)
                {
                    var readCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                    await memoryStream.WriteAsync(buffer, 0, readCount);
                    if (readCount == 0) break;
                }
                return memoryStream.ToArray();
            }

            throw new GoProException();
        }
    }
}
