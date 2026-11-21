using RM.App.ViewModels;

namespace RM.App.Factories
{
    /// <summary>
    /// Фабрика модели представления <see cref="WorkTypesViewModel"/>.
    /// </summary>
    public interface IWorkTypesViewModelFactory
    {
        /// <summary>
        /// Создает модель представления <see cref="WorkTypesViewModel"/>.
        /// </summary>
        /// <returns>Модель представления.</returns>
        WorkTypesViewModel Create();
    }
}
