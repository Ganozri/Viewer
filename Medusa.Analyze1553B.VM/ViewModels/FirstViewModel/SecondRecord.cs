
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
{
    class SecondRecord : BaseMainModel
    {
        public int FirstNumber => dataRecord.FirstNumber;
        public int ThirdNumber => dataRecord.ThirdNumber;


        private readonly MainModel dataRecord;
        public SecondRecord(MainModel dataRecord) : base(dataRecord)
        {
            this.dataRecord = dataRecord;
        }
    
    }
}
