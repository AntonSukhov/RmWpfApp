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
        public short Id
        {
            get => GetValue<short>();
            set => SetValue(value);
        }

        /// <summary>
        /// Получает или задает название единицы работ.
        /// </summary>
        /// <value>Название единицы работ.</value>
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
    }
}
