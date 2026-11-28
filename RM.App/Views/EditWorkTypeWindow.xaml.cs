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

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is EditWorkTypeWindowViewModel oldViewModel)
            {
                oldViewModel.RequestClose -= OnRequestClose;
            }

            if (e.NewValue is EditWorkTypeWindowViewModel newViewModel)
            {
                newViewModel.RequestClose += OnRequestClose;
            }
        }

        private void OnRequestClose(object sender, System.EventArgs e)
        {
            DialogResult = true;
            Close();
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
