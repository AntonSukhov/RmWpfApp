using RM.App.ViewModels;

namespace RM.App.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class AboutWindowViewModelFactory : IAboutWindowViewModelFactory
    {
        /// <inheritdoc/>
        public AboutWindowViewModel Create() => new AboutWindowViewModel();
    }
}
