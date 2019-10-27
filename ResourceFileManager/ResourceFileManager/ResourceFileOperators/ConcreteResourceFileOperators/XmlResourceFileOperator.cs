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

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fullPath);
                result = serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException
                || e is IOException || e is FileNotFoundException 
                || e is DirectoryNotFoundException || e is InvalidOperationException)
            {
                throw e;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return result;
        }

        public bool Write(string fullPath, object value, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);

            try
            {
                using (StreamWriter fs = new StreamWriter(@fullPath))
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
