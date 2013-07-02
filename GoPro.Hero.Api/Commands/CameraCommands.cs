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

    [Command(HeroCommands.CAMERA_PROTUNE)]
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

    [Command(HeroCommands.CAMERA_ORIENTATION)]
    class CommandCameraOrientation : CommandRequest
    {
        private const string UP = "%00";
        private const string DOWN = "%01";

        public Orientation Orientation
        {
            get
            {
                return string.IsNullOrEmpty(base.parameter)
                ? Orientation.Up: base.parameter == UP
                ? Orientation.Up: Orientation.Down;
            }

            set
            {
                base.parameter = value == Orientation.Up ? UP : DOWN;
            }
        }
    }
}
