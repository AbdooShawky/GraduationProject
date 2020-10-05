

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Servicely.Models;

namespace Servicely.Controllers
{
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginApiController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        public CustomCitizen GetLoginCitizen(string Login_CitizenNId, string Login_Password)
        {
            string pass = Encrypt.enc(Login_Password);
           var data =  db.LoginCitizens.Where(a => a.Login_Password == pass && a.Login_CitizenNId == Login_CitizenNId).ToList().SingleOrDefault();
            if(data == null)
            {
                CustomCitizen c1 = new CustomCitizen();
                return c1;
            }
            else
            {
                var c = db.Citizens.Where( a=> a.citizen_id == data.Login_CitizenId).Select(a=> new CustomCitizen { citizen_id =a.citizen_id , citizen_first_name= a.citizen_first_name, citizen_second_name=a.citizen_second_name , citizen_third_name=a.citizen_third_name , citizen_fourth_name=a.citizen_fourth_name, citizen_first_name_arabic = a.citizen_first_name_arabic, citizen_second_name_arabic = a.citizen_second_name_arabic, citizen_third_name_arabic = a.citizen_third_name_arabic, citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,citizen_birthDate=a.citizen_birthDate,citizen_gender=a.citizen_gender }).SingleOrDefault();
                db.Configuration.ProxyCreationEnabled = false;
                 return c;
            }

        }
        public CustomCitizen GetLogin(string Login_CitizenNId, string Login_QRcode)
        {
            
            var data = db.LoginCitizens.Where(a => a.Login_QRcode == Login_QRcode && a.Login_CitizenNId == Login_CitizenNId).ToList().SingleOrDefault();
            if (data == null)
            {
                CustomCitizen c1 = new CustomCitizen();
                return c1;
            }
            else
            {
                var c = db.Citizens.Where(a => a.citizen_id == data.Login_CitizenId).Select(a => new CustomCitizen { citizen_id = a.citizen_id, citizen_first_name = a.citizen_first_name, citizen_second_name = a.citizen_second_name, citizen_third_name = a.citizen_third_name, citizen_fourth_name = a.citizen_fourth_name, citizen_first_name_arabic = a.citizen_first_name_arabic, citizen_second_name_arabic = a.citizen_second_name_arabic, citizen_third_name_arabic = a.citizen_third_name_arabic, citizen_fourth_name_arabic = a.citizen_fourth_name_arabic, citizen_birthDate = a.citizen_birthDate, citizen_gender = a.citizen_gender }).SingleOrDefault();
                db.Configuration.ProxyCreationEnabled = false;
                return c;
            }

        }
        public IHttpActionResult GetChangePin(int Login_CitizenId, string new_pass,Boolean Ar)
        {
            string pass = Encrypt.enc(new_pass);
            var data = db.LoginCitizens.Where( a=> a.Login_CitizenId == Login_CitizenId).SingleOrDefault();
            if (data == null)
            {
                if (Ar == true)
                    return Ok ("لا يوجد");
                else
                    return Ok("can't change pin number");
            }
            else
            {
                data.Login_Password  = Encrypt.enc(new_pass);
                db.SaveChanges();
                if (Ar == true)
                {
                    return Ok("تم تغير الرقم السري بنجاح ");
                }
                return Ok("Successful change pin Number");

            }

        }
        public String  GetLoginCitizen( string Login_PinNumber)
        {
            var data = db.LoginCitizens.Where(a => a.Login_PinNumber == Login_PinNumber).ToList().SingleOrDefault();
            if (data == null)
            {
                return "0";
            }
            else
            {
              //  var c = db.Citizens.Where(a => a.citizen_national_id == data.Login_CitizenNId).ToList().SingleOrDefault();
                return "1";
            }

        }
        public IEnumerable<LoginCitizen> GetLoginCitizen()
        {
            return db.LoginCitizens.ToList();
        }
        //[HttpGet]
        //public IEnumerable<Citizen> GetAuthentication(LoginCitizen log)
        //{


        //    var Nid = db.Citizens.Where(a => a.citizen_national_id == log.Login_CitizenNId).ToList().SingleOrDefault();
        //    if (Nid == null)
        //    {
        //        if national id not exist in the system
        //        yield return null;
        //    }
        //    var Login = db.LoginCitizens.Where(a => a.Login_CitizenNId == log.Login_CitizenNId && a.Login_Password == log.Login_Password).SingleOrDefault();

        //    if (Login != null)
        //    {
        //        yield return Nid;
        //    }
        //    else
        //        yield return null;


        //}

    }
}
