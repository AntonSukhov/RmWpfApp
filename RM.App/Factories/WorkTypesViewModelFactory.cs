using Infrastructure.Mapping.Interfaces;
using Infrastructure.Wpf.Services.Dialogs;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.DTOs.Responses;
using RM.Api.Services;
using RM.App.Models;
using RM.App.ViewModels;
using System;

namespace RM.App.Factories
{
    /// <summary>
    /// Реализация фабрики модели представления <see cref="WorkTypesViewModel"/>.
    /// </summary>
    public class WorkTypesViewModelFactory : IWorkTypesViewModelFactory
    {
        private readonly IWorkTypeService _workTypeService;
        private readonly IWorkUnitService _workUnitService;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;
        private readonly IMapper<WorkTypeResponse, WorkType> _workTypeMapper;
        private readonly IEditWorkTypeWindowViewModelFactory _editWorkTypeWindowViewModelFactory;


        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModelFactory"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис вида работ.</param>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="workTypeMapper">Маппет вида работы.</param>
        /// <param name="editWorkTypeWindowViewModelFactory">
        /// Фабрика модели представления <see cref="EditWorkTypeWindowViewModel"/>.
        /// </param>
        public WorkTypesViewModelFactory(IWorkTypeService workTypeService,
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
        }

        /// <inheritdoc/>
        public WorkTypesViewModel Create() =>
            new WorkTypesViewModel(_workTypeService,
                                    _workUnitService,
                                    _notificationService,
                                    _dialogService,
                                    _workTypeMapper,
                                    _editWorkTypeWindowViewModelFactory);
    }
}
