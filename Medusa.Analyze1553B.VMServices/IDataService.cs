using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public interface IDataService
    {
        object[] updateDataRerordsList(string input);
        object[] updateDataRerordsList(object[] currentData, string input);

        object[] GetDataByBMDLoader(string path);
        object[] GetDataByParser1553MT(string path);

        object[] GetData(string path, string ViewModelName);
    }
}
