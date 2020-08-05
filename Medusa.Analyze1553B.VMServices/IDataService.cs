using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public interface IDataService
    {
        object[] dataRecordsList();
        object[] dataRecordsList(string path);
        object[] Data(int currentRow, object[] currentData);
    }
}
