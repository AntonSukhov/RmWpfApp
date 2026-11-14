using Infrastructure.Foundation.Bindings;
using Infrastructure.Foundation.Commands;
using Infrastructure.Wpf.Services.Regions;
using Infrastructure.Wpf.Services.Windows;
using RM.App.Factories;
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
        private readonly IWorkUnitsViewModelFactory _workUnitsViewModelFactory;
        private readonly IWorkTypesViewModelFactory _workTypesViewModelFactory;
        private readonly IPerformersViewModelFactory _performersViewModelFactory;
        private readonly IAboutWindowViewModelFactory _aboutWindowViewModelFactory;

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
        /// <param name="workUnitsViewModelFactory">Фабрика для <see cref="WorkUnitsViewModel"/>.</param>
        /// <param name="workTypesViewModelFactory">Фабрика для <see cref="WorkTypesViewModel"/>.</param>
        /// <param name="performersViewModelFactory">Фабрика для <see cref="PerformersViewModel"/>.</param>
        /// <param name="aboutWindowViewModelFactory">Фабрика для <see cref="AboutWindowViewModel"/>.</param>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationWindowViewModel(IWindowService windowService,
                                          IRegionService regionService,
                                          IWorkUnitsViewModelFactory workUnitsViewModelFactory,
                                          IWorkTypesViewModelFactory workTypesViewModelFactory,
                                          IPerformersViewModelFactory performersViewModelFactory,
                                          IAboutWindowViewModelFactory aboutWindowViewModelFactory)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _regionService = regionService ?? throw new ArgumentNullException(nameof(regionService));
            _workUnitsViewModelFactory = workUnitsViewModelFactory 
                ?? throw new ArgumentNullException(nameof(workUnitsViewModelFactory));
            _workTypesViewModelFactory = workTypesViewModelFactory 
                ?? throw new ArgumentNullException(nameof(workTypesViewModelFactory));
            _performersViewModelFactory = performersViewModelFactory 
                ?? throw new ArgumentNullException(nameof(performersViewModelFactory));
            _aboutWindowViewModelFactory = aboutWindowViewModelFactory 
                ?? throw new ArgumentNullException(nameof(aboutWindowViewModelFactory));

            ShowAboutCommand = new RelayCommand(ExecuteShowAbout);
            ShowWorkUnitsCommand = new RelayCommand(ExecuteShowWorkUnits);
            ShowWorkTypesCommand = new RelayCommand(ExecuteShowWorkTypes);
            ShowPerformersCommand = new RelayCommand(ExecuteShowPerformers);
            CloseRegionItemsCommand = new RelayCommand(ExecuteCloseRegionItems);
        }

        private void ExecuteShowAbout(object parameter)
        {
            _windowService.Show(_aboutWindowViewModelFactory.Create());
        }

        private void ExecuteShowWorkUnits(object parameter)
        {
            var viewModel = _workUnitsViewModelFactory.Create();

            _regionService.Show(viewModel);
        }

        private void ExecuteShowWorkTypes(object parameter)
        {
            _regionService.Show(_workTypesViewModelFactory.Create());
        }

        private void ExecuteShowPerformers(object parameter)
        {
            _regionService.Show(_performersViewModelFactory.Create());
        }

        private void ExecuteCloseRegionItems(object parameter)
        {
            _regionService.CloseAll();
        }
    }
}
