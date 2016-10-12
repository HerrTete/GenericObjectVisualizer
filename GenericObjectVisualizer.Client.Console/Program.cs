using System;
using GenericObjectVisualizer.UiWrapper;

namespace GenericObjectVisualizer.Client.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GenericObjectVisualizer.VisualizerWindow.VisualizeObject(new TestObject(), VisualizerDisplayStyle.KeyValue, VisualizerWindowStyle.Modal);
        }
    }
}
