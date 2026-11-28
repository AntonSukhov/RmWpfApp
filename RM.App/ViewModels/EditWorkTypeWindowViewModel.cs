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
        private readonly WorkUnit _emptyWorkUnit;

        /// <summary>
        /// Событие запроса на закрытие диалогово окна.
        /// </summary>
        public event EventHandler RequestClose;

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
        /// Получает или задает название вида работ.
        /// </summary>
        /// <value>Название вида работ.</value>
        public string WorkTypeName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// Получает единицы работ.
        /// </summary>
        /// <value>Единицы работ.</value>
        public ObservableCollection<WorkUnit> WorkUnits { get; }

        /// <summary>
        /// Получает или задает выбранную единицу работ.
        /// </summary>
        /// <value>Выбранная единица работ.</value>
        public WorkUnit SelectedWorkUnit
        {
            get => GetValue<WorkUnit>();
            set => SetValue(value);
        }

        /// <summary>
        /// Получает итоговую единицу работ для сохранения.
        /// </summary>
        /// <value>Итоговая единица работ для сохранения.</value>
        public WorkUnit ResultWorkUnit => SelectedWorkUnit == _emptyWorkUnit
            ? null : SelectedWorkUnit;

        /// <summary>
        /// Получает команду перезагрузки списка единиц работ из сервиса.
        /// </summary>
        /// <value>Команда перезагрузки списка единиц работ.</value>
        public RelayCommand ReloadWorkUnitsCommand { get; }

        /// <summary>
        /// Получает команду сохранения вида работ.
        /// </summary>
        /// <value>Команда сохранения вида работ.</value>
        public RelayCommand SaveWorkTypeCommand { get; }

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
            _emptyWorkUnit = new WorkUnit
            {
                Name = "не указана"
            };

            ReloadWorkUnitsCommand = new RelayCommand(ExecuteReloadWorkUnits);
            SaveWorkTypeCommand = new RelayCommand(ExecuteSaveWorkType);
        }

        private async void ExecuteReloadWorkUnits(object obj)
        {
            IsLoading = true;

            try
            {
                var workUnitDtos = await _workUnitService.GetAllAsync();

                WorkUnits.Clear();

                WorkUnits.Add(_emptyWorkUnit);

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
        private void ExecuteSaveWorkType(object obj)
        {
            if (string.IsNullOrWhiteSpace(WorkTypeName))
            {
                _notificationService.Show(
                    "Отсутствует значение названия вида работ",
                    WindowTitle);
                return;
            }

            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}