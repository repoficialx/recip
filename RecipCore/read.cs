using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class RecipApp
{ 
    public static List<string> ReadSection(string filePath, string sectionName)
    {
        List<string> lines = File.ReadAllLines(filePath).ToList();
        List<string> sectionLines = new List<string>();
        bool inSection = false;

        foreach (string line in lines)
        {
            if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
            {
                inSection = line.Trim().Equals($"[{sectionName}]", StringComparison.OrdinalIgnoreCase);
            }
            else if (inSection)
            {
                if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                {
                    break; // Salir de la sección si encontramos otra sección
                }
                sectionLines.Add(line);
            }
        }

        return sectionLines;
    }
    public static void ReadFile_v2(string filePath, out string recipeName, out List<Ingrediente> ingredientes, out List<Paso> pasos, out Metadata metadata, out int ver)
    {
        var receta = Newtonsoft.Json.JsonConvert.DeserializeObject<RecetaV2>(File.ReadAllText(filePath));

        recipeName = receta.name;
        ingredientes = receta.ingredients;
        pasos = receta.steps;
        metadata = receta.metadata;
        ver = receta.version;
    }
}

#pragma warning disable
public class RecetaV2
{
    public int version { get; set; } = 2;
    public string name { get; set; }
    public List<Ingrediente> ingredients { get; set; }
    public List<Paso> steps { get; set; }
    public Metadata metadata { get; set; }
}

public class Ingrediente
{
    public string name { get; set; }
    public double quantity { get; set; }
    public string unit { get; set; }
}

public class Paso
{
    public string text { get; set; }
    public int time { get; set; } // en segundos
    public string image { get; set; }
}

public class Metadata
{
    public string id { get; set; }
    public string source { get; set; }
    public string createdAt { get; set; }
}
#pragma warning restore