namespace NEA_system;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//  Routes

		//User
		Routing.RegisterRoute(nameof(Page_CreateUser), typeof(Page_CreateUser));
        Routing.RegisterRoute(nameof(Page_EnterPassword), typeof(Page_EnterPassword));

		//Workout
        Routing.RegisterRoute(nameof(Page_CreateWorkout), typeof(Page_CreateWorkout));
        Routing.RegisterRoute(nameof(Page_EditWorkout), typeof(Page_EditWorkout));

        //Exercise
        Routing.RegisterRoute(nameof(Page_EditExercise), typeof(Page_EditExercise));

        //ExerciseType
        Routing.RegisterRoute(nameof(Page_EditExerciseType), typeof(Page_EditExerciseType));
        Routing.RegisterRoute(nameof(Page_CreateExerciseType), typeof(Page_CreateExerciseType));

    }

    private void LogoutClicked(object sender, EventArgs e)
	{
		Session.Logout();
	}
}
