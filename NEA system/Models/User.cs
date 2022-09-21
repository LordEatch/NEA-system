using SQLite;
using System.Diagnostics;
using System.Linq;
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

    //Use this method if the user does not want to password-protect their account.
    public static int CreateUser(SQLiteConnection db, string username)
    {
        var user = new User()
        {
            Username = username,
        };
        db.Insert(user);

        Debug.WriteLine($"User.CreateUser(): User created with id: '{user.UserID}', username: '{user.Username}' and no password.");

        return user.UserID;
    }
    //This method handles hashing the password.
    public static int CreateUser(SQLiteConnection db, string username, string plaintextPassword)
    {
        var user = new User()
        {
            Username = username,
            PasswordHash = CalculatePasswordHash(plaintextPassword)
        };
        db.Insert(user);

        Debug.WriteLine($"User.CreateUser(): User created with id: '{user.UserID}', username: '{user.Username}' and password hash: '{user.PasswordHash}'.");

        return user.UserID;
    }

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
            Debug.WriteLine("CalculatePasswordHash(): Cannot create a hash from an empty string.");
            return null;
        }
    }

    public void InsertUser(SQLiteConnection db)
    {
        db.Insert(this);
    }
}