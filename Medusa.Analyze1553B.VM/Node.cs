using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Medusa.Analyze1553B.VM
{
    public class Node
    {
        public string Name { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }

        public Func<DataRecord, bool> FiltrationCondition = (x) => true;
        public Type type { get;set;}
    }
}
