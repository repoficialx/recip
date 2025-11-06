/*using static System.Net.Mime.MediaTypeNames;

namespace RecipConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Recip Console!");
        Loop:
            bool providedFile = false;
            if (args.Length > 0) {
                if (string.IsNullOrEmpty(args[0]) || !args[0].EndsWith(".rec"))
                {
                    Console.WriteLine("Invalid file path provided in arguments. Please enter a valid *.rec file.");
                    goto Loop;
                }
                providedFile = true;
            }
            if (!providedFile) { Console.Write("Input a *.rec file: "); }
            string? filePath = !providedFile ? Console.ReadLine() : args[0];
            if (string.IsNullOrEmpty(filePath) || !filePath.EndsWith(".rec"))
            {
                Console.WriteLine("Invalid file path. Please enter a valid *.rec file.");
                goto Loop;
            }
            try
            {
                Console.WriteLine($"Reading recipe from {filePath}...");
                string sectionName = "Ingredients";
                List<string> sectionLines = ReadSection(filePath, sectionName);

                string ingredients = "";

                foreach (string line in sectionLines)
                {
                    if (string.IsNullOrEmpty(ingredients))
                    {
                        ingredients = line;
                    }
                    else
                    {
                        ingredients += "\r\n" + line;
                    }
                }

                sectionName = "Steps";
                sectionLines = ReadSection(filePath, sectionName);

                string steps="";

                foreach (string line in sectionLines)
                {
                    if (string.IsNullOrEmpty(steps))
                    {
                        steps = line;
                    }
                    else
                    {
                        steps += "\r\n" + line;
                    }
                }


                sectionName = "RecipeName";
                sectionLines = ReadSection(filePath, sectionName);

                string name="";

                foreach (string line in sectionLines)
                {
                    name = line;
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
}
*/
using System;
using System.Collections.Generic;
using System.IO;

public class RecipConsole
{
    private static readonly string Separator = new string('-', 40);

    // Tu logo ASCII, pégalo aquí desde tu portapapeles
    private const string LogoAscii = @"
                          :-==--===--=-                                       
                   -=-====================--                                  
            -==================================-                              
       -=============+***######****-    =========-                            
 -=-============  +####################**+  =======-                          
===========     ######**+++++******########**  =====-                         
=====++++        *###*                =+#####*  =====                         
=   ++++++        *###*                  **###*  ====-                        
     =+++++       +*###*                  *####   ====-                       
      +++++-       *####=                =####-   =====                       
       +++++=       +####+              +####*    ====:                       
        +++++        +###*=            +#####    =====                        
        ++++++        +###*+         *######   =====-                         
         ++++++        *###+       -*####*    ======                          
          -+++++        ####*    :+####*=   =======                           
           ++++++       =####+ :+*####*   ======-                             
            +++++        **###**####*   =====--:........                      
             ++++=        *#######*  ====-:...............                    
             =++++=        *####*+===--:....................                  
              ++++++        *  ===-::.......      +*#*:......                 
               +++++=       ===-:........     ++*#####*- .....                
                +++++    ===-::.......    :+*##########* ......               
                 +++++===-:.......     **#######*- =*###*  .....              
                  ++++++:......    =**######**-     -*###+  .....             
               =+++++++++:..    =*#######**          +*###*  .....            
           ++++++++++++++  -+*#######*=               +###*-  .....           
       =+++++++++++ +++++ +######**=                  +*###+  ......          
    +++++++++=       =+++= *###*                       *###*    .....         
++++++++++-           ++++= *###+                      =###+    ......        
++++++=-              ++++++-*###+                     +###+     ......       
++-                    +++++ **###*                    *###*      .....       
                        +++++:=*####*                =*###*+       ......     
                         +++++  +####**           =*#####*          ......    
                         ++++++  =*########***#########*+            .....    
                          =++++=   -**#############***                .....   
                           =++++          :--:                         .....  
                            =++++-                                     ...... 
                             +++++                                      ......
                             ++++++                                      .....
                              =++++-                                      ....
                            .::::-++-                                      ...
                       ::::::::::-=++                                       ..
";

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Recip Console!");
        bool exit = false;

