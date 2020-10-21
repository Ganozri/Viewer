
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Program.ByteSumCountingProgram.VM.ViewModels
{
    public abstract class BaseMainModel 
    {
        private readonly MainModel dataRecord;

        public BaseMainModel(MainModel dataRecord)
        {
            this.dataRecord = dataRecord;
        }

    }
}
