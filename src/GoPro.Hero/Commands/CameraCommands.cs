using System;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Commands
{
    [Command(HeroCommands.CAMERA_GET_NAME, Parameterless = true)]
    internal class CommandCameraGetName : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_SET_NAME)]
    internal class CommandCameraSetName : CommandRequest<Camera>
    {
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(base.Parameter))
                    return string.Empty;

                return base.Parameter.Substring(2);
            }
            set
            {
                var length = value.Length.ToString("x2");
                base.Parameter = string.Format("%{0}{1}", length, value);
            }
        }

        public CommandCameraSetName Set(string name)
        {
            Name = name;
            return this;
        }
    }

    [Command(HeroCommands.CAMERA_TIME)]
    public class CommandCameraSetTime : CommandRequest<Camera>
    {
        public DateTime Date
        {
            get
            {
                if (string.IsNullOrEmpty(base.Parameter))
                    return DateTime.Now;

                return base.Parameter.ToDateTimeHeroString();
            }
            set { base.Parameter = value.ToHeroDateTime(); }
        }

        public CommandCameraSetTime Set(DateTime dateTime)
        {
            Date = dateTime;
            return this;
        }
    }

    [Command(HeroCommands.CAMERA_VIDEO_RESOLUTION)]
    public class CommandCameraVideoResolution : CommandMultiChoice<VideoResolution, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_ORIENTATION)]
    public class CommandCameraOrientation : CommandMultiChoice<Orientation, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_TIMELAPSE_TI)]
    public class CommandCameraTimeLapse : CommandMultiChoice<TimeLapse, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_BEEP)]
    public class CommandCameraBeepSound : CommandMultiChoice<BeepSound, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_PROTUNE)]
    public class CommandCameraProtune : CommandBoolean<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_PHOTO_RESOLUTION)]
    public class CommandCameraPhotoResolution : CommandMultiChoice<PhotoResolution, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_OSD)]
    public class CommandCameraOnScreenDisplay : CommandBoolean<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_VIDEO_MODE)]
    public class CommandCameraVideoStandard : CommandMultiChoice<VideoStandard, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_MODE)]
    public class CommandCameraMode : CommandMultiChoice<Mode, Camera>
    {
    }

    [Command(HeroCommands.CAMREA_LOCATE)]
    public class CommandCameraLocate : CommandBoolean<Camera>
    {
    }

    [Command(HeroCommands.CAMREA_LIVE_PREVIEW)]
    public class CommandCameraPreview : CommandBoolean<Camera>
    {
        private const string PREVIEW_ON = "%02";

        public override bool State
        {
            get { return string.IsNullOrEmpty(base.Parameter) ? false : base.Parameter == OFF ? false : true; }
            set { base.Parameter = value ? PREVIEW_ON : OFF; }
        }
    }

    [Command(HeroCommands.CAMREA_LED_BLINK)]
    public class CommandCameraLedBlink : CommandMultiChoice<LedBlink, Camera>
    {
    }

    [Command(HeroCommands.CAMREA_FIELD_OF_VIEW)]
    public class CommandCameraFieldOfView : CommandMultiChoice<FieldOfView, Camera>
    {
    }

    [Command(HeroCommands.CAMREA_EXPOSURE)]
    public class CommandCameraSpotMeter : CommandBoolean<Camera>
    {
    }

    [Command(HeroCommands.CAMREA_DEFAULT_MODE)]
    public class CommandCameraDefaultMode : CommandMultiChoice<Mode, Camera>
    {
    }

    [Command(HeroCommands.CAMREA_AUTO_POWER_OFF)]
    public class CommandCameraAutoPowerOff : CommandMultiChoice<AutoPowerOff, Camera>
    {
    }

    [Command(HeroCommands.CAMREA_DELETE_ALL_SD, Parameterless = true)]
    public class CommandCameraDeleteAllFilesOnSd : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_DELETE_LAST_SD, Parameterless = true)]
    public class CommandCameraDeleteLastFileOnSd : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMREA_INFORMATION, Parameterless = true)]
    public class CommandCameraInformation : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_SETTINGS, Parameterless = true)]
    public class CommandCameraSettings : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    public class CommandCameraExtendedSettings : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_WHITE_BALANCE)]
    public class CommandCameraWhiteBalance : CommandMultiChoice<WhiteBalance, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_LOOPING_VIDEO)]
    public class CommandCameraLoopingVideo : CommandMultiChoice<LoopingVideo, Camera>
    {
        //NOTE: Camera only responds to the command at 1080@24, 1080@30, 1440@24 and 720@60
    }

    [Command(HeroCommands.CAMERA_FRAMERATE)]
    public class CommandCameraFrameRate : CommandMultiChoice<FrameRate, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_BURSTRATE)]
    public class CommandCameraBurstRate : CommandMultiChoice<BurstRate, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_CONTINUOUS)]
    public class CommandCameraContinuousShot : CommandMultiChoice<ContinuousShot, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_DELETE_FILE)]
    public class CommandCameraDeleteFile : CommandRequest<Camera>
    {
        public string Path
        {
            get { return base.Parameter.Substring(3); }
            set { base.Parameter = string.Format("%15{0}", value); }
        }

        public CommandCameraDeleteFile Set(string path)
        {
            Path = path;
            return this;
        }
    }

    [Command(HeroCommands.CAMERA_LOW_LIGHT)]
    public class CommandCameraAutoLowLight : CommandBoolean<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_CAPABILITIES, Parameterless = true)]
    public class CommandCameraCapabilities : CommandRequest<Camera>
    {
    }

    [Command(HeroCommands.CAMERA_PHOTO_IN_VIDEO)]
    public class CommandCameraPhotoInVideo : CommandMultiChoice<PhotoInVideo, Camera>
    {
    }

    [Command(HeroCommands.CAMERA_ONE_BUTTON_MODE)]
    public class CommandCameraOneButtonMode : CommandBoolean<Camera>
    {
    }
}