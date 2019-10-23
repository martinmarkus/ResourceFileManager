namespace Test
{
    [DisplayerAttribute(1)]
    public class Displayer : IDisplayer
    {
        public void Display()
        {
            System.Console.WriteLine("display an xml file");
        }
    }
}
