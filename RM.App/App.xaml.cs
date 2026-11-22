using Microsoft.Extensions.DependencyInjection;
using RM.App.Extensions;
using RM.App.ViewModels;
using RM.App.Views;
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

            var services = GetServices();

            var serviceProvider = services.BuildServiceProvider();

            var applicationWindow = serviceProvider.GetService<ApplicationWindow>();
            var applicationWindowViewModel = serviceProvider.GetService<ApplicationWindowViewModel>();
            applicationWindow.DataContext = applicationWindowViewModel;
            Current.MainWindow = applicationWindow;

            applicationWindow.Show();
        }

        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();

            services.RegisterHttpClients();
            services.RegisterServices();
            services.RegisterUiObjects();
            services.RegisterUiConfigurations();
            services.RegisterFactories();
            services.RegisterViewModels(); ;
            services.RegisterAutoMapperProfiles();

            return services;
        }
    }
}
