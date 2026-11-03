using RM.App.Services;
using RM.App.ViewModels;
using RM.App.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RM.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var applicationWindow = new ApplicationWindow();

            var windowConfigurations = new Dictionary<Type, WindowConfiguration>
            {
                {
                    typeof(AboutWindowViewModel),
                    new WindowConfiguration
                    {
                        OwnerFactory = () => applicationWindow,
                        WindowFactory = () => new AboutWindow()
                    }
                }
            };

            var windowService = new WindowService(windowConfigurations);

            var applicationWindowViewModel = new ApplicationWindowViewModel(windowService);
            applicationWindow.DataContext = applicationWindowViewModel;
            Current.MainWindow = applicationWindow;

            applicationWindow.Show();
        }
    }
}
