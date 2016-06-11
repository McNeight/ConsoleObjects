using System.Threading;

namespace ConsoleObjects.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleObject(125, 30, "Sample Console");

            var section1 = new Section(console, Size.FromRatio(1), Layout.Horizontal);
            new Section(section1, Size.FromRatio(1), "Section 1A");
            new Section(section1, Size.FromRatio(1), "Section 1B");

            var section2 = new Section(console, Size.FromRatio(1), "Section 2");
            new Section(section2, Size.FromValues(20, 10), "Section 2A");

            console.Flush();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
