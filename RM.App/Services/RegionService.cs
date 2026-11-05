using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace RM.App.Services
{
    /// <summary>
    /// Реализация сервиса области.
    /// </summary>
    public class RegionService : IRegionService
    {
        private readonly TabControl _tabControl;
        private readonly Dictionary<Type, RegionItemConfiguration> _regionConfigurations;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RegionService"/>.
        /// </summary>
        /// <param name="tabControl">Контрол вкладок.</param>
        /// <param name="regionConfigurations">Конфигурации области.</param>
        /// <exception cref="ArgumentNullException"/>
        public RegionService(TabControl tabControl,
                             Dictionary<Type, RegionItemConfiguration> regionConfigurations)
        {
            _tabControl = tabControl
                ?? throw new ArgumentNullException(nameof(tabControl));
            _regionConfigurations = regionConfigurations
                ?? throw new ArgumentNullException(nameof(regionConfigurations));
        }

        /// <inheritdoc/>
        public void Show<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            if (!_regionConfigurations.TryGetValue(typeof(TViewModel), out var config))
            {
                throw new InvalidOperationException(
                    $"Для типа {typeof(TViewModel).Name} не зарегистрирована конфигурация элемента области.");
            }

            // Сначала ищем, не открыта ли уже эта вкладка
            foreach (TabItem tab in _tabControl.Items)
            {
                if (tab.Content is UserControl view &&
                    view.DataContext?.GetType() == typeof(TViewModel))
                {
                    _tabControl.SelectedItem = tab;

                    return;
                }
            }

            // Создаём новый элемент (вкладка)
            var newView = config.ViewFactory();
            newView.DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            var tabItem = new TabItem
            {
                Header = config.Title,
                Content = newView
            };

            _tabControl.Items.Add(tabItem);
            _tabControl.SelectedItem = tabItem;
        }

        /// <inheritdoc/>
        public void Close<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            foreach (TabItem tab in _tabControl.Items)
            {
                if (tab.Content is UserControl view &&
                    ReferenceEquals(view.DataContext, viewModel))
                {
                    _tabControl.Items.Remove(tab);

                    return;
                }
            }
        }

        /// <inheritdoc/>
        public void CloseAll() => _tabControl.Items.Clear();
    }
}
