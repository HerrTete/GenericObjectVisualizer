using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericObjectVisualizer.Client.Console;

namespace GenericObjectVisualizer.Client.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var dlg = new GenericObjectVisualizer.VisualizerDialog();
            dlg.ViewModel = new TestObject();
            dlg.ShowDialog();
        }
    }
}
