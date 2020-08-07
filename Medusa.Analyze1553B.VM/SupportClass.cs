using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ReactiveUI;
using static Medusa.Analyze1553B.VM.IPageViewModel;
using ReactiveUI.Fody.Helpers;

namespace Medusa.Analyze1553B.VM
{
    public class SupportClass : ReactiveObject
    {

        //
        public string Name { get; set; }
        [Reactive] public object[] dataRecordsList { get; set; }
        [Reactive] public object[] Data { get; set; }
        [Reactive] public int currentRow { get; set; }
        [Reactive] public int rowCount { get; set; }
        //
        public States currentState { get; set; }

        public SupportClass()
        {
            currentState = States.Red;
        }
    }
}
