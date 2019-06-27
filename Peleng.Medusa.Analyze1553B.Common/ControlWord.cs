using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Peleng.Medusa.Common.Checkers;

namespace Peleng.Medusa.Analyze1553B.Common
{
    public struct ControlWord
    {
        public ushort Value { get; }

        public int Address => Value >> 11;

        public DataDirection Direction => (DataDirection) ((Value >> 10) & 1);

        public int Subaddress => (Value >> 5) & 0x1F;

        public int Length => (Value & 0x1F).Replace(0, 32);

        public CommandCode.Raw CommandCode => GetCommandCode();

        private CommandCode.Raw GetCommandCode()
        {
            if (Subaddress == (int)CommandSubaddress.Zero || Subaddress == (int)CommandSubaddress.Ones)
            {
                return (CommandCode.Raw) (Value & 0x1F);
            }

            throw new InvalidOperationException();
        }

        public ControlWord(ushort value)
        {
            Value = value;
        }

        public ControlWord(int address, DataDirection direction, int subaddress, int length)
        {
            length.CheckRange(1, 32, nameof(length));
            Value = BuildCommand(address, direction, subaddress, length == 32 ? 0 : 1).Value;
        }

        public ControlWord(int address, DataDirection direction, CommandCode.Raw code, CommandSubaddress subaddress = CommandSubaddress.Ones)
        {
            Value = BuildCommand(address, direction, (int)subaddress, (int)code).Value;
        }

        public ControlWord(int address, CommandCode.WithDirection codeWithDirection, CommandSubaddress subaddress = CommandSubaddress.Ones)
        {
            Value = BuildCommand(address, (int)subaddress, (int)codeWithDirection).Value;
        }


        public static ControlWord BuildCommand(int address, DataDirection direction, int subaddress, int lengthOrCode)
        {
            address.CheckMask(0x1F, nameof(address));
            subaddress.CheckMask(0x1F, nameof(subaddress));
            lengthOrCode.CheckMask(0x1F, nameof(lengthOrCode));

            return new ControlWord((ushort) ((address << 11) | ((int) direction << 10) | (subaddress << 5) | lengthOrCode));
        }


        private static ControlWord BuildCommand(int address, int subaddress, int codeWithDirection)
        {
            address.CheckMask(0x1F, nameof(address));
            subaddress.CheckList(nameof(subaddress), 0, 0x1F);
            codeWithDirection.CheckMask(0x41F, nameof(codeWithDirection));

            return new ControlWord((ushort)((address << 11) | (subaddress << 5) | codeWithDirection));
        }
    }

    public static class CommandCode
    {
        public enum WithDirection
        {
            DynamicBusControl = 0x400,
            Synchronize = 0x401,
            TransmitStatusWord = 0x402,
            InitiateSelfTest = 0x403,
            TransmitterShutdown = 0x404,
            OverrideTransmitterShutdown = 0x405,
            InhibitTerminalFlagBit = 0x406,
            OverrideInhibitTerminalFlagBit = 0x407,
            ResetRemoteTerminal = 0x408,
            TransmitVectorWord = 0x410,
            SynchronizeWithDataWord = 0x011,
            TransmitLastCommandWord = 0x412,
            TransmitBuiltInTestWord = 0x413,
        }

        public enum Raw
        {
            DynamicBusControl = 0x00,
            Synchronize = 0x01,
            TransmitStatusWord = 0x02,
            InitiateSelfTest = 0x03,
            TransmitterShutdown = 0x04,
            OverrideTransmitterShutdown = 0x05,
            InhibitTerminalFlagBit = 0x06,
            OverrideInhibitTerminalFlagBit = 0x07,
            ResetRemoteTerminal = 0x08,
            TransmitVectorWord = 0x10,
            SynchronizeWithDataWord = 0x11,
            TransmitLastCommandWord = 0x12,
            TransmitBuiltInTestWord = 0x13,
        }

        public static Raw ToRaw(this WithDirection command)
        {
            return (Raw)((int)command & ~0x400);
        }

        public static WithDirection ToDirection(this Raw command)
        {
            if (ToDirectionMap.TryGetValue(command, out var value))
            {
                return value;
            }

            return (WithDirection) (int) command;
        }

        private static readonly IDictionary<Raw, WithDirection> ToDirectionMap = FillToDirectionMap();

        private static Dictionary<Raw, WithDirection> FillToDirectionMap()
        {
            var rawFields =
                typeof(Raw).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(f => !f.IsSpecialName)
                    .ToDictionary(f => f.Name, f => (Raw)f.GetValue(null));

            var withDirectionFields =
                typeof(WithDirection).GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(f => !f.IsSpecialName)
                    .ToDictionary(f => f.Name, f => (WithDirection)f.GetValue(null));

            return rawFields.ToDictionary(
                raw => raw.Value,
                raw => withDirectionFields[raw.Key]
            );
        }
    }
}