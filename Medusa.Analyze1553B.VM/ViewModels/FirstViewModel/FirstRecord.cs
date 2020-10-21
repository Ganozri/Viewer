
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Program.ByteSumCountingProgram.VM.ViewModels
{
    public class FirstRecord : BaseMainModel
    {
        public int FirstNumber => dataRecord.FirstNumber;
        public int SecondNumber => dataRecord.SecondNumber;

        private readonly MainModel dataRecord;
        public FirstRecord(MainModel dataRecord) : base(dataRecord)
        {
            this.dataRecord = dataRecord;
        }
    
    }
}
