using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    [Command(HeroCommands.CAMERA_MODE)]
    class CommandCameraMode : CommandMultiChoice<Mode>
    {
    }

    [Command(HeroCommands.CAMERA_BEEP)]
    class CommandCameraBeepSound : CommandMultiChoice<BeepSound>
    {
    }

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

    [Command(HeroCommands.CAMREA_LIVE_PREVIEW)]
    class CommandCameraPreview : CommandBoolean
    {
        private const string PREVIEW_ON = "%02";

        public override bool Enable
        {
            get
            {
                return string.IsNullOrEmpty(base.parameter) ? false : base.parameter == OFF ? false : true;
            }
            set
            {
                base.parameter = value ? PREVIEW_ON : OFF;
            }
        }
    }

    [Command(HeroCommands.CAMREA_LED_BLINK)]
    class CommandCameraLedBlink : CommandMultiChoice<LedBlink>
    {
    }

    [Command(HeroCommands.CAMREA_FIELD_OF_VIEW)]
    class CommandCameraFieldOfView : CommandMultiChoice<FieldOfView>
    {
    }

    [Command(HeroCommands.CAMREA_DEFAULT_MODE)]
    class CommandCameraDefaultMode : CommandMultiChoice<Mode>
    {
    }

    [Command(HeroCommands.CAMERA_VIDEO_RESOLUTION)]
    class CommandCameraVideoResolution : CommandMultiChoice<VideoResolution>
    {
    }

    [Command(HeroCommands.CAMERA_VIDEO_MODE)]
    class CommandCameraVideoStandard : CommandMultiChoice<VideoStandard>
    {
    }

    [Command(HeroCommands.CAMERA_TIMELAPSE_TI)]
    class CommandCameraTimeLapse : CommandMultiChoice<TimeLapse>
    {
    }

    [Command(HeroCommands.CAMERA_PHOTO_RESOLUTION)]
    class CommandCameraPhotoResolution : CommandMultiChoice<PhotoResolution>
    {
    }

    [Command(HeroCommands.CAMERA_ORIENTATION)]
    class CommandCameraOrientation : CommandMultiChoice<Orientation>
    {
    }
}
