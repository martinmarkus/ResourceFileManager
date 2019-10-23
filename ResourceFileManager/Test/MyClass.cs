using ResourceFileManager.Attributes;

namespace Test
{
    public class MyClass
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }

        public MyClass(string value1, string value2, string value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }
        public MyClass()
        {

        }
    }
}
