using System.Diagnostics;
using System.Numerics;

namespace NEA_system.Models;

public static class MyHash
{
    private static readonly ulong internalStateConstant = 0b_10101111_01110011_11100110_10101010_10000001_11101101_10101101_01011001;

    public static string HashPassword(string plaintextPassword)
    {
        return HashInteger(CalculatePasswordConstant(plaintextPassword)).ToString("X8");
    }

    private static ulong CalculatePasswordConstant(string plaintextPassword)
    {
        //Do not attempt if the input is null or limit is exceeded.
        if (plaintextPassword.Length > 30 || plaintextPassword == null)
            return 0;

        byte k = 3;
        ulong hash = 0;

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

    private static ulong HashInteger(ulong integerInput)
    {
        Debug.WriteLine($"Input: {integerInput.ToString("X8")}");
        Debug.WriteLine("");

        //Constant with relatively spread 1's and 0's.
        ulong internalState = internalStateConstant;

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
            Debug.WriteLine("InternalState after << n: " + NumberToBinaryString(internalState));

            Debug.WriteLine($"internal state [{i}]: {internalState.ToString("X8")}");
            Debug.WriteLine("");
        }

        Debug.WriteLine("");
        Debug.WriteLine($"Output: {internalState.ToString("X8")}");

        return internalState;



        //test
        static string NumberToBinaryString(ulong bits)
        {
            string ret = string.Empty;
            if (bits == 0)
                ret = "0";
            else
                while (bits != 0)
                {
                    ret += (char)((bits & 1) + '0'); // NOTE: does not use conditional
                    bits >>= 1;
                }
            char[] chars = ret.ToCharArray();
            Array.Reverse(chars);
            return string.Join("", chars);
        }






        uint CountSetBits(ulong n)
        {
            //Long is 64-bit. Will never need a value larger than 64. Largest value a byte can hold is just over this, 255.
            byte count = 0;
            while (n > 0)
            {
                count += (byte)(n & 1);
                n >>= 1;
            }
            return count;
        }
    }
}

//Use the below code if I ever want to store hashes as fixed-length hex strings instead of integers.

//static byte[] IntToByteArray(int n)
//{
//    //An integer has 32 bits (4 bytes).
//    byte[] bytes = new byte[4];
//    //Shifts the int down by 8 bits each time, then compares with 255 (00000000000000000000000011111111 in binary), to get an array of bytes.
//    bytes[3] = (byte)(n & 255);
//    bytes[2] = (byte)((n >> 8) & 255);
//    bytes[1] = (byte)((n >> 16) & 255);
//    bytes[0] = (byte)((n >> 24) & 255);

//    return bytes;
//}

////Converts an array of bytes to a string of hexadecimal characters.
//static string ByteArrayToHexString(byte[] bA)
//{
//    string hexString = null;
//    foreach (byte b in bA)
//    {
//        hexString += ByteToHexPair(b);
//    }
//    return hexString;



//    //Converts a single byte to a pair of hexadecimal characters.
//    static string ByteToHexPair(byte b)
//    {
//        string hexPair = null;

//        //Bitwise AND comparison with 11110000 leaves the first half of the byte.
//        byte byteH1 = (byte)(b & 240);
//        byteH1 = (byte)(byteH1 >> 4);

//        //AND 00001111 leaves the second half.
//        byte byteH2 = (byte)(b & 15);

//        hexPair += FourBitToHex(byteH1);
//        hexPair += FourBitToHex(byteH2);

//        return hexPair;



//        static string FourBitToHex(byte b)
//        {
//            string hex = null;

//            //Choose which character to use.
//            switch (b)
//            {
//                #region
//                case 0:
//                    hex = "0";
//                    break;
//                case 1:
//                    hex = "1";
//                    break;
//                case 2:
//                    hex = "2";
//                    break;
//                case 3:
//                    hex = "3";
//                    break;
//                case 4:
//                    hex = "4";
//                    break;
//                case 5:
//                    hex = "5";
//                    break;
//                case 6:
//                    hex = "6";
//                    break;
//                case 7:
//                    hex = "7";
//                    break;
//                case 8:
//                    hex = "8";
//                    break;
//                case 9:
//                    hex = "9";
//                    break;
//                case 10:
//                    hex = "A";
//                    break;
//                case 11:
//                    hex = "B";
//                    break;
//                case 12:
//                    hex = "C";
//                    break;
//                case 13:
//                    hex = "D";
//                    break;
//                case 14:
//                    hex = "E";
//                    break;
//                case 15:
//                    hex = "F";
//                    break;
//                    #endregion
//            }
//            return hex;
//        }
//    }
//}