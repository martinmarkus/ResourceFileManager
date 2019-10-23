using FactorySupporter;
using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using ResourceFileManager.Attributes;
using ResourceFileManager.Converters;
using ResourceFileManager.ResourceFileOperators;
using ResourceFileManager.ResourceFiles;
using System;
using System.IO;
using System.Reflection;

namespace ResourceFileManager.ResourceFileFactories
{
    public class ResourceFileFactory
    {
        protected IImplementationFactory _implementationFactory;

        protected string _extension;

        public ResourceFileFactory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _implementationFactory = new ImplementationFactory(assembly);
        }

        public IResourceFile Create<T>(string fullPath) where T : class
        {
            _extension = GetExtension(fullPath);

            IResourceFile resourceFile = new ResourceFile();

            IResourceFileOperator resourceFileOperator = _implementationFactory
                .Create<IResourceFileOperator, ResourceFileOperatorAttribute>(ResourceFileOpreatorIdentifierFunc);

            resourceFile.ResourceFileOperator = resourceFileOperator;
            resourceFile.LoadFrom(fullPath);

            return resourceFile;
        }

        protected TChild CreateChild<TChild, TAttribute>(string fullPath, IdentifierFunc<TAttribute> identifierFunc, Assembly executingAssembly)
            where TChild : class, IResourceFile
            where TAttribute : IdentifierAttribute
        {
            IResourceFile resourceFile = Create<TChild>(fullPath);
            ResourceFileConverter resourceFileConverter = new ResourceFileConverter();

            TChild result = resourceFileConverter.Convert<TChild, TAttribute>(resourceFile, identifierFunc, executingAssembly);

            return result;
        }

        protected bool ResourceFileOpreatorIdentifierFunc(ResourceFileOperatorAttribute resourceFileAttribute)
        {
            string handledFormat = resourceFileAttribute.HandledFormat;
            return _extension.Equals(handledFormat, StringComparison.OrdinalIgnoreCase);
        }

        protected string GetExtension(string fullPath)
        {
            string extension = string.Empty;

            try
            {
                extension = Path.GetExtension(fullPath);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.ToString());
            }

            return extension;
        }
    }
}
