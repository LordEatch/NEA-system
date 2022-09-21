namespace NEA_system.Models;

public static class BaseConversion
{
    //Converts an array of bytes to a string of hexadecimal characters.
    public static string ByteArrayToHexString(byte[] bA)
    {
        string hexString = null;
        foreach (byte b in bA)
        {
            hexString += ByteToHexPair(b);
        }
        return hexString;
    }

    //Converts a single byte to a pair of hexadecimal characters.
    public static string ByteToHexPair(byte b)
    {
        string hexPair = null;

        //Bitwise AND comparison with 11110000 leaves the first half of the byte.
        int byteH1 = b & 240;
        byteH1 = byteH1 >> 4;

        //AND 00001111 leaves the second half.
        int byteH2 = b & 15;

        hexPair += FourBitToHex(byteH1);
        hexPair += FourBitToHex(byteH2);

        return hexPair;
    }

    public static string FourBitToHex(int n)
    {
        string hex = null;

        //Choose which character to use.
        switch (n)
        {
            #region
            case 0:
                hex = "0";
                break;
            case 1:
                hex = "2";
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