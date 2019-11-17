using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;

namespace Women_Safety_App
{
    public interface ISQLiteInterface
    {
        SQLite.SQLiteConnection GetSQLiteConnection();
    }
}
