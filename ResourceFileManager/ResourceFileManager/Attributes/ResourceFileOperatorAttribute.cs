using FactorySupporter.Attributes;

namespace ResourceFileManager.Attributes
{
    public class ResourceFileOperatorAttribute : IdentifierAttribute
    {
        public string HandledFormat { get; set; }

        public ResourceFileOperatorAttribute(string handledFormat)
        {
            HandledFormat = handledFormat;
        }
    }
}