        while (!exit)
        {
            ShowMainMenu();
            string? choice = Console.ReadLine();

            switch (choice?.ToLower())
            {
                case "f":
                case "file":
                    HandleFileMenu();
                    break;
                case "s":
                case "settings":
                    HandleSettingsMenu();
                    break;
                case "r":
                case "recent":
                    HandleRecentMenu();
                    break;
                case "q":
                case "quit":
                    exit = true;
                    Console.WriteLine("Exiting application...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    private static void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine(Separator);
        Console.WriteLine(" Recip v1.2");
        Console.WriteLine(Separator);
        Console.WriteLine("| F | File");
        Console.WriteLine("| S | Settings");
        Console.WriteLine("| R | Recent");
        Console.WriteLine("| Q | Quit");
        Console.WriteLine(Separator);
        Console.Write("Enter option: ");
    }

    private static void HandleFileMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine(Separator);
            Console.WriteLine(" File Menu");
            Console.WriteLine(Separator);
            Console.WriteLine("| O | Open");
            Console.WriteLine("| N | New");
            Console.WriteLine("| B | Back to Main Menu");
            Console.WriteLine(Separator);
            Console.Write("Enter option: ");

            string? choice = Console.ReadLine();
            switch (choice?.ToLower())
            {
                case "o":
                case "open":
                    OpenFile();
                    break;
                case "n":
                case "new":
                    CreateNewFile();
                    break;
                case "b":
                case "back":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    private static void OpenFile()
    {
        Console.WriteLine("Functionality to open a file will be here.");
        string name;
        string ingredients;
        string steps;
        Console.Write("Enter the path to the recipe file: ");
        string? filePath = Console.ReadLine();
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            Console.WriteLine("Invalid file path. Please enter a valid *.rec file.");
            return;
        }
        ReadAllSections(out name, out ingredients, out steps, filePath);
        Console.WriteLine(Separator);
        Console.WriteLine($"Recipe Name: {name}");
        Console.WriteLine(Separator);
        Console.WriteLine("Ingredients:");
        Console.WriteLine(ingredients);
        Console.WriteLine(Separator);
        Console.WriteLine("Steps:");
        Console.WriteLine(steps);
        Console.WriteLine(Separator);
        Console.WriteLine("Press any key to return to File Menu.");
        Console.ReadKey(true);
    }

    private static void HandleSettingsMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine(Separator);
            Console.WriteLine(" Settings Menu");
            Console.WriteLine(Separator);
            Console.WriteLine("| A | About");
            Console.WriteLine("| L | Show Logo");
            Console.WriteLine("| B | Back to Main Menu");
            Console.WriteLine(Separator);
            Console.Write("Enter option: ");

            string? choice = Console.ReadLine();
            switch (choice?.ToLower())
            {
                case "a":
                case "about":
                    ShowAbout();
                    break;
                case "l":
                case "logo":
                    ShowLogo();
                    break;
                case "b":
                case "back":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    private static void ShowAbout()
    {
        Console.Clear();
        Console.WriteLine(Separator);
        Console.WriteLine(" About Recip v1.2");
        Console.WriteLine(Separator);
        Console.WriteLine("Developed by [Tu Nombre]");
        Console.WriteLine("Version: 1.2");
        Console.WriteLine("Release Date: [Fecha]");
        Console.WriteLine("\nRecip is a simple console application for managing recipes.");
        Console.WriteLine("It allows you to view, create, and organize your favorite recipes in a simple text-based format.");
        Console.WriteLine(Separator);
        Console.WriteLine("Press any key to return to Settings Menu.");
        Console.ReadKey(true);
    }

    private static void ShowLogo()
    {
        Console.Clear();
        Console.WriteLine(LogoAscii);
        Console.WriteLine(Separator);
        Console.WriteLine("Press any key to return to Settings Menu.");
        Console.ReadKey(true);
    }

    private static void HandleRecentMenu()
    {
        Console.WriteLine("Recent menu selected.");
        // Implementar la lógica para mostrar archivos recientes aquí.
        Console.WriteLine("Press any key to return to Main Menu.");
        Console.ReadKey(true);
    }

    public static void ReadAllSections(out string name, out string ingredients, out string steps, string filePath)
    {
        name = "";
        ingredients = "";
        steps = "";
        List<string> sectionLines = ReadSection(filePath, "RecipeName");
        if (sectionLines.Count > 0)
        {
            name = sectionLines[0];
        }
        sectionLines = ReadSection(filePath, "Ingredients");
        foreach (string line in sectionLines)
        {
            if (string.IsNullOrEmpty(ingredients))
            {
                ingredients = line;
            }
            else
            {
                ingredients += "\r\n" + line;
            }
        }
        sectionLines = ReadSection(filePath, "Steps");
        foreach (string line in sectionLines)
        {
            if (string.IsNullOrEmpty(steps))
            {
                steps = line;
            }
            else
            {
                steps += "\r\n" + line;
            }
        }
    }

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

    private static void CreateNewFile()
    {
        Console.Clear();
        Console.WriteLine(Separator);
        Console.WriteLine(" Create New Recipe");
        Console.WriteLine(Separator);

        Console.Write("Recipe Name: ");
        string? recipeName = Console.ReadLine();

        if (string.IsNullOrEmpty(recipeName))
        {
            Console.WriteLine("Recipe name cannot be empty. Returning to menu.");
            Console.ReadKey(true);
            return;
        }

        Console.WriteLine("\nEnter Ingredients (type 'done' on a new line when finished):");
        List<string> ingredients = new List<string>();
        string? ingredientLine;
        while ((ingredientLine = Console.ReadLine())?.ToLower() != "done")
        {
            ingredients.Add(ingredientLine!);
        }

        Console.WriteLine("\nEnter Steps (type 'done' on a new line when finished):");
        List<string> steps = new List<string>();
        string? stepLine;
        while ((stepLine = Console.ReadLine())?.ToLower() != "done")
        {
            steps.Add(stepLine!);
        }

        SaveRecipe(recipeName, ingredients, steps);

        Console.WriteLine("\nRecipe saved successfully! Press any key to continue...");
        Console.ReadKey(true);
    }

    private static void SaveRecipe(string name, List<string> ingredients, List<string> steps)
    {
        // Genera un nombre de archivo a partir del nombre de la receta
        string fileName = $"{name.Replace(" ", "_").ToLower()}.rec";
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        // Usa StreamWriter para escribir el archivo
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"[RecipeName]");
            writer.WriteLine(name);
            writer.WriteLine(); // Añade una línea en blanco para el formato

            writer.WriteLine($"[Ingredients]");
            foreach (var ingredient in ingredients)
            {
                writer.WriteLine(ingredient);
            }
            writer.WriteLine();

            writer.WriteLine($"[Steps]");
            foreach (var step in steps)
            {
                writer.WriteLine(step);
            }
        }
    }
}