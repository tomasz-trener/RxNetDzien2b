using System.Reactive.Linq;

namespace Example1
{
    internal class Program
    {
        class Market
        {
            private float _price;

            public float Price
            {
                get => _price;
                set
                {
                    if (value.Equals(_price)) return;
                    _price = value;
                    PriceChange?.Invoke(this, value);
                }
            }

            public void ChangePrice(float price)
            {
                Price = price;
            }

            public event EventHandler<float> PriceChange;
        }

        static void Main1(string[] args)
        {
            var market = new Market();
          

            var priceChanges= Observable.FromEventPattern<float>(
                h=> market.PriceChange += h,
                h => market.PriceChange -= h
                );

            
            priceChanges.Subscribe(
                x => Console.WriteLine($"Price is {x.EventArgs}"),
                ex => Console.WriteLine($"Error: {ex.Message}"),
                () => Console.WriteLine("Completed")
            );

            market.ChangePrice(123);
            market.ChangePrice(456);
            market.ChangePrice(789);

        }
    }
}

namespace Example2
{
    internal class Program
    {
         

        static void Main(string[] args)
        {
           
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var observable = items.ToObservable();
            observable.Subscribe(
                x => Console.WriteLine($"OnNext: {x}"),
                ex => Console.WriteLine($"OnError: {ex.Message}"),
                () => Console.WriteLine("OnCompleted")
            );
        }
    }
}