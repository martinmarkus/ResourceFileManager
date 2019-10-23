using ResourceFileManager.ResourceFiles;

namespace Test
{
    public interface IDisplayableResourceFile : IResourceFile
    {
        IDisplayer Displayer { get; set; }
    }
}
