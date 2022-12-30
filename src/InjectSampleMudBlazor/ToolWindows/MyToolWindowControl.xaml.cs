using Core.Models;
using Core.Store;
using InjectSampleMudBlazor.Commands;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InjectSampleMudBlazor
{
    public partial class MyToolWindowControl : UserControl
    {
        private ICommand _sampleComponentCommand = new SampleComponentCommand();

        public MyToolWindowControl()
        {
            InitializeComponent();
            AddSampleButtons();
        }

        private void AddSampleButtons()
        {
            if (ComponentsStore.Components.Any())
            {
                foreach (var father in ComponentsStore.Components)
                {
                    foreach (var sampleSon in father.SampleComponents)
                    {
                        stackPanel.Children.Add(new Button()
                        {
                            Content = sampleSon.Name,
                            HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left,
                            Command = _sampleComponentCommand,
                            CommandParameter = new RawComponentDetails(father.ComponentsArea, sampleSon.Name)
                        });  
                    }
                }
            }
            
            // Clear components store components
            ComponentsStore.Components.Clear();
        }
    }
}