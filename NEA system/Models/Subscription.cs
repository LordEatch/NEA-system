using SQLite;

namespace NEA_system.Models;

internal class Subscription
{
    [PrimaryKey, AutoIncrement]
    public int SubscriptionID { get; set; }
    [NotNull]
    public int UserID { get; set; }
    [NotNull]
    public int ExerciseTypeID { get; set; }
}