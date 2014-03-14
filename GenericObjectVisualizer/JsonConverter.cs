using System;
using System.Web.Script.Serialization;

namespace GenericObjectVisualizer
{
    internal class JsonConverter : IConverter
    {
        public object ConvertToObject(string inputString, Type t)
        {
            return new JavaScriptSerializer().Deserialize(inputString, t);
        }

        public string ConvertFromObject(object o)
        {
            return new JavaScriptSerializer().Serialize(o);
        }
    }
}