using ResourceFileManager;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo(10, 30.2f, 23, true);
            MyClass myClass = new MyClass("one", "two", "three", foo);

            string fullPath = @"D:\Repos\ResourceFileManager\ResourceFileManager\ResourceFileManager\MyClass.xml";

            DisplayableResourceFileFactory displayableResourceFileFactory = new DisplayableResourceFileFactory();
            IDisplayableResourceFile displayableResourceFile = displayableResourceFileFactory.Create<MyClass>(fullPath);

            IResourceFile resourceFile = new ResourceFile();

            displayableResourceFile.Content = myClass;
            displayableResourceFile.Save();

           

        }
    }
}
