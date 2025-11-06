using Recip4Linux;
using System;
using System.IO;
using System.Text.Json;

public static class SettingsManager
{
    private static readonly string FilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecipSettings.json");

    public static Settings Load()
    {
        if (!File.Exists(FilePath))
            return new Settings();

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
    }

    public static void Save(Settings settings)
    {
        string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}