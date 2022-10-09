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

    public static string CalculatePasswordHash(string plaintextPassword)
    {
        if (!string.IsNullOrEmpty(plaintextPassword))
        {
            string cyphertextPassword;

            byte[] plaintextPasswordBytes = Encoding.UTF8.GetBytes(plaintextPassword);

            //Auto disposes sHA after use.
            using (SHA256 sHA = SHA256.Create())
            {
                //Calculate hash and covert to hex string.
                byte[] hash = sHA.ComputeHash(plaintextPasswordBytes);
                cyphertextPassword = BaseConversion.ByteArrayToHexString(hash);
            }

            return cyphertextPassword;
        }
        else
        {
            return null;
        }
    }
}