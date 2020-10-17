using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public interface IDataService
    {
        DataRecord[] GetData(string path, object ViewModel);
        object GetData(string path);
    }
}
