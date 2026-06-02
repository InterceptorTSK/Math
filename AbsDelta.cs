
namespace Sys
{
    public static partial class Math
    {

        // Absolute delta value function returns unsigned result (no overflows).


        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(int a, int b)
        {
            return (a <= b) ? (uint)b - (uint)a : (uint)a - (uint)b;
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(uint a, uint b)
        {
            return (a <= b) ? b - a : a - b;
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(long a, long b)
        {
            return (a <= b) ? (ulong)b - (ulong)a : (ulong)a - (ulong)b;
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(ulong a, ulong b)
        {
            return (a <= b) ? b - a : a - b;
        }

    }
}
