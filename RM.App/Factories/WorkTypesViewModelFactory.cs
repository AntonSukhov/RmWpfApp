using Infrastructure.Wpf.Services.Notifications;
using RM.Api.Services;
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
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypesViewModelFactory"/>.
        /// </summary>
        /// <param name="workTypeService">Сервис видов работ.</param>
        /// <param name="notificationService">Сервис уведомления пользователя.</param>
        /// <exception cref="ArgumentNullException" />
        public WorkTypesViewModelFactory(IWorkTypeService workTypeService, INotificationService notificationService)
        {
            _workTypeService = workTypeService
                ?? throw new ArgumentNullException(nameof(workTypeService));
            _notificationService = notificationService
                ?? throw new ArgumentNullException(nameof(notificationService));
        }

        /// <inheritdoc/>
        public WorkTypesViewModel Create() => new WorkTypesViewModel(_workTypeService, _notificationService);
    }
}
