using System;

namespace ResourceFileManager.Attributes
{
    public class XmlResourceAttribute : Attribute
    {
        public string AttributeName { get; set; }

        public XmlResourceAttribute(string attributeName)
        {
            AttributeName = attributeName;
        }
    }
}
