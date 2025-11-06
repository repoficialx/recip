using System.Security.Cryptography;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Recip4Linux;

public partial class App : Application
{
    public Settings Settings { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Settings = SettingsManager.Load();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += (_, _) => { SettingsManager.Save(Settings); };

            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}