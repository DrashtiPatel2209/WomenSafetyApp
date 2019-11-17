using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Women_Safety_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LogInPage : ContentPage
{
   
        UserDb userData;
        public LogInPage()
        {
            InitializeComponent();
            userData = new UserDb();
            NavigationPage.SetHasBackButton(this, false);
            userNameEntry.ReturnCommand = new Command(() => passwordEntry.Focus());
            firstPassword.ReturnCommand = new Command(() => secondPassword.Focus());
            var forgetpassword_tap = new TapGestureRecognizer();
            forgetpassword_tap.Tapped += Forgetpassword_tap_Tapped;
            forgetLabel.GestureRecognizers.Add(forgetpassword_tap);
        }
        private async void Forgetpassword_tap_Tapped(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
        }
        string logesh;
        private async void userIdCheckEvent(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(useridValidationEntry.Text)) || (string.IsNullOrWhiteSpace(useridValidationEntry.Text)))
            {
                await DisplayAlert("Alert", "Enter Mail Id", "OK");
            }
            else
            {
                //userData = new UserDb();
                logesh = useridValidationEntry.Text;
                var textresult = userData.updateUserValidation(useridValidationEntry.Text);
                if (textresult)
                {
                    popupLoadingView.IsVisible = false;
                    passwordView.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("User Mail Id Not Exist", "Enter Correct User Name", "OK");
                }
            }
        }
        private async void Password_ClickedEvent(object sender, EventArgs e)
        {
            if (!string.Equals(firstPassword.Text, secondPassword.Text))
            {
                warningLabel.Text = "Enter Same Password";
                warningLabel.TextColor = Color.IndianRed;
                warningLabel.IsVisible = true;
            }
            else if ((string.IsNullOrWhiteSpace(firstPassword.Text)) || (string.IsNullOrWhiteSpace(secondPassword.Text)))
            {
                await DisplayAlert("Alert", " Enter Password", "OK");
            }
            else
            {
                try
                {
                   // userData = new UserDb();
                    var return1 = userData.updateUser(logesh, firstPassword.Text);
                    passwordView.IsVisible = false;
                    if (return1)
                    {
                        await DisplayAlert("Password Updated", "User Data updated", "OK");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private async void LoginValidation_ButtonClicked(object sender, EventArgs e)
        {
            if (userNameEntry.Text != null && passwordEntry.Text != null)
            {
                //userData = new UserDb();
                var validData = userData.LoginValidate(userNameEntry.Text, passwordEntry.Text);
                if (validData)
                {
                  //  popupLoadingView.IsVisible = false;
                    await DisplayAlert("Login Successfull", "Successfull", "OK");
                }
                else
                {
                    popupLoadingView.IsVisible = false;
                    await DisplayAlert("Login Failed", "Username or Password Incorrect", "OK");
                }
            }
            else
            {
                popupLoadingView.IsVisible = false;
                await DisplayAlert("He He", "Enter User Name and Password Please", "OK");
            }
        }


    }
}