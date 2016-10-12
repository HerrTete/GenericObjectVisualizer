using System;
using GenericObjectVisualizer.Demo;
using GenericObjectVisualizer.UiWrapper;

namespace Genericobjectvisualizer.ConsoleClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            VisualizerWindow.VisualizeObject(new TestObject(), VisualizerDisplayStyle.KeyValue, VisualizerWindowStyle.Modal);
        }
    }
}
