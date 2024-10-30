using System.Reactive.Linq;

namespace P19SequenceFiltering
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var values = Observable.Range(-10, 20);

            values
                .Select(x=> x * x)
                .Distinct()
                .Where(x => x % 2 == 0)
                .Subscribe(Console.WriteLine);


            new List<int> { 1,2,3,4,4,4,5,5 }
                .ToObservable()
                .DistinctUntilChanged()
                .Subscribe(Console.WriteLine);

            Observable.Range(1, 10)
                .Skip(3)
                .Take(3)
                .Subscribe(Console.WriteLine);

            Observable.Range(1, 10)
               .SkipWhile(x=> x< 0)
               .TakeWhile(x=>x<6)
               .Subscribe(Console.WriteLine);

            Observable.Range(1, 10)
                .SkipLast(3)
             .Subscribe(Console.WriteLine);
        }
    }
}
