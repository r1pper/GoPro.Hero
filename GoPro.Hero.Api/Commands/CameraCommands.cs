using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    [Command(HeroCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    class CommandCameraExtendedSettings : CommandRequest
    {
        public static CommandCameraExtendedSettings Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraExtendedSettings>(address, HeroCommands.CAMERA_EXTENDED_SETTINGS, passPhrase);
        }
    }

    [Command(HeroCommands.CAMREA_INFORMATION, Parameterless = true)]
    class CommandCameraInformation : CommandRequest
    {
        public static CommandCameraInformation Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraInformation>(address, HeroCommands.CAMREA_INFORMATION, passPhrase);
        }
    }

    [Command(HeroCommands.CAMREA_LOCATE)]
    class CommandCameraLocate : CommandBoolean
    {
        public static CommandCameraLocate Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraLocate>(address, HeroCommands.CAMREA_LOCATE, passPhrase);
        }
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    class CommandCameraProtune : CommandBoolean
    {
        public static CommandCameraSpotMeter Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraSpotMeter>(address, HeroCommands.CAMREA_EXPOSURE, passPhrase);
        }
    }

    [Command(HeroCommands.CAMERA_SETTINGS, Parameterless = true)]
    class CommandCameraSettings : CommandRequest
    {
        public static CommandCameraExtendedSettings Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraExtendedSettings>(address, HeroCommands.CAMERA_SETTINGS, passPhrase);
        }
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    class CommandCameraSpotMeter : CommandBoolean
    {
        public static CommandCameraSpotMeter Create(string address, string passPhrase)
        {
            return CommandRequest.Create<CommandCameraSpotMeter>(address, HeroCommands.CAMREA_EXPOSURE, passPhrase);
        }
    }
}
