namespace ResourceFileManager.ResourceFileOperators
{
    public interface IResourceFileOperator
    {
        T Read<T>(string fullPath) where T : class;
        bool Write<T>(string fullPath, T value) where T : class;
    }
}
