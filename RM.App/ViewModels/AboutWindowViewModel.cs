using Infrastructure.Foundation.Bindings;
using RM.App.Views;

namespace RM.App.ViewModels
{
    /// <summary>
    /// Модель представления для окна <see cref="AboutWindow"/>.
    /// </summary>
    public class AboutWindowViewModel : NotifyPropertyChangedValueStoreBase
    {
        public string WindowTitle => "О программе";
    }
}
