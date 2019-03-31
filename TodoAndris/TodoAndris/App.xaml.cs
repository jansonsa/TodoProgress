using System;
using Xamarin.Forms;
using TodoAndris.Services;
using TodoAndris.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace TodoAndris
{
	public partial class App : Application
	{
		// Url of my deployed azure backend
        public static string AzureBackendUrl = "http://todoandris.azurewebsites.net";
        public static bool UseMockDataStore = false;
		
		public App ()
		{
			InitializeComponent();

			if (UseMockDataStore)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<AzureDataStore>();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
