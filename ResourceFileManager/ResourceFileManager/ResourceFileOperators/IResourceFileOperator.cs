using System;

namespace ResourceFileManager.ResourceFileOperators
{
    public interface IResourceFileOperator
    {
        object Read(string fullPath, Type type);
        bool Write(string fullPath, object value);
    }
}
