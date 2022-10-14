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
            hash = hash + (plaintextPassword[i] * (k ^ i));
        }

        //Make hash larger.
        hash *= 100003;

        System.Diagnostics.Debug.WriteLine("hash: " + hash);

        return BaseConversion.IntToHexString(hash);
    }

    //FINISH
    public static byte[] Hash(byte[] bytes)
    {
        byte[] hash = new byte[4];




        return hash;
    }
}