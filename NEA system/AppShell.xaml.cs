﻿namespace NEA_system;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Routes

		//User
		Routing.RegisterRoute(nameof(Page_CreateUser), typeof(Page_CreateUser));
        Routing.RegisterRoute(nameof(Page_EnterPassword), typeof(Page_EnterPassword));

		//Workout
        Routing.RegisterRoute(nameof(Page_CreateWorkout), typeof(Page_CreateWorkout));
        Routing.RegisterRoute(nameof(Page_FocusedWorkout), typeof(Page_FocusedWorkout));
        Routing.RegisterRoute(nameof(Page_EditWorkout), typeof(Page_EditWorkout));

        //Exercise

        //ExerciseType
    }

	private void LogoutClicked(object sender, EventArgs e)
	{
		Session.Logout();
	}
}
