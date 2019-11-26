using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Women_Safety_App.Droid;
using Android.Content;
using Android.Provider;
using Women_Safety_App;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Women_Safety_App.Droid
{
    [Activity(Label = "Women_Safety_App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        string locationString = "";
        int volumeKeyPressedCount = 0;
        int volumeUpKeyPressedCount = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<ISQLiteInterface>();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
          if (keyCode == Keycode.VolumeDown)
            {
                volumeKeyPressedCount = volumeKeyPressedCount + 1;
                if (volumeKeyPressedCount == 3) {
                    var datad = new Women_Safety_App.EmergencyContactDB();
                    var rawdata = new List<EmerygencyContact>();
                    rawdata = datad.GetUserEmergencyContancts(App.userName);
                    if (rawdata.Count > 0)
                    {
                        var phonos = new List<string>();
                        var LocationTask = GetLocation();
                        foreach (EmerygencyContact element in rawdata)
                        {
                            phonos.Add(element.phone1);
                        }
                        try
                        {

                            var task = SendSms("I am in Emergency.", phonos.ToArray());
                            Task.WhenAll(LocationTask);
                            //var result = AsyncContext.RunTask(MyAsyncMethod).Result;
                            volumeKeyPressedCount = 0;
                        }
                        catch (FeatureNotSupportedException ex)
                        {
                            // Sms is not supported on this device.
                        }
                        catch (Exception ex)
                        {
                            // Other error has occurred.
                        }
                    }
                    else {
                        volumeKeyPressedCount = 0;
                        Toast.MakeText(this, "No Contacts Added", ToastLength.Long);
                    }
                  
                }
                
            }
            if (keyCode == Keycode.VolumeUp)
            {
                volumeUpKeyPressedCount = volumeUpKeyPressedCount + 1;
                if (volumeUpKeyPressedCount == 3)
                {
                    var datad = new Women_Safety_App.EmergencyContactDB();
                    var rawdata = new EmerygencyContact();
                    rawdata = datad.GetUserEmergencyContanctsForCall(App.userName);
                    if (rawdata == null) {
                        Toast.MakeText(this, "No Contacts Added", ToastLength.Long);
                    }
                    else {
                        PlacePhoneCall(rawdata.phone1);
                       
                    }
                    volumeUpKeyPressedCount = 0;
                }
            }
          
            //if (keyCode == Keycode.VolumeUp)
            //{
            //    Intent intent = new Intent(MediaStore.ActionImageCapture);
            //    App_test._file = new File(App_test._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            //    intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App_test._file));
            //    StartActivityForResult(intent, 0);
            //    Toast.MakeText(this, "OnKeyDown-VolumeUp", ToastLength.Short).Show();
            //    return true;
            //}
            return base.OnKeyDown(keyCode, e);
        }
        
        public async Task SendSms(string messageText, string[] recipients)
        {
            try
            {
                var message = new SmsMessage(messageText, recipients);
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
        public async Task GetLocation()
        {
            try
            {
              //  string locationDetail = "";
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //var location = new Task<Location>();
                var location  = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    locationString = "Latitude: {" + location.Latitude + "}, Longitude: { "+ location.Longitude +"}, Altitude: {"+ location.Altitude+"}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

    }
}