using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Women_Safety_App
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetSQLiteConnection();
    }
}
