
namespace Sys
{
    public static partial class Math
    {
        // Author: Ivan Tsaryov
        // Date: June 2, 2026
        // Version: 0.01
        // Revision: Experimental, Stable working draft

        // Title: Absolute delta value |b - a|. The function returns an unsigned result without overflow.


        // int a = int.MinValue; // =-2147483648
        // int b = int.MaxValue; // = 2147483647
        // // Real math difference |b - a|:  | 4294967295| = 4294967295
        // // Real math difference |a - b|:  |-4294967295| = 4294967295
        // //                                                4294967295 (=uint.MaxValue)
        //
        // Console.WriteLine( Sys.Math.AbsDelta(b, a) );  // Out as uint: 4294967295 - correct
        // Console.WriteLine( Sys.Math.AbsDelta(a, b) );  // Out as uint: 4294967295 - correct
        //
        // // Common and incorrect calculations like this:
        // Console.WriteLine( System.Math.Abs(b - a) );   // Out as int: 1 - incorrect!
        // Console.WriteLine( System.Math.Abs(a - b) );   // Out as int: 1 - incorrect!


        // // The delta value can be converted to int type, if delta <= int.MaxValue.
        //
        // int a, b;
        // uint uintDelta = AbsDelta(a, b);
        //
        // if (uintDelta <= int.MaxValue)
        // {
        //     int intDelta = (int)uintDelta; // safe cast
        // }

        /// <summary>Absolute delta value |b - a| without overflow.</summary>
        [System.CLSCompliant(false)]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(int a, int b)
        {
            unchecked { return (a <= b) ? (uint)b - (uint)a : (uint)a - (uint)b; }
        }


        /// <summary>Absolute delta value |b - a| without overflow.</summary>
        [System.CLSCompliant(false)]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static uint AbsDelta(uint a, uint b)
        {
            unchecked { return (a <= b) ? b - a : a - b; }
        }


        // // The delta value can be converted to long type, if delta <= long.MaxValue.
        //
        // long a, b;
        // ulong ulongDelta = AbsDelta(a, b);
        //
        // if (ulongDelta <= long.MaxValue)
        // {
        //     long longDelta = (long)ulongDelta; // safe cast
        // }

        /// <summary>Absolute delta value |b - a| without overflow.</summary>
        [System.CLSCompliant(false)]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(long a, long b)
        {
            unchecked { return (a <= b) ? (ulong)b - (ulong)a : (ulong)a - (ulong)b; }
        }


        /// <summary>Absolute delta value |b - a| without overflow.</summary>
        [System.CLSCompliant(false)]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ulong AbsDelta(ulong a, ulong b)
        {
            unchecked { return (a <= b) ? b - a : a - b; }
        }


        // Note: (uint)value - (uint)other, (ulong)value - (ulong)other
        // throws System.OverflowException if assembly compiled with \checked flag.
    }
}
