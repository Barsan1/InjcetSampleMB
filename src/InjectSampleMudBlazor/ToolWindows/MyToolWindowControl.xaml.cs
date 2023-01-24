using Core.Models;
using Core.Store;
using InjectSampleMudBlazor.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace InjectSampleMudBlazor
{
    public partial class MyToolWindowControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private ICommand _sampleComponentCommand = new SampleComponentCommand();

        public MyToolWindowControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _currentArea = "0";
        public string CurrentArea
        {
            get { return _currentArea; }
            set
            {
                _currentArea = value;
                Exampels = null;
            }
        }

        private List<ComponentSon> _exampels = ComponentsStore.Components[0].SampleComponents;
        public List<ComponentSon> Exampels
        {
            get { return _exampels; }
            set
            {
                _exampels = ComponentsStore.Components[Convert.ToInt32(_currentArea)].SampleComponents;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Exampels)));
            }
        }

        private void list_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var x = (sender as System.Windows.Controls.ListView).SelectedItem as ComponentSon;
            if (x != null)
            {
                _sampleComponentCommand.Execute(new RawComponentDetails(ComponentsStore.Components[Convert.ToInt32(_currentArea)].ComponentsArea, x.Name));  
            }
        }
    }
}