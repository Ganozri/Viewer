
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Medusa.Analyze1553B.VM.ViewModels
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
