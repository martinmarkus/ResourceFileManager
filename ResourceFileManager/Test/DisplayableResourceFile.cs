using ResourceFileManager;

namespace Test
{
    [DisplayableResourceFile(".xml")]
    public class DisplayableResourceFile : ResourceFile, IDisplayableResourceFile
    {
        public DisplayableResourceFile(IDisplayer displayer, string fullpath) : base(fullpath)
        {
            Displayer = displayer;
        }

        public IDisplayer Displayer { get; set; }
    }
}
