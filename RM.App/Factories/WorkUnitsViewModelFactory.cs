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
        /// <param name="workUnitMapper">Маппер единицы работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkUnitsViewModelFactory(IWorkUnitService workUnitService,
                                         IMapper<WorkUnitResponse, WorkUnit> workUnitMapper,
                                         INotificationService notificationService)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
            _workUnitMapper = workUnitMapper
                ?? throw new ArgumentNullException(nameof(workUnitMapper));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
        }

        /// <inheritdoc/>
        public WorkUnitsViewModel Create() =>
            new WorkUnitsViewModel(_workUnitService, _workUnitMapper, _notificationService);
    }
}