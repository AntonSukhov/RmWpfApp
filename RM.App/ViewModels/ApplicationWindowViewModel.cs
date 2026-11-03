using Infrastructure.Foundation.Bindings;
using Infrastructure.Foundation.Commands;
using RM.App.Services;
using RM.App.Views;
using System;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для окна <see cref="ApplicationWindow"/>.
    /// </summary>
    public class ApplicationWindowViewModel : NotifyPropertyChangedValueStoreBase
    {
        private readonly IWindowService _windowService;

        public string WindowTitle => "Рабочее место сотрудника";

        /// <summary>
        /// Получает команду показа о программе.
        /// </summary>
        /// <value>Команда показа о программе.</value>
        public RelayCommand ShowAboutCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="ApplicationWindowViewModel"/>.
        /// </summary>
        /// <param name="windowService">Сервис обычного окна.</param>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            ShowAboutCommand = new RelayCommand(ExecuteShowAbout);
        }

        private void ExecuteShowAbout(object parameter)
        {
            _windowService.Show(new AboutWindowViewModel());
        }
    }
}
