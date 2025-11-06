using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Recip4Linux;

public partial class RecipeOpenedWindow : Window
{
    public static string RecipeRute { get; set; }

    public RecipeOpenedWindow()
    {
        InitializeComponent();
    }
}