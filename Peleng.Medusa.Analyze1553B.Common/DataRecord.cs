using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peleng.Medusa.Common.Checkers;

namespace Peleng.Medusa.Analyze1553B.Common
{
    public sealed class DataRecord
    {
        public long Index { get; internal set; }

        public double MonitorTime { get; internal set; }

        public BusChannel Channel { get; internal set; }

        public Error Error { get; internal set; }

        public ControlWord CW1 { get; internal set; }

        public ControlWord? CW2 { get; internal set; }

        public ResponseWord? RW1 { get; internal set; }

        public ResponseWord? RW2 { get; internal set; }

        public ushort[] Data { get; internal set; }

        public ushort? WordAt(int pos)
        {
            return (Data != null && Data.Length > pos) ? Data[pos] : default(ushort?);
        }
    }

    public sealed class DataRecordBuilder
    { 
        private readonly DataRecord record = new DataRecord();

        public DataRecordBuilder(
            long? index = null, 
            double? monitorTime = null, 
            BusChannel? channel = null,
            Error? error = null,
            ControlWord? cw1 = null,
            ControlWord? cw2 = null,
            ResponseWord? rw1 = null,
            ResponseWord? rw2 = null,
            ushort[] data = null
            )
        {

            if (index != null) record.Index = index.Value;

            if (monitorTime != null) record.MonitorTime = monitorTime.Value;

            if (channel != null) record.Channel = channel.Value;

            if (error != null) record.Error = error.Value;

            if (cw1 != null) record.CW1 = cw1.Value;

            if (cw2 != null) record.CW2 = cw2.Value;

            if (rw1 != null) record.RW1 = rw1.Value;

            if (rw2 != null) record.RW2 = rw2.Value;

            if (data != null)
            {
                Data(data);
            }
        }

        public DataRecordBuilder Index(int index)
        {
            record.Index = index;
            return this;
        }

        public DataRecordBuilder MonitorTime(double time)
        {
            record.MonitorTime = time;
            return this;
        }

        public DataRecordBuilder Channel(BusChannel channel)
        {
            record.Channel = channel;
            return this;
        }

        public DataRecordBuilder Error(Error error)
        {
            record.Error = error;
            return this;
        }

        public DataRecordBuilder CW1(ControlWord cw1)
        {
            record.CW1 = cw1;
            return this;
        }

        public DataRecordBuilder CW2(ControlWord cw2)
        {
            record.CW2 = cw2;
            return this;
        }

        public DataRecordBuilder RW1(ResponseWord rw1)
        {
            record.RW1 = rw1;
            return this;
        }

        public DataRecordBuilder RW2(ResponseWord rw2)
        {
            record.RW2 = rw2;
            return this;
        }

        public DataRecordBuilder Data(ushort[] data)
        {
            data.Length.CheckRange(1, 32, nameof(data));
            record.Data = data.ToArray(); // make a copy
            return this;
        }

        public DataRecord GetRecord()
        {
            return record;
        }

        public static implicit operator DataRecord(DataRecordBuilder builder)
        {
            return builder.GetRecord();
        }
    }
}
