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

namespace RM.App.Extensions
{
    /// <summary>
    /// Расширение функционала объекта <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация клиентов Http.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterHttpClients(this IServiceCollection services)
        {
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

            return services;
        }

        /// <summary>
        /// Регистрация сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IWorkTypeService, WorkTypeService>();
            services.AddTransient<IWorkUnitService, WorkUnitService>();
            services.AddTransient<IPerformerService, PerformerService>();
            services.AddSingleton<INotificationService, MessageBoxNotificationService>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IRegionService, RegionService>((provider) =>
            {
                var tabControl = provider.GetService<ApplicationWindow>().ContentTabs;
                var regionConfigurations = provider.GetService<Dictionary<Type, RegionItemConfiguration>>();
                return new RegionService(tabControl, regionConfigurations);
            }
            );

            return services;
        }

        /// <summary>
        /// Регистрация объектов интерфейса пользователя.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterUiObjects(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationWindow>();
            services.AddTransient<AboutWindow>();
            services.AddTransient<WorkUnitsView>();
            services.AddTransient<WorkTypesView>();
            services.AddTransient<PerformersView>();

            return services;
        }

        /// <summary>
        /// Регистрация конфигураций объектов интерфейса пользователя.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterUiConfigurations(this IServiceCollection services)
        {
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

            return services;
        }

        /// <summary>
        /// Регистрация моделей представления.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationWindowViewModel>();
            services.AddTransient<AboutWindowViewModel>();

            return services;
        }

        /// <summary>
        /// Регистрация фабрик.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            services.AddSingleton<IWorkUnitsViewModelFactory, WorkUnitsViewModelFactory>();
            services.AddSingleton<IWorkTypesViewModelFactory, WorkTypesViewModelFactory>();
            services.AddSingleton<IPerformersViewModelFactory, PerformersViewModelFactory>();
            services.AddSingleton<IAboutWindowViewModelFactory, AboutWindowViewModelFactory>();

            return services;
        }

        /// <summary>
        /// Регистрация профилей настроек преобразований объектов через AutoMapper.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection RegisterAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(config => { },
                typeof(Mapping.Profiles.WorkUnitMappingProfile));
            services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

            return services;
        }
    }
}
