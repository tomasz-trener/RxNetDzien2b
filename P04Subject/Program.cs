using System.Reactive.Subjects;

namespace P04Subject
{
    internal class Program : IObserver<float>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Market data completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Market data error");
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"Market value: {value}");
        }

        static void Main(string[] args)
        {
            var market = new Subject<float>(); // obiekt obserwowalny 

            market.Subscribe(new Program());

            var anotherObserver = new AnotherObserver();
            market.Subscribe(anotherObserver);
           // market.Subscribe(anotherObserver);

            market.Subscribe(
                value => Console.WriteLine($"Lambda observer value: {value}"),
                error => Console.WriteLine("Lambda observer error"),
                () => Console.WriteLine("Lambda observer completed"));


            market.OnNext(123.45f); // wysyłanie danych do obserwatorów

            market.OnCompleted(); // zakończenie strumienia danych
        }
    }

    public class AnotherObserver : IObserver<float>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Another observer completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Another observer error");
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"Another observer value: {value}");
        }
    }
}
