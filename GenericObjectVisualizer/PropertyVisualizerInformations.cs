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

        public PropertyVisualizerInformations(PropertyVisualizerInformations propertyVisualizerInformationse, int iteration)
        {
            Name = propertyVisualizerInformationse.Name;
            Value = propertyVisualizerInformationse.Value;
            Path = string.Concat(propertyVisualizerInformationse, "[", iteration, "]");
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Value { get; set; }
    }
}
