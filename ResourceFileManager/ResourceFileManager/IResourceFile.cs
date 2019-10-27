using ResourceFileManager.ResourceFileOperators;
using System;

namespace ResourceFileManager
{
    public interface IResourceFile
    {
        string Path { get; set; }
        string Name { get; set; }
        string Extension { get; set; }

        bool Save();
        bool SaveAs(string fullPath);

        bool Load();
        bool LoadFrom(string fullPath);

        string GetFullPath();
        string GetPathWithoutExtension();

        object Content { get; set; }
        Type ContentType { get; set; }

        IResourceFileOperator ResourceFileOperator { get; set; }
    }
}
