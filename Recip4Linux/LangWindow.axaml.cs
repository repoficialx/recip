using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;

namespace Recip4Linux;

public partial class LangWindow : Window
{
    public LangWindow()
    {
        InitializeComponent();
    }
    private async void OnSpanishClick(object? sender, RoutedEventArgs e)
    {
        Strings.SetLang(Language.Spanish);
        Settings.Default.Language = "ES";

        var dialog = MsBox.Avalonia.MessageBoxManager
            .GetMessageBoxStandard("Recip", Strings.MsgRestartToApply, MsBox.Avalonia.Enums.ButtonEnum.YesNo);

        var result = await dialog.ShowWindowDialogAsync(this);
        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            Process.Start(Environment.ProcessPath!);
            Environment.Exit(0);
        }
    }

    private async void OnEnglishClick(object? sender, RoutedEventArgs e)
    {
        Strings.SetLang(Language.English);
        Settings.Default.Language = "EN";
        var dialog = 
            MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard("Recip", Strings.MsgRestartToApply, MsBox.Avalonia.Enums.ButtonEnum.YesNo);

        var result = await dialog.ShowWindowDialogAsync(this);
        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            Process.Start(Environment.ProcessPath!);
            Environment.Exit(0);
        }
    }
}