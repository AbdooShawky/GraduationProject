using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Cors;
using Servicely.Models;

namespace Servicely.ATMApi
{
    public class p
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class ci
    {
        public int             citizen_id                 { get; set; }
        public string          citizen_national_id         { get; set; }
        public string          citizen_first_name          { get; set; }
        public string          citizen_second_name        { get; set; }
        public string          citizen_third_name         { get; set; }
        public string          citizen_fourth_name        { get; set; }
        public string          citizen_gender        { get; set; }
       
  
      
 
        public string          citizen_first_name_arabic   { get; set; }
        public string          citizen_second_name_arabic   { get; set; }
        public string          citizen_third_name_arabic   { get; set; }
        public string          citizen_fourth_name_arabic   { get; set; }
  
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AtmLoginController : ApiController
    {
        
        DbMasterEntities1 db = new DbMasterEntities1();
        public IEnumerable<ci> GetLogin(string username, string password)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var dataa = Encrypt.enc(password);
            var data1 = db.LoginCitizens.Where(a => a.Login_CitizenNId == username && a.Login_Password == dataa).SingleOrDefault();

            if (data1 != null)
            {
                  var citizenInfo = db.Citizens.Where(a => a.citizen_national_id == data1.Login_CitizenNId).Select(a=> new ci { 
                
                citizen_id = a.citizen_id,
                citizen_first_name_arabic = a.citizen_first_name_arabic,
                citizen_second_name = a.citizen_second_name,
                citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,
                citizen_second_name_arabic = a.citizen_second_name_arabic,
                citizen_third_name_arabic = a.citizen_third_name_arabic,
                citizen_third_name = a.citizen_third_name,
                citizen_first_name = a.citizen_first_name,
                citizen_national_id =  a.citizen_national_id,
                citizen_fourth_name = a.citizen_fourth_name ,
                citizen_gender = a.citizen_gender
                 
                });
               

                return citizenInfo ;

            }
            return null;
        }
        public IEnumerable<ci> PostLogin(p username)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var dataa = Encrypt.enc(username.password);
            var data1 = db.LoginCitizens.Where(a => a.Login_CitizenNId == username.username && a.Login_Password == dataa).SingleOrDefault();

            if (data1 != null)
            {
                //var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data1.Login_CitizenId && a.Photo_isCurrent == true).SingleOrDefault();
                //if (citizenPhoto != null)
                //{
                //    Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                //}

                //var data3 = db.Citizens.Where(a => a.citizen_national_id == b.user_name).Select(a => new { a.citizen_first_name, a.citizen_second_name }).SingleOrDefault();
                //Session["citizenID"] = db.Citizens.Where(a => a.citizen_national_id == b.user_name).Select(a => a.citizen_id).SingleOrDefault();
                //Session["citizenFirstName"] = data3.citizen_first_name + " " + data3.citizen_second_name;
                var citizenInfo = db.Citizens.Where(a => a.citizen_national_id == data1.Login_CitizenNId).Select(a => new ci
                {

                    citizen_id = a.citizen_id,
                    citizen_first_name_arabic = a.citizen_first_name_arabic,
                    citizen_second_name = a.citizen_second_name,
                    citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,
                    citizen_second_name_arabic = a.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.citizen_third_name_arabic,
                    citizen_third_name = a.citizen_third_name,
                    citizen_first_name = a.citizen_first_name,
                    citizen_national_id = a.citizen_national_id,
                    citizen_fourth_name = a.citizen_fourth_name,
                    citizen_gender = a.citizen_gender

                });


                return citizenInfo;

            }
            return null;
        }
    }
}
