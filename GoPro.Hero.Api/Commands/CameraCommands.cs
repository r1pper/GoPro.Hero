using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api.Commands.CameraCommands
{
    [Command(HeroCommands.CAMERA_GET_NAME, Parameterless = true)]
    class CommandCameraGetName : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_SET_NAME)]
    class CommandCameraSetName : CommandRequest<ICamera>
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
                var length=value.Length.ToString("x2");
                base.parameter = string.Format("%{0}{1}",length, value);
            }
        }

        public CommandCameraSetName Set(string name)
        {
            this.Name = name;
            return this;
        }
    }

    [Command(HeroCommands.CAMERA_TIME)]
    public class CommandCameraSetTime : CommandRequest<ICamera>
    {
        public DateTime Date
        {
            get
            {
                if (string.IsNullOrEmpty(base.parameter))
                    return DateTime.Now;

                return base.parameter.ToDateTimeHeroString();
            }
            set
            {
                base.parameter = value.ToHeroDateTime();
            }
        }

        public CommandCameraSetTime Set(DateTime dateTime)
        {
            this.Date = dateTime;
            return this;
        }
    }

    [Command(HeroCommands.CAMERA_VIDEO_RESOLUTION)]
    public class CommandCameraVideoResolution : CommandMultiChoice<VideoResolution, ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_ORIENTATION)]
    public class CommandCameraOrientation : CommandMultiChoice<Orientation, ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_TIMELAPSE_TI)]
    public class CommandCameraTimeLapse : CommandMultiChoice<TimeLapse, ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_BEEP)]
    public class CommandCameraBeepSound : CommandMultiChoice<BeepSound, ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_PROTUNE)]
    public class CommandCameraProtune : CommandBoolean<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_PHOTO_RESOLUTION)]
    public class CommandCameraPhotoResolution : CommandMultiChoice<PhotoResolution, ICamera>
    {
    }

    //[Command(HeroCommands.CAMERA_OSD)]
    //public class CommandCameraOnScreenDisplay : CommandBoolean<IHeroCamera>
    //{
    //}

    [Command(HeroCommands.CAMERA_VIDEO_MODE)]
    public class CommandCameraVideoStandard : CommandMultiChoice<VideoStandard, ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_MODE)]
    public class CommandCameraMode : CommandMultiChoice<Mode,ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_LOCATE)]
    public class CommandCameraLocate : CommandBoolean<ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_LIVE_PREVIEW)]
    public class CommandCameraPreview : CommandBoolean<ICamera>
    {
        private const string PREVIEW_ON = "%02";

        public override bool State
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
    public class CommandCameraLedBlink : CommandMultiChoice<LedBlink, ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_FIELD_OF_VIEW)]
    public class CommandCameraFieldOfView : CommandMultiChoice<FieldOfView, ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    public class CommandCameraSpotMeter : CommandBoolean<ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_DEFAULT_MODE)]
    public class CommandCameraDefaultMode : CommandMultiChoice<Mode, ICamera>
    {
    }

    //[Command(HeroCommands.CAMREA_AUTO_POWER_OFF)]
    //public class CommandCameraAutoPower : CommandBoolean<IHeroCamera>
    //{
    //}

    [Command(HeroCommands.CAMREA_DELETE_ALL_SD, Parameterless = true)]
    public class CommandCameraDeleteAllFilesOnSd : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_DELETE_LAST_SD,Parameterless=true)]
    public class CommandCameraDeleteLastFileOnSd : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMREA_INFORMATION, Parameterless = true)]
    public class CommandCameraInformation : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_SETTINGS, Parameterless = true)]
    public class CommandCameraSettings : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    public class CommandCameraExtendedSettings : CommandRequest<ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_WHITE_BALANCE)]
    public class CommandCameraWhiteBalance : CommandMultiChoice<WhiteBalance,ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_LOOPING_VIDEO)]
    public class CommandCameraLoopingVideo : CommandMultiChoice<LoopingVideo,ICamera>
    {
        //NOTE: Hero3 Black Edition does not respond to the command!
    }

    [Command(HeroCommands.CAMERA_FRAMERATE)]
    public class CommandCameraFrameRate : CommandMultiChoice<FrameRate,ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_BURSTRATE)]
    public class CommandCameraBurstRate : CommandMultiChoice<BurstRate,ICamera>
    {
    }

    [Command(HeroCommands.CAMERA_CONTINUOUS)]
    public class CommandCameraContinuousShot : CommandMultiChoice<ContinuousShot,ICamera>
    {
    }
}
