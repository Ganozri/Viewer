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

           
        }
        


     

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int firstItemIndex = GetIndexOfFirstVisibleListItem();
            for (int i = firstItemIndex; i < firstItemIndex+100; i++)
            {          
               ChangeColorByIndex(i,Colors.AliceBlue);
            }
            //ItemCollection x = myListView;
        }

        private int GetIndexOfFirstVisibleListItem()
        {
            VirtualizingStackPanel panel = FindVisualChild<VirtualizingStackPanel>(myListView);
            if (myListView.Items.Count > 0 && panel != null)
            {
              int offset = (panel.Orientation == Orientation.Horizontal) 
                            ? (int)panel.HorizontalOffset 
                            : (int)panel.VerticalOffset;
              return offset;
            }
            else
            {
              return -1;
            }
           
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
          for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
          {
            DependencyObject child = VisualTreeHelper.GetChild(obj, i);
         
            if (child is T)
            {
              return (T)child;
            }
            else
            {
              child = FindVisualChild<T>(child);
              if (child != null)
              {
                return (T)child;
              }
            }
          }
          return null;
        }

        private void ChangeColorByIndex(int index, System.Windows.Media.Color color)
        {
            ListViewItem row = myListView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
            row.Background = new SolidColorBrush(color);
        }

        private void myListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (myListView.IsVisible)
            {

                //int firstItemIndex = GetIndexOfFirstVisibleListItem();
                //for (int i = firstItemIndex; i < firstItemIndex+100; i++)
                //{          
                //   ChangeColorByIndex(i,Colors.AliceBlue);
                //}
                //MessageBox.Show("asds");
            }
          
        }
    }
}
