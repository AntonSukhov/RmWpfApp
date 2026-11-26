using RM.Api.Services;
using RM.App.Enums;
using RM.App.ViewModels;

namespace RM.App.Factories
{
    /// <summary>
    /// Фабрика модели представления <see cref="EditWorkTypeWindowViewModel"/>.
    /// </summary>
    public interface IEditWorkTypeWindowViewModelFactory
    {
        /// <summary>
        /// Создает модель представления <see cref="EditWorkTypeWindowViewModel"/>.
        /// </summary>
        /// <param name="dialogMode">Режим открытия диалогово окна.</param>
        /// <returns>Модель представления.</returns>
        EditWorkTypeWindowViewModel Create(DialogMode dialogMode);
    }
}
