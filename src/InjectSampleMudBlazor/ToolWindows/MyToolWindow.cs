using Core.Services;
using Core.Store;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Core.Data;

namespace InjectSampleMudBlazor
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        private string _cacheDataFolder;

        public override string GetTitle(int toolWindowId) => Core.Constants.Extention_Name;

        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            try
            {
                using MISComponentsService _componentsService = new MISComponentsService();

                try
                {
                    if (string.IsNullOrEmpty(_cacheDataFolder))
                    {
                        _cacheDataFolder = Path.GetDirectoryName(typeof(Element).Assembly.Location);
                        Directory.CreateDirectory($"{_cacheDataFolder}/cache");
                    }

                    ComponentsStore.RegisterComponentsFromCache(await _componentsService.LoadComponentsFromJsonFileAsync($"{_cacheDataFolder}/cache/componenets.json"));
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                    {
                        var data = await _componentsService.GetTreeResponseAsync();
                        ComponentsStore store = new ComponentsStore(data.tree);
                        await _componentsService.WriteComponentsToJsonFileAsync($"{_cacheDataFolder}/cache/componenets.json", ComponentsStore.Components);
                    }
                    Console.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await VS.MessageBox.ShowWarningAsync("Inject sample package throw exception!", $"ex : {ex.Message}");
            }

            return await Task.FromResult<FrameworkElement>(new MyToolWindowControl());
        }

        [Guid("c1b41ddc-fd1c-4040-8063-e6e26f571e98")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}