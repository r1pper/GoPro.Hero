using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    [Command(HeroCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    class CommandCameraExtendedSettings : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_INFORMATION, Parameterless = true)]
    class CommandCameraInformation : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_LOCATE)]
    class CommandCameraLocate : CommandBoolean
    {
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    class CommandCameraProtune : CommandBoolean
    {
    }

    [Command(HeroCommands.CAMERA_SETTINGS, Parameterless = true)]
    class CommandCameraSettings : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    class CommandCameraSpotMeter : CommandBoolean
    {
    }
}
