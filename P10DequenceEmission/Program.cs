using Newtonsoft.Json;
using P12Weather.Shared;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace P10DequenceEmission
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var obs=  Observable.Return(42);

            //obs.Subscribe(x=> Console.WriteLine($"Completed {x}"));

            var weatherService = new WeatherService(new HttpClient());
            string apiUrl = "http://localhost:5043/weatherforecast";

            //var weatherForecast = weatherService.GetWeatherForecast(apiUrl);
            var weatherForecast = weatherService.GetWeatherForecast(apiUrl);


            weatherForecast.Subscribe(
                x => Console.WriteLine($"Data from forecast: {x}"),
                error => Console.WriteLine($"Error fetching data {error.Message}"));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }


    public class WeatherService
    {
        private HttpClient _httpClient;

        public WeatherService(HttpClient client)
        {
            _httpClient = client;
        }

        public virtual IObservable<string> GetWeatherForecast(string apiUrl)
        {
            var o = Observable
                .FromAsync(async ct => await GetWeatherForecastAsync(apiUrl))
                .Catch(Observable.Return("0"));
            return o;

        }
        public IObservable<string> GetWeatherForecast2(string apiUrl)
        {
            var asyncSubject = new AsyncSubject<string>();
            var anotherSubject = new Subject<string>();
            GetWeatherForecastAsync(apiUrl).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    anotherSubject.OnError(t.Exception);
                }
                else
                {
                    anotherSubject.OnNext("Started processing...");
                    anotherSubject.OnNext(t.Result);
                    anotherSubject.OnCompleted();

                    asyncSubject.Subscribe(anotherSubject);
                }
            });
            return anotherSubject;
        }

        public async Task<string> GetWeatherForecastAsync(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<WeatherForecast[]>(content);

            return string.Join("\n", weatherForecast.Select(x => $"Date: {x.Date}, Temp: {x.TemperatureC}, Summary {x.Summary}"));

        }
    }
}
