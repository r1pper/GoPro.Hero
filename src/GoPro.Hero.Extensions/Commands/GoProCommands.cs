using System;
using System.Threading.Tasks;
using GoPro.Hero.Utilities;
using Sockets.Plugin;
using System.IO;
using System.Text;

namespace GoPro.Hero.Commands
{
    [Command(HeroCommands.GOPRO_MEDIALIST,InSecure=true,Parameterless=true)]
    public class CommandUnsafeGoProMediaList : CommandRequest
    {
        protected override sealed async Task<CommandResponse> SendRequestAsync()
        {
            var uri = base.GetUri();

            var tcpClient = new TcpSocketClient();
            await tcpClient.ConnectAsync(uri.Host, uri.Port);

            var writer = new StreamWriter(tcpClient.WriteStream);
            await writer.WriteLineAsync(string.Format("GET {0} HTTP/1.1",uri));
            await writer.WriteLineAsync(string.Format("Host: {0}", uri.Host));
            await writer.WriteLineAsync();
            await writer.WriteLineAsync();
            await writer.FlushAsync();

            var reader = new StreamReader(tcpClient.ReadStream,Encoding.UTF8,false,1);
            while (true)
            {
                var line = reader.ReadLine();
                if (line == string.Empty)
                    break;
            }

            var len = int.Parse(reader.ReadLine(), System.Globalization.NumberStyles.HexNumber);

            var json = reader.ReadLine();

            using (var ms = new MemoryStream())
            using (var w = new StreamWriter(ms))
            {
                await w.WriteAsync(json);
                await w.FlushAsync();
                if (ms.Length != len-1)
                    throw new Exceptions.GoProException();
                return CommandResponse.Create(ms.ToArray());
            }
        }
    }

    [Command(HeroCommands.GOPRO_MEDIALIST, InSecure = true, Parameterless = true)]
    public class CommandGoProMediaList : CommandRequest
    {
        protected override sealed async Task<CommandResponse> SendRequestAsync()
        {
            var uri = GetUri();
            var buffer = await WebHelper.BufferRequestAsync(uri.ToString());

            return CommandResponse.Create(buffer);
        }
    }

    [Command(HeroCommands.GOPRO_MEDIADATA,InSecure=true)]
    public class CommandGoProThumbnail : CommandRequest
    {
        public string Path
        {
            get { return base.Parameter.Substring(1); }
            set { base.Parameter = string.Format("/{0}", value); }
        }

        public CommandGoProThumbnail Set(string path)
        {
            Path = path;
            return this;
        }
    }

    [Command(HeroCommands.GOPRO_MEDIADATA)]
    public class CommandGoProBigThumbnail : CommandGoProMetaData
    {
        protected override void Initialize()
        {
            base.Initialize();
            base.PassPhrase = "scr";
        }
    }

    [Command(HeroCommands.GOPRO_MEDIADATA)]
    public class CommandGoProVideoInfo : CommandGoProMetaData
    {
        protected override void Initialize()
        {
            base.Initialize();
            base.PassPhrase = "dur";
        }
    }

    public abstract class CommandGoProMetaData : CommandRequest
    {
        public string Path
        {
            get { return base.Parameter.Substring(1); }
            set { base.Parameter = string.Format("/{0}", value); }
        }

        public CommandGoProMetaData Set(string path)
        {
            Path = path;
            return this;
        }

        public override Uri GetUri()
        {
            var addressParts = Address.Split(':');
            var builder = new UriBuilder { Host = addressParts[0], Port = int.Parse(addressParts[1]), Path = Command };

            builder.Query = string.Format("p={0}&t={1}", Parameter, PassPhrase);

            return builder.Uri;
        }

        protected override sealed async Task<CommandResponse> SendRequestAsync()
        {
            var uri = GetUri();
            var buffer = await WebHelper.BufferRequestAsync(uri.ToString());

            return CommandResponse.Create(buffer);
        }
    }
}
