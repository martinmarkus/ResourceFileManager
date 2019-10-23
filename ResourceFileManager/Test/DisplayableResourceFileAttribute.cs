using FactorySupporter.Attributes;

namespace Test
{
    public class DisplayableResourceFileAttribute : IdentifierAttribute
    {
        public string HandledFormat { get; set; }

        public DisplayableResourceFileAttribute(string handledFormat)
        {
            HandledFormat = handledFormat;
        }
    }
}
