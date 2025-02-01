using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Controls;
using WPF.Deepseek.Wrapper.Helpers;
using WPF.Deepseek.Wrapper.ViewModels.Pages;
using WPF.Deepseek.Wrapper.Views.Windows;

namespace WPF.Deepseek.Wrapper.Views.Pages
{
    public partial class ChatPage : INavigableView<ChatViewModel>
    {
        public ChatViewModel ViewModel { get; }
        private MainWindow? _mainWindow;
        private System.Windows.Controls.Button? _chatButton;
        public static bool IsAppStarted = false;


        public ChatPage(ChatViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this; // Set DataContext to ViewModel directly
            InitializeComponent();
            Loaded += OnLoaded; // Use Loaded event to handle initialization
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            #region to update the theme of old colors
            // to update the theme of old colors
            var tempMessages = ViewModel.Messages;
            ViewModel.Messages = new System.Collections.ObjectModel.ObservableCollection<Models.ChatMessage>();
            ViewModel.Messages = tempMessages; 
            #endregion

            if (!IsAppStarted)
            {
                _mainWindow = Application.Current.MainWindow as MainWindow;
                _chatButton = this.ChatButton;
                if (_mainWindow is not null)
                {
                    _mainWindow.Content.SizeChanged += OnMainWindowSizeChanged;
                    ViewModel.ChatRunning += OnChatRunning;
                    ViewModel.CaretPositionChanged += MoveCaretToEnd;
                    //UpdateMainGridSize(); // Initial size update
                }
                IsAppStarted = true;
            }
        }
        private void MoveCaretToEnd(int newPosition)
        {
            Dispatcher.Invoke(() =>
            {
                UserInputTextBox.CaretIndex = newPosition;
            });
        }
        private void OnChatRunning(bool isChatRunning)
        {
            if (!isChatRunning)
            {
                if (_chatButton?.Content is ContentControl contentControl &&
                               contentControl.Content is SymbolIcon icon)
                {
                    icon.Symbol = SymbolRegular.Send16;
                }
            }
        }

        private void OnMainWindowSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateMainGridSize();


        private void ChatText_SizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateMainGridSize();

        private void UpdateMainGridSize()
        {
            if (ViewModel.Messages.Count != 0 && _mainWindow is not null)
            {
                MainGrid.Height = _mainWindow.Content.ActualHeight - 90;
                MainGrid.Width = _mainWindow.Content.ActualWidth - 100;
            }
        }


        private void ToggleChatButtonIcon()
        {
            if (_chatButton?.Content is ContentControl contentControl &&
                contentControl.Content is SymbolIcon icon &&
                !string.IsNullOrEmpty(UserInputTextBox.Text))
            {
                icon.Symbol = icon.Symbol == SymbolRegular.Send16 ? SymbolRegular.RecordStop16 : SymbolRegular.Send16;
            }
        }

        private void StartChatButton_Click(object sender, RoutedEventArgs e)
            => ToggleChatButtonIcon();

        private void UserInputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    // Shift + Enter: Add a new line
                    ViewModel.UserInput += "\n";
                    ViewModel.FireCaretPositionChanged(ViewModel.UserInput.Length);
                }
                else
                {
                    // Just Enter: Send the message
                    ViewModel.SendMessageCommand.Execute(null);
                    ToggleChatButtonIcon();
                }
            }
        }
    }
}
