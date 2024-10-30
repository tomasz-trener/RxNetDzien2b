using System.Reactive.Linq;

namespace P17ObservableCreate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var o = Observable.Create<string>(observer =>
            {
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += (sender, e) =>
                {
                    observer.OnNext($"Tick {e.SignalTime}");
                };

                timer.Start();
                return () =>
                {
                    timer.Stop();
                    timer.Dispose();
                };
            });


            var subscription = o.Subscribe(x => Console.WriteLine(x));

            Console.ReadKey();

        }
    }
}
