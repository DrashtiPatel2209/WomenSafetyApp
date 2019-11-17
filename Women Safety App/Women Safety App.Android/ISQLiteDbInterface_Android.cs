using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using Women_Safety_App;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Women_Safety_App.Droid.Dependancies.GetSQLiteConnnection))]
namespace Women_Safety_App.Droid.Dependancies
{
    class GetSQLiteConnnection : Women_Safety_App.ISQLiteInterface
    {
        public GetSQLiteConnnection()
        {
        }
        public SQLite.SQLiteConnection GetSQLiteConnection()
        {
            var fileName = "UserDatabase.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.SQLiteConnection(platform.ToString(), true);
            return connection;
        }
    }
}