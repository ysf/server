using BenchmarkDotNet.Running;
using Bit.Icons.Benchmarks.Services;

namespace Bit.Icons.Benchmarks
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<IconFetchingServiceBenchmarks>();
        }
    }
}
