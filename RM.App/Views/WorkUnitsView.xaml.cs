using RM.App.ViewModels;
using System.Windows.Controls;

namespace RM.App.Views
{
    /// <summary>
    /// Interaction logic for WorkUnitsView.xaml
    /// </summary>
    public partial class WorkUnitsView : UserControl
    {
        public WorkUnitsView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(DataContext is WorkUnitsViewModel viewModel)
            {
                viewModel.ReloadWorkUnitsCommand.Execute(this);
            }
        }
    }
}
