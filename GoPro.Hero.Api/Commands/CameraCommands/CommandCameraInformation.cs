using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    class CommandCameraInformation:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.CAMREA_INFORMATION;
            base.parameter = null;
        }

        public static CommandCameraInformation Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraInformation>(address, HeroCommands.CAMREA_INFORMATION, passPhrase);
        }
    }
}
