using System.Reactive.Linq;

namespace P16SequenceGenerators
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var values = Observable.Range(10, 20);

          //  values.Subscribe(x => Console.WriteLine(x));


            var values2 = Observable.Generate(
                1,
                x => x < 100,
                x => x * 2,
                x => $"[{x}]"
                );

            values2.Subscribe(x => Console.WriteLine(x));


            var timer = Observable.Timer(TimeSpan.FromSeconds(2));

            timer.Subscribe(x => Console.WriteLine($"Timer {x}"));
        }
    }
}
