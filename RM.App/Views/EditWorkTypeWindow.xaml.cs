using RM.App.ViewModels;
using System.Windows;

namespace RM.App.Views
{
    /// <summary>
    /// Interaction logic for EditWorkTypeWindow.xaml
    /// </summary>
    public partial class EditWorkTypeWindow : Window
    {
        public EditWorkTypeWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditWorkTypeWindowViewModel viewModel)
            {
                viewModel.ReloadWorkUnitsCommand.Execute(this);
            }
        }
    }
}
