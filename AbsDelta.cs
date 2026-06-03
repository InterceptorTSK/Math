
namespace Sys
{
    public static partial class Math
    {

        // Absolute delta value function returns unsigned result (without overflows).


        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(int a, int b)
        {
            unchecked { return (a <= b) ? (uint)b - (uint)a : (uint)a - (uint)b; }
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(uint a, uint b)
        {
            unchecked { return (a <= b) ? b - a : a - b; }
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(long a, long b)
        {
            unchecked { return (a <= b) ? (ulong)b - (ulong)a : (ulong)a - (ulong)b; }
        }

        /// <summary>Absolute delta value |b - a|.</summary>
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(ulong a, ulong b)
        {
            unchecked { return (a <= b) ? b - a : a - b; }
        }

        // Note: (uint)value - (uint)other, (ulong)value - (ulong)other
        // throws System.OverflowException if assembly compiled with \checked flag.
    }
}
