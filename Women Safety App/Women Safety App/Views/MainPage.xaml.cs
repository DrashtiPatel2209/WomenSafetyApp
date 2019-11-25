using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Women_Safety_App
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
             await Navigation.PushModalAsync(new NavigationPage(new LogInPage()));


            //// In App.cs
            //var login = new NavigationPage(new LogInPage());

            //// Elsewhere in your solution with respect to corner cases
            //await Navigation.PushAsync(login);


        }
        private async void SignUp_Clicked(object sender, EventArgs e)
        {

            //var Signup = new NavigationPage(new RegisterPage());

            //// Elsewhere in your solution with respect to corner cases
            //await Navigation.PushAsync(Signup);
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
        }
    }

}
