using System;
using System.Windows;

namespace RM.App.Services
{
    /// <summary>
    /// Конфигурация диалога с пользователем.
    /// </summary>
    public class DialogConfiguration
    {
        /// <summary>
        /// Получает или задает фабрику создания окна диалога.
        /// </summary>
        /// <value>Фабрика создания окна диалога.</value>
        public Func<Window> WindowFactory { get; set; }

        /// <summary>
        /// Получает или задает фабрику создания владельца окна диалога.
        /// </summary>
        /// <remarks>
        /// Владелец может быть: 
        /// 1. Динамическим (ActiveWindow)
        /// 2. Статическим (MainWindow)
        /// 3. Null (тогда будет дефолтное поведение WPF)
        /// </remarks>
        /// <value>Фабрика создания владельца окна диалога.</value>
        public Func<Window> OwnerFactory { get; set; }
    }
}
