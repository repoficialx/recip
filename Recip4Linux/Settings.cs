using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupBox.Avalonia.Converters;

namespace Recip4Linux
{
    public class Settings
    {
        public string Theme { get; set; } = "Light";
        public bool NotificationsEnabled { get; set; } = true;

        public class Default
        {
            public static void Reset()
            {
                penLastRecipe = "No recipe";
                LastRecipe = "No last recipe";
                SavedLogo = "COLORNEW";
                LRRoute = "about:blank";
                pLRRoute = "about:blank";
                noLastRecipe = true;
                Language = "EN";
            }
            public static string penLastRecipe { get; set; } = "No recipe";
            public static string LastRecipe { get; set; } = "No last recipe";
            public static string SavedLogo { get; set; } = "COLORNEW";
            public static string LRRoute { get; set; } = "about:blank";
            public static string pLRRoute { get; set; } = "about:blank";
            public static bool noLastRecipe { get; set; } = true;
            public static string Language { get; set; } = "EN";
        }
    }

}
