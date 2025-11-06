namespace Recip4Linux;

public enum Language
{
    English,
    Spanish
}

public static class Strings
{
    // UI Texts
    public static string BtnOpen;
    public static string BtnMake;
    public static string GroupAppSettings;
    public static string ComboColor;
    public static string ComboColorRescaled;
    public static string ComboMonochromatic;
    public static string ComboBW;
    public static string BtnSetLogo;
    public static string GroupLatestRecipes;
    public static string MenuFile;
    public static string MenuHelp;
    public static string MenuFileNew;
    public static string MenuFileOpen;
    public static string MenuHelpAbout;
    public static string LabelAbout;
    public static string LabelRecipeName;
    public static string LabelIngredients;
    public static string LabelSteps;
    public static string BtnSave;
    public static string BtnClose;
    public static string BtnBack;
    public static string BtnNoLastRecipe;
    public static string BtnNoRecipe;
    public static string MsgNoRecipeSelected;
    public static string MsgResetSettings;
    public static string MsgRestartToApply;
    public static string ToolTipReset;
    public static string ToolTipRestart;
    public static string MenuLanguage;
    public static string MenuLanguageChange;
    public static string StatusLoading;
    public static string ErrorRetrievingRecipes;
    public static string StatusPublishing;
    public static string PublishSuccess;
    public static string PublishError;
    public static string BtnPublish;

    public static void SetLang(Language lang)
    {
        switch (lang)
        {
            case Language.English:
                BtnOpen = "Open";
                BtnMake = "New";
                GroupAppSettings = "App settings";
                ComboColor = "Color";
                ComboColorRescaled = "Color (rescaled)";
                ComboMonochromatic = "Monochromatic";
                ComboBW = "B&W";
                BtnSetLogo = "Set logo";
                GroupLatestRecipes = "Latest recipes";
                MenuFile = "&File";
                MenuHelp = "&Help";
                MenuFileNew = "&New";
                MenuFileOpen = "&Open";
                MenuHelpAbout = "&About Recip";
                LabelAbout = "Recip v1.2 is the version published August 25, 2025. Its license is MIT\n License, check the license terms and rights just below. To contribute,\nvisit the project site: https://github.com/repoficialx/recip";
                LabelRecipeName = "Recipe Name:";
                LabelIngredients = "Ingredients:";
                LabelSteps = "Steps:";
                BtnSave = "Save";
                BtnClose = "Close";
                BtnBack = "< Back";
                BtnNoLastRecipe = "No last recipe";
                BtnNoRecipe = "No recipe";
                MsgNoRecipeSelected = "No Recipe Selected";
                MsgResetSettings = "Are you sure you want to reset the settings?";
                MsgRestartToApply = "Restart app to apply changes?";
                ToolTipReset = "Reset settings to default";
                ToolTipRestart = "Restart app";
                MenuLanguage = "&Language";
                MenuLanguageChange = "&Change Language";
                StatusLoading = "Loading...";
                ErrorRetrievingRecipes = "Couldn't retrieve recipes.";
                StatusPublishing = "Publishing...";
                PublishSuccess = "Recipe published successfully 🍽️";
                PublishError = "Error publishing recipe.";
                BtnPublish = "Publish";
                break;

            case Language.Spanish:
                BtnOpen = "Abrir";
                BtnMake = "Crear";
                GroupAppSettings = "Ajustes de la app";
                ComboColor = "Color";
                ComboColorRescaled = "Color (reescalado)";
                ComboMonochromatic = "Monocromático";
                ComboBW = "B/N";
                BtnSetLogo = "Cambiar logo";
                GroupLatestRecipes = "Recetas recientes";
                MenuFile = "Ar&chivo";
                MenuHelp = "A&yuda";
                MenuFileNew = "&Nuevo";
                MenuFileOpen = "&Abrir";
                MenuHelpAbout = "Acerca de &Recip";
                LabelAbout = "Recip v1.2 es la versión publicada el 25 Ago. 2025. Su licencia es la \nLicencia MIT. Puedes ver tus derechos y los términos debajo. Para \ncontribuir, visita el proyecto: https://github.com/repoficialx/recip";
                LabelRecipeName = "Nombre de la receta:";
                LabelIngredients = "Ingredientes:";
                LabelSteps = "Pasos:";
                BtnSave = "Guardar";
                BtnClose = "Cerrar";
                BtnBack = "< Atrás";
                BtnNoLastRecipe = "Sin última receta";
                BtnNoRecipe = "Sin receta";
                MsgNoRecipeSelected = "No se ha seleccionado ninguna receta";
                MsgResetSettings = "¿Estás seguro de que quieres restablecer la configuración?";
                MsgRestartToApply = "¿Reiniciar app para aplicar cambios?";
                ToolTipReset = "Restablecer configuración a predeterminada";
                ToolTipRestart = "Reiniciar app";
                MenuLanguage = "&Idioma";
                MenuLanguageChange = "Ca&mbiar idioma";
                StatusLoading = "Cargando...";
                ErrorRetrievingRecipes = "No se pudieron obtener las recetas.";
                StatusPublishing = "Publicando...";
                PublishSuccess = "Receta publicada con éxito 🍽️";
                PublishError = "Error al publicar la receta.";
                BtnPublish = "Publicar";
                break;
        }
    }
}
