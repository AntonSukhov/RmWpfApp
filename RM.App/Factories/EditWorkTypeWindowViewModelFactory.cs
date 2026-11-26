using Infrastructure.Mapping.Interfaces;
using Infrastructure.Wpf.Services.Notifications;
using RM.Api.DTOs.Responses;
using RM.Api.Services;
using RM.App.Enums;
using RM.App.Models;
using RM.App.ViewModels;
using System;

namespace RM.App.Factories
{
    /// <summary>
    /// Реализация фабрики модели представления <see cref="EditWorkTypeWindowViewModel"/>.
    /// </summary>
    public class EditWorkTypeWindowViewModelFactory : IEditWorkTypeWindowViewModelFactory
    {
        private readonly IWorkUnitService _workUnitService;
        private readonly INotificationService _notificationService;
        private readonly IMapper<WorkUnitResponse, WorkUnit> _workUnitMapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref=" EditWorkTypeWindowViewModelFactory"/>.
        /// </summary>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="workUnitMapper">Маппер единицы работ.</param>
        public EditWorkTypeWindowViewModelFactory(IWorkUnitService workUnitService,
                                                  INotificationService notificationService,
                                                  IMapper<WorkUnitResponse, WorkUnit> workUnitMapper)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
            _workUnitMapper = workUnitMapper
                ?? throw new ArgumentNullException(nameof(workUnitMapper));
        }

        /// <inheritdoc/>
        public EditWorkTypeWindowViewModel Create(DialogMode dialogMode) =>
            new EditWorkTypeWindowViewModel(dialogMode,
                                            _workUnitService,
                                            _notificationService,
                                            _workUnitMapper);
    }
}