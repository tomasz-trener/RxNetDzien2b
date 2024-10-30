using System.Data.SqlTypes;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace P20Inspections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var subject  = new Subject<int>();

            subject.Any(x=>x>1)
                .Subscribe(x=> Console.WriteLine($"if any element higher then 1{x}" ));


            var values = Observable.Range(-10, 20);
            values.All(x => x > 0)
                .Subscribe(x => Console.WriteLine($"All elements are higher then 0 {x}"));


            var subj2 = new Subject<int>();
            subj2.Contains(5)
                .Subscribe(x => Console.WriteLine($"Contains 5 {x}"));

            subj2.OnNext(1);
            subj2.OnNext(2);
            subj2.OnNext(5);


            var subj3 = new Subject<int>();
            subj3.DefaultIfEmpty(42)
                .Subscribe(x => Console.WriteLine($"DefaultIfEmpty {x}"));


            var numbers = Observable.Range(1, 10);
            numbers.ElementAt(5)
                .Subscribe(x => Console.WriteLine($"ElementAt {x}"));


            var seq1 = new Subject<int>();
            var seq2 = new Subject<int>();

            seq1.SequenceEqual(seq2)
                .Subscribe(x => Console.WriteLine($"SequenceEqual {x}"));

            seq1.Subscribe(x => Console.WriteLine($"seq1 {x}"));
            seq2.Subscribe(x => Console.WriteLine($"seq2 {x}"));


            seq1.OnNext(1);
            seq1.OnNext(2);

            seq2.OnNext(1);
            seq2.OnNext(3);

            seq1.OnCompleted();
            seq2.OnCompleted();

        }
    }
}
