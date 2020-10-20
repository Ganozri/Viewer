using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VMServices
{
    public interface IDataService
    {
        MainModel[] GetData(string path, object ViewModel);
        object GetData(string path);
    }
}
