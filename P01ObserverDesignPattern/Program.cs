
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Example1{
    public class Market : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;


        private float volatility; // Market volatility

        public float Volatility
        {
            get { return volatility; }
            set
            {
                if (value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    static class Program
    {
        public static void Main1()
        {
            Market market = new Market();


            market.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Volatility")
                {
                    Console.WriteLine($"Volatility change to{((Market)sender).Volatility}");
                }
            };


            market.Volatility = 0.2f; // Set the market volatility
        }


    }
}

namespace Example2
{
    public class Market 
    {


        public List<float> prices = new List<float>();

        public void AddPrice(float price)
        {
            prices.Add(price);
            PriceAdded?.Invoke(this, price);

        }

        public event EventHandler<float> PriceAdded;


    }

    static class Program
    {
        public static void Main2()
        {
            Market market = new Market();


            market.PriceAdded += (sender, price) =>
            {
                Console.WriteLine($"Price added: {price}");
            };

           
            market.AddPrice(1.2f);
        }


    }
}

namespace Example3
{
    public class Market
    {


        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);

        }


    }

    static class Program
    {
        public static void Main()
        {
            Market market = new Market();


            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender)[eventArgs.NewIndex];
                    Console.WriteLine($"Price added: {price}");
                }
                    
            };


            market.AddPrice(1.2f);
        }


    }
}