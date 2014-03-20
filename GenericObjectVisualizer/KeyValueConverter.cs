using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericObjectVisualizer
{
    internal class KeyValueConverter
    {
        public static object ConvertToObject(List<PropertyVisualizerInformations> properties, object targetObject)
        {
            if (!StandardTypeConverter.IsStandardType(targetObject))
            {
                var subOjects = new HashSet<string>();
                foreach (var property in properties)
                {
                    if (property.Path == null) //Root
                    {
                        if (property.Name.EndsWith("]"))
                        {
                            targetObject = SetValueOnEnumeration(targetObject, property.Name, property.Value);
                        }
                        else
                        {
                            targetObject = SetValueOnStandardTypeProperty(targetObject, property.Name, property.Value);
                        }
                    }
                    else
                    {
                        if (property.Path.EndsWith("]") && !property.Path.Contains("\\")) //Enumeration
                        {
                            targetObject = SetValueOnEnumeration(
                                targetObject,
                                property.Name,
                                property.Value,
                                property.Path);
                        }
                        else if (property.Path.Contains("\\")) //Komplexes Objekt
                        {
                            subOjects.Add(property.Path.Split('\\')[0]);
                        }
                        else
                        {
                            throw new Exception("strange input!!!");
                        }
                    }
                }
                foreach (var subOject in subOjects)
                {
                    targetObject = SetSubObject(properties, targetObject, subOject);
                }
            }
            else
            {
                targetObject = StandardTypeConverter.ConvertFromString(properties[0].Value, targetObject.GetType());
            }
            return targetObject;
        }

        public static List<PropertyVisualizerInformations> ConvertFromObject(object content)
        {
            var retVal = new List<PropertyVisualizerInformations>();
            if (content != null)
            {
                if (!StandardTypeConverter.IsStandardType(content.GetType()))//Fast immer, nur wenn man zBsp direkt einen string als content in die Methode steckt
                {
                    foreach (var propertyInfo in content.GetType().GetProperties())
                    {
                        if (StandardTypeConverter.IsStandardType(propertyInfo.PropertyType)) //Basistyp
                        {
                            retVal.Add(ConvertStandardType(content, propertyInfo, propertyInfo.Name));
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)) //IEnumerable
                        {
                            retVal.AddRange(
                                ConvertEnumeration(
                                    propertyInfo.GetValue(content, null) as IEnumerable<object>,
                                    propertyInfo.Name));
                        }
                        else //Komplexe Object
                        {
                            var innerObject = propertyInfo.GetValue(content, null);
                            var innerObjectProperties = ConvertFromObject(innerObject);
                            foreach (var propertyVisualizerInformationse in innerObjectProperties)
                            {
                                retVal.Add(
                                    new PropertyVisualizerInformations(
                                        propertyVisualizerInformationse,
                                        propertyInfo.Name));
                            }
                        }
                    }
                }
                else
                {
                    retVal.Add(new PropertyVisualizerInformations(name: null, value: StandardTypeConverter.ConvertToString(content)));
                }
            }
            return retVal;
        }

        private static object SetSubObject(IEnumerable<PropertyVisualizerInformations> properties, object targetObject, string subOject)
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
            if (subOject.EndsWith("]"))
            {
                var split = subOject.Split('[', ']', '\\');
                var index = Convert.ToInt32(split[split.Length - 2]);
                var propName = split[split.Length - 3];
                var propInfo = targetObject.GetType().GetProperty(propName);
                var targetEnumeration = propInfo.GetValue(targetObject, null) as IEnumerable<object>;
                var indexer = propInfo.PropertyType.GetProperty("Item");
                var targetEnumerationItem = targetEnumeration.ElementAt(index);
                targetEnumerationItem = ConvertToObject(newSubObjectProperties, targetEnumerationItem);
                indexer.SetValue(targetEnumeration, targetEnumerationItem, new object[] { index });
            }
            else
            {
                var propInfo = targetObject.GetType().GetProperty(subOject);
                var targetSubObject = propInfo.GetValue(targetObject, null);
                targetSubObject = ConvertToObject(newSubObjectProperties, targetSubObject);
                propInfo.SetValue(targetObject, targetSubObject, null);
            }
            return targetObject;
        }

        private static object SetValueOnStandardTypeProperty(object targetObject, string propertyName, string propertyValue)
        {
            var propInfo = targetObject.GetType().GetProperty(propertyName);
            var targetValue = StandardTypeConverter.ConvertFromString(propertyValue, propInfo.PropertyType);
            propInfo.SetValue(targetObject, targetValue, null);
            return targetObject;
        }

        private static object SetValueOnEnumeration(object targetObject, string propertyName, string propertyValue, string propertyPath = null)
        {
            var splitter = propertyName;
            if (propertyPath != null)
            {
                splitter = propertyPath;
            }
            var split = splitter.Split('[', ']', '\\');
            var index = Convert.ToInt32(split[split.Length - 2]);
            var propName = split[split.Length - 3];
            var propInfo = targetObject.GetType().GetProperty(propName);
            var targetEnumeration = propInfo.GetValue(targetObject, null) as IEnumerable<object>;
            var indexer = propInfo.PropertyType.GetProperty("Item");
            var targetEnumerationItem = targetEnumeration.ElementAt(index);

            if (StandardTypeConverter.IsStandardType(targetEnumerationItem.GetType())) //In der Enumeration steckt ein Basistyp
            {
                var targetValue = StandardTypeConverter.ConvertFromString(propertyValue, targetEnumerationItem.GetType());
                indexer.SetValue(targetEnumeration, targetValue, new object[] { index });
            }
            else //In der Enumeration steckt kein Basistyp
            {
                var inputProperty = new PropertyVisualizerInformations(propertyName, propertyValue);
                var targetValue = ConvertToObject(
                    new List<PropertyVisualizerInformations> { inputProperty },
                    targetEnumerationItem);
                indexer.SetValue(targetEnumeration, targetValue, new object[] { index });
            }
            return targetObject;
        }

        private static IEnumerable<PropertyVisualizerInformations> ConvertEnumeration(IEnumerable<object> enumerable, string propertyName)
        {
            var retVal = new List<PropertyVisualizerInformations>();
            if (enumerable != null)
            {
                int i = 0;
                foreach (var o in enumerable)
                {
                    var name = propertyName + "[" + i++ + "]";
                    if (StandardTypeConverter.IsStandardType(o.GetType()))//Standardtype
                    {
                        retVal.Add(new PropertyVisualizerInformations(name, StandardTypeConverter.ConvertToString(o)));
                    }
                    else//Komplexes Objekt
                    {
                        var enumeration = ConvertFromObject(o);
                        foreach (var propertyVisualizerInformationse in enumeration)
                        {
                            var path = name;
                            if (propertyVisualizerInformationse.Path != null)
                            {
                                path += "\\" + propertyVisualizerInformationse.Path;
                            }
                            retVal.Add(
                                new PropertyVisualizerInformations(
                                    propertyVisualizerInformationse.Name,
                                    propertyVisualizerInformationse.Value, 
                                    path));
                        }
                    }
                }
            }
            return retVal;
        }

        private static PropertyVisualizerInformations ConvertStandardType(object content, PropertyInfo propertyInfo, string name)
        {
            string value = StandardTypeConverter.ConvertToString(propertyInfo.GetValue(content, null));
            return new PropertyVisualizerInformations(name, value);
        }
    }
}