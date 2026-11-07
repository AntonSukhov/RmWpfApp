using Infrastructure.Wpf.Configurations;
using Infrastructure.Wpf.Services.Regions;
using Infrastructure.Wpf.Services.Windows;
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

            var regionConfigurations = new Dictionary<Type, RegionItemConfiguration>
            {
                {
                    typeof(WorkUnitsViewModel),
                    new RegionItemConfiguration
                    {
                        ViewFactory = () => new WorkUnitsView(),
                        Title = "Единицы работ"
                    }
                },
                {
                    typeof(WorkTypesViewModel),
                    new RegionItemConfiguration
                    {
                        ViewFactory = () => new WorkTypesView(),
                        Title = "Виды работ"
                    }
                },
                {
                    typeof(PerformersViewModel),
                    new RegionItemConfiguration
                    {
                        ViewFactory = () => new PerformersView(),
                        Title = "Исполнители договоров"
                    }
                }
            };

            var windowService = new WindowService(windowConfigurations);
            var regionService = new RegionService(applicationWindow.ContentTabs, regionConfigurations);

            var applicationWindowViewModel = new ApplicationWindowViewModel(windowService, regionService);
            applicationWindow.DataContext = applicationWindowViewModel;
            Current.MainWindow = applicationWindow;

            applicationWindow.Show();
        }
    }
}
