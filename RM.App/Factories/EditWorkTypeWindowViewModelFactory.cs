using RM.Api.Services;
using RM.App.Enums;
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

        /// <summary>
        /// Инициализирует экземпляр <see cref=" EditWorkTypeWindowViewModelFactory"/>.
        /// </summary>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        public EditWorkTypeWindowViewModelFactory(IWorkUnitService workUnitService)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
        }

        /// <inheritdoc/>
        public EditWorkTypeWindowViewModel Create(DialogMode dialogMode) =>
            new EditWorkTypeWindowViewModel(dialogMode, _workUnitService);
    }
}