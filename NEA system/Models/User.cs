using SQLite;
using System.Security.Cryptography;
using System.Text;
//Example of overloading with CreateUser() methods.

namespace NEA_system.Models;

public class User
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }
    [NotNull]
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    //SQLite.net will handle construction.

    //SQLite.net will create tables with automatically assigned names and column names.



    //Methods

    //FINISH This needs to return a 128 bit (32 hexa letter) string regardless of the input length. Each letter is 16 bit so will return 4 hex characters currently per letter.
    public static string CalculatePasswordHash(string plaintextPassword)
    {
        if (!string.IsNullOrEmpty(plaintextPassword))
        {
            byte[] plaintextPasswordBytes = Encoding.Unicode.GetBytes(plaintextPassword);
            //test
            //produces 'number of 8-bit bytes: 2';
            System.Diagnostics.Debug.WriteLine("number of 8-bit bytes: " + plaintextPasswordBytes.Length);

            byte[] cyphertextBytes = new byte[plaintextPasswordBytes.Length];
            // foreach 8-bit byte in the byte array (which should be 2 for unicode)...
            for (int i = 0; i < plaintextPasswordBytes.Length; i++)
            {
                cyphertextBytes[i] = Hash(plaintextPasswordBytes[i]);
            }

            return BaseConversion.ByteArrayToHexString(cyphertextBytes);
        }
        else
        {
            return null;
        }
    }

    private static byte Hash(byte b)
    {
        int decimalNumber = (int)b;

        //Hashing bit...
        decimalNumber = (decimalNumber * (decimalNumber + 3)) % 11;

        return (byte)decimalNumber;
    }
}