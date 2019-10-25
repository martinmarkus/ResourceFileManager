using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using System;
using System.Reflection;

namespace ResourceFileManager.Facades.ResourceFileTypeIdentifiers
{
    public interface IResourceFileTypeIdentifier
    {
        Type GetInstantiationType<TAttribute>(IdentifierFunc<TAttribute> strategySupportFunc, Assembly assembly)
            where TAttribute : IdentifierAttribute;
    }
}
