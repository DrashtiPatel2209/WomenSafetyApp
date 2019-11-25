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
public partial class RegisterPage : ContentPage
{
        User users = new User();
        UserDb userDB = new UserDb();
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            emailEntry.ReturnCommand = new Command(() => userNameEntry.Focus());
            userNameEntry.ReturnCommand = new Command(() => passwordEntry.Focus());
            passwordEntry.ReturnCommand = new Command(() => confirmpasswordEntry.Focus());
            confirmpasswordEntry.ReturnCommand = new Command(() => phoneEntry.Focus());
        }
        private async void SignupValidation_ButtonClicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(userNameEntry.Text)) || (string.IsNullOrWhiteSpace(emailEntry.Text)) ||
                (string.IsNullOrWhiteSpace(passwordEntry.Text)) || (string.IsNullOrWhiteSpace(phoneEntry.Text)) ||
                (string.IsNullOrEmpty(userNameEntry.Text)) || (string.IsNullOrEmpty(emailEntry.Text)) ||
                (string.IsNullOrEmpty(passwordEntry.Text)) || (string.IsNullOrEmpty(phoneEntry.Text)))
            {
                await DisplayAlert("Enter Data", "Enter Valid Data", "OK");
            }
            else if (!string.Equals(passwordEntry.Text, confirmpasswordEntry.Text))
            {
                warningLabel.Text = "Enter Same Password";
                passwordEntry.Text = string.Empty;
                confirmpasswordEntry.Text = string.Empty;
                warningLabel.TextColor = Color.IndianRed;
                warningLabel.IsVisible = true;
            }
            else if (phoneEntry.Text.Length < 10)
            {
                phoneEntry.Text = string.Empty;
                phoneWarLabel.Text = "Enter 10 digit Number";
                phoneWarLabel.TextColor = Color.IndianRed;
                phoneWarLabel.IsVisible = true;
            }
            else
            {
                users.name = emailEntry.Text;
                users.userName = userNameEntry.Text;
                users.password = passwordEntry.Text;
                users.phone = phoneEntry.Text.ToString();
                try
                {
                    var retrunvalue = userDB.AddUser(users);
                    if (retrunvalue == "Sucessfully Added")
                    {
                        await DisplayAlert("User Add", retrunvalue, "OK");
                        await Navigation.PushAsync(new LogInPage());
                    }
                    else
                    {
                        await DisplayAlert("User Add", retrunvalue, "OK");
                        warningLabel.IsVisible = false;
                        emailEntry.Text = string.Empty;
                        userNameEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;
                        confirmpasswordEntry.Text = string.Empty;
                        phoneEntry.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }
        private async void login_ClickedEvent(object sender, EventArgs e)
        {
            // In App.cs
            var MainPage = new NavigationPage(new LogInPage());

            // Elsewhere in your solution with respect to corner cases
            await Navigation.PushAsync(new MainPage());
        }
    }
}