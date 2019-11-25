using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLite.Net;


namespace Women_Safety_App
{
    public class EmerygencyContact
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string name { get; set; }
    public string username { get; set; }

        [MaxLength(10)]
    public string phone1 { get; set; }

    [MaxLength(10)]
    public string relation { get; set; }

    public EmerygencyContact()
    {
    }
}
}
