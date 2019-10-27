using FactorySupporter;
using FactorySupporter.Attributes;
using FactorySupporter.Delegates;
using ResourceFileManager.Attributes;
using ResourceFileManager.ResourceFileOperators;
using System;
using System.IO;
using System.Reflection;
using ResourceFileManager.Facades.Converters;
namespace ResourceFileManager.ResourceFileFactories
{
    public class ResourceFileFactory
    {
        protected IImplementationFactory _implementationFactory;

        protected string _extension;

        private Assembly OwnExecutingAssembly { get; } = Assembly.GetExecutingAssembly();

        public Assembly ExecutingAssembly
        {
            get
            {
                return _implementationFactory?.ExecutingAssembly;
            }
            set
            {
                _implementationFactory.ExecutingAssembly = value;
            }
        }

        public ResourceFileFactory()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _implementationFactory = new ImplementationFactory(assembly);
            ExecutingAssembly = assembly;
        }

        public ResourceFileFactory(Assembly assembly)
        {
            _implementationFactory = new ImplementationFactory(assembly);
            ExecutingAssembly = assembly;
        }


        public IResourceFile Create<TContentType>(string fullPath)
            where TContentType : class
        {
            _extension = GetExtension(fullPath);

            IResourceFile resourceFile = new ResourceFile();

            IResourceFileOperator resourceFileOperator = _implementationFactory
                .Create<IResourceFileOperator, ResourceFileOperatorAttribute>(OwnExecutingAssembly, ResourceFileOpreatorIdentifierFunc);

            resourceFile.ResourceFileOperator = resourceFileOperator;
            resourceFile.ContentType = typeof(TContentType);

            return resourceFile;
        }

        protected TChildResourceFile CreateChild<TContentType, TChildResourceFile, TAttribute>(string fullPath, IdentifierFunc<TAttribute> identifierFunc)
            where TContentType : class
            where TChildResourceFile : IResourceFile
            where TAttribute : IdentifierAttribute
        {
            IResourceFile resourceFile = Create<TContentType>(fullPath);
            ResourceFileConverter resourceFileConverter = new ResourceFileConverter();

            TChildResourceFile result = resourceFileConverter.Convert<TChildResourceFile, TAttribute>(resourceFile, identifierFunc, ExecutingAssembly);

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
