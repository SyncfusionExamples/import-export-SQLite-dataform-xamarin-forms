using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DataFormXamarin
{
    public partial class App : Application
    {
        static DataFormDataBase database;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static DataFormDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataFormDataBase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
