using System;

namespace GenericObjectVisualizer.Client.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            GenericObjectVisualizer.VisualizerWindow.VisualizeObject(new TestObject(), VisualizerWindowStyle.KeyValue, true);
        }
    }
}
