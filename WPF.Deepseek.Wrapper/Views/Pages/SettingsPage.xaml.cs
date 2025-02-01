using Wpf.Ui.Controls;
using WPF.Deepseek.Wrapper.ViewModels.Pages;

namespace WPF.Deepseek.Wrapper.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
