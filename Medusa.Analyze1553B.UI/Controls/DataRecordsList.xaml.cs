using Medusa.Analyze1553B.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
namespace Medusa.Analyze1553B.UI.Controls
{
    /// <summary>
    /// Interaction logic for DataRecordsList.xaml
    /// </summary>
    public partial class DataRecordsList : CommonPageUserControl
    {
        public DataRecordsList()
        {
            InitializeComponent();

            myListView.ItemContainerGenerator.StatusChanged += new EventHandler(ContainerStatusChanged);
        }
        
        private void ContainerStatusChanged(object sender, EventArgs e)  
        {  
            if (myListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)  
            {

                //MessageBox.Show(x.ToString());
                //if (myListView.Items.Count>101)
                //{
                //      ChangeColorByIndex(40, Colors.AliceBlue);
                //}


              

                
                    
             
                   
                
            }  
        }

        private List<object> GetVisibleItemsFromListbox(ListView listBox)
        {
            var items = new List<object>();
        
            foreach (var item in listBox.Items)
            {
                if (IsUserVisible((ListViewItem)listBox.ItemContainerGenerator.ContainerFromItem(item), listBox))
                {
                    items.Add(item);
                }
                else if (items.Any())
                {
                    break;
                }
            }
        
            return items;
        }

        private static bool IsUserVisible(FrameworkElement element, FrameworkElement container)
        {
            if (!element.IsVisible)
                return false;
        
            Rect bounds =
                element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            var rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           //var index = 100;
           //var x = (DataRecord)myListView.Items[25];
           //ListViewItem row = myListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
          
     
           
             var x = GetVisibleItemsFromListbox(myListView);
           MessageBox.Show(x.Count.ToString());
            
        }

        private void ChangeColorByIndex(int index, System.Windows.Media.Color color)
        {
            ListViewItem row = myListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
            row.Background = new SolidColorBrush(color);
        }
    }
}
