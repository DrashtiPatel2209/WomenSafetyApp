using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Women_Safety_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedIn : ContentPage
    {

        
        string title = "demo";
        public LoggedIn()
        {
            
            /// ContextDemoList = new ListView();
            var datad = new EmergencyContactDB();
            var rawdata = datad.GetUserEmergencyContancts(App.userName);
            bool AddButtonEnable = true;
            if (rawdata == null)
            {
                AddButtonEnable = true;
            }
            else if (rawdata != null && rawdata.Count < 3)
            {
                AddButtonEnable = true;
            }
            else if (rawdata != null && rawdata.Count >= 3)
            {
                AddButtonEnable = false;
             
            }

            Label header = new Label
            {
                Text = "List Of Contacts",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };
            ToolbarItem addbutton = new ToolbarItem { 
                IconImageSource = "Resources/drawable/PlusIcon.png",
                Priority = 0,
                Command = new Command(this.AddItemCalled),
                IsEnabled = AddButtonEnable
                //Text = "Add"
            };
            ToolbarItem logoutbutton = new ToolbarItem
            {
                IconImageSource = "Resources/drawable/logoutIcon.png",
                Priority = 1,
                Command = new Command(this.LogOutClicked)
                //Text = "Add"
            };

            var moreAction = new MenuItem { Text = "More" };
            moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            moreAction.Clicked += async (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
            };

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += async (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
            };

            //itemList.Add("A");
            //itemList.Add("B");
            //itemList.Add("C");
            //itemList.Add("D");
            //itemList.Add("E");
            //itemList.Add("F");
            //itemList.Add("G");
            //ContextDemoList.ItemsSource = itemList;

          
            // Create the ListView.
            ListView listView = new ListView
            {
                ItemsSource = rawdata,            
             
                ItemTemplate = new DataTemplate(() =>
                {

                    Label nameLabel = new Label { TextColor = Color.Black };
                    nameLabel.SetBinding(Label.TextProperty, "name");

                    Label relationLabel = new Label { TextColor = Color.Black };
                    relationLabel.SetBinding(Label.TextProperty, "relation");
                    return  new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            relationLabel
                                        }
                                        }
                                },
                          
                        },
                        
                    };
                })
               

            };
            
            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                // DisplayAlert("ItemSelected", e.SelectedItem.ToString(), "Ok");
                //var ContactDetailPage = new ContacDetails();
                //ContactDetailPage.BindingContext = e.SelectedItem;
                //EmerygencyContact selected = new EmerygencyContact();

                //this.Navigation.PushAsync(new ContacDetails(e.SelectedItem));
            };
            // add to the ViewCell's ContextActions property

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {   
                    
                    header,
                    listView
                }
               
            };
            
            this.ToolbarItems.Add(addbutton);
            this.ToolbarItems.Add(logoutbutton);
            InitializeComponent();

        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        //public ObservableCollection<EmerygencyContact> getData(){

        //    var EmerContacts = new ObservableCollection<EmerygencyContact>();

        //    MyData1 _context = new MyData1();

        //    foreach (var human in _context.rawdata)
        //    {
        //        EmerContacts.Add(human);
        //    }
        //    return EmerContacts;
        //}
        //private async void AddItemCalled(object sender, EventArgs args)
        //{

        //    Women_Safety_App.App.userName = "";
        //    await Navigation.PushAsync(new LogInPage());
        //}
        private void AddItemCalled() {
            this.Navigation.PushAsync(new ContacDetails(new EmerygencyContact()));
        }
        private void LogOutClicked()
        {

            Women_Safety_App.App.userName = "";
            this.Navigation.PushAsync(new LogInPage());
        }

    }
    class EqualsParameterContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == ((View)parameter).BindingContext;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}