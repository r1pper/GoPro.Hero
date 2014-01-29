using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GoPro.Hero
{
    public class CameraCapabilities
    {
        [Capability(Position = 0, LocalIndex = 30, Mask = 1)]
        public bool CamreaRoll { get; private set; }
        [Capability(Position=1,LocalIndex=30,Mask=2)]
        public bool OtaUpdateable { get; private set; }
        [Capability(Position = 2, LocalIndex = 30, Mask = 4)]
        public bool Ltp { get; private set; }

        private CameraCapabilities()
        {
        }

        internal static CameraCapabilities Parse(byte[] bytes, int capabilityLevel=1)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            var status = bytes[bytes.Length - 1];
            if (status == 0)
                return null;

            var cameraCapabilities = new CameraCapabilities();
            var type = cameraCapabilities.GetType();

            var properties=type.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(CapabilityAttribute), true);
                if (attributes.Length == 0)
                    continue;

                var capability = attributes[0] as CapabilityAttribute;

                SetCapability(bytes, capabilityLevel, cameraCapabilities, property, capability);
            }

            return cameraCapabilities;
        }

        private static void SetCapability
            (
                byte[] bytes, int capabilityLevel,
                CameraCapabilities cameraCapabilities,
                PropertyInfo property,
                CapabilityAttribute capability
            )
        {
            if (capabilityLevel + capability.LocalIndex < bytes.Length &&
                (bytes[capabilityLevel + capability.LocalIndex] & capability.Mask) == capability.Mask)
                property.SetValue(cameraCapabilities, true, null);
        }
    }
}
