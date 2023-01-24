global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;

namespace InjectSampleMudBlazor
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MyToolWindow.Pane), Style = VsDockStyle.Tabbed, DockedWidth = 400, Window = WindowGuids.DocumentWell, Orientation = ToolWindowOrientation.Left)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.InjectSampleMudBlazorString)]
    public sealed class InjectSampleMudBlazorPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();

            this.RegisterToolWindows();
        }
    }
}