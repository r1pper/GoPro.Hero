using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    static class HeroCommands
    {
        public const string CAMERA_TIME = "/camera/TM";
        public const string CAMERA_GET_NAME = "/camera/cn";
        public const string CAMERA_SET_NAME = "/camera/CN";
        public const string CAMERA_GET_HLS_SEGMENT = "/camera/hs";
        public const string CAMERA_SET_HLS_SEGMENT = "/camera/HS";
        //public const string CAMERA_VIDEO_RESOLUTION = "/camera/VR";
        public const string CAMERA_VIDEO_RESOLUTION="/camera/VV";
        public const string CAMERA_ORIENTATION="/camera/UP"; //up-down
        public const string CAMERA_TIMELAPSE_TI="/camera/TI";
        public const string CAMERA_BEEP = "/camera/BS";
        public const string CAMERA_PROTUNE = "/camera/PT";
        public const string CAMERA_PHOTO_RESOLUTION = "/camera/PR";
        public const string CAMERA_OSD = "/camera/DS";
        public const string CAMERA_VIDEO_MODE = "/camera/VM"; //ntsc-pal
        public const string CAMERA_MODE = "/camera/CM";
        public const string CAMREA_LOCATE = "/camera/LL";
        public const string CAMREA_LIVE_PREVIEW = "/camera/PV";
        public const string CAMREA_LED_BLINK = "/camera/LB";
        public const string CAMREA_FIELD_OF_VIEW = "/camera/FV";
        public const string CAMREA_EXPOSURE = "/camera/EX";
        public const string CAMREA_DEFAULT_MODE = "/camera/DM";
        public const string CAMREA_AUTO_POWER_OFF = "/camera/AO";
        public const string CAMREA_DELETE_ALL_SD = "/camera/DA";
        public const string CAMERA_DELETE_LAST_SD = "/camera/DL";
        public const string CAMREA_INFORMATION = "/camera/cv";
        public const string CAMERA_SETTINGS = "/camera/se";
        public const string CAMERA_EXTENDED_SETTINGS = "/camera/sx";
        public const string CAMERA_WHITE_BALANCE = "/camera/WB"; //only with protune
        public const string CAMERA_LOOPING_VIDEO = "/camera/LO";
        public const string CAMERA_FRAMERATE = "/camera/FS";
        public const string CAMERA_BURSTRATE = "/camera/BU";
        public const string CAMERA_CONTINUOUS = "/camera/CS";

        public const string BACPAC_SHUTTER = "/bacpac/SH";
        public const string BACPAC_POWER = "/bacpac/PW";
        public const string BACPAC_WIFI_MODE = "/bacpac/WI";
        public const string BACPAC_GET_PASSWORD = "/bacpac/sd";
        public const string BACPAC_INFORMATION = "/bacpac/cv";
        public const string BACPAC_STATUS = "/bacpac/se";



    }
}
