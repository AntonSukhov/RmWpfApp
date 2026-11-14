using RM.App.ViewModels;

namespace RM.App.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class PerformersViewModelFactory : IPerformersViewModelFactory
    {
        /// <inheritdoc/>
        public PerformersViewModel Create() => new PerformersViewModel();
    }
}
