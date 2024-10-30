using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Channels;

namespace P06Proxy
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
        static void Main(string[] args)
        {
           
            var market = new Subject<float>(); //  Rynek , który jest obserwowany przez inwestorów
            var marketConsumer = new Subject<float>(); // Inwestorzy, którzy obserwują rynek

            marketConsumer.SubscribeTo(market); // subskrypcja inwestorów do rynku
          
            market
                .Where(value => value > 2)
                .Subscribe(marketConsumer); // subskrypcja rynku do inwestorów


            marketConsumer.Subscribe(Console.WriteLine); // wyswietlenie wartosci 

            market.OnNext(1);
            market.OnNext(2);
            market.OnNext(3);
            market.OnNext(4);
            market.OnCompleted();
        }
    }
}
