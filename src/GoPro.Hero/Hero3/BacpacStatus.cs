using System.IO;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Hero3
{
    public class BacpacStatus
    {
        public byte BacpacBattery { get; private set; }
        public byte WifiMode { get; private set; }
        public byte BluetoothMode { get; private set; }
        public SignalStrength Rssi { get; private set; }
        public byte ShutterStatus { get; private set; }
        public byte AutoPowerOff { get; private set; }
        public byte BluetoothAudioChannel { get; private set; }
        public byte FileServer { get; private set; }
        public bool CameraPower { get; private set; }
        public bool CameraI2CError { get; private set; }
        public bool CameraReady { get; private set; }
        public Model CameraModel { get; private set; }
        public byte CameraProtocolVersion { get; private set; }
        public bool CameraAttached { get; private set; }
        public bool BossReady { get; private set; }

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                BacpacBattery = binReader.ReadByte();
                WifiMode = binReader.ReadByte();
                BluetoothMode = binReader.ReadByte();
                Rssi = binReader.ReadEnum<SignalStrength>();
                ShutterStatus = binReader.ReadByte();
                AutoPowerOff = binReader.ReadByte();
                BluetoothAudioChannel = binReader.ReadByte();
                FileServer = binReader.ReadByte();
                CameraPower = binReader.ReadByte() > 0;
                CameraI2CError = binReader.ReadByte() > 0;
                CameraReady = binReader.ReadByte() > 0;
                CameraModel = binReader.ReadEnum<Model>();
                CameraProtocolVersion = binReader.ReadByte();
                CameraAttached = binReader.ReadByte() > 0;
                BossReady = binReader.ReadByte() > 0;
            }
        }
    }
}