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
        private readonly IMapper<WorkTypeResponse, WorkType> _workTypeMapper;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;
        private readonly IEditWorkTypeWindowViewModelFactory _editWorkTypeWindowViewModelFactory;


        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModelFactory"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис видов работ.</param>
        /// <param name="workTypeMapper">Маппет вида работы.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="dialogService">Сервис диалогово окна.</param>
        ///<param name="editWorkTypeWindowViewModelFactory">
        /// Фабрика модели представления <see cref="EditWorkTypeWindowViewModel"/>.
        ///</param>
        public WorkTypesViewModelFactory(IWorkTypeService workTypeService,
                                         IMapper<WorkTypeResponse, WorkType> workTypeMapper,
                                         INotificationService notificationService,
                                         IDialogService dialogService,
                                         IEditWorkTypeWindowViewModelFactory editWorkTypeWindowViewModelFactory)
        {
            _workTypeService = workTypeService
                ?? throw new ArgumentNullException(nameof(workTypeService));
            _workTypeMapper = workTypeMapper
                ?? throw new ArgumentNullException(nameof(workTypeMapper));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
            _dialogService = dialogService
                ?? throw new ArgumentNullException(nameof(dialogService));
            _editWorkTypeWindowViewModelFactory = editWorkTypeWindowViewModelFactory
                ?? throw new ArgumentNullException(nameof(editWorkTypeWindowViewModelFactory));
        }

        /// <inheritdoc/>
        public WorkTypesViewModel Create() =>
            new WorkTypesViewModel(_workTypeService,
                                   _workTypeMapper,
                                   _notificationService,
                                   _dialogService,
                                   _editWorkTypeWindowViewModelFactory);
    }
}
