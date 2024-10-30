using System.Reactive.Linq;

namespace P24SequenceAggregations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Observable.Range(1, 10)
                .Aggregate((acc, x) => acc + x)
                .Subscribe(Console.WriteLine);


            Observable.Range(1, 10)
                .Count().Subscribe(Console.WriteLine);

            Observable.Range(1, 10)
                .Min().Subscribe(Console.WriteLine);

            Observable.Range(1, 10)
              .Max().Subscribe(Console.WriteLine);
        }
    }
}
