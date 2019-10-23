using ResourceFileManager.Attributes;
using System;

namespace ResourceFileManager.ResourceFileOperators.ConcreteResourceFileOperators
{
    [ResourceFileOperator(".xml")]
    public class XmlResourceFileOperator : IResourceFileOperator
    {
        public T Read<T>(string fullPath) where T : class
        {
            throw new NotImplementedException();
        }

        public bool Write<T>(string fullPath, T value) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
