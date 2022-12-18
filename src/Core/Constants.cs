using System;

namespace Core
{
    /// <summary>
    /// Show extention constants 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Mud Blazor github url to featch tree source code.
        /// </summary>
        public static string MudBlazor_Github_Url = @"https://api.github.com/repos/MudBlazor/MudBlazor/git/trees/dev?recursive=1";

        /// <summary>
        /// Save temp componnents name in json file to not hit http client all time.
        /// </summary>
        public static string SourceTree_Temp_Path = @"Core/data/src_tree.json";

        /// <summary>
        /// Using product header to put in user agent request.
        /// </summary>
        public static string ProductInfoHeader = "InjectSampleMudBlazorVSExtention";

        /// <summary>
        /// Time span to hit source code to see if there is changes in source code and update it.
        /// </summary>
        public static TimeSpan Reload_ComponentsNames_TimeSpan = TimeSpan.FromDays(3);
    }
}

