using ResourceFileManager.Attributes;
using ResourceFileManager.Converters;
using ResourceFileManager.ResourceFileFactories;
using ResourceFileManager.ResourceFileOperators;
using System;
using System.Reflection;

namespace Test
{
    public class DisplayableResourceFileFactory : ResourceFileFactory
    {
        public new IDisplayableResourceFile Create<T>(string fullPath) where T : class
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            IDisplayableResourceFile displayableResourceFile = CreateChild<IDisplayableResourceFile, DisplayableResourceFileAttribute>(fullPath, ResourceFileIdentifierFunc, executingAssembly);

            IDisplayer displayer = _implementationFactory.Create<IDisplayer, DisplayerAttribute>(executingAssembly, DisplayerIdentifierFunc);
            displayableResourceFile.Displayer = displayer;

            return displayableResourceFile;
        }

        private bool DisplayerIdentifierFunc(DisplayerAttribute displayerAttribute)
        {
            return displayerAttribute.id == 1;
        }

        private bool ResourceFileIdentifierFunc(DisplayableResourceFileAttribute displayableResourceFileAttribute)
        {
            return displayableResourceFileAttribute.HandledFormat.Equals(".xml", StringComparison.OrdinalIgnoreCase);
        }


    }
}
