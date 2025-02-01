using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Wpf.Ui.Appearance;

namespace WPF.Deepseek.Wrapper.Helpers;

public class BooleanToBrushConverter : DependencyObject, IValueConverter
{
    public static readonly DependencyProperty TrueBrushProperty =
        DependencyProperty.Register("TrueBrush", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(default(Brush)));

    public static readonly DependencyProperty FalseBrushProperty =
        DependencyProperty.Register("FalseBrush", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(default(Brush)));

    public Brush TrueBrush
    {
        get => (Brush)GetValue(TrueBrushProperty);
        set => SetValue(TrueBrushProperty, value);
    }

    public Brush FalseBrush
    {
        get => (Brush)GetValue(FalseBrushProperty);
        set => SetValue(FalseBrushProperty, value);
    }

    public BooleanToBrushConverter()
    {
        UpdateBrushes();
        ApplicationThemeManager.Changed += OnThemeChanged;
    }

    private void OnThemeChanged(ApplicationTheme theme, Color accentColor)
    {
        UpdateBrushes();
    }

    public void UpdateBrushes()
    {
        switch (ApplicationThemeManager.GetAppTheme())
        {
            case ApplicationTheme.Light:
                Application.Current.Resources["UserMessageBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#87e09f"));
                Application.Current.Resources["ModelMessageBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a4bbed"));
                break;
            case ApplicationTheme.Dark:
                Application.Current.Resources["UserMessageBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#785dc8"));
                Application.Current.Resources["ModelMessageBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#292E33"));
                break;
        }
        TrueBrush = (Brush)Application.Current.Resources["UserMessageBrush"];
        FalseBrush = (Brush)Application.Current.Resources["ModelMessageBrush"];
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean)
        {
            return boolean ? TrueBrush : FalseBrush;
        }
        return FalseBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
