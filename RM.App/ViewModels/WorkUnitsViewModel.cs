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
    /// Модель представления для представления <see cref="WorkUnitsView"/>.
    /// </summary>
    public class WorkUnitsViewModel : LoadingViewModelBase
    {
        private readonly IWorkUnitService _workUnitService;
        private readonly IMapper<WorkUnitResponse, WorkUnit> _workUnitMapper;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Получает единицы работ.
        /// </summary>
        /// <value>Единицы работ.</value>
        public ObservableCollection<WorkUnit> WorkUnits { get; }

        /// <summary>
        /// Получает команду перезагрузки списка единиц работ из сервиса.
        /// </summary>
        /// <value>Команда перезагрузки списка единиц работ.</value>
        public RelayCommand ReloadWorkUnitsCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkUnitsViewModel"/>.
        /// </summary>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="workUnitMapper">Маппер единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkUnitsViewModel(IWorkUnitService workUnitService,
                                  IMapper<WorkUnitResponse, WorkUnit> workUnitMapper,
                                  INotificationService notificationService)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
            _workUnitMapper = workUnitMapper
                ?? throw new ArgumentNullException(nameof(workUnitMapper));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));

            WorkUnits = new ObservableCollection<WorkUnit>();

            ReloadWorkUnitsCommand = new RelayCommand(ExecuteReloadWorkUnits);
        }

        private async void ExecuteReloadWorkUnits(object obj)
        {
            IsLoading = true;

            try
            {
                var workUnitDtos = await _workUnitService.GetAllAsync();

                WorkUnits.Clear();

                foreach (var item in workUnitDtos)
                {
                    var workUnit = _workUnitMapper.Map(item);

                    WorkUnits.Add(workUnit);
                }
            }
            catch
            {
                _notificationService.Show(
                    "Не удалось загрузить единицы работ. Попробуйте повторить позже.",
                    "Ошибка при загрузке единиц работ",
                    icon: Infrastructure.Wpf.Enums.NotificationIcon.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
