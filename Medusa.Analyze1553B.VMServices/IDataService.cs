using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Program.ByteSumCountingProgram.VMServices
{
    public interface IDataService
    {
        MainModel[] GetData(string path, object ViewModel);
        object GetData(string path);
    }
}
