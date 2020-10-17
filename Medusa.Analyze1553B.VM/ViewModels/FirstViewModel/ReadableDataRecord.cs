using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class ReadableDataRecord : BaseDataRecords
    {
        public long Index => dataRecord.Index;

        public double MonitorTime => dataRecord.MonitorTime;

        public Error Error => dataRecord.Error;
        public int Value => dataRecord.Cw1.Value;
        public int Address => dataRecord.Cw1.Address;
        public DataDirection Direction => dataRecord.Cw1.Direction;
        public int Subaddress => dataRecord.Cw1.Subaddress;
        public int Length => dataRecord.Cw1.Length;

        public int Cw2 => dataRecord.Cw2.Value.Value;

        public int Rw1 => dataRecord.Rw1.Value.Value;

        public int Rw2 => dataRecord.Rw2.Value.Value;

        public ushort D0 => dataRecord.Data[0];
        public ushort D1 => dataRecord.Data[1];
        public ushort D2 => dataRecord.Data[2];
        public ushort D3 => dataRecord.Data[3];
        public ushort D4 => dataRecord.Data[4];
        public ushort D5 => dataRecord.Data[5];
        public ushort D6 => dataRecord.Data[6];
        public ushort D7 => dataRecord.Data[7];
        public ushort D8 => dataRecord.Data[8];
        public ushort D9 => dataRecord.Data[9];
                     
        public ushort D10 => dataRecord.Data[10];
        public ushort D11 => dataRecord.Data[11];
        public ushort D12 => dataRecord.Data[12];
        public ushort D13 => dataRecord.Data[13];
        public ushort D14 => dataRecord.Data[14];
        public ushort D15 => dataRecord.Data[15];
        public ushort D16 => dataRecord.Data[16];
        public ushort D17 => dataRecord.Data[17];
        public ushort D18 => dataRecord.Data[18];
        public ushort D19 => dataRecord.Data[19];
                     
        public ushort D20 => dataRecord.Data[20];
        public ushort D21 => dataRecord.Data[21];
        public ushort D22 => dataRecord.Data[22];
        public ushort D23 => dataRecord.Data[23];
        public ushort D24 => dataRecord.Data[24];
        public ushort D25 => dataRecord.Data[25];
        public ushort D26 => dataRecord.Data[26];
        public ushort D27 => dataRecord.Data[27];
        public ushort D28 => dataRecord.Data[28];
        public ushort D29 => dataRecord.Data[29];
                     
        public ushort D30 => dataRecord.Data[30];
        public ushort D31 => dataRecord.Data[31];



        private readonly DataRecord dataRecord;
        public ReadableDataRecord(DataRecord dataRecord) : base(dataRecord)
        {
            this.dataRecord = dataRecord;
        }
    }
}
