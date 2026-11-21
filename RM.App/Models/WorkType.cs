using Infrastructure.Foundation.Bindings;
using System;

namespace RM.App.Models
{
    /// <summary>
    /// Модель вида работ.
    /// </summary>
    public class WorkType : NotifyPropertyChangedValueStoreBase
    {
        /// <summary>
        /// Получает или задает ИД вида работ.
        /// </summary>
        /// <value>ИД вида работ.</value>
        public Guid Id
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        /// <summary>
        /// Получает или задает название вида работ.
        /// </summary>
        /// <value>Название вида работ.</value>
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// Получает или задает единицу работ.
        /// </summary>
        /// <value>Единица работ.</value>
        public WorkUnit WorkUnit
        {
            get => GetValue<WorkUnit>();
            set => SetValue(value);
        }
    }
}
