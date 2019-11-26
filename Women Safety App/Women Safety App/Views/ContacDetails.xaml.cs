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
    public partial class ContacDetails : ContentPage
    {
        EmerygencyContact emergencyContact = new EmerygencyContact();
        EmerygencyContact existingEmergencyContact = new EmerygencyContact();
        EmergencyContactDB emergencyContactDB = new EmergencyContactDB();
        string username = "";
        string selectedRelation = "";
        string selectedPriority = "";
        public ContacDetails(EmerygencyContact e)
        {
            username = Women_Safety_App.App.userName;
            InitializeComponent();
            emergencyContact1.ReturnCommand = new Command(() => emergencyContact1.Focus());
            //relationName = new Command(() => relationName.Focus());
            name.ReturnCommand = new Command(() => name.Focus());
        
        }

        private async void Save_ButtonClicked(object sender, EventArgs args)
        {


            if ((string.IsNullOrWhiteSpace(emergencyContact1.Text)) || (string.IsNullOrWhiteSpace(selectedRelation)) ||
                (string.IsNullOrWhiteSpace(name.Text)) || (string.IsNullOrWhiteSpace(selectedPriority))
                ) 
            {
                await DisplayAlert("Enter Data", "Enter Valid Data", "OK");
            }
           
            else
            {

                emergencyContact.name = name.Text.ToString();
                emergencyContact.username = Women_Safety_App.App.userName;
                emergencyContact.phone1 = emergencyContact1.Text.ToString();
                emergencyContact.relation = selectedRelation.ToString();
                emergencyContact.priority = selectedPriority.ToString();
                try
                {
                    var retrunvalue = emergencyContactDB.AddEmergencyContanct(emergencyContact);
                    if (retrunvalue == "Sucessfully Added")
                    {
                        await DisplayAlert("Contacts are saved", retrunvalue, "OK");
                        await Navigation.PushAsync(new LoggedIn());
                    }
                    else
                    {
                        await DisplayAlert("User Add", retrunvalue, "OK");
                        emergencyContact1.Text = string.Empty;
                        selectedRelation = string.Empty;
                        //emergencyContact3.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
           
        }
        private async void OnPickerSelectedChanged(object sender, EventArgs args){
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; // This is the model selected in the picker
            selectedRelation = selectedItem.ToString();
        }
        private async void OnPickerPrioritySelectedChanged(object sender, EventArgs args)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem; // This is the model selected in the picker
            selectedPriority = selectedItem.ToString();
        }
        private async void LogOutClicked(object sender, EventArgs args) {

            Women_Safety_App.App.userName = "";
            await Navigation.PushAsync(new LogInPage());
        }

        private async void ListCalled(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoggedIn());
        }
    }
  
}
