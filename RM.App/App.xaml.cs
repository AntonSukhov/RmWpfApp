using Infrastructure.Mapping.AutoMapper;
using Infrastructure.Mapping.Interfaces;
using Infrastructure.Wpf.Configurations;
using Infrastructure.Wpf.Services.Notifications;
using Infrastructure.Wpf.Services.Regions;
using Infrastructure.Wpf.Services.Windows;
using Microsoft.Extensions.DependencyInjection;
using RM.Api.Services;
using RM.App.Factories;
using RM.App.ViewModels;
using RM.App.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            services.AddHttpClient("RmWebApi", client =>
            {
                const string keyName = "RmWebApiBaseUrl";
                var webApiUrl = ConfigurationManager.AppSettings[keyName];

                if (string.IsNullOrWhiteSpace(webApiUrl))
                {
                    throw new InvalidOperationException($"Ключ '{keyName}' не задан в app.config.");
                }

                client.BaseAddress = new Uri(webApiUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddTransient<IWorkTypeService, WorkTypeService>();
            services.AddTransient<IWorkUnitService, WorkUnitService>();
            services.AddTransient<IPerformerService, PerformerService>();

            services.AddSingleton<ApplicationWindow>();
            services.AddTransient<AboutWindow>();
            services.AddTransient<WorkUnitsView>();
            services.AddTransient<WorkTypesView>();
            services.AddTransient<PerformersView>();

            services.AddSingleton((provider) =>
                    new Dictionary<Type, WindowConfiguration>
                    {
                        {
                            typeof(AboutWindowViewModel),
                            new WindowConfiguration
                            {
                                OwnerFactory = () => provider.GetService<ApplicationWindow>(),
                                WindowFactory = () => provider.GetService<AboutWindow>()
                            }
                        }
                    }
            );
            services.AddSingleton((provider) =>
                    new Dictionary<Type, RegionItemConfiguration>
                {
                    {
                        typeof(WorkUnitsViewModel),
                        new RegionItemConfiguration
                        {
                            ViewFactory = () => provider.GetService<WorkUnitsView>(),
                            Title = "Единицы работ"
                        }
                    },
                    {
                        typeof(WorkTypesViewModel),
                        new RegionItemConfiguration
                        {
                            ViewFactory = () => provider.GetService<WorkTypesView>(),
                            Title = "Виды работ"
                        }
                    },
                    {
                        typeof(PerformersViewModel),
                        new RegionItemConfiguration
                        {
                            ViewFactory = () => provider.GetService<PerformersView>(),
                            Title = "Исполнители договоров"
                        }
                    }
                }
            );

            services.AddSingleton<INotificationService, MessageBoxNotificationService>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IRegionService, RegionService>((provider) =>
                {
                    var tabControl = provider.GetService<ApplicationWindow>().ContentTabs;
                    var regionConfigurations = provider.GetService<Dictionary<Type, RegionItemConfiguration>>();
                    return new RegionService(tabControl, regionConfigurations);
                }
            );

            services.AddSingleton<IWorkUnitsViewModelFactory, WorkUnitsViewModelFactory>();
            services.AddSingleton<IWorkTypesViewModelFactory, WorkTypesViewModelFactory>();
            services.AddSingleton<IPerformersViewModelFactory, PerformersViewModelFactory>();
            services.AddSingleton<IAboutWindowViewModelFactory, AboutWindowViewModelFactory>();
            services.AddSingleton<ApplicationWindowViewModel>();
            services.AddTransient<AboutWindowViewModel>();

            services.AddAutoMapper(config => { },
                typeof(Mapping.Profiles.WorkUnitMappingProfile));
            services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

            return services;
        }
    }
}
