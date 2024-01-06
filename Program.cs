using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace LoopBenchmarkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkDemo1>();
        }
    }

    [MemoryDiagnoser]
    public class BenchmarkDemo1
    {
        private int[] _myArray;

        [Params(100, 1000, 10000, 100000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void SetUp()
        {
            _myArray = new int[Size];
            for (int index = 0; index < Size; index++)
            {
                _myArray[index] = index;
            }
        }

        [Benchmark(Baseline = true)]
        public int WhileLoop()
        {
            int index = 0;
            int count = 0;
            while (_myArray.Length > index)
            {
                count = +_myArray[index];
                index++;
            }
            return count;
        }

        [Benchmark]
        public int DoWhileLoop()
        {
            int index = 0;
            int count = 0;
            do
            {
                count = +_myArray[index];
                index++;
            } while (_myArray.Length > index);
            return count;
        }

        [Benchmark]
        public int ForLoop()
        {
            int index = 0;
            int count = 0;
            for (; index < _myArray.Length; index++)
            {
                count = +_myArray[index];
            }
            return count;
        }

        [Benchmark]
        public int ForEachLoop()
        {
            int count = 0;
            foreach (int num in _myArray)
            {
                count = +num;
            }
            return count;
        }
    }
}