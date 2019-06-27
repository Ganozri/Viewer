using System;
using Peleng.Medusa.Common.Checkers;

namespace Peleng.Medusa.Analyze1553B.Common
{
    public struct ResponseWord
    {
        public ushort Value { get; }

        public int Address => Value >> 11;

        public int Flags => Value & ((1 << 11) - 1);

        public bool GenericError => (Value & 0x400) != 0;

        public bool Response => (Value & 0x200) != 0;

        public bool ServiceRequest => (Value & 0x100) != 0;

        public bool BroadcastReceived => (Value & 0x10) != 0;

        public bool Busy => (Value & 0x8) != 0;

        public bool DeviceError => (Value & 0x4) != 0;

        public bool ControlAccepted => (Value & 0x2) != 0;

        public bool EndPointError => (Value & 0x1) != 0;

        public bool HasError => (Value & 0x405) != 0;

        public ResponseWord(ushort value)
        {
            Value = value;
        }

        public ResponseWord(int address, int errors)
        {
            Value = BuildResponseWord(address, errors).Value;
        }

        public static ResponseWord BuildResponseWord(int address, int errors)
        {
            address.CheckRange(0, 0x1E, nameof(address));
            errors.CheckMask(0x7FF, nameof(errors));
            return new ResponseWord((ushort)((address << 11) | errors));
        }
    }
}