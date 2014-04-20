using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace GenericObjectVisualizer.TestUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dlg = new VisualizerWindow();
            dlg.ViewModel = new TestObject1();
            dlg.ShowDialog();
        }
    }
}
