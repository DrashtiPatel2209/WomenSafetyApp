using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLite.Net;


namespace Women_Safety_App
{
    class EmerygencyContact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string name { get; set; }

    [MaxLength(10)]
    public string phone1 { get; set; }

    [MaxLength(10)]
    public string phone2 { get; set; }

    [MaxLength(10)]
    public string phone3 { get; set; }
    public EmerygencyContact()
    {
    }
}
}
