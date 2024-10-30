using System.Reactive.Subjects;

namespace Example1
{
    internal class Program
    {
        static void Main1(string[] args)
        {
           
            var sensor = new BehaviorSubject<float>(1.0f);


            sensor.Subscribe(Console.WriteLine);

            sensor.OnNext(2.0f);

            var otherObserver = sensor.Subscribe(
                x=> Console.WriteLine($"other observer generated value {x}"),
                ex => Console.WriteLine($"error : {ex}"));
        }
    }
}

namespace Example2
{
    public class Scada
    {

        private BehaviorSubject<int> sensorValue;

        public Scada(int initialValue)
        {
            sensorValue = new BehaviorSubject<int>(initialValue);
        }

        public IObservable<int> SensorValue => sensorValue;

        internal void UpdateSensorValue(int v)
        {
            sensorValue.OnNext(v);
        }

        internal void CompleteSensor()
        {
            sensorValue.OnCompleted();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            var scadaSystem = new Scada(1);

            scadaSystem.SensorValue.Subscribe(Console.WriteLine);

            scadaSystem.UpdateSensorValue(2);

            scadaSystem.CompleteSensor();
        }
    }
}
