using System;

namespace Test
{
    [Serializable]
    public class MyClass
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public Foo Foo { get; set; }

        public MyClass(string value1, string value2, string value3, Foo foo)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Foo = foo;
        }
        public MyClass()
        {

        }
    }
}
