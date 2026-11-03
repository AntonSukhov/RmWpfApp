using System;
using System.Windows;

namespace RM.App.Services
{

    /// <summary>
    /// Конфигурация обычного окна.
    /// </summary>
    /// <remarks>
    /// Конфигурация немодального и не диалогово окна.
    /// </remarks>
    public class WindowConfiguration
    {
        /// <summary>
        /// Получает или задает фабрику создания окна.
        /// </summary>
        /// <value>Фабрика создания окна.</value>
        public Func<Window> WindowFactory { get; set; }

        /// <summary>
        /// Получает или задает фабрику создания владельца окна.
        /// </summary>
        /// <remarks>
        /// Владелец может быть: 
        /// 1. Динамическим (ActiveWindow)
        /// 2. Статическим (MainWindow)
        /// 3. Null (тогда будет дефолтное поведение WPF)
        /// </remarks>
        /// <value>Фабрика создания владельца окна.</value>
        public Func<Window> OwnerFactory { get; set; }

        /// <summary>
        /// Получает или задает признак открытия окна.
        /// </summary>
        /// <value>
        /// Признак открытия окна. 
        /// <c>true</c> может открываться многократно. 
        /// <c>false</c> может открываться в единственном экземпляре.
        /// </value>
        public bool AllowMultipleInstances { get; set; }
    }
}
