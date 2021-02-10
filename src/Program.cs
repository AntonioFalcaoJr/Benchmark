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

    [SimpleJob(RuntimeMoniker.NetCoreApp50, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter]
    public class Scenarios
    {
        private IEnumerable<Guid> _ids;
        
        [GlobalSetup]
        public void Setup()
            => _ids = null;

        [Benchmark]
        public void Scenario1()
        {
            var list = _ids?.ToList() ?? new List<Guid>();
            if (list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario2()
        {
            var list = _ids?.ToArray() ?? Array.Empty<Guid>();
            if (list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario3_toList()
        {
            var list = _ids?.ToList();
            if (list is null || list.Any() is false) { }
        }
        
        [Benchmark]
        public void Scenario3_toArray()
        {
            var list = _ids?.ToArray();
            if (list is null || list.Any() is false) { }
        }

        [Benchmark]
        public void Scenario4_toList()
        {
            var list = _ids?.ToList();
            if (list is {Count: > 0} is false) { }
        }
        
        [Benchmark]
        public void Scenario4_toArray()
        {
            var list = _ids?.ToArray();
            if (list is {Length: > 0} is false) { }
        }

        [Benchmark]
        public void Scenario5_toList()
        {
            var list = _ids?.ToList();
            if (list?.Count <= 0) { }
        }
        
        [Benchmark]
        public void Scenario5_toArray()
        {
            var list = _ids?.ToArray();
            if (list?.Length <= 0) { }
        }
    }
}