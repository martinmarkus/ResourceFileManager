using ResourceFileManager;

namespace Test
{
    [DisplayableResourceFile(".xml")]
    public class DisplayableResourceFile : ResourceFile, IDisplayableResourceFile
    {
        public IDisplayer Displayer { get; set; }
    }
}
