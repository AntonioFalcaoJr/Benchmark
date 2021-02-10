# Benchmark

Sometimes, you just need to check if a list is empty to skip execution. But how expensive is it?

This very simple benchmark tested some ways to do this:

## Scenarios

```c#
// SCENARIO 1
    var list = Ids?.ToList() ?? new List<Guid>();
    if (list.Any() is false) { }

// SCENARIO 2
    var list = Ids?.ToArray() ?? Array.Empty<Guid>();
    if (list.Any() is false) { }

// SCENARIO 3
    // toList(); 
    var list = _ids?.ToList();
    if (list is null || list.Any() is false) { }
    // toArray();
    var list = _ids?.ToArray();
    if (list is null || list.Any() is false) { }

// SCENARIO 4
    // toList();
    var list = Ids?.ToList();
    if (list is {Count: > 0} is false) { }
    // toArray();
    var list = _ids?.ToArray();
    if (list is {Length: > 0} is false) { }

// SCENARIO 5
    // toList();
    var list = Ids?.ToList();
    if (list?.Count <= 0) { }
    // toArray();
    var list = _ids?.ToArray();
    if (list?.Length <= 0) { }
```

### Result

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.101
  [Host]        : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
```
|            Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------ |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|         Scenario1 | .NET Core 3.1 | .NET Core 3.1 | 25.0384 ns | 0.5082 ns | 0.5649 ns |  2.64 |    0.06 |
|         Scenario1 | .NET Core 5.0 | .NET Core 5.0 |  9.5186 ns | 0.1186 ns | 0.1109 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
|         Scenario2 | .NET Core 3.1 | .NET Core 3.1 |  7.0908 ns | 0.0687 ns | 0.0537 ns |  0.63 |    0.00 |
|         Scenario2 | .NET Core 5.0 | .NET Core 5.0 | 11.2512 ns | 0.0510 ns | 0.0426 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
|  Scenario3_toList | .NET Core 3.1 | .NET Core 3.1 |  0.4756 ns | 0.0099 ns | 0.0093 ns |  0.85 |    0.04 |
|  Scenario3_toList | .NET Core 5.0 | .NET Core 5.0 |  0.5622 ns | 0.0278 ns | 0.0246 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
| Scenario3_toArray | .NET Core 3.1 | .NET Core 3.1 |  0.4801 ns | 0.0074 ns | 0.0069 ns |  0.99 |    0.02 |
| Scenario3_toArray | .NET Core 5.0 | .NET Core 5.0 |  0.4854 ns | 0.0108 ns | 0.0101 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
|  Scenario4_toList | .NET Core 3.1 | .NET Core 3.1 |  0.2452 ns | 0.0024 ns | 0.0022 ns |  1.14 |    0.03 |
|  **Scenario4_toList** | **.NET Core 5.0** | **.NET Core 5.0** |  **0.2164 ns** | **0.0058 ns** | **0.0045 ns** |  **1.00** |    **0.00** |
|                   |               |               |            |           |           |       |         |
| Scenario4_toArray | .NET Core 3.1 | .NET Core 3.1 |  0.4812 ns | 0.0082 ns | 0.0064 ns |  0.99 |    0.03 |
| Scenario4_toArray | .NET Core 5.0 | .NET Core 5.0 |  0.4856 ns | 0.0119 ns | 0.0111 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
|  Scenario5_toList | .NET Core 3.1 | .NET Core 3.1 |  0.2381 ns | 0.0060 ns | 0.0050 ns |  1.06 |    0.06 |
|  Scenario5_toList | .NET Core 5.0 | .NET Core 5.0 |  0.2255 ns | 0.0166 ns | 0.0129 ns |  1.00 |    0.00 |
|                   |               |               |            |           |           |       |         |
| Scenario5_toArray | .NET Core 3.1 | .NET Core 3.1 |  0.6015 ns | 0.0365 ns | 0.0391 ns |  1.26 |    0.10 |
| Scenario5_toArray | .NET Core 5.0 | .NET Core 5.0 |  0.4802 ns | 0.0115 ns | 0.0107 ns |  1.00 |    0.00 |

### Hints

#### Outliers

```ini
Scenarios.Scenario1: .NET Core 3.1         -> 1 outlier  was  removed (29.51 ns)
Scenarios.Scenario2: .NET Core 3.1         -> 3 outliers were removed (8.72 ns..8.88 ns)
Scenarios.Scenario2: .NET Core 5.0         -> 2 outliers were removed (12.83 ns, 13.51 ns)
Scenarios.Scenario3_toList: .NET Core 5.0  -> 1 outlier  was  removed (2.01 ns)
Scenarios.Scenario4_toList: .NET Core 5.0  -> 3 outliers were removed (1.65 ns..1.67 ns)
Scenarios.Scenario4_toArray: .NET Core 3.1 -> 3 outliers were removed (1.87 ns..1.93 ns)
Scenarios.Scenario5_toList: .NET Core 3.1  -> 2 outliers were removed (1.64 ns, 1.65 ns)
Scenarios.Scenario5_toList: .NET Core 5.0  -> 3 outliers were removed (1.65 ns..1.68 ns)
Scenarios.Scenario5_toArray: .NET Core 3.1 -> 1 outlier  was  removed (2.08 ns)
```
---

### Legends

```ini
Mean    : Arithmetic mean of all measurements
Error   : Half of 99.9% confidence interval
StdDev  : Standard deviation of all measurements
Ratio   : Mean of the ratio distribution ([Current]/[Baseline])
RatioSD : Standard deviation of the ratio distribution ([Current]/[Baseline])
1 ns    : 1 Nanosecond (0.000000001 sec)
```