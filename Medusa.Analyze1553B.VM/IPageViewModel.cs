using System;
using System.Collections.Generic;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public interface IPageViewModel 
    {
        string Name { get; set; }
        //
        object[] dataRecordsList { get; set; }
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

