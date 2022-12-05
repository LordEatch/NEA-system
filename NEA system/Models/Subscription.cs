using SQLite;

namespace NEA_system.Models;

internal class Subscription
{
    [PrimaryKey, AutoIncrement]
    public int SubscriptionID { get; set; }
    public int UserID { get; set; }
    public int ExerciseTypeID { get; set; }
}