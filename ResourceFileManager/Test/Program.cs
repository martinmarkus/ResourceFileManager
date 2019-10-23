using ResourceFileManager.ResourceFileFactories;
using ResourceFileManager.ResourceFiles;
using System.Reflection;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceFileFactory resourceFileFactory = new ResourceFileFactory();

            MyClass myClass = new MyClass("one", "two", "three");

            string fullPath = @"D:\Repos\ResourceFileManager\ResourceFileManager\ResourceFileManager\MyClass.xml";

            IResourceFile resourceFile = resourceFileFactory.Create<MyClass>(fullPath);

        }
    }
}
