using Infrastructure.Foundation.Commands;
using Infrastructure.Mvvm.ViewModels;
using RM.Api.Services;
using RM.App.Models;
using RM.App.Views;
using System;
using System.Collections.ObjectModel;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для представления <see cref="WorkUnitsView"/>.
    /// </summary>
    public class WorkUnitsViewModel : LoadingViewModelBase
    {
        private readonly IWorkUnitService _workUnitService;

        /// <summary>
        /// Получает единицы работ.
        /// </summary>
        /// <value>Единицы работ.</value>
        public ObservableCollection<WorkUnit> WorkUnits { get; }

        /// <summary>
        /// Получает команду обновления единиц работ.
        /// </summary>
        /// <value>Команда обновления единиц работ.</value>
        public RelayCommand UpdateWorkUnitsCommand { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkUnitsViewModel"/>.
        /// </summary>
        /// <param name="workUnitService"></param>
        public WorkUnitsViewModel(IWorkUnitService workUnitService)
        {
            _workUnitService = workUnitService ?? throw new ArgumentNullException(nameof(workUnitService));

            WorkUnits = new ObservableCollection<WorkUnit>();

            UpdateWorkUnitsCommand = new RelayCommand(ExecuteUpdateWorkUnits);
        }

        private async void ExecuteUpdateWorkUnits(object obj)
        {
            IsLoading = true;

            try
            {
                var workUnitDtos = await _workUnitService.GetAllAsync();

                WorkUnits.Clear();

                foreach (var item in workUnitDtos)
                {
                    WorkUnits.Add(new WorkUnit { Id = item.Id, Name = item.Name });
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
