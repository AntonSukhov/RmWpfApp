using RM.App.ViewModels;

namespace RM.App.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkTypesViewModelFactory : IWorkTypesViewModelFactory
    {
        /// <inheritdoc/>
        public WorkTypesViewModel Create() => new WorkTypesViewModel();
    }
}
