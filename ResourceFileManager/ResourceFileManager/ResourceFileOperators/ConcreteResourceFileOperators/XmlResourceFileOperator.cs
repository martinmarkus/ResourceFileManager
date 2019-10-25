using ResourceFileManager.Attributes;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ResourceFileManager.ResourceFileOperators.ConcreteResourceFileOperators
{
    [ResourceFileOperator(".xml")]
    public class XmlResourceFileOperator : IResourceFileOperator
    {
       
        public object Read(string fullPath, Type type)
        {
            object result = default(object);

            string attributeName = type.Name;

            XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(attributeName);
            XmlSerializer serializer = new XmlSerializer(type, xmlRootAttribute);

            StreamReader reader = new StreamReader(fullPath);
            result = serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        public bool Write(string fullPath, object value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(object));

            try
            {
                using (FileStream fs = new FileStream(@fullPath, FileMode.Create))
                {
                    serializer.Serialize(fs, value);
                }
                return true;
            }
            catch (Exception e) when (e is InvalidOperationException || e is IOException)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
