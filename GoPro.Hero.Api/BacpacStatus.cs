using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class BacpacStatus
    {
        public byte BacpacBattery { get; private set; }
        public byte WifiMode { get; private set; }
        public byte BluetoothMode { get; private set; }
        public byte Rssi { get; private set; }
        public byte ShutterStatus { get; private set; }
        public byte AutoPowerOff { get; private set; }
        public byte BluetoothAudioChannel { get; private set; }
        public byte FileServer { get; private set; }
        public byte CameraPower { get; private set; }
        public byte CameraI2cError { get; private set; }
        public byte CameraReady { get; private set; }
        public byte CameraModel { get; private set; }
        public byte CameraProtocolVersion { get; private set; }
        public byte CameraAttached { get; private set; }
        public byte BossReady { get; private set; }

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                this.BacpacBattery = binReader.ReadByte();
                this.WifiMode = binReader.ReadByte();
                this.BluetoothMode = binReader.ReadByte();
                this.Rssi = binReader.ReadByte();
                this.ShutterStatus = binReader.ReadByte();
                this.AutoPowerOff = binReader.ReadByte();
                this.BluetoothAudioChannel = binReader.ReadByte();
                this.FileServer = binReader.ReadByte();
                this.CameraPower = binReader.ReadByte();
                this.CameraI2cError = binReader.ReadByte();
                this.CameraReady = binReader.ReadByte();
                this.CameraModel = binReader.ReadByte();
                this.CameraProtocolVersion = binReader.ReadByte();
                this.CameraAttached = binReader.ReadByte();
                this.BossReady = binReader.ReadByte();
            }
        }
    }
}
