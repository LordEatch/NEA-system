namespace NEA_system.Models;

public static class BaseConversion
{
    public static string IntToHexString(int n)
    {
        return ByteArrayToHexString(IntToByteArray(n));
    }

    private static byte[] IntToByteArray(int n)
    {
        //An integer has 32 bits (4 bytes).
        byte[] bytes = new byte[4];
        /*Shifts the uint down by 8 bits each time, then compares with 255 (00000000000000000000000011111111 in binary), to get an array of bytes.
        This is little endian since the smallest side of the uint is the first element of the bytes array?*/
        bytes[3] = (byte)(n & 255);
        bytes[2] = (byte)((n >> 8) & 255);
        bytes[1] = (byte)((n >> 16) & 255);
        bytes[0] = (byte)((n >> 24) & 255);

        return bytes;
    }

    //Converts an array of bytes to a string of hexadecimal characters.
    private static string ByteArrayToHexString(byte[] bA)
    {
        string hexString = null;
        foreach (byte b in bA)
        {
            hexString += ByteToHexPair(b);
        }
        return hexString;
    }

    //Converts a single byte to a pair of hexadecimal characters.
    private static string ByteToHexPair(byte b)
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
    }

    private static string FourBitToHex(byte b)
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