using ResourceFileManager;
using ResourceFileManager.ResourceFileFactories;
using System;
using System.Reflection;

namespace Test
{
    public class DisplayableResourceFileFactory : ResourceFileFactory
    {
        public new IDisplayableResourceFile Create<TChild>(string fullPath)
            where TChild : class
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            IDisplayableResourceFile displayableResourceFile = CreateChild<TChild, IDisplayableResourceFile, DisplayableResourceFileAttribute>(fullPath, ResourceFileIdentifierFunc);

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
