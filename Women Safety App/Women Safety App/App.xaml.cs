using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Women_Safety_App
{
    public partial class App : Application
    {
        public static string userName { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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

    //public class loginData
    //{
    //    private string userName { get; set; }
    //    public void setUsername(string uname) {
    //        userName = uname;
    //    }
    //    public string getUsername()
    //    {
    //        return userName;
    //    }
    //}
}
