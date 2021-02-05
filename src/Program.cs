using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class Program
    {
        public static void Main()
            => BenchmarkRunner.Run<Scenarios>();
    }

    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [RPlotExporter]
    public class Scenarios
    {
        private static IEnumerable<Guid> Ids
            => default;

        [Benchmark]
        public void Scenario1()
        {
            var list = Ids?.ToList() ?? new List<Guid>();
            if (list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario2()
        {
            var list = Ids?.ToArray() ?? Array.Empty<Guid>();
            if (list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario3()
        {
            var list = Ids?.ToList();
            if (list is null || list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario4()
        {
            var list = Ids?.ToList();
            if (list is {Count: > 0} is false) { }
        }

        [Benchmark]
        public void Scenario5()
        {
            var list = Ids?.ToList();
            if (list?.Count <= 0) { }
        }
    }
}