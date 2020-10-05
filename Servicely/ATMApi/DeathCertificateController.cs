using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Servicely.Models;

namespace Servicely.Api
{
    public class abdoo
    {
        public int Id { get; set; }
    }
    public class deceacedinfo
    {
        
       
       
        public string citizen_national_id { get; set; }
        public string deceased_deathPlace { get; set; }
        public string deceased_deathDate { get; set; }
        public string state_name { get; set; }
        public string state_arabic_name { get; set; }
        public string citizen_birthPlace { get; set; }
        public string citizen_birthDate { get; set; }
        public string citizen_gender { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_first_name { get; set; }
    }
    public class IdNationalId
    {
        public int Id { get; set; }
        public string NId { get; set; }

        public string citizen_fourth_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_first_name { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DeathCertificateController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        [HttpPost]
        public IEnumerable<IdNationalId> PostFamilyById(abdoo h)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<IdNationalId> aa = new List<IdNationalId>();
            // father id
            var father = db.Citizens.Find(h.Id).citizen_father_id;
            var fatherData = db.Deceaseds.Where(a=> a.deceased_citizenId == father).Select(a=> new IdNationalId{ NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id, citizen_first_name = a.Citizen.citizen_first_name, citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic, citizen_fourth_name = a.Citizen.citizen_fourth_name, citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic, citizen_second_name = a.Citizen.citizen_second_name, citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic, citizen_third_name = a.Citizen.citizen_third_name, citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic }).SingleOrDefault();
            if(fatherData != null)
            {
                aa.Add(fatherData);
            }

            // mother id
            var mother = db.Citizens.Find(h.Id).citizen_mother_id;
            var motherData = db.Deceaseds.Where(a => a.deceased_citizenId == father).Select(a => new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id, citizen_first_name = a.Citizen.citizen_first_name, citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic, citizen_fourth_name = a.Citizen.citizen_fourth_name, citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic, citizen_second_name = a.Citizen.citizen_second_name, citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic, citizen_third_name = a.Citizen.citizen_third_name, citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic }).SingleOrDefault();
            if (motherData != null)
            {
                aa.Add(motherData);
            }

            // all childs 
            var child = db.Citizens.Where(a => (a.citizen_father_id == h.Id || a.citizen_mother_id == h.Id)).ToList();
           
            foreach (var item in child)
            {
                var childsData = db.Deceaseds.Where(a => a.deceased_citizenId == item.citizen_id).Select(a=> new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id, citizen_first_name = a.Citizen.citizen_first_name, citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic, citizen_fourth_name = a.Citizen.citizen_fourth_name, citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic, citizen_second_name = a.Citizen.citizen_second_name, citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic, citizen_third_name = a.Citizen.citizen_third_name, citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic }).SingleOrDefault();
                if( childsData != null)
                {
                    aa.Add(childsData);
                }
                
            }

            return aa;
        }
        public string GetAge(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DateTime data = Convert.ToDateTime(db.Deceaseds.Where(a => a.deceased_isDeleted != true && a.deceased_citizenId == Id).SingleOrDefault().deceased_deathDate);
            DateTime birthdate = db.Citizens.Find(Id).citizen_birthDate;
            TimeSpan age = data.Subtract(birthdate);
            int years = age.Days / 365;
            return years.ToString() ;
        }
        public IEnumerable<deceacedinfo> GetDeceasedInfoById(int citi)
        {

            var data = db.Citizens.Find(citi);
            //  string[] ddd = data.citizen_birthDate.Split('-');
            // int age = Convert.ToInt32(ddd[0]);
            var father = db.Citizens.Find(data.citizen_father_id);
            var mother = db.Citizens.Find(data.citizen_mother_id);


            var addresCiti = (
                from ct in db.Citizens
                from adc in db.AddressCitizens
                from stt in db.States
                from ci in db.Cities
                from re in db.Regions
                from dis in db.Districts
                from dc in db.Deceaseds

                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                && dc.deceased_citizenId == ct.citizen_id && dc.deceased_citizenId == citi
                )
                select new deceacedinfo
                {

                  citizen_national_id=  ct.citizen_national_id,
                   citizen_first_name= ct.citizen_first_name,
                   citizen_second_name= ct.citizen_second_name,
                   citizen_third_name= ct.citizen_third_name,
                   citizen_fourth_name= ct.citizen_fourth_name,
                   citizen_first_name_arabic= ct.citizen_first_name_arabic,
                   citizen_second_name_arabic= ct.citizen_second_name_arabic,
                   citizen_third_name_arabic= ct.citizen_third_name_arabic,
                   citizen_fourth_name_arabic= ct.citizen_fourth_name_arabic,
                   citizen_gender= ct.citizen_gender,
                   citizen_birthDate= ct.citizen_birthDate.ToString(),
                   citizen_birthPlace= ct.citizen_birthPlace,
                   state_arabic_name= stt.state_arabic_name,
                   state_name= stt.state_name,
                   deceased_deathDate= dc .deceased_deathDate,
                   deceased_deathPlace= dc .deceased_deathPlace
                }
                );

            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            return addresCiti;
        }
        public string GetSocialStatusByCitizenId(int social)
        {
          
            var citizen = db.Citizens.Find(social);
            if (citizen.citizen_gender == "Male")
            {
                var married = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == citizen.citizen_id && a.social_status_isStill == true).FirstOrDefault();
                if (married != null)
                {
                    return Languages.Language.Married;
                }


                return Languages.Language.Unmarried;




            }
            else
            {
                var married = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == citizen.citizen_id && a.social_status_isStill == true).FirstOrDefault();
                if (married != null)
                {
                    return Languages.Language.MarriedW;
                }
                return Servicely.Languages.Language.UnMarriedW;
            }



        }

    }
}
