using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    class CommandCameraSettings:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.CAMERA_SETTINGS;
            base.parameter = null;
        }

        public static CommandCameraExtendedSettings Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraExtendedSettings>(address, HeroCommands.CAMERA_SETTINGS, passPhrase);
        }
    }
}
