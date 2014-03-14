using System;

namespace GenericObjectVisualizer
{
    public interface IConverter
    {
        object ConvertToObject(string inputString, Type t);
        string ConvertFromObject(object o);
    }
}