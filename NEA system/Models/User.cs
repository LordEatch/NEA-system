using SQLite;

namespace NEA_system.Models;

public class User
{
    //Properties

    [PrimaryKey, AutoIncrement]
    public int UserID { get; set; }
    [NotNull]
    public string Username { get; set; }
    [NotNull]
    public bool IsPasswordProtected { get; set; }
    [NotNull]
    public int PasswordHash { get; set; }
    [NotNull]
    public bool LightMode { get; set; }
}