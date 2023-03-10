using System;

namespace Core
{
    /// <summary>
    /// Show extention constants 
    /// </summary>
    public static class Constants
    {
        #region TODO list


        // TODO: Create better ui look
        // TODO: Create logo to extention
        // TODO: Create search of sample components with auto complete dropdown?
        // TODO: Fix toolkit:Themes.UseVsTheme="True" bug
        // TODO: Add tests to project
        // TODO: Use http call or do clone to project?


        #endregion

        /// <summary>
        /// Mud Blazor github url to fetch tree source code.
        /// </summary>
        public static string MudBlazor_Github_Url = @"https://api.github.com/repos/MudBlazor/MudBlazor/git/trees/dev?recursive=1";

        /// <summary>
        /// First path of url to fetch raw component from github 
        /// </summary>
        public static string MudBlazor_Raw_Component = @"https://raw.githubusercontent.com/MudBlazor/MudBlazor/dev/src/MudBlazor.Docs/Pages/Components/";

        /// <summary>
        /// Using product header to put in user agent request.
        /// </summary>
        public static string ProductInfoHeader = "InjectSampleMudBlazorVSExtention";

        /// <summary>
        /// Time span to hit source code to see if there is changes in source code and update it.
        /// </summary>
        public static TimeSpan Reload_ComponentsNames_TimeSpan = TimeSpan.FromDays(3);

        /// <summary>
        /// Project extention name 
        /// </summary>
        public static string Extention_Name = "Inject Sample MudBlazor";

        /// <summary>
        /// Auto generated comment adds after injectd the component
        /// </summary>
        public static string AutoGenerated_Comment = "This is auto genereted comment Injected by Inject Sample MudBlazor Extention";

    }
}

