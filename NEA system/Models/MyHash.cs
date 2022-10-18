namespace NEA_system.Models;
internal static class MyHash
{
    public static string CalculatePasswordHash(string plaintextPassword)
    {
        //Should be prime (to reduce collisions?).
        int k = 31;

        int hash = 0;
        for (int i = 0; i < plaintextPassword.Length; i++)
        {
            hash += ((plaintextPassword[i] ^ i) * (k ^ i));
        }

        System.Diagnostics.Debug.WriteLine("hash: " + hash);

        return BaseConversion.IntToHexString(hash);
    }
}