using RM.Api.Services;
using RM.App.ViewModels;
using System;

namespace RM.App.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkUnitsViewModelFactory : IWorkUnitsViewModelFactory
    {
        private readonly IWorkUnitService _workUnitService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workUnitService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WorkUnitsViewModelFactory(IWorkUnitService workUnitService)
        {
            _workUnitService = workUnitService
                ?? throw new ArgumentNullException(nameof(workUnitService));
        }

        /// <inheritdoc/>
        public WorkUnitsViewModel Create() => new WorkUnitsViewModel(_workUnitService);
    }
}
