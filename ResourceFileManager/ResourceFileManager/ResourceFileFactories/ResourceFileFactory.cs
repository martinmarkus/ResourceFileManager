using FactorySupporter;
using ResourceFileManager.Attributes;
using ResourceFileManager.ResourceFileOperators;
using ResourceFileManager.ResourceFiles;
using System;
using System.IO;
using System.Reflection;

namespace ResourceFileManager.ResourceFileFactories
{
    public class ResourceFileFactory
    {
        private IImplementationFactory _implementationFactory;

        private string _extension;

        public ResourceFileFactory()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            _implementationFactory = new ImplementationFactory(executingAssembly);
        }

        public IResourceFile Create<T>(string fullPath) where T : class
        {
            _extension = GetExtension(fullPath);

            IResourceFile resourceFile = new ResourceFile();

            IResourceFileOperator resourceFileOperator = _implementationFactory
                .Create<IResourceFileOperator, ResourceFileOperatorAttribute>(ResourceFileOpreatorIdentifierFunc);

            resourceFile.ResourceFileOperator = resourceFileOperator;
            resourceFile.LoadFrom(fullPath);

            resourceFile.Content = (T)resourceFile.Content;

            return resourceFile;
        }

        private bool ResourceFileOpreatorIdentifierFunc(ResourceFileOperatorAttribute resourceFileAttribute)
        {
            string handledFormat = resourceFileAttribute.HandledFormat;
            return _extension.Equals(handledFormat, StringComparison.OrdinalIgnoreCase);
        }

        private string GetExtension(string fullPath)
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
