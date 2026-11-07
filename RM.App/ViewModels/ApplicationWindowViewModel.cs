using Infrastructure.Foundation.Bindings;
using Infrastructure.Foundation.Commands;
using Infrastructure.Wpf.Services.Regions;
using Infrastructure.Wpf.Services.Windows;
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
        private readonly IRegionService _regionService;

        public string WindowTitle => "Рабочее место сотрудника";

        public RelayCommand ShowAboutCommand { get; }
        public RelayCommand ShowWorkUnitsCommand { get; }
        public RelayCommand ShowWorkTypesCommand { get; }
        public RelayCommand ShowPerformersCommand { get; }
        public RelayCommand CloseRegionItemsCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="ApplicationWindowViewModel"/>.
        /// </summary>
        /// <param name="windowService">Сервис обычного окна.</param>
        /// <param name="regionService">Сервис области.</param>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationWindowViewModel(IWindowService windowService,
                                          IRegionService regionService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _regionService = regionService ?? throw new ArgumentNullException(nameof(regionService));

            ShowAboutCommand = new RelayCommand(ExecuteShowAbout);
            ShowWorkUnitsCommand = new RelayCommand(ExecuteShowWorkUnits);
            ShowWorkTypesCommand = new RelayCommand(ExecuteShowWorkTypes);
            ShowPerformersCommand = new RelayCommand(ExecuteShowPerformers);
            CloseRegionItemsCommand = new RelayCommand(ExecuteCloseRegionItems);
        }

        private void ExecuteShowAbout(object parameter)
        {
            _windowService.Show(new AboutWindowViewModel());
        }

        private void ExecuteShowWorkUnits(object parameter)
        {
            _regionService.Show(new WorkUnitsViewModel());
        }

        private void ExecuteShowWorkTypes(object parameter)
        {
            _regionService.Show(new WorkTypesViewModel());
        }

        private void ExecuteShowPerformers(object parameter)
        {
            _regionService.Show(new PerformersViewModel());
        }

        private void ExecuteCloseRegionItems(object parameter)
        {
            _regionService.CloseAll();
        }
    }
}
