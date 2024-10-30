
namespace P15ValueTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start operation!");

            var result = await GetDataAsync();
            Console.WriteLine($"REsult {result}");
        }

        static ValueTask<int> GetDataAsync()
        {
            bool isDataAvailable = false;

            if (isDataAvailable)
            {
                return new ValueTask<int>(42);
            }
            else
            {
                return new ValueTask<int>(GetDefaultValue());
            }
        }

        private static async Task<int> GetDefaultValue()
        {
            await Task.Delay(1000);
            return 10;
        }
    }
}
