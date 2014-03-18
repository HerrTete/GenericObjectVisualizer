namespace GenericObjectVisualizer
{
    public class PropertyVisualizerInformations
    {
        public PropertyVisualizerInformations(
            PropertyVisualizerInformations propertyVisualizerInformations,
            string parent)
        {
            Name = propertyVisualizerInformations.Name;
            Value = propertyVisualizerInformations.Value;
            Path = string.Concat(parent, "\\", propertyVisualizerInformations.Path);
        }

        public PropertyVisualizerInformations(string name, string value, string path = null )
        {
            Name = name;
            Path = path;
            Value = value;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Value { get; set; }
    }
}
