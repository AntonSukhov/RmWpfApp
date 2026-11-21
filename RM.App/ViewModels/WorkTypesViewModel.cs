using Infrastructure.Foundation.Commands;
using Infrastructure.Mvvm.ViewModels;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.Services;
using RM.App.Models;
using RM.App.Views;
using System;
using System.Collections.ObjectModel;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для представления <see cref="WorkTypesView"/>.
    /// </summary>
    public class WorkTypesViewModel : LoadingViewModelBase
    {
        private readonly IWorkTypeService _workTypeService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Получает виды работ.
        /// </summary>
        /// <value>Виды работ.</value>
        public ObservableCollection<WorkType> WorkTypes { get; }

        /// <summary>
        /// Получает команду обновления видов работ.
        /// </summary>
        /// <value>Команда обновления видов работ.</value>
        public RelayCommand UpdateWorkTypesCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModel"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис видов работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        public WorkTypesViewModel(IWorkTypeService workTypeService, INotificationService notificationService)
        {
            _workTypeService = workTypeService ?? throw new ArgumentNullException(nameof(workTypeService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));

            WorkTypes = new ObservableCollection<WorkType>();

            UpdateWorkTypesCommand = new RelayCommand(ExecuteUpdateWorkTypes);
        }

        private async void ExecuteUpdateWorkTypes(object obj)
        {
            IsLoading = true;

            try
            {
                var workTypeDtos = await _workTypeService.GetAllAsync(
                    new Api.DTOs.Requests.PageOptionsRequest 
                    { 
                        PageNumber = 1, 
                        PageSize = 1000
                    });

                WorkTypes.Clear();

                foreach (var item in workTypeDtos)
                {

                    var workType = new WorkType
                    {
                        Id = item.Id,
                        Name = item.Name
                    };

                    if (item.WorkUnit != null)
                    {
                        workType.WorkUnit = new WorkUnit 
                        { 
                            Id = item.WorkUnit.Id, 
                            Name = item.WorkUnit.Name 
                        };
                    }

                    WorkTypes.Add(workType);
                }
            }
            catch
            {
                _notificationService.Show("Не удалось загрузить виды работ. Попробуйте повторить позже.",
                    "Ошибка при загрузке видов работ", icon: Infrastructure.Wpf.Enums.NotificationIcon.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
