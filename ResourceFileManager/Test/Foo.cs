namespace Test
{
    public class Foo
    {
        public int Integer { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public bool Bool { get; set; }

        public Foo(int integer, double @double, float @float, bool @bool)
        {
            Integer = integer;
            Double = @double;
            Float = @float;
            Bool = @bool;
        }

        public Foo()
        {
        }
    }
}
