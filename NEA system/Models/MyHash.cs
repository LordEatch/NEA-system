using System.Diagnostics;
using System.Numerics;

namespace NEA_system.Models;

public static class MyHash
{
    public static string HashPassword(string plaintextPassword)
    {
        return JumbleNumber(CalculatePasswordNumber(plaintextPassword)).ToString("X8");
    }

    private static ulong CalculatePasswordNumber(string plaintextPassword)
    {
        if (plaintextPassword == null)
            return 0;

        //Maximum hash value storable by a ulong allows for 30 characters only.
        if (plaintextPassword.Length > 30)
            return 0;

        ulong hash = 0;
        byte k = 3;

        for (int i = 0; i < plaintextPassword.Length; i++)
        {
            /* 
             * Use i + 1 to create at least a somewhat complex hash for even 1 character long password.
             * k ^ 0 = 1
             * ASCII value * 1 = ASCII value
             * So start at k ^ 1.
             */
            ulong x = (ulong)Math.Pow(k, i + 1);

            hash += plaintextPassword[i] * x;
        }

        return hash;
    }

    private static ulong JumbleNumber(ulong integerInput)
    {
        Debug.WriteLine($"Input: {integerInput.ToString("X8")}");
        Debug.WriteLine("");

        //Constant with relatively spread 1's and 0's.
        ulong internalState = 0b_10101111_01110011_11100110_10101010_10000001_11101101_10101101_01011001;

        //Repeat 4 times.
        for (int i = 0; i < 4; i++)
        {
            Debug.WriteLine("Iteration: " + i);

            //XOR comparison.
            internalState ^= integerInput;
            Debug.WriteLine("InternalState after xor: " + NumberToBinaryString(internalState));

            /*
             * Circularly shifts the internal binary value by (number of 1's in the input * (2(iteration) + 1)).
             * The first term makes the shift unique to the input.
             * The second term makes the shift unique to the iteration. It also always returns an odd number.
             * This is useful because sometimes the offset was calculated as a multiple of 64 and hence did notthing to the internal value.
             */
            Debug.WriteLine("n: " + (int)CountSetBits(integerInput) * (2 * i + 1));
            internalState = BitOperations.RotateLeft(internalState, (int)CountSetBits(integerInput) * (2 * i + 1));
            Debug.WriteLine("InternalState after << n times: " + NumberToBinaryString(internalState));

            Debug.WriteLine($"internal state [{i}]: {internalState.ToString("X8")}");
            Debug.WriteLine("");
        }

        Debug.WriteLine("");
        Debug.WriteLine($"Output: {internalState.ToString("X8")}");

        return internalState;

        static uint CountSetBits(ulong n)
        {
            //Long is 64-bit. Will never need a value larger than 64. 2^6 = 64 so only 6 bits are needed. A byte is sufficient to store the count.
            byte count = 0;
            while (n > 0)
            {
                count += (byte)(n & 1);
                n >>= 1;
            }
            return count;
        }

        //NOT MY CODE!
        static string NumberToBinaryString(ulong bits)
        {
            string ret = string.Empty;
            if (bits == 0) 
            {
                ret = "0";
            }
            else
            {
                while (bits != 0)
                {
                    ret += (char)((bits & 1) + '0'); // NOTE: does not use conditional
                    bits >>= 1;
                }
            }
            char[] chars = ret.ToCharArray();
            Array.Reverse(chars);
            return string.Join("", chars);
        }
    }
}