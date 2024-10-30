using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace P21SequenceTransfomrations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //var numbers = Observable.Range(1, 10);
            //numbers.Select(x => x * x)
            //    .Subscribe(Console.WriteLine);

            //var subj = new Subject<object>();
            //subj.OfType<float>().Subscribe(x=> Console.WriteLine("of type"));

            //subj.Cast<float>().Subscribe(x => Console.WriteLine("cast"));

            //subj.OnNext(1.0f);
            //subj.OnNext(2);
            //subj.OnNext(3.0);

            Observable.Range(1, 10)
                .SelectMany(x => Observable.Range(1, x,ImmediateScheduler.Instance))
                .Subscribe(Console.WriteLine);
        }
    }
}
