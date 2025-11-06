using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Microsoft.VisualBasic;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Recip4Linux;

public partial class MainWindow : Window
{
    public enum LogoOpt
    {
        COLORNEW,
        COLORRESCNEW,
        COLORMONONEW,
        COLORBNNEW
    }
    public static LogoOpt selLogo;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        Recent1.Content = Settings.Default.LastRecipe;
        Recent2.Content = Settings.Default.penLastRecipe;
        fileToolStripMenuItem.Header = Strings.MenuFile;
        newToolStripMenuItem.Header = Strings.MenuFileNew;
        openToolStripMenuItem.Header = Strings.MenuFileOpen;
        helpToolStripMenuItem.Header = Strings.MenuHelp;
        aboutRecipToolStripMenuItem.Header = Strings.MenuHelpAbout;
        button1.Content = Strings.BtnOpen;
        button2.Content = Strings.BtnMake;
        groupBox2.Header = Strings.GroupAppSettings;
        comboBox1.Items.Clear();
        comboBox1.Items.Add(Strings.ComboBW);
        comboBox1.Items.Add(Strings.ComboColor);
        comboBox1.Items.Add(Strings.ComboMonochromatic);
        groupBox1.Header = Strings.GroupLatestRecipes;
        languageToolStripMenuItem.Header = Strings.MenuLanguage;
        toolStripMenuItem2.Header = Strings.MenuLanguageChange;
    }
    Avalonia.Controls.WindowIcon seticon(byte[] x)
    {
        using MemoryStream ms = new(x);
        return new Avalonia.Controls.WindowIcon(ms);
    }/*

    private void button8_Click(object sender, EventArgs e)
    {
        if (button8.Content == "-")
        {
            this.ClientSize = this.;
            button8.Text = "+";
        }
        else if (button8.Text == "+")
        {
            Size = this.MaximumSize;
            button8.Text = "-";
        }
    }*/

    void ResetSettings()
    {
        Settings.Default.Reset();
        MessageBoxManager.GetMessageBoxStandard("Settings resetted", "Recip v1.2", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info);
        RestartApplication();
    }
    void RestartApplication()
    {
        try
        {
            // Intentar obtener la ruta del ejecutable / entry assembly
            string? entry = Environment.ProcessPath
                            ?? Assembly.GetEntryAssembly()?.Location
                            ?? Process.GetCurrentProcess().MainModule?.FileName;


            var args = Environment.GetCommandLineArgs();
            string additionalArgs = args.Length > 1 ? string.Join(" ", args.Skip(1).Select(a => $"\"{a}\"")) : "";

            ProcessStartInfo psi;

            // macOS app bundle (.app) -> usar 'open -n' para abrir nuevo ejemplar del bundle
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && entry!.EndsWith(".app", StringComparison.OrdinalIgnoreCase))
            {
                psi = new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = $"-n \"{entry}\"",
                    UseShellExecute = true
                };
            }
            // Si la entrada es un DLL, probablemente estamos ejecutando con 'dotnet <app>.dll'
            else if (entry!.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                psi = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"\"{entry}\"" + (string.IsNullOrEmpty(additionalArgs) ? "" : " " + additionalArgs),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
            }
            // Ejecutable nativo (Windows/Linux single-file, macOS bin inside .app/Contents/MacOS)
            else
            {
                psi = new ProcessStartInfo
                {
                    FileName = entry,
                    Arguments = additionalArgs,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
            }

            Process.Start(psi);
        }
        catch// (Exception ex)
        {
           // MessageBox.Show($"Error al reiniciar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // Cerrar la app Avalonia limpiamente
            var lifetime = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
            lifetime?.Shutdown();
        }
    }

    private async void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
        var langWindow = new LangWindow();
        await langWindow.ShowDialog(this);
    }

    private async void OpenMenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // 1. Obtener el StorageProvider (necesario para diálogos de archivo en Avalonia)
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || desktop.MainWindow is null)
        {
            // Esto solo ocurre si no estás en una aplicación de escritorio o si no hay ventana principal.
            // Podrías lanzar una excepción o simplemente retornar.
            return;
        }

        var storageProvider = desktop.MainWindow.StorageProvider;

        try
        {
            // 2. Definir los filtros del diálogo
            var fileType = new FilePickerFileType("Recipes")
            {
                Patterns = ["*.rec"]
            };

            var allFilesType = FilePickerFileTypes.All;

            // 3. Mostrar el diálogo de Abrir Archivo de forma asíncrona
            var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Where is the recipe?",
                AllowMultiple = false,
                FileTypeFilter = [fileType, allFilesType]
            });

            var selectedFile = result.FirstOrDefault();

            if (selectedFile != null)
            {
                // El resultado es un IStorageFile, que ya contiene la ruta (Path) y otras propiedades.
                string filePath = selectedFile.Path.LocalPath; // Obtiene la ruta de archivo del sistema

                string fileName = Path.GetFileName(filePath);

                Settings.Default.penLastRecipe = Settings.Default.LastRecipe;

                Settings.Default.pLRRoute = Settings.Default.LRRoute;

                Settings.Default.LastRecipe = fileName;

                Settings.Default.LRRoute = filePath;

                var recipeOpenedWindow = new RecipeOpenedWindow();

                await recipeOpenedWindow.ShowDialog(desktop.MainWindow);
            }
        }
        catch (Exception ex)
        {
            await MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
                "Error", $"Error: {ex.Message}", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error
            ).ShowAsync();

            System.Diagnostics.Debug.WriteLine($"Error al abrir receta: {ex.Message}");
        }

        var dialog = new OpenFileDialog
        {
            Title = "Where is the recipe?",
            Filters =
            {
                new FileDialogFilter { Name = "Recipes", Extensions = { "rec" } },
                new FileDialogFilter { Name = "All files", Extensions = { "*" } }
            },
            AllowMultiple = false
        };

        // Mostrar el diálogo de forma asíncrona (ShowDialog, no ShowDialog() de WinForms)
        /*var result = await dialog.ShowAsync(this);

        if (result != null && result.Length > 0)
        {
            var filePath = result[0];
            try
            {
                Settings.Default.penLastRecipe = Settings.Default.LastRecipe;
                Settings.Default.pLRRoute = Settings.Default.LRRoute;
                Settings.Default.LastRecipe = System.IO.Path.GetFileName(filePath);
                Settings.Default.LRRoute = filePath;

                RecipeOpened.RecipeRute = filePath;
                var recipeWindow = new RecipeOpened();
                await recipeWindow.ShowDialog(this);
            }
            catch (Exception ex)
            {
                var errorDialog = MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow("Error", $"Error: {ex.Message}",
                        MessageBox.Avalonia.Enums.ButtonEnum.Ok,
                        MessageBox.Avalonia.Enums.Icon.Error);

                await errorDialog.ShowDialog(this);
            }
        }*/
    }

    private void button2_Click(object sender, EventArgs e)
    {
        //new MakeRecipe().ShowDialog();
    }

    private async void button3_Click(object sender, EventArgs e)
    {
        string nlr = Strings.BtnNoLastRecipe;
        string nr = Strings.BtnNoRecipe;
        string nrs = Strings.MsgNoRecipeSelected;
        if (Recent1.Content?.ToString() == nlr || Recent1.Content?.ToString() == nr)
        {//NLR: No Last Recipe | NR: No Recipe | NRS: No Recipe Selected
            MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard("Recip", nrs, ButtonEnum.Ok,
                MsBox.Avalonia.Enums.Icon.Error);
            return;
        }
        RecipeOpenedWindow.RecipeRute = Settings.Default.LRRoute;
        //new RecipeOpened().ShowDialog();
        var recipeWindow = new RecipeOpenedWindow();
        await recipeWindow.ShowDialog(this);
    }

    private async void button4_Click(object sender, EventArgs e)
    {
        string nlr = Strings.BtnNoLastRecipe;
        string nr = Strings.BtnNoRecipe;
        string nrs = Strings.MsgNoRecipeSelected;
        if (Recent2.Content?.ToString() == nr || Recent2.Content?.ToString() == nlr)
        {
            MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard("Recip", nrs, ButtonEnum.Ok,
                MsBox.Avalonia.Enums.Icon.Error);
            return;
        }
        RecipeOpenedWindow.RecipeRute = Settings.Default.pLRRoute;
        //new RecipeOpened().ShowDialog();
        var recipeWindow = new RecipeOpenedWindow();
        await recipeWindow.ShowDialog(this);
    }

    private void aboutRecipToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //new About().ShowDialog();
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //new MakeRecipe().ShowDialog();
    }

    private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //new GetRecetas().ShowDialog();
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void button9_Click(object sender, EventArgs e)
    {
        //new GetRecetas().ShowDialog();
    }

    private void New_Click(object? sender, RoutedEventArgs e)
    {
    }

    private async void Open_Click(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || desktop.MainWindow is null)
        {
            // Esto solo ocurre si no estás en una aplicación de escritorio o si no hay ventana principal.
            // Podrías lanzar una excepción o simplemente retornar.
            return;
        }

        var storageProvider = desktop.MainWindow.StorageProvider;

        try
        {
            // 2. Definir los filtros del diálogo
            var fileType = new FilePickerFileType("Recipes")
            {
                Patterns = ["*.rec"]
            };

            var allFilesType = FilePickerFileTypes.All;

            // 3. Mostrar el diálogo de Abrir Archivo de forma asíncrona
            var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Where is the recipe?",
                AllowMultiple = false,
                FileTypeFilter = [fileType, allFilesType]
            });

            var selectedFile = result.FirstOrDefault();

            if (selectedFile != null)
            {
                // El resultado es un IStorageFile, que ya contiene la ruta (Path) y otras propiedades.
                string filePath = selectedFile.Path.LocalPath; // Obtiene la ruta de archivo del sistema

                string fileName = Path.GetFileName(filePath);

                Settings.Default.penLastRecipe = Settings.Default.LastRecipe;

                Settings.Default.pLRRoute = Settings.Default.LRRoute;

                Settings.Default.LastRecipe = fileName;

                Settings.Default.LRRoute = filePath;

                var recipeOpenedWindow = new RecipeOpenedWindow();

                await recipeOpenedWindow.ShowDialog(desktop.MainWindow);
            }
        }
        catch (Exception ex)
        {
            await MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
                "Error", $"Error: {ex.Message}", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error
            ).ShowAsync();

            // Por ahora, para ser simple:
            System.Diagnostics.Debug.WriteLine($"Error al abrir receta: {ex.Message}");
        }
        var dialog = new OpenFileDialog
        {
            Title = "Where is the recipe?",
            Filters =
            {
                new FileDialogFilter { Name = "Recipes", Extensions = { "rec" } },
                new FileDialogFilter { Name = "All files", Extensions = { "*" } }
            },
            AllowMultiple = false
        };
    }

    private void Online_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void Lang_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void About_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void Recent1_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void Recent2_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void Set_Click(object? sender, RoutedEventArgs e)
    {
        byte[] reciplogobn = Properties.Resources.reciplogobn;
        using var bnms = new MemoryStream(reciplogobn);
        var RL_BN = new Bitmap(bnms);
        using var nbms = new MemoryStream(Properties.Resources.recip_new);
        var RL_NB = new Bitmap(nbms);
        using var rems = new MemoryStream(Properties.Resources.reciplogoresc);
        var RL_RE = new Bitmap(rems);
        using var mnms = new MemoryStream(Properties.Resources.reciplogomono);
        var RL_MN = new Bitmap(mnms);
        switch (comboBox1.SelectedItem?.ToString()?.ToUpper())
        {
            case "B&W":
                Icon = seticon(Properties.Resources.recipiconbnnew);
                pictureBox1.Source = RL_BN;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORBNNEW);
                break;
            case "B/N":
                Icon = seticon(Properties.Resources.recipiconbnnew);
                pictureBox1.Source = RL_BN;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORBNNEW);
                break;
            case "COLOR":
                Icon = seticon(Properties.Resources.recipiconnbnew);
                pictureBox1.Source = RL_NB;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORNEW);
                break;
            case "COLOR (REESCALADO)":
                Icon = seticon(Properties.Resources.recipiconrescnew);
                pictureBox1.Source = RL_RE;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORRESCNEW);
                break;
            case "COLOR (RESCALED)":
                Icon = seticon(Properties.Resources.recipiconrescnew);
                pictureBox1.Source = RL_RE;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORRESCNEW);
                break;
            case "MONOCHROMATIC":
                Icon = seticon(Properties.Resources.recipiconmononew);
                pictureBox1.Source = RL_MN;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORMONONEW);
                break;
            case "MONOCROMÁTICO":
                Icon = seticon(Properties.Resources.recipiconmononew);
                pictureBox1.Source = RL_MN;
                Settings.Default.SavedLogo = nameof(LogoOpt.COLORMONONEW);
                break;
        }
    }

    private void Restart_Click(object? sender, RoutedEventArgs e)
    {
        RestartApplication();
    }

    private async void Reset_Click(object? sender, RoutedEventArgs e)
    {
        string msg = Strings.MsgResetSettings;
        var msbox = MessageBoxManager.GetMessageBoxStandard("RECIP", msg, ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question);
        ButtonResult dr = await msbox.ShowAsync();

        switch (dr)
        {
            case ButtonResult.Yes:
                ResetSettings();
                break;
            case ButtonResult.No:
                break;
        }
    }
}