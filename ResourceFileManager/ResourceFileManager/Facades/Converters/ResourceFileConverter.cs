using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using System;
using System.Reflection;
using ResourceFileManager.Facades.ResourceFileTypeIdentifiers;

namespace ResourceFileManager.Facades.Converters
{
    internal class ResourceFileConverter
    {
        internal TChildResourceFile Convert<TChildResourceFile, TAttribute>(IResourceFile parentResourceFile,
            IdentifierFunc<TAttribute> identifierFunc, Assembly executingAssembly)
                where TChildResourceFile : IResourceFile
                where TAttribute : IdentifierAttribute
        {
            IResourceFileTypeIdentifier resourceFileTypeIdentifier = new ResourceFileTypeIdentifier();

            Type type = resourceFileTypeIdentifier.GetInstantiationType(identifierFunc, executingAssembly);

            TChildResourceFile childResourceFile = (TChildResourceFile)Activator.CreateInstance(type);

            foreach (PropertyInfo property in parentResourceFile.GetType().GetProperties())
            {
                PropertyInfo childProp = childResourceFile.GetType().GetProperty(property.Name);
                childProp.SetValue(childResourceFile, property.GetValue(parentResourceFile, null), null);
            }

            return childResourceFile;
        }
    }
}
