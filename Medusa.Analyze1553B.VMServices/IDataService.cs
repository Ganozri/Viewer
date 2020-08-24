﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public interface IDataService
    {
        object[] updateDataRerordsList(string input);
        object[] updateDataRerordsList(object[] currentData, string input);
        //
        object[] dataRecordsList<T>(string path);
        object[] Data(int currentRow, object[] currentData);

     
    }
}
