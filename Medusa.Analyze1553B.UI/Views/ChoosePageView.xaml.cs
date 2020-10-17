using MahApps.Metro.Controls;
using Medusa.Analyze1553B.UIServices;
using Medusa.Analyze1553B.VMServices;
using Medusa.Analyze1553B.VM;
using SynchronizationContext = System.Threading.SynchronizationContext;

namespace Medusa.Analyze1553B.UI.Views
{
    /// <summary>
    /// Interaction logic for ChoosePageView.xaml
    /// </summary>
    public partial class ChoosePageView : MetroWindow, ISynchronizationContextProvider
    {


        public SynchronizationContext SynchronizationContext { get; }
        public ChoosePageView()
        {
            InitializeComponent();

            //Title = "Менеджер продуктов";
        }

        
    }
}
