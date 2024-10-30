using System.Reactive.Subjects;

namespace P05Unsubscribing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sensor = new Subject<float>();

            using(var subscription = sensor.Subscribe(Console.WriteLine))
            {
                sensor.OnNext(1.0f);
                sensor.OnNext(2.0f);
                sensor.OnNext(3.0f);
            }

            // subskrypcja zostanie usunięta

            sensor.OnNext(4.0f);
            sensor.OnNext(5.0f);

          //  var subscription2 = sensor.Subscribe(Console.WriteLine);
          //  subscription2.Dispose();
        }
    }
}
