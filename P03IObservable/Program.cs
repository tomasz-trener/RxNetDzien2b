namespace P03IObservable
{

    public class Market : IObservable<float>
    {
        private List<IObserver<float>> observers = new List<IObserver<float>>();

 
        public IDisposable Subscribe(IObserver<float> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        public void Notify(float stock)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(stock);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
            {
                if (observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }
            observers.Clear();
        }

        public void Error(Exception error)
        {
            foreach (var observer in observers)
            {
                observer.OnError(error);
            }
        }

        internal class Unsubscriber : IDisposable
        {
            private List<IObserver<float>> _observers;
            private IObserver<float> _observer;

            public Unsubscriber(List<IObserver<float>> observers, IObserver<float> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }

            }
        }
    }

  

    internal class Program : IObserver<float>
    {
        static void Main(string[] args)
        {
             var market = new Market(); // obiekt obserwowany

            var observer = new Program(); // obiekt obserwujący

            using (market.Subscribe(observer))
            {
                market.Notify(123.45f);
                market.Notify(123.46f);
                market.Notify(123.47f);
                market.EndTransmission();
            }
        }

        public void OnCompleted()
        {
            Console.WriteLine($"Market data transmission completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Error {error.Message}");
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"Received new market value {value}");
        }
    }
}
