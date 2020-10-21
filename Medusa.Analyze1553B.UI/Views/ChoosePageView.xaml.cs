using MahApps.Metro.Controls;
using Program.ByteSumCountingProgram.UIServices;
using Program.ByteSumCountingProgram.VMServices;
using Program.ByteSumCountingProgram.VM;
using SynchronizationContext = System.Threading.SynchronizationContext;

namespace Program.ByteSumCountingProgram.UI.Views
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
