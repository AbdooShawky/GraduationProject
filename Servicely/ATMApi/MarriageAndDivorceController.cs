using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Servicely.Models;

namespace Servicely.ATMApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MarriageAndDivorceController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
     
        public IEnumerable<Social_Status_Type> GetSocialType()
        {
            return db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true);
        }
        public IEnumerable<CustomCitizen> GetAllWifeOrHusbandByTypeAndCId(int social , int hw)
        {
            var gender = db.Citizens.Find(hw).citizen_gender;
            if(gender == "Male")
            {
                var data = db.Social_status.Where(a => a.social_status_isDeleted != true && a.socialStatus_citizenId_Husband == hw && a.socialStatus_TypeId == social).Select(a => new CustomCitizen
                {
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_id = a.socialStatus_citizenId_Wife
                });
                return data;
                
            }
            else
            {
                var data = db.Social_status.Where(a => a.social_status_isDeleted != true && a.socialStatus_citizenId_Wife == hw && a.socialStatus_TypeId == social).Select(a => new CustomCitizen
                {
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_id = a.socialStatus_citizenId_Husband
                });
                return data;
            }
           
        }

        public IEnumerable<CustomCitizen> GetWifeByHusId(int hid)
        {
            

            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == hid).Join(db.Citizens, a => a.socialStatus_citizenId_Wife, b => b.citizen_id, (a, b) => new CustomCitizen{ citizen_id =b.citizen_id, citizen_national_id =b.citizen_national_id });
            return data;

        }
        
       

        public IEnumerable<CustomCitizenDataBirthDocument> GetMarriageHusband(int h )
        {
            db.Configuration.ProxyCreationEnabled = false;
            //int h = Convert.ToInt32(Session["hid"]);
            //int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
           
                

                var ww = db.Citizens.Where(a => a.citizen_id == h).Select(a => new CustomCitizenDataBirthDocument
                {


                    citizen_first_name = a.citizen_first_name,
                    citizen_second_name = a.citizen_second_name,
                    citizen_third_name = a.citizen_third_name,
                    citizen_fourth_name = a.citizen_fourth_name,
                    citizen_first_name_arabic = a.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,
                    citizen_gender = a.citizen_gender,
                    citizen_birthDate = a.citizen_birthDate.ToString(),
                    citizen_relegion = a.citizen_relegion

                });

                return ww;
           
           

        }
        public IEnumerable<CustomCitizenDataBirthDocument> GetMarriageWife(int w )
        {
            db.Configuration.ProxyCreationEnabled = false;
           

          
            var wi = db.Citizens.Find(w);

            var ww = db.Citizens.Where(a => a.citizen_id == w).Select(a => new CustomCitizenDataBirthDocument
            {


                citizen_first_name = a.citizen_first_name,
                citizen_second_name = a.citizen_second_name,
                citizen_third_name = a.citizen_third_name,
                citizen_fourth_name = a.citizen_fourth_name,
                citizen_first_name_arabic = a.citizen_first_name_arabic,
                citizen_second_name_arabic = a.citizen_second_name_arabic,
                citizen_third_name_arabic = a.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,
                citizen_gender = a.citizen_gender,
                citizen_birthDate = a.citizen_birthDate.ToString(),
                citizen_relegion = a.citizen_relegion

            });

            return ww;

        }
        public string GetSocialStatusmarriage(int w , int h)
        {
            db.Configuration.ProxyCreationEnabled = false;
         

            var hus = db.Citizens.Find(h);
            if(hus.citizen_gender == "Male")
            {
                var wi = db.Citizens.Find(w);
                var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == h && a.socialStatus_citizenId_Wife == w && a.social_status_isStill == true && a.socialStatus_TypeId == 1).SingleOrDefault().social_status_from;
                return data.ToString();
            }
            else
            {
                var wi = db.Citizens.Find(w);
                var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == w && a.socialStatus_citizenId_Wife == h && a.social_status_isStill == true && a.socialStatus_TypeId == 1).SingleOrDefault().social_status_from;


                return data.ToShortDateString();
            }


          

        }
        public string GetSocialStatusDivorce(int huss , int wifee)
        {
            db.Configuration.ProxyCreationEnabled = false;
            

            var hus = db.Citizens.Find(huss);
            var wi = db.Citizens.Find(wifee);
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == huss && a.socialStatus_citizenId_Wife == wifee && a.social_status_isStill == true && a.socialStatus_TypeId == 2).SingleOrDefault().social_status_from;


            return data.ToShortDateString();

        }


    }
}
