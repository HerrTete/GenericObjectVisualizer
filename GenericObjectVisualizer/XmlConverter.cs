using System;
using System.IO;
using System.Xml.Serialization;

namespace GenericObjectVisualizer
{
    public class XmlConverter : IConverter
    {
        public static T ConvertXmlToObject<T>(string xmlString)
        {

            T retObj = default(T);
            try
            {
                var sReader = new StringReader(xmlString);
                var seri = new XmlSerializer(typeof(T));
                retObj = (T)seri.Deserialize(sReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message + "\nInner: " + ex.InnerException.Message);
            }

            return retObj;
        }

        public object ConvertToObject(string inputString, Type t)
        {
            object retObj = null;
            try
            {
                var sReader = new StringReader(inputString);
                var seri = new XmlSerializer(t);
                retObj = seri.Deserialize(sReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message + "\nInner: " + ex.InnerException.Message);
            }

            return retObj;
        }

        public string ConvertFromObject(object o)
        {
            var xmlString = string.Empty;
            try
            {
                if (o != null)
                {
                    using (var memStream = new MemoryStream())
                    {
                        var serializer = new XmlSerializer(o.GetType());
                        serializer.Serialize(memStream, o);
                        xmlString = memStream.ToString();
                        memStream.Position = 0;
                        xmlString = new StreamReader(memStream).ReadToEnd();
                        memStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message: " + ex.Message + "\nInner: " + ex.InnerException.Message);
            }
            return xmlString;
        }
    }
}
