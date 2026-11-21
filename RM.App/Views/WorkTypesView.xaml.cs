using RM.App.ViewModels;
using System.Windows.Controls;

namespace RM.App.Views
{
    /// <summary>
    /// Interaction logic for WorkTypesView.xaml
    /// </summary>
    public partial class WorkTypesView : UserControl
    {
        public WorkTypesView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is WorkTypesViewModel viewModel)
            {
                viewModel.UpdateWorkTypesCommand.Execute(this);
            }
        }
    }
}
