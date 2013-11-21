using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Commands
{
    [Command(HeroCommands.GOPRO_MEDIALIST,InSecure=true,Parameterless=true)]
    public class CommandGoProMediaList : CommandRequest<ICamera>
    {
        protected override sealed async Task<CommandResponse> SendRequestAsync()
        {
            var uri = base.GetUri();
            var buffer = await WebHelper.BufferRequestAsync(uri.ToString());

            return CommandResponse.Create(buffer);
        }
    }

    [Command(HeroCommands.GOPRO_MEDIADATA,InSecure=true)]
    public class CommandGoProThumbnail : CommandRequest<ICamera>
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

    public abstract class CommandGoProMetaData : CommandRequest<ICamera>
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
