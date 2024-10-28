

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Example1
{

    class Program : IObserver<float>
    {
        public static void Main()
        {
           
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(float value)
        {
            throw new NotImplementedException();
        }
    }
}
