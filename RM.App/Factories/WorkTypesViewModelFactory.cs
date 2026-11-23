using Infrastructure.Mapping.Interfaces;
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

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModelFactory"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис видов работ.</param>
        /// <param name="workTypeMapper">Маппет вида работы.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkTypesViewModelFactory(IWorkTypeService workTypeService,
                                         IMapper<WorkTypeResponse, WorkType> workTypeMapper,
                                         INotificationService notificationService)
        {
            _workTypeService = workTypeService
                ?? throw new ArgumentNullException(nameof(workTypeService));
            _workTypeMapper = workTypeMapper
                ?? throw new ArgumentNullException(nameof(workTypeMapper));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
        }

        /// <inheritdoc/>
        public WorkTypesViewModel Create() =>
            new WorkTypesViewModel(_workTypeService, _workTypeMapper, _notificationService);
    }
}
