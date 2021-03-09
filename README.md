# Benchmark

Sometimes, you just need to check whether a collection is empty or null to skip execution. But how expensive is that?

This simple benchmark tested some ways to do this:

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
    var list = _ids?.ToList();
    if (list is not {Count: > 0}) { }
    // toArray();
    var list = _ids?.ToArray();
    if (list is not {Length: > 0}) { }


// SCENARIO 6

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
.NET Core SDK=5.0.103
  [Host]        : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.12 (CoreCLR 4.700.21.6504, CoreFX 4.700.21.6905), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
```
|            Method |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------ |-------------- |-----------:|----------:|----------:|------:|--------:|
|         Scenario1 | .NET Core 3.1 | 22.2840 ns | 0.2214 ns | 0.2071 ns |  2.55 |    0.04 |
|         Scenario1 | .NET Core 5.0 |  8.7312 ns | 0.1323 ns | 0.1238 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
|         Scenario2 | .NET Core 3.1 |  7.7519 ns | 0.1216 ns | 0.1078 ns |  0.90 |    0.01 |
|         Scenario2 | .NET Core 5.0 |  8.5893 ns | 0.0382 ns | 0.0357 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
|  Scenario3_toList | .NET Core 3.1 |  0.5135 ns | 0.0123 ns | 0.0115 ns |  1.01 |    0.03 |
|  Scenario3_toList | .NET Core 5.0 |  0.5075 ns | 0.0134 ns | 0.0105 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
| Scenario3_toArray | .NET Core 3.1 |  0.5193 ns | 0.0127 ns | 0.0119 ns |  0.64 |    0.03 |
| Scenario3_toArray | .NET Core 5.0 |  0.8007 ns | 0.0188 ns | 0.0293 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
|  Scenario4_toList | .NET Core 3.1 |  0.2388 ns | 0.0053 ns | 0.0049 ns |  0.85 |    0.05 |
|  Scenario4_toList | .NET Core 5.0 |  0.2836 ns | 0.0185 ns | 0.0155 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
| Scenario4_toArray | .NET Core 3.1 |  0.4935 ns | 0.0115 ns | 0.0102 ns |  0.98 |    0.02 |
| Scenario4_toArray | .NET Core 5.0 |  0.5018 ns | 0.0090 ns | 0.0075 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
|  Scenario5_toList | .NET Core 3.1 |  0.2488 ns | 0.0094 ns | 0.0079 ns |  1.80 |    0.19 |
|  **Scenario5_toList** | **.NET Core 5.0** |  **0.1400 ns** | **0.0149 ns** | **0.0132 ns** |  **1.00** |    **0.00** |
|                   |               |            |           |           |       |         |
| Scenario5_toArray | .NET Core 3.1 |  0.5848 ns | 0.0342 ns | 0.0320 ns |  0.83 |    0.05 |
| Scenario5_toArray | .NET Core 5.0 |  0.7082 ns | 0.0160 ns | 0.0142 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
|  Scenario6_toList | .NET Core 3.1 |  0.2231 ns | 0.0071 ns | 0.0066 ns |  0.98 |    0.06 |
|  Scenario6_toList | .NET Core 5.0 |  0.2282 ns | 0.0110 ns | 0.0103 ns |  1.00 |    0.00 |
|                   |               |            |           |           |       |         |
| Scenario6_toArray | .NET Core 3.1 |  0.6167 ns | 0.0390 ns | 0.0365 ns |  1.12 |    0.10 |
| Scenario6_toArray | .NET Core 5.0 |  0.5741 ns | 0.0386 ns | 0.0516 ns |  1.00 |    0.00 |

### Hints

#### Outliers

```ini
Scenarios.Scenario2: .NET Core 3.1         -> 1 outlier  was  removed (10.01 ns)
Scenarios.Scenario3_toList: .NET Core 5.0  -> 3 outliers were removed (1.99 ns..2.12 ns)
Scenarios.Scenario3_toArray: .NET Core 5.0 -> 9 outliers were removed (2.37 ns..2.44 ns)
Scenarios.Scenario4_toList: .NET Core 5.0  -> 2 outliers were removed (1.77 ns, 1.77 ns)
Scenarios.Scenario4_toArray: .NET Core 3.1 -> 1 outlier  was  removed (1.97 ns)
Scenarios.Scenario4_toArray: .NET Core 5.0 -> 2 outliers were removed (1.96 ns, 1.99 ns)
Scenarios.Scenario5_toList: .NET Core 3.1  -> 2 outliers were removed (1.73 ns, 1.75 ns)
Scenarios.Scenario5_toList: .NET Core 5.0  -> 1 outlier  was  removed (1.60 ns)
Scenarios.Scenario5_toArray: .NET Core 5.0 -> 1 outlier  was  removed, 3 outliers were detected (2.04 ns, 2.05 ns, 2.10 ns)
Scenarios.Scenario6_toArray: .NET Core 5.0 -> 2 outliers were removed, 7 outliers were detected (1.85 ns..1.86 ns, 2.05 ns, 2.08 ns)
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
