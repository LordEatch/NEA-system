using System.Diagnostics;

namespace NEA_system.Models;
internal static class MyHash
{
    public static long CalculatePasswordHash(string plaintextPassword)
    {
        //Return a null password hash if the plaintext password is empty or null.
        if (string.IsNullOrEmpty(plaintextPassword))
            return -1;

        //Should be prime to reduce collisions after being modulo'd.
        int k = 31;

        long hash = 0;
        for (int i = 0; i < plaintextPassword.Length; i++)
        {
            long x = (long)Math.Pow(k, i);

            hash += plaintextPassword[i] * x;
        }

        //test
        Debug.WriteLine("Hash value: " + hash);

        return hash;





        static byte[] IntToByteArray(int n)
        {
            //An integer has 32 bits (4 bytes).
            byte[] bytes = new byte[4];
            //Shifts the int down by 8 bits each time, then compares with 255 (00000000000000000000000011111111 in binary), to get an array of bytes.
            bytes[3] = (byte)(n & 255);
            bytes[2] = (byte)((n >> 8) & 255);
            bytes[1] = (byte)((n >> 16) & 255);
            bytes[0] = (byte)((n >> 24) & 255);

            return bytes;
        }

        //Converts an array of bytes to a string of hexadecimal characters.
        static string ByteArrayToHexString(byte[] bA)
        {
            string hexString = null;
            foreach (byte b in bA)
            {
                hexString += ByteToHexPair(b);
            }
            return hexString;



            //Converts a single byte to a pair of hexadecimal characters.
            static string ByteToHexPair(byte b)
            {
                string hexPair = null;

                //Bitwise AND comparison with 11110000 leaves the first half of the byte.
                byte byteH1 = (byte)(b & 240);
                byteH1 = (byte)(byteH1 >> 4);

                //AND 00001111 leaves the second half.
                byte byteH2 = (byte)(b & 15);

                hexPair += FourBitToHex(byteH1);
                hexPair += FourBitToHex(byteH2);

                return hexPair;



                static string FourBitToHex(byte b)
                {
                    string hex = null;

                    //Choose which character to use.
                    switch (b)
                    {
                        #region
                        case 0:
                            hex = "0";
                            break;
                        case 1:
                            hex = "1";
                            break;
                        case 2:
                            hex = "2";
                            break;
                        case 3:
                            hex = "3";
                            break;
                        case 4:
                            hex = "4";
                            break;
                        case 5:
                            hex = "5";
                            break;
                        case 6:
                            hex = "6";
                            break;
                        case 7:
                            hex = "7";
                            break;
                        case 8:
                            hex = "8";
                            break;
                        case 9:
                            hex = "9";
                            break;
                        case 10:
                            hex = "A";
                            break;
                        case 11:
                            hex = "B";
                            break;
                        case 12:
                            hex = "C";
                            break;
                        case 13:
                            hex = "D";
                            break;
                        case 14:
                            hex = "E";
                            break;
                        case 15:
                            hex = "F";
                            break;
                            #endregion
                    }
                    return hex;
                }
            }
        }
    }



    public static byte[] test2(byte[] input)
    {
        //Append the input array with 0's until its index is a multiple of 4.
        int emptySpaces = 4 - (input.Length % 4);
        byte[] filledInput = new byte[input.Length + emptySpaces];
        filledInput = input;

        for (int i = 0; i < emptySpaces; i++)
        {
            filledInput[i] = 0;
        }



        foreach (byte b in filledInput)
        {

        }
    }


    public static byte[] Combine(byte[] input)
    {
        byte[] internalNumbers = input;

        for (byte i = 0; i < 4; i++)
        {
            internalNumbers[i] ^= 13;
        }



        //test
        Debug.Write("\nInput:\n");
        foreach (byte val in input)
        {
            Debug.Write(val + ", ");
        }
        Debug.Write("\nOutput:\n");
        foreach (byte val in internalNumbers)
        {
            Debug.Write(val + ", ");
        }



        return internalNumbers;
    }
}