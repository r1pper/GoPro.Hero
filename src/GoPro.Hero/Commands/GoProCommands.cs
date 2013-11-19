using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Commands
{
    [Command(HeroCommands.GOPRO_MEDIALIST,InSecure=true,Parameterless=true)]
    public class CommandGoProMediaList : CommandRequest<ICamera>
    {
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
    public class CommandGoProBigThumbnail : CommandRequest<ICamera>
    {
        protected override void Initialize()
        {
            base.Initialize();
            base.PassPhrase = "scr";
        }

        public string Path
        {
            get { return base.Parameter.Substring(1); }
            set { base.Parameter = string.Format("/{0}", value); }
        }

        public CommandGoProBigThumbnail Set(string path)
        {
            Path = path;
            return this;
        }
    }

    [Command(HeroCommands.GOPRO_MEDIADATA)]
    public class CommandGoProVideoInfo : CommandRequest<ICamera>
    {
        protected override void Initialize()
        {
            base.Initialize();
            base.PassPhrase = "dur";
        }

        public string Path
        {
            get { return base.Parameter.Substring(1); }
            set { base.Parameter = string.Format("/{0}", value); }
        }

        public CommandGoProVideoInfo Set(string path)
        {
            Path = path;
            return this;
        }
    }
}
