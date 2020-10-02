using LiveCharts;
using LiveCharts.Geared;
using Medusa.Analyze1553B.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Medusa.Analyze1553B.UI
{
    /// <summary>
    /// Interaction logic for CommonPageUserControl.xaml
    /// </summary>
    /// 

     public class Rule
    {
        public int Address {get;set;}
        public DataDirection Direction {get;set;}
        public int Subaddress {get;set;}
        public int Length {get;set;}

        public string Name{get;set;}
        public string Color{get;set;}

        public Rule(int Address,DataDirection Direction,int Subaddress,int Length)
        {
            new Rule(Address,Direction,Subaddress,Length,"","White");
        }

        public Rule(int Address,DataDirection Direction,int Subaddress,int Length,string Name,string Color)
        {
            this.Address = Address;
            this.Direction = Direction;
            this.Subaddress = Subaddress; 
            this.Length = Length;

            this.Name = Name;
            this.Color = Color;
        }

    }

    public partial class CommonPageUserControl : UserControl
    {
        public CommonPageUserControl()
        {
           
        }

        public void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
         
        }

    }
}
