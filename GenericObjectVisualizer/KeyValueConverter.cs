using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace GenericObjectVisualizer
{
    internal class KeyValueConverter
    {
        private readonly Dictionary<Type, string> _supportedTypes = new Dictionary<Type, string>();

        internal KeyValueConverter()
        {
            _supportedTypes.Add(typeof(string), typeof(string).Name);
            _supportedTypes.Add(typeof(double), typeof(double).Name);
            _supportedTypes.Add(typeof(int), typeof(int).Name);
            _supportedTypes.Add(typeof(TimeSpan), typeof(TimeSpan).Name);
            _supportedTypes.Add(typeof(DateTime), typeof(DateTime).Name);
        }

        public object ConvertToObject(List<PropertyVisualizerInformations> properties, object targetObject)
        {
            var subOjects = new HashSet<string>();
            var targetType = targetObject.GetType();

            foreach (var property in properties)
            {
                var path = property.Path;
                if (path == null)
                {
                    if (property.Name.EndsWith("]"))
                    {
                        var split = property.Name.Split('[', ']');
                        var index = Convert.ToInt32(split[1]);
                        var propName = split[0];
                        var propInfo = targetType.GetProperty(propName);
                        var targetEnumeration = propInfo.GetValue(targetObject, null) as IEnumerable<object>;
                        var indexer = propInfo.PropertyType.GetProperty("Item");
                        var targetEnumerationItem = targetEnumeration.ElementAt(index);
                        if (_supportedTypes.Values.Contains(targetEnumerationItem.GetType().Name))//In der Enumeration steckt ein Basistyp
                        {
                            var targetValue = GetTargetValue(property.Value, targetEnumerationItem.GetType());
                            indexer.SetValue(targetEnumeration, targetValue, new object[] { index });
                        }
                        else//In der Enumeration steckt kein Basistyp
                        {

                        }
                    }
                    else
                    {
                        var propInfo = targetType.GetProperty(property.Name);
                        var targetValue = GetTargetValue(property.Value, propInfo.PropertyType);
                        propInfo.SetValue(targetObject, targetValue, null);
                    }
                }
                else
                {
                    var firstBackSlash = path.IndexOf("\\");
                    var propname = path.Substring(0, path.Length - (path.Length - firstBackSlash));
                    subOjects.Add(propname);
                }
            }
            foreach (var subOject in subOjects)
            {
                var subObjectProperties = properties.Where(p => p.Path != null && p.Path.StartsWith(subOject + "\\")).ToList();
                var newSubObjectProperties = new List<PropertyVisualizerInformations>();
                foreach (var propertyVisualizerInformationse in subObjectProperties)
                {
                    var name = propertyVisualizerInformationse.Name;
                    var path = propertyVisualizerInformationse.Path;
                    var value = propertyVisualizerInformationse.Value;
                    int firstBackslash = path.IndexOf("\\");
                    path = path.Substring(firstBackslash + 1);
                    if (path == string.Empty)
                    {
                        path = null;
                    }
                    newSubObjectProperties.Add(new PropertyVisualizerInformations(name, value, path));
                }
                var propInfo = targetType.GetProperty(subOject);
                var targetSubObject = propInfo.GetValue(targetObject, null);
                targetSubObject = ConvertToObject(newSubObjectProperties, targetSubObject);
                propInfo.SetValue(targetObject, targetSubObject, null);
            }
            return targetObject;
        }

        public List<PropertyVisualizerInformations> ConvertFromObject(object content)
        {
            var retVal = new List<PropertyVisualizerInformations>();
            if (content != null)
            {
                var type = content.GetType();
                if (_supportedTypes.Values.Contains(type.Name))
                {
                    retVal.Add(new PropertyVisualizerInformations(null, content.ToString(), null)); //ToDo:Timespan passt noch nicht
                }
                else
                {
                    foreach (var propertyInfo in type.GetProperties())
                    {
                        var typeName = propertyInfo.PropertyType.Name;
                        var name = propertyInfo.Name;
                        if (_supportedTypes.Values.Contains(typeName)) //Basistyp
                        {
                            retVal.Add(HandleSupportedBaseType(content, propertyInfo, name));
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)) //IEnumerable
                        {
                            retVal.AddRange(HandleEnumerations(content, propertyInfo));
                        }
                        else //Komplexe Object
                        {
                            var innerObject = propertyInfo.GetValue(content, null);
                            var innerObjectProperties = ConvertFromObject(innerObject);
                            foreach (var propertyVisualizerInformationse in innerObjectProperties)
                            {
                                retVal.Add(new PropertyVisualizerInformations(propertyVisualizerInformationse, name));
                            }
                        }
                    }

                }
            }
            return retVal;
        }

        private IEnumerable<PropertyVisualizerInformations> HandleEnumerations(object content, PropertyInfo propertyInfo)
        {
            var retVal = new List<PropertyVisualizerInformations>();
            var enumerable = propertyInfo.GetValue(content, null) as IEnumerable<object>;
            if (enumerable != null)
            {
                int i = 0;
                foreach (var o in enumerable)
                {
                    var enumeration = ConvertFromObject(o);
                    foreach (var propertyVisualizerInformationse in enumeration)
                    {
                        var name = propertyInfo.Name + "[" + i++ + "]";
                        if (propertyVisualizerInformationse.Name != null)
                        {
                            name += propertyVisualizerInformationse.Name;
                        }

                        retVal.Add(
                            new PropertyVisualizerInformations(
                                name,
                                propertyVisualizerInformationse.Value));
                    }
                }
            }
            return retVal;
        }

        private static object GetTargetValue(string value, Type targetType)
        {
            object targetValue = null;
            if (targetType != typeof(string))
            {
                switch (targetType.Name)
                {
                    case "DateTime":
                        targetValue = Convert.ToDateTime(value);
                        break;
                    case "Double":
                        targetValue = Convert.ToDouble(value);
                        break;
                    case "Int32":
                        targetValue = Convert.ToInt32(value);
                        break;
                    case "TimeSpan":
                        targetValue = TimeSpan.FromMilliseconds(Convert.ToDouble(value));
                        break;
                }
            }
            else
            {
                targetValue = value;
            }
            return targetValue;
        }

        private static PropertyVisualizerInformations HandleSupportedBaseType(object content, PropertyInfo propertyInfo, string name)
        {
            string value = GetStringValue(propertyInfo.PropertyType, propertyInfo.GetValue(content, null));
            return new PropertyVisualizerInformations(name, value);
        }

        private static string GetStringValue(Type sourceType, object sourceContent)
        {
            string value;
            switch (sourceType.Name)
            {
                case "TimeSpan":
                    value = ((TimeSpan)sourceContent).TotalMilliseconds.ToString();
                    break;
                default:
                    value = sourceContent.ToString();
                    break;
            }
            return value;
        }
    }
}