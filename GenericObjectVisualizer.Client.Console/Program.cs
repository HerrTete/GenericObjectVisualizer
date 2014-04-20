using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericObjectVisualizer.Client.Console;
using System.Threading;

namespace GenericObjectVisualizer.Client.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GenericObjectVisualizer.VisualizerDialog.VisualizeObject(new TestObject(), true);
            /*var dlg = new GenericObjectVisualizer.VisualizerDialog();
            dlg.ViewModel = new TestObject();
            dlg.ShowDialog();*/
        }
    }
}
