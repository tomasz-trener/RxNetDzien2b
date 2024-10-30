using System.Reactive.Subjects;

namespace Example1
{
    internal class Program
    {
        static void Main1(string[] args)
        {
            var market = new Subject<float>(); 

            market.OnNext(1);

            market.Subscribe(x => Console.WriteLine($"Observer 1: {x}"));
        }
    }
}

namespace Example2
{
    internal class Program
    {
        static void Main2(string[] args)
        {
            var market = new ReplaySubject<float>(2);

            market.OnNext(1);
           
            market.OnNext(2);

            market.OnNext(3);

            market.Subscribe(x => Console.WriteLine($"Observer 1: {x}"));

        }
    }
}

namespace Example3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var timeWinodw = TimeSpan.FromMilliseconds(500);
            var market = new ReplaySubject<float>(timeWinodw);

            market.OnNext(1);
            Thread.Sleep(200);
            market.OnNext(2);
            Thread.Sleep(200);
            market.OnNext(3);
            Thread.Sleep(200);

            market.Subscribe(x => Console.WriteLine($"Observer 1: {x}"));

            market.OnNext(4);

        }
    }
}