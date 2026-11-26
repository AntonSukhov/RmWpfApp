using Infrastructure.Foundation.Bindings;
using RM.Api.Services;
using RM.App.Enums;
using RM.App.Views;
using System;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для окна <see cref="EditWorkTypeWindow"/>.
    /// </summary>
    public class EditWorkTypeWindowViewModel : NotifyPropertyChangedValueStoreBase
    {
        private readonly IWorkUnitService _workUnitService;

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
        /// Инициализирует экземпляр <see cref="EditWorkTypeWindowViewModel"/>.
        /// </summary>
        /// <param name="dialogMode">Режим открытия окна.</param>
        /// <param name="workUnitService">Сервис единицы работ.</param>
        public EditWorkTypeWindowViewModel(DialogMode dialogMode,
                                           IWorkUnitService workUnitService)
        {
            DialogMode = dialogMode;
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
        }
    }
}