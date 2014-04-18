using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GenericObjectVisualizer.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dlg = new VisualizerDialog();
            dlg.ViewModel = new TestObject();
            dlg.ShowDialog();
        }
    }
}
