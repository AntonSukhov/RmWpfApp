using RM.App.Enums;

namespace RM.App.Services
{
    /// <summary>
    /// Сервис диалога с пользователем.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Показывает диалог с пользоватлем.
        /// </summary>
        /// <typeparam name="TViewModel">Тип данных модели представления диалога с пользователем.</typeparam>
        /// <param name="viewModel">Модель представления диалога с пользователем.</param>
        /// <returns>Результат диалога с пользователем.</returns>
        DialogResult Show<TViewModel>(TViewModel viewModel) where TViewModel : class;
    }
}
