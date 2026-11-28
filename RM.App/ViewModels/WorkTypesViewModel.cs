using Infrastructure.Foundation.Commands;
using Infrastructure.Mapping.Interfaces;
using Infrastructure.Mvvm.ViewModels;
using Infrastructure.Wpf.Enums;
using Infrastructure.Wpf.Services.Dialogs;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.DTOs.Requests;
using RM.Api.DTOs.Responses;
using RM.Api.Services;
using RM.App.Enums;
using RM.App.Factories;
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
        private readonly IWorkUnitService _workUnitService;
        private readonly IMapper<WorkTypeResponse, WorkType> _workTypeMapper;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;
        private readonly IEditWorkTypeWindowViewModelFactory _editWorkTypeWindowViewModelFactory;

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
        /// Получает команду создания вида работ.
        /// </summary>
        /// <value>Команда создания вида работ.</value>
        public RelayCommand CreateWorkTypeCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModel"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис вида работ.</param>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="workTypeMapper">Маппет вида работы.</param>
        /// <param name="editWorkTypeWindowViewModelFactory">
        /// Фабрика модели представления <see cref="EditWorkTypeWindowViewModel"/>.
        /// </param>
        public WorkTypesViewModel(IWorkTypeService workTypeService,
                                  IWorkUnitService workUnitService,
                                  INotificationService notificationService,
                                  IDialogService dialogService,
                                  IMapper<WorkTypeResponse, WorkType> workTypeMapper,
                                  IEditWorkTypeWindowViewModelFactory editWorkTypeWindowViewModelFactory)
        {
            _workTypeService = workTypeService
                ?? throw new ArgumentNullException(nameof(workTypeService));
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
            _dialogService = dialogService
                ?? throw new ArgumentNullException(nameof(dialogService));
            _workTypeMapper = workTypeMapper
                ?? throw new ArgumentNullException(nameof(workTypeMapper));
            _editWorkTypeWindowViewModelFactory = editWorkTypeWindowViewModelFactory
                ?? throw new ArgumentNullException(nameof(editWorkTypeWindowViewModelFactory));


            WorkTypes = new ObservableCollection<WorkType>();

            ReloadWorkTypesCommand = new RelayCommand(ExecuteReloadWorkTypes);
            CreateWorkTypeCommand = new RelayCommand(ExecuteCreateWorkType);
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

        private async void ExecuteCreateWorkType(object obj)
        {
            var viewModel = _editWorkTypeWindowViewModelFactory.Create(DialogMode.Create);

            if (_dialogService.Show(viewModel) == DialogResult.Confirmed)
            {
                IsLoading = true;

                try
                {
                    var workTypeCreationRequest = new WorkTypeCreationRequest
                    {
                        Name = viewModel.WorkTypeName,
                        WorkUnitId = viewModel.ResultWorkUnit?.Id
                    };


                    var workTypeId = await _workTypeService.CreateAsync(
                        workTypeCreationRequest);

                    WorkUnit workUnit = null;

                    if (workTypeCreationRequest.WorkUnitId != null)
                    {

                        var workUnitResponse = await _workUnitService.GetByIdAsync(workTypeCreationRequest.WorkUnitId.Value);
                        workUnit = new WorkUnit
                        {
                            Id = workUnitResponse.Id,
                            Name = workUnitResponse.Name,
                        };
                    }

                    var workType = new WorkType
                    {
                        Id = workTypeId,
                        Name = workTypeCreationRequest.Name,
                        WorkUnit = workUnit
                    };

                    WorkTypes.Add(workType);

                }
                catch
                {
                    _notificationService.Show(
                        "Не удалось создать вид работ. Попробуйте повторить позже.",
                        "Ошибка при создании вида работ",
                        icon: NotificationIcon.Error);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }
    }
}
