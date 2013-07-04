using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api
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
        public bool CameraI2cError { get; private set; }
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

                this.BacpacBattery = binReader.ReadByte();
                this.WifiMode = binReader.ReadByte();
                this.BluetoothMode = binReader.ReadByte();
                this.Rssi = binReader.ReadEnum<SignalStrength>();
                this.ShutterStatus = binReader.ReadByte();
                this.AutoPowerOff = binReader.ReadByte();
                this.BluetoothAudioChannel = binReader.ReadByte();
                this.FileServer = binReader.ReadByte();
                this.CameraPower = binReader.ReadByte()>0;
                this.CameraI2cError = binReader.ReadByte()>0;
                this.CameraReady = binReader.ReadByte()>0;
                this.CameraModel = binReader.ReadEnum<Model>();
                this.CameraProtocolVersion = binReader.ReadByte();
                this.CameraAttached = binReader.ReadByte()>0;
                this.BossReady = binReader.ReadByte()>0;
            }
        }
    }
}
