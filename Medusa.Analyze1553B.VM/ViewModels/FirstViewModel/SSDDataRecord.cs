using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public class SSDDataRecord : BaseDataRecord
    {
        public long Index => dataRecord.Index;

        public double MonitorTime => dataRecord.MonitorTime;

        public BusChannel Channel => dataRecord.Channel;

        public Error Error => dataRecord.Error;


        private readonly DataRecord dataRecord;
        public SSDDataRecord(DataRecord dataRecord) : base(dataRecord)
        {
            this.dataRecord = dataRecord;
        }
    
    }
}
