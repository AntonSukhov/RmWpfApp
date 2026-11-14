using Infrastructure.Foundation.Bindings;

namespace RM.App.Models
{
    /// <summary>
    /// Модель единицы работ.
    /// </summary>
    public class WorkUnit : NotifyPropertyChangedValueStoreBase
    {
        /// <summary>
        /// Получает или задает ИД единицы работ.
        /// </summary>
        /// <value>ИД единицы работ.</value>
        public int Id
        {
            get => GetValue<int>(nameof(Id));
            set => SetValue(value, nameof(Id));
        }

        /// <summary>
        /// Получает или задает название единицы работ.
        /// </summary>
        /// <value>Название единицы работ.</value>
        public string Name
        {
            get => GetValue<string>(nameof(Name));
            set => SetValue(value, nameof(Name));
        }
    }
}
