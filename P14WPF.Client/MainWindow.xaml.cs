using P12Weather.Shared;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace P14WPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IObservable<WeatherForecast> _weatherForecast;

        private ObservableCollection<WeatherForecast> _weatherForecasts = new ObservableCollection<WeatherForecast>();
        private ObservableCollection<WeatherForecast> _anotherWeatherForecast = new ObservableCollection<WeatherForecast>();
        public MainWindow()
        {
            InitializeComponent();

            WeatherListBox.ItemsSource = _weatherForecasts;
            AnotherListBox.ItemsSource = _anotherWeatherForecast;

            _weatherForecast = GetWeatherUpdates("http://localhost:5043/weatherforecast")
                .Publish()  // publikujemy strumien zeby byl wspoldzielony miedzy subskrybentami nazywa się to hot observable
                .RefCount();

          //  StartExtremeTemperatureListener();
        }

        private void btnGetWeather_Click(object sender, RoutedEventArgs e)
        {
            StartWeatherListener();
        }

        private void btnAnotherGetWeather_Click(object sender, RoutedEventArgs e)
        {
            StartHighTemperatureListener();
        }

        private void StartWeatherListener()
        {
            _weatherForecast
                  .RetryWhen(error =>
                error
                    .Delay(TimeSpan.FromSeconds(5))
                    .Select(_ => Observable.Return("empty")))
                .Subscribe(
                    x => Dispatcher.Invoke(()=> _weatherForecasts.Add(x)),
                    error => MessageBox.Show(error.Message));
        }

        private void StartHighTemperatureListener() 
        {
            _weatherForecast
                .Subscribe(
                    x => Dispatcher.Invoke(() => _anotherWeatherForecast.Add(x)),
                    error => MessageBox.Show(error.Message));
        }

        private void StartExtremeTemperatureListener()
        {
            _weatherForecast
                .DistinctUntilChanged(x => x.TemperatureC)
                  .Where(x => x.TemperatureC > 30 || x.TemperatureC < 0)
              //  .Retry(3)
              .RetryWhen(error => 
                error
                    .Delay(TimeSpan.FromSeconds(5))
                    .Select( _=>Observable.Return("empty")))
            .Subscribe(
                    x => Dispatcher.Invoke(() => MessageBox.Show($"{x.Date} , {x.TemperatureC}")),
                    error => MessageBox.Show(error.Message));
        }

        private IObservable<WeatherForecast> GetWeatherUpdates(string apiUrl)
        {
            return Observable.Create<WeatherForecast>(async (observer, ct) =>
             {
                 try
                 {
                     using(var client = new HttpClient())
                     {
                         while (true) 
                         {
                             var response = await client.GetAsync(apiUrl);
                             if (response.IsSuccessStatusCode)
                             {
                                 var content = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();

                                 foreach (var item in content)
                                 {
                                     observer.OnNext(item);
                                 }
                             }
                             else
                             {
                                 observer.OnError(new Exception("Api error"));
                                 break;
                             }

                             await Task.Delay(5000);
                         }
                     }
                      observer.OnCompleted();

                 }
                 catch (Exception ex)
                 {
                     observer.OnError(ex);
                 }
             });
        }


    }
}