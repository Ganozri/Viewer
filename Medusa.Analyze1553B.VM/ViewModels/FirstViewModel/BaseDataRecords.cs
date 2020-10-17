using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    public abstract class BaseDataRecords 
    {
        private readonly DataRecord dataRecord;
        public BaseDataRecords(DataRecord dataRecord)
        {
            this.dataRecord = dataRecord;
        }
    }
}
