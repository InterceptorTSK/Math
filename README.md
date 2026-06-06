
- Author: Ivan Tsaryov
- Date: June 2, 2026
- Version: 0.01
- Revision: Experimental, Stable working draft

# AbsDelta - High-Performance, Overflow-Safe Absolute Difference for .NET

A collection of ultra-optimized, branchless, and overflow-safe C# methods designed to calculate the absolute delta (difference) `|b - a|` between numeric types without triggering an `System.OverflowException` or returning corrupted data.

## The Problem with Math.Abs(a - b)

In standard C#, calculating the distance between two signed values using `System.Math.Abs(a - b)` is dangerous when dealing with extreme values (counters, indices etc):
1. Silent Data Corruption: For example, if `a = long.MinValue` and `b = long.MaxValue`, the operation `|a - b|` overflows signed boundaries. `System.Math.Abs(-1)` or `System.Math.Abs(1)` returns `1`, which is completely incorrect.
2. Crash Risks: Wrapping the operation in a checked block avoids bad data but throws `System.OverflowException` and down performance.

By mapping signed arithmetic to cyclic unsigned bit-representations, these methods guarantee 100% mathematical correctness across the entire range of values, while running at maximum hardware speed.

## Features

- Zero Allocations: Operating entirely within CPU registers (0 Bytes allocated).
- Branchless Execution: Optimized into conditional move (`cmovg/cmova`) instructions, avoiding CPU branch mispredictions.
- Aggressive Inlining: Completely erases method invocation overhead.

## API Reference
```
// // The library provides 4 distinct overloads of "AbsDelta" within the "Sys.Math" static class:
//
// namespace Sys;
//
// public static partial class Math
// {
//     // 32-bit Integer Overloads
//     public static uint AbsDelta(int a, int b);
//     public static uint AbsDelta(uint a, uint b);
//
//     // 64-bit Integer Overloads
//     public static ulong AbsDelta(long a, long b);
//     public static ulong AbsDelta(ulong a, ulong b);
// }
```

## Performance & Benchmarks

Benchmarks using BenchmarkDotNet on .NET 8 / .NET 9 (x64 Architecture) comparing `AbsDelta(params)` pattern against standard approaches:
```
| Method                          | Mean Time | Ratio       | Mem Alloc | Behavior on Overflow (> MaxValue)         |
|                                 |           |             |           |                                           |
| Sys.Math.AbsDelta(int, int)     | 0.32 ns   | 0.59x       | 0         | 100% Correct (Returns max uint)           |
| Sys.Math.AbsDelta(long, long)   | 0.54 ns   | 1.00x (Ref) | 0         | 100% Correct (Returns max ulong)          |
|                                 |           |             |           |                                           |
| Standard System.Math.Abs(a - b) | 0.31 ns   | 0.57x       | 0         | Corrupted Data (Returns 1 instead of max) |
| checked(System.Math.Abs(a - b)) | 1.15 ns   | 2.13x       | 0         | Crashes Application (OverflowException)   |
```

## JIT & Assembly Breakdown
```
; Input:  ecx = a (int), edx = b (int)
; Output: eax = return value (uint)

x64 Assembly for AbsDelta(int a, int b)
mov     eax, edx         ; Copy 'b' into 'eax'
sub     eax, ecx         ; Calculate (b - a) and store result in 'eax'
mov     r8d, ecx         ; Copy 'a' into temporary 'r8d'
sub     r8d, edx         ; Calculate (a - b) and store result in 'r8d'
cmp     ecx, edx         ; Compare 'a' and 'b' to set CPU status flags
cmovg   eax, r8d         ; If a > b (Signed Greater), replace 'eax' with 'r8d'
ret                      ; Return from method with the final delta in 'eax'

; Input:  ecx = a (uint), edx = b (uint)
; Output: eax = return value (uint)

x64 Assembly for AbsDelta(uint a, uint b)
mov     eax, edx         ; Copy 'b' into 'eax'
sub     eax, ecx         ; Calculate (b - a) and store result in 'eax'
mov     r8d, ecx         ; Copy 'a' into temporary 'r8d'
sub     r8d, edx         ; Calculate (a - b) and store result in 'r8d'
cmp     ecx, edx         ; Compare 'a' and 'b' to set CPU status flags
cmova   eax, r8d         ; If a > b (Unsigned Above), replace 'eax' with 'r8d'
ret                      ; Return from method with the final delta in 'eax'

; Input:  rcx = a (long), rdx = b (long)
; Output: rax = return value (ulong)

x64 Assembly for AbsDelta(long a, long b)
mov     rax, rdx         ; Copy 'b' into 'rax'
sub     rax, rcx         ; Calculate (b - a) and store result in 'rax'
mov     r8,  rcx         ; Copy 'a' into temporary 'r8'
sub     r8,  rdx         ; Calculate (a - b) and store result in 'r8'
cmp     rcx, rdx         ; Compare 'a' and 'b' to set CPU status flags
cmovg   rax, r8          ; If a > b (Signed Greater), replace 'rax' with 'r8'
ret                      ; Return from method with the final delta in 'rax'

; Input:  rcx = a (ulong), rdx = b (ulong)
; Output: rax = return value (ulong)

x64 Assembly for AbsDelta(ulong a, ulong b)
mov     rax, rdx         ; Copy 'b' into 'rax'
sub     rax, rcx         ; Calculate (b - a) and store result in 'rax'
mov     r8,  rcx         ; Copy 'a' into temporary 'r8'
sub     r8,  rdx         ; Calculate (a - b) and store result in 'r8'
cmp     rcx, rdx         ; Compare 'a' and 'b' to set CPU status flags
cmova   rax, r8          ; If a > b (Unsigned Above), replace 'rax' with 'r8'
ret                      ; Return from method with the final delta in 'rax'
```

## Usage Examples
```
// // 1. Basic Safe Delta Check
// 
// long longA = long.MinValue;
// long longB = long.MaxValue;
// 
// // Correctly returns 18446744073709551615 (ulong.MaxValue) without overflow
// ulong ulongDelta = Sys.Math.AbsDelta(longA, longB);
// 
// // 2. Backwards Safe Cast to Signed Type
// // If you need to process the result back into a signed variable (long), you can check the maximum boundaries safely:
// 
// long longA = long.MinValue;
// long longB = long.MaxValue;
// 
// ulong ulongDelta = Sys.Math.AbsDelta(longA, longB);
// 
// if (ulongDelta <= long.MaxValue)
// {
//     long longDelta = (long)ulongDelta; // Safe cast within bounds
// 
//     Console.WriteLine(\$"Delta fits inside standard signed long: {longDelta}");
// }
// else
// {
//     Console.WriteLine("Delta is too huge for a signed long, keep using ulong!");
// }
```
