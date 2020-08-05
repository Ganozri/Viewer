using System.Collections.Generic;
using System.Linq;
using Olympus.Checkers;

namespace Medusa.Analyze1553B.Common
{
    public sealed class DataRecord
    {
        public long Index { get; internal set; }

        public double MonitorTime { get; internal set; }

        public BusChannel Channel { get; internal set; }

        public Error Error { get; internal set; }

        public ControlWord Cw1 { get; internal set; }

        public ControlWord? Cw2 { get; internal set; }

        public ResponseWord? Rw1 { get; internal set; }

        public ResponseWord? Rw2 { get; internal set; }

        public ushort[] Data { get; internal set; }

        public ushort? WordAt(int pos)
        {
            return (Data != null && pos < Data.Length) ? Data[pos] : default(ushort?);
        }
    }

    public struct DataRecordBuilder
    { 
        private readonly DataRecord record;

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
            record = new DataRecord();

            if (index != null) record.Index = index.Value;

            if (monitorTime != null) record.MonitorTime = monitorTime.Value;

            if (channel != null) record.Channel = channel.Value;

            if (error != null) record.Error = error.Value;

            if (cw1 != null) record.Cw1 = cw1.Value;

            if (cw2 != null) record.Cw2 = cw2.Value;

            if (rw1 != null) record.Rw1 = rw1.Value;

            if (rw2 != null) record.Rw2 = rw2.Value;

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

        public DataRecordBuilder Cw1(ControlWord cw1)
        {
            record.Cw1 = cw1;
            return this;
        }

        public DataRecordBuilder Cw2(ControlWord cw2)
        {
            record.Cw2 = cw2;
            return this;
        }

        public DataRecordBuilder Rw1(ResponseWord rw1)
        {
            record.Rw1 = rw1;
            return this;
        }

        public DataRecordBuilder Rw2(ResponseWord rw2)
        {
            record.Rw2 = rw2;
            return this;
        }

        public DataRecordBuilder Data(ushort dataWord)
        {
            record.Data = new[] {dataWord};
            return this;
        }

        public DataRecordBuilder Data(IEnumerable<ushort> data)
        {
            var temp = data.ToArray(); // make a copy
            temp.Length.CheckRange(1, 32, nameof(data));
            record.Data = temp;
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
