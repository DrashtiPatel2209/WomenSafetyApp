using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;
using SQLite.Net;
using System.Linq;


namespace Women_Safety_App
{
    class EmergencyContactDB
    {
        private SQLite.SQLiteConnection _SQLiteConnection;
        public EmergencyContactDB()
        {

            try
            {
                ISQLiteInterface _SQLiteConnectionInterface = DependencyService.Get<ISQLiteInterface>();
                _SQLiteConnection  = _SQLiteConnectionInterface.GetSQLiteConnection();

               // _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
                _SQLiteConnection.CreateTable<EmerygencyContact>();
            }
            catch(Exception ex){
                System.Diagnostics.Debug.WriteLine("exception : " + ex.Message);
            }
        
         }
    public IEnumerable<EmerygencyContact> GetEmergencyContancts()
    {
        return (from u in _SQLiteConnection.Table<EmerygencyContact>()
                select u).ToList();
    }
    public EmerygencyContact GetSpecificEmergencyContanct(int id)
    {
        return _SQLiteConnection.Table<EmerygencyContact>().FirstOrDefault(t => t.Id == id);
    }
    public void DeleteEmergencyContanct(int id)
    {
        _SQLiteConnection.Delete<EmerygencyContact>(id);
    }
    public string AddEmergencyContanct(EmerygencyContact emerygencycontact)
    {
        var data = _SQLiteConnection.Table<EmerygencyContact>();
        var d1 = data.Where(x => x.name.ToLower() == emerygencycontact.name.ToLower()).FirstOrDefault();
        if (d1 == null)
        {
            _SQLiteConnection.Insert(emerygencycontact);
            return "Sucessfully Added";
        }
        else
            { 
               var d2 = (from values in data
                          where values.name == d1.name
                          select values).FirstOrDefault();
            if (d2 != null)
            {
                    d2.phone1 = emerygencycontact.phone1;
                    d2.phone2 = emerygencycontact.phone2;
                    d2.phone3 = emerygencycontact.phone3;
                    return "Sucessfully Updated";
            }
            else
                return "Already exist";
            }
        }
    public bool updateUserValidation(string userid)
    {
        var data = _SQLiteConnection.Table<EmerygencyContact>();
        var d1 = (from values in data
                  where values.name == userid
                  select values).FirstOrDefault();
        if (d1 != null)
        {
            return true;
        }
        else
            return false;
    }
    //public bool updateUser(string username, string pwd)
    //{
    //    var data = _SQLiteConnection.Table<EmerygencyContact>();
    //    var d1 = (from values in data
    //              where values.name == username
    //              select values).Single();
    //    if (true)
    //    {
    //        d1.password = pwd;
    //        _SQLiteConnection.Update(d1);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
    //public bool LoginValidate(string userName1, string pwd1)
    //{
    //    var data = _SQLiteConnection.Table<EmerygencyContact>();
    //    var d1 = data.Where(x => x.name == userName1 && x.password == pwd1).FirstOrDefault();
    //    if (d1 != null)
    //    {
    //        return true;
    //    }
    //    else
    //        return false;
    //}

}
}
