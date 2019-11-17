using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Women_Safety_App;
using Foundation;
using UIKit;
using System.IO;
using Women_Safety_App.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLiteDbInterface_iOS))]
namespace Women_Safety_App.iOS
{
    public class ISQLiteDbInterface_iOS 
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Student.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.SQLiteConnection(path);
            return connection;
        }
    }
}