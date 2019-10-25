using ResourceFileManager;

namespace Test
{
    public interface IDisplayableResourceFile : IResourceFile
    {
        IDisplayer Displayer { get; set; }
    }
}
