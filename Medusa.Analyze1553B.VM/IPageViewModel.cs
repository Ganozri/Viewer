﻿using DynamicData.Binding;
using Medusa.Analyze1553B.Common;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using System.Collections.ObjectModel;

namespace Medusa.Analyze1553B.VM
{
    public interface IPageViewModel 
    {
        string Name { get;set;}
        ObservableCollection<Node> Nodes { get; set; }
        ObservableCollection<Item> Items { get;set;}
        Item SelectedItem { get;set;}
        ObservableCollectionExtended<DataRecord> DataRecords { get;}

        IDialogService DialogService { get; set; }
        
    }
}

