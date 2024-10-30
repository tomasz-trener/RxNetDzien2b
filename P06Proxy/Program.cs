using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace Example1
{
    public static class ExtensionMethod
    {
         public static IDisposable SubscribeTo<T>(this IObserver<T> observer, IObservable<T> observable)
        {
            return observable.Subscribe(observer);
        }
    }


    internal class Program
    {
        static void Main1(string[] args)
        {
           
            var market = new Subject<float>(); //  Rynek , który jest obserwowany przez inwestorów
            var marketConsumer = new Subject<float>(); // Inwestorzy, którzy obserwują rynek

            marketConsumer.SubscribeTo(market); // subskrypcja inwestorów do rynku
          
            market
                .Where(value => value > 2)
                .Subscribe(marketConsumer); // subskrypcja rynku do inwestorów


           // marketConsumer.Subscribe(Console.WriteLine); // wyswietlenie wartosci 
           var anotherObserver = new AnotherObserver();
            marketConsumer.Subscribe(anotherObserver);


            market.OnNext(1);
            market.OnNext(2);
            market.OnNext(3);
            market.OnNext(4);
            market.OnCompleted();
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


namespace Example2
{
    public static class ExtensionMethod
    {
     public static IDisposable Inspect<T>(this IObservable<T> self, string name)
        {
            return self.Subscribe(
                value => Console.WriteLine($"{name} - OnNext({value})"),
                error => Console.WriteLine($"{name} - OnError({error.Message})"),
                () => Console.WriteLine($"{name} - OnCompleted()"));
        }  

        public static IObserver<T> OnNext<T>(this IObserver<T> self, params T[] args)
        {
            foreach (var arg in args)
            {
                self.OnNext(arg);
            }
            return self;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            var market = new Subject<float>(); //  Rynek , który jest obserwowany przez inwestorów
            var marketConsumer = new Subject<float>(); // Inwestorzy, którzy obserwują rynek

         //   marketConsumer.SubscribeTo(market); // subskrypcja inwestorów do rynku

            market.Subscribe(marketConsumer); // subskrypcja rynku do inwestorów


            // marketConsumer.Subscribe(Console.WriteLine); // wyswietlenie wartosci 
            //var anotherObserver = new AnotherObserver();
           // marketConsumer.Subscribe(anotherObserver);
           marketConsumer.Inspect("MarketConsumer");


            //market.OnNext(1);
            //market.OnNext(2);
            //market.OnNext(3);
            //market.OnNext(4);
            market.OnNext(1, 2, 3, 4);
            market.OnCompleted();
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