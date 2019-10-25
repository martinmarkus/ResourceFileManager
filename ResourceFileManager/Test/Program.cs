﻿using ResourceFileManager;
using ResourceFileManager.ResourceFileFactories;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass("one", "two", "three");

            string fullPath = @"D:\Repos\ResourceFileManager\ResourceFileManager\ResourceFileManager\MyClass.xml";

            ResourceFileFactory resourceFileFactory = new ResourceFileFactory();
            IResourceFile resourceFile = resourceFileFactory.Create<MyClass>(fullPath);
            myClass = (MyClass)resourceFile.Content;

            DisplayableResourceFileFactory displayableResourceFileFactory = new DisplayableResourceFileFactory();
            IDisplayableResourceFile displayableResourceFile = displayableResourceFileFactory.Create<MyClass>(fullPath);

            resourceFile.Content = myClass;
            resourceFile.Save();

           

        }
    }
}
