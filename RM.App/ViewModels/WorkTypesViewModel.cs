using Infrastructure.Foundation.Commands;
using Infrastructure.Mapping.Interfaces;
using Infrastructure.Mvvm.ViewModels;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.DTOs.Responses;
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
        private readonly IMapper<WorkTypeResponse, WorkType> _workTypeMapper;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Получает виды работ.
        /// </summary>
        /// <value>Виды работ.</value>
        public ObservableCollection<WorkType> WorkTypes { get; }

        /// <summary>
        /// Получает команду перезагрузки списка видов работ из сервиса.
        /// </summary>
        /// <value>Команда перезагрузки списка видов работ.</value>
        public RelayCommand ReloadWorkTypesCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModel"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис видов работ.</param>
        /// <param name="workTypeMapper">Маппет вида работы.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkTypesViewModel(IWorkTypeService workTypeService,
                                  IMapper<WorkTypeResponse, WorkType> workTypeMapper,
                                  INotificationService notificationService)
        {
            _workTypeService = workTypeService 
                ?? throw new ArgumentNullException(nameof(workTypeService));
            _workTypeMapper = workTypeMapper
                ?? throw new ArgumentNullException(nameof(workTypeMapper));
            _notificationService = notificationService 
                ?? throw new ArgumentNullException(nameof(notificationService));

            WorkTypes = new ObservableCollection<WorkType>();

            ReloadWorkTypesCommand = new RelayCommand(ExecuteReloadWorkTypes);
        }

        private async void ExecuteReloadWorkTypes(object obj)
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
                    var workType = _workTypeMapper.Map(item);

                    WorkTypes.Add(workType);
                }
            }
            catch
            {
                _notificationService.Show(
                    "Не удалось загрузить виды работ. Попробуйте повторить позже.",
                    "Ошибка при загрузке видов работ", 
                    icon: Infrastructure.Wpf.Enums.NotificationIcon.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
