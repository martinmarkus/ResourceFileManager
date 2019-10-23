using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using FactorySupporter.Facades;
using ResourceFileManager.Facades;
using ResourceFileManager.ResourceFiles;
using System;
using System.Reflection;

namespace ResourceFileManager.Converters
{
    internal class ResourceFileConverter
    {
        internal TChild Convert<TChild, TAttribute>(IResourceFile parentResourceFile, IdentifierFunc<TAttribute> identifierFunc, Assembly executingAssembly)
            where TChild : class
            where TAttribute : IdentifierAttribute
        {
            IResourceFileTypeIdentifier resourceFileTypeIdentifier = new ResourceFileTypeIdentifier();

            Type type = resourceFileTypeIdentifier.GetInstantiationType(identifierFunc, executingAssembly);

            TChild child = (TChild)Activator.CreateInstance(type);

            foreach (PropertyInfo property in parentResourceFile.GetType().GetProperties())
            {
                PropertyInfo childProp = child.GetType().GetProperty(property.Name);
                childProp.SetValue(child, property.GetValue(parentResourceFile, null), null);
            }

            return child;
        }
    }
}
