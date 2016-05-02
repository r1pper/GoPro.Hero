using System;
using GoPro.Hero.Utilities;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Hero3
{
    [Command(LegacyCommands.CAMERA_GET_NAME, Parameterless = true)]
    internal class CommandCameraGetName : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_SET_NAME)]
    internal class CommandCameraSetName : CommandRequest<LegacyCamera>
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

    [Command(LegacyCommands.CAMERA_TIME)]
    public class CommandCameraSetTime : CommandRequest<LegacyCamera>
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

    [Command(LegacyCommands.CAMERA_VIDEO_RESOLUTION)]
    public class CommandCameraVideoResolution : CommandMultiChoice<VideoResolution, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_ORIENTATION)]
    public class CommandCameraOrientation : CommandMultiChoice<Orientation, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_TIMELAPSE_TI)]
    public class CommandCameraTimeLapse : CommandMultiChoice<TimeLapse, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_BEEP)]
    public class CommandCameraBeepSound : CommandMultiChoice<BeepSound, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_PROTUNE)]
    public class CommandCameraProtune : CommandBoolean<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_PHOTO_RESOLUTION)]
    public class CommandCameraPhotoResolution : CommandMultiChoice<PhotoResolution, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_OSD)]
    public class CommandCameraOnScreenDisplay : CommandBoolean<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_VIDEO_MODE)]
    public class CommandCameraVideoStandard : CommandMultiChoice<VideoStandard, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_MODE)]
    public class CommandCameraMode : CommandMultiChoice<Mode, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_LOCATE)]
    public class CommandCameraLocate : CommandBoolean<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_LIVE_PREVIEW)]
    public class CommandCameraPreview : CommandBoolean<LegacyCamera>
    {
        private const string PREVIEW_ON = "%02";

        public override bool State
        {
            get { return string.IsNullOrEmpty(base.Parameter) ? false : base.Parameter == OFF ? false : true; }
            set { base.Parameter = value ? PREVIEW_ON : OFF; }
        }
    }

    [Command(LegacyCommands.CAMREA_LED_BLINK)]
    public class CommandCameraLedBlink : CommandMultiChoice<LedBlink, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_FIELD_OF_VIEW)]
    public class CommandCameraFieldOfView : CommandMultiChoice<FieldOfView, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_EXPOSURE)]
    public class CommandCameraSpotMeter : CommandBoolean<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_DEFAULT_MODE)]
    public class CommandCameraDefaultMode : CommandMultiChoice<Mode, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_AUTO_POWER_OFF)]
    public class CommandCameraAutoPowerOff : CommandMultiChoice<AutoPowerOff, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_DELETE_ALL_SD, Parameterless = true)]
    public class CommandCameraDeleteAllFilesOnSd : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_DELETE_LAST_SD, Parameterless = true)]
    public class CommandCameraDeleteLastFileOnSd : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMREA_INFORMATION, Parameterless = true)]
    public class CommandCameraInformation : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_SETTINGS, Parameterless = true)]
    public class CommandCameraSettings : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_EXTENDED_SETTINGS, Parameterless = true)]
    public class CommandCameraExtendedSettings : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_WHITE_BALANCE)]
    public class CommandCameraWhiteBalance : CommandMultiChoice<WhiteBalance, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_LOOPING_VIDEO)]
    public class CommandCameraLoopingVideo : CommandMultiChoice<LoopingVideo, LegacyCamera>
    {
        //NOTE: Camera only responds to the command at 1080@24, 1080@30, 1440@24 and 720@60
    }

    [Command(LegacyCommands.CAMERA_FRAMERATE)]
    public class CommandCameraFrameRate : CommandMultiChoice<FrameRate, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_BURSTRATE)]
    public class CommandCameraBurstRate : CommandMultiChoice<BurstRate, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_CONTINUOUS)]
    public class CommandCameraContinuousShot : CommandMultiChoice<ContinuousShot, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_DELETE_FILE)]
    public class CommandCameraDeleteFile : CommandRequest<LegacyCamera>
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

    [Command(LegacyCommands.CAMERA_LOW_LIGHT)]
    public class CommandCameraAutoLowLight : CommandBoolean<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_CAPABILITIES, Parameterless = true)]
    public class CommandCameraCapabilities : CommandRequest<LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_PHOTO_IN_VIDEO)]
    public class CommandCameraPhotoInVideo : CommandMultiChoice<PhotoInVideo, LegacyCamera>
    {
    }

    [Command(LegacyCommands.CAMERA_ONE_BUTTON_MODE)]
    public class CommandCameraOneButtonMode : CommandBoolean<LegacyCamera>
    {
    }
}