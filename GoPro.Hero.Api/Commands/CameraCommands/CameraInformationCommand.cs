using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    class CameraInformationCommand:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.CAMREA_INFORMATION;
            base.parameter = null;
        }

        public static CameraInformationCommand Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CameraInformationCommand>(address, HeroCommands.CAMREA_INFORMATION, passPhrase);
        }
    }
}
