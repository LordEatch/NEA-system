using SQLite;

namespace NEA_system.Models;

public class User
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }
    [NotNull]
    public string Username { get; set; }
    public string PasswordHash { get; set; }




    //Methods

    /*NOTE the highest hash that this method will produce is within the limits of the 32-bit int in C#. 2^32 - 1 is the max value and 
    will return FFFFFFFF. If the hash is greater than this the program crashes. I could not get it to crash. */
    public static string CalculatePasswordHash(string plaintextPassword)
    {
        //Should be prime (to reduce collisions?). Determines the size of hashes.
        int k = 104729;

        int hash = 0;
        for (int i = 0; i < plaintextPassword.Length; i++)
        {
            hash = hash + (plaintextPassword[i] * (k ^ i));
        }

        System.Diagnostics.Debug.WriteLine("hash: " + hash);

        return BaseConversion.IntToHexString(hash);
    }
}