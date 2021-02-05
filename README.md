# Benchmark

Sometimes, you just need to check if a list is empty to skip execution. But how expensive is it?

This very simple benchmark tested five ways to do this:

### Scenarios
```c#
// scenario 1
    var list = Ids?.ToList() ?? new List<Guid>();
    if (list.Any() is false) { }

// scenario 2
    var list = Ids?.ToArray() ?? Array.Empty<Guid>();
    if (list.Any() is false) { }

// scenario 3
    var list = Ids?.ToList();
    if (list is null || list.Any() is false) { }

// scenario 4
    var list = Ids?.ToList();
    if (list is {Count: > 0} is false) { }

// scenario 5
    var list = Ids?.ToList();
    if (list?.Count <= 0) { }
```

### Result

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.101
  [Host]        : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Job=.NET Core 5.0  Runtime=.NET Core 5.0  

```
|    Method |      Mean |     Error |    StdDev |    Median |
|---------- |----------:|----------:|----------:|----------:|
| Scenario1 | 9.4311 ns | 0.2108 ns | 0.2886 ns | 9.3079 ns |
| Scenario2 | 7.7172 ns | 0.0317 ns | 0.0265 ns | 7.7128 ns |
| Scenario3 | 0.0295 ns | 0.0190 ns | 0.0178 ns | 0.0195 ns |
| Scenario4 | 0.0038 ns | 0.0019 ns | 0.0015 ns | 0.0043 ns |
| Scenario5 | 0.0512 ns | 0.0267 ns | 0.0400 ns | 0.0408 ns |
