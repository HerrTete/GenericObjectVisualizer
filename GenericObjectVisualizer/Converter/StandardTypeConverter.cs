using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericObjectVisualizer
{
    internal static class StandardTypeConverter
    {
        private static readonly Dictionary<Type, string> _supportedTypes = new Dictionary<Type, string>();
        static StandardTypeConverter()
        {
            AddType(typeof(string));
            AddType(typeof(double));
            AddType(typeof(int));
            AddType(typeof(TimeSpan));
            AddType(typeof(DateTime));
            AddType(typeof(byte));
        }

        private static void AddType(Type type)
        {
            _supportedTypes.Add(type, type.Name);
        }

        internal static bool IsStandardType(object inputObject)
        {
            return IsStandardType(inputObject.GetType());
        }

        internal static bool IsStandardType(Type type)
        {
            return IsStandardType(type.Name);
        }

        internal static bool IsStandardType(string typeName)
        {
            return _supportedTypes.Values.Contains(typeName);
        }

        internal static string ConvertToString(object sourceContent)
        {
            if (!IsStandardType(sourceContent.GetType()))
            {
                throw new Exception("Wrong Type Input");
            }
            string value;
            switch (sourceContent.GetType().Name)
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

        internal static object ConvertFromString(string value, Type targetType)
        {
            object targetValue = null;
            if (targetType != typeof(string))
            {
                switch (targetType.Name)
                {
                    case "Byte":
                        targetValue = Convert.ToByte(value);
                        break;
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
    }
}