using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace WPF.Deepseek.Wrapper.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Deepseek.Wrapper";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems =
        [
            new NavigationViewItem()
            {
                Content = "Chat",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Chat24 },
                TargetPageType = typeof(Views.Pages.ChatPage)
            },
        ];

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems =
        [
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        ];

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = 
        [
            new MenuItem { Header = "Chat", Tag = "tray_home" }
        ];
    }
}
