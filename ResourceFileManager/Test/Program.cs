using ResourceFileManager;
using ResourceFileManager.ResourceFileFactories;
using System.Reflection;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo(10, 30.2f, 23, true);
            MyClass myClass = new MyClass("one", "two", "three", foo);

            string fullPath = @"D:\Repos\ResourceFileManager\ResourceFileManager\ResourceFileManager\MyClass.xml";

            ResourceFileFactory resourceFileFactory = new ResourceFileFactory(Assembly.GetExecutingAssembly());

            IResourceFile resourceFile = resourceFileFactory.Create<MyClass>(fullPath);

            DisplayableResourceFileFactory displayableResourceFileFactory = new DisplayableResourceFileFactory(Assembly.GetExecutingAssembly());
            IDisplayableResourceFile displayableResourceFile = displayableResourceFileFactory.Create<MyClass>(fullPath);

            displayableResourceFile.Content = myClass;
            displayableResourceFile.Save();
            


        }
    }
}
