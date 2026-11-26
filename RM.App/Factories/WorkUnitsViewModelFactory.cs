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
    /// Реализация фабрики модели представления <see cref="WorkUnitsViewModel"/>.
    /// </summary>
    public class WorkUnitsViewModelFactory : IWorkUnitsViewModelFactory
    {
        private readonly IWorkUnitService _workUnitService;
        private readonly IMapper<WorkUnitResponse, WorkUnit> _workUnitMapper;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkUnitsViewModelFactory"/>.
        /// </summary>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <param name="workUnitMapper">Маппер единицы работ.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkUnitsViewModelFactory(IWorkUnitService workUnitService,
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
        public WorkUnitsViewModel Create() =>
            new WorkUnitsViewModel(_workUnitService, _notificationService, _workUnitMapper);
    }
}