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
}