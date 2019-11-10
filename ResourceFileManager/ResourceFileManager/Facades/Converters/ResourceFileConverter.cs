using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using System;
using System.Reflection;
using ResourceFileManager.Facades.ResourceFileTypeIdentifiers;
using System.Runtime.InteropServices;

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
            if (type == null) return default(TChildResourceFile);

            TChildResourceFile childResourceFile = default(TChildResourceFile);
            try
            {
                childResourceFile = (TChildResourceFile)Activator.CreateInstance(type);

                foreach (PropertyInfo property in parentResourceFile.GetType().GetProperties())
                {
                    PropertyInfo childProp = childResourceFile.GetType().GetProperty(property.Name);
                    childProp.SetValue(childResourceFile, property.GetValue(parentResourceFile, null), null);
                }
            }
            catch(Exception e) when (e is ArgumentException || e is NotSupportedException || e is TargetInvocationException
                || e is MethodAccessException || e is MemberAccessException || e is InvalidComObjectException
                || e is MissingMethodException || e is COMException || e is TypeLoadException)
            {
                Console.WriteLine(e.ToString());
            }

            return childResourceFile;
        }
    }
}
