using System.Reactive.Subjects;

namespace Example1
{
    internal class Program
    {
        static void Main1(string[] args)
        {
            AsyncSubject<double> subject = new AsyncSubject<double>();

            subject.Subscribe(value => System.Console.WriteLine($"Observer 1: {value}"));

            subject.OnNext(1.0);
            subject.OnNext(2.0);

            subject.OnCompleted();

        }
    }
}

namespace Example2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AsyncSubject<double> subject = new AsyncSubject<double>();

            subject.Subscribe(value => System.Console.WriteLine($"Observer 1: {value}"));


            //   Task.Run(() => SimulateAsyncOperation(subject));
            await SimulateAsyncOperation(subject);

            Console.WriteLine("Finish process");
            Console.ReadKey();

        }

        static async Task SimulateAsyncOperation(AsyncSubject<double> subject)
        {
            Console.WriteLine("Start operation..");
            await Task.Delay(1000);
            subject.OnNext(1.0);
            await Task.Delay(1000);
            subject.OnNext(2.0);
            subject.OnCompleted();
        }
    }
}