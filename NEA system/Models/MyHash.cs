using System.Diagnostics;
using System.Numerics;

namespace NEA_system.Models;
internal static class MyHash
{
    public static string HashPassword(string plaintextPassword)
    {
        return HashInteger(CalculatePasswordConstant(plaintextPassword)).ToString("X8");
    }

    public static uint CalculatePasswordConstant(string plaintextPassword)
    {
        //test CHANGE TO INT?
        byte k = 3;
        uint hash = 0;

        for (int i = 0; i < plaintextPassword.Length; i++)
        {
            /* 
             * Use i + 1 to create at least a somewhat complex hash for even 1 character long password.
             * k ^ 0 = 1
             * ASCII value * 1 = ASCII value
             * So start at k ^ 1.
             */
            uint x = (uint)Math.Pow(k, i + 1);

            hash += plaintextPassword[i] * x;
        }

        return hash;
    }

    static uint HashInteger(uint integerInput)
    {
        Debug.WriteLine($"Input: {integerInput.ToString("X8")}");
        Debug.WriteLine("");

        //Constant with relatively spread 1's and 0's.
        uint internalState = 2796564047;

        //Repeat 4 times.
        for (int i = 0; i < 4; i++)
        {
            //XOR comparison.
            internalState ^= integerInput;

            /*
             * Circularly shifts the internal binary value by (number of 1's in the input * (2(iteration) + 1)).
             * The first term makes the shift unique to the input.
             * The second term makes the shift unique to the iteration. It also always returns an odd number.
             * This is useful because sometimes the offset was calculated as a multiple of 32 and hence did notthing to the internal value.
             */
            internalState = BitOperations.RotateLeft(internalState, (int)CountSetBits(internalState) * (2 * i + 1));

            Debug.WriteLine($"internal state [{i}]: {internalState.ToString("X8")}");
        }

        Debug.WriteLine("");
        Debug.WriteLine($"Output: {internalState.ToString("X8")}");

        return integerInput;



        uint CountSetBits(uint n)
        {
            uint count = 0;
            while (n > 0)
            {
                count += n & 1;
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
