using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public interface IPageViewModel 
    {
        string Name { get; }
        //
        object[] dataRecordsList { get; set; }
        object[] Data { get; set; }
        int currentRow { get; set; }
        int rowCount { get; set; }

        States currentState { get; set; }

        enum States
        {
            Red,
            Yellow,
            Green
        }
        //

    }
}

