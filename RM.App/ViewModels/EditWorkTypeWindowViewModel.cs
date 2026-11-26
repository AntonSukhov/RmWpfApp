using Infrastructure.Foundation.Commands;
using Infrastructure.Mapping.Interfaces;
using Infrastructure.Mvvm.ViewModels;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.DTOs.Responses;
using RM.Api.Services;
using RM.App.Enums;
using RM.App.Models;
using RM.App.Views;
using System;
using System.Collections.ObjectModel;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для окна <see cref="EditWorkTypeWindow"/>.
    /// </summary>
    public class EditWorkTypeWindowViewModel : LoadingViewModelBase
    {
        private readonly IWorkUnitService _workUnitService;
        private readonly INotificationService _notificationService;
        private readonly IMapper<WorkUnitResponse, WorkUnit> _workUnitMapper;

        /// <summary>
        /// Получает режим открытия окна.
        /// </summary>
        /// <value>Режим открытия окна.</value>
        public DialogMode DialogMode { get; }

        /// <summary>
        /// Получает заголовок окна в зависимости от режима открытия.
        /// </summary>
        /// <value>Заголовок окна.</value>
        public string WindowTitle => DialogMode == DialogMode.Edit
            ? "Редактирование вида работ"
            : "Создание вида работ";

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
        /// Инициализирует экземпляр <see cref="EditWorkTypeWindowViewModel"/>.
        /// </summary>
        /// <param name="dialogMode">Режим открытия окна.</param>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="workUnitMapper">Маппер единицы работ.</param>
        public EditWorkTypeWindowViewModel(DialogMode dialogMode,
                                           IWorkUnitService workUnitService,
                                           INotificationService notificationService,
                                           IMapper<WorkUnitResponse, WorkUnit> workUnitMapper)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
            _workUnitMapper = workUnitMapper
                ?? throw new ArgumentNullException(nameof(workUnitMapper));

            DialogMode = dialogMode;
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