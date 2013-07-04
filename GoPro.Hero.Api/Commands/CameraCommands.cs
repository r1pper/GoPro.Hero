using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    [Command(HeroCommands.CAMERA_GET_NAME, Parameterless = true)]
    class CommandCameraGetName : CommandRequest
    {
    }

    [Command(HeroCommands.CAMERA_SET_NAME)]
    class CommandCameraSetName : CommandRequest
    {
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(base.parameter))
                    return string.Empty;

                return base.parameter.Substring(2);
            }
            set
            {
                base.parameter = string.Format("%0{0}", value);
            }
        }
    }

    [Command(HeroCommands.CAMERA_MODE)]
    public class CommandCameraMode : CommandMultiChoice<Mode>
    {
    }

    [Command(HeroCommands.CAMERA_DELETE_LAST_SD,Parameterless=true)]
    public class CommandCameraDeleteLastFileOnSd : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_DELETE_ALL_SD, Parameterless = true)]
    public class CommandCameraDeleteAllFilesOnSd : CommandRequest
    {
    }

    [Command(HeroCommands.CAMERA_BEEP)]
    public class CommandCameraBeepSound : CommandMultiChoice<BeepSound>
    {
    }

    [Command(HeroCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    public class CommandCameraExtendedSettings : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_INFORMATION, Parameterless = true)]
    public class CommandCameraInformation : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_LOCATE)]
    public class CommandCameraLocate : CommandBoolean
    {
    }

    [Command(HeroCommands.CAMERA_PROTUNE)]
    public class CommandCameraProtune : CommandBoolean
    {
    }

    [Command(HeroCommands.CAMERA_SETTINGS, Parameterless = true)]
    public class CommandCameraSettings : CommandRequest
    {
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    public class CommandCameraSpotMeter : CommandBoolean
    {
    }

    [Command(HeroCommands.CAMREA_LIVE_PREVIEW)]
    public class CommandCameraPreview : CommandBoolean
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
    public class CommandCameraLedBlink : CommandMultiChoice<LedBlink>
    {
    }

    [Command(HeroCommands.CAMREA_FIELD_OF_VIEW)]
    public class CommandCameraFieldOfView : CommandMultiChoice<FieldOfView>
    {
    }

    [Command(HeroCommands.CAMREA_DEFAULT_MODE)]
    public class CommandCameraDefaultMode : CommandMultiChoice<Mode>
    {
    }

    [Command(HeroCommands.CAMERA_VIDEO_RESOLUTION)]
    public class CommandCameraVideoResolution : CommandMultiChoice<VideoResolution>
    {
    }

    [Command(HeroCommands.CAMERA_VIDEO_MODE)]
    public class CommandCameraVideoStandard : CommandMultiChoice<VideoStandard>
    {
    }

    [Command(HeroCommands.CAMERA_TIMELAPSE_TI)]
    public class CommandCameraTimeLapse : CommandMultiChoice<TimeLapse>
    {
    }

    [Command(HeroCommands.CAMERA_PHOTO_RESOLUTION)]
    public class CommandCameraPhotoResolution : CommandMultiChoice<PhotoResolution>
    {
    }

    [Command(HeroCommands.CAMERA_ORIENTATION)]
    public class CommandCameraOrientation : CommandMultiChoice<Orientation>
    {
    }

    [Command(HeroCommands.CAMERA_WHITE_BALANCE)]
    public class CommandCameraWhiteBalance : CommandMultiChoice<WhiteBalance>
    {
    }

    [Command(HeroCommands.CAMERA_LOOPING_VIDEO)]
    public class CommandCameraLoopingVideo : CommandMultiChoice<LoopingVideo>
    {
    }

    [Command(HeroCommands.CAMERA_FRAMERATE)]
    public class CommandCameraFrameRate : CommandMultiChoice<FrameRate>
    {
    }

    [Command(HeroCommands.CAMERA_BURSTRATE)]
    public class CommandCameraBurstRate : CommandMultiChoice<BurstRate>
    {
    }

    [Command(HeroCommands.CAMERA_CONTINUOUS)]
    public class CommandCameraContinuousShot : CommandMultiChoice<ContinuousShot>
    {
    }

    [Command(HeroCommands.CAMERA_OSD)]
    public class CommandCameraOnScreenDisplay : CommandBoolean
    {
    }
}
