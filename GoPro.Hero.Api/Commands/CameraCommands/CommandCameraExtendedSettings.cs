using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    class CommandCameraExtendedSettings:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.CAMERA_EXTENDED_SETTINGS;
            base.parameter = null;
        }

        public static CommandCameraExtendedSettings Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraExtendedSettings>(address, HeroCommands.CAMERA_EXTENDED_SETTINGS, passPhrase);
        }
    }
}
