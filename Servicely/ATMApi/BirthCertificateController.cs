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
    public class MotherOrFather
    {


        public string citizen_first_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_relegion { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }




    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BirthCertificateController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();



        public IEnumerable<IdNationalId> GetFamilyById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<IdNationalId> aa = new List<IdNationalId>();
            // father id
            var fatherid = db.Citizens.Find(Id).citizen_father_id;
            var father = db.Citizens.Where(a=> a.citizen_id ==fatherid).Select(a => new IdNationalId { NId = a.citizen_national_id, Id = a.citizen_id, citizen_first_name = a.citizen_first_name, citizen_first_name_arabic = a.citizen_first_name_arabic, citizen_fourth_name = a.citizen_fourth_name, citizen_fourth_name_arabic = a.citizen_fourth_name_arabic, citizen_second_name = a.citizen_second_name, citizen_second_name_arabic = a.citizen_second_name_arabic, citizen_third_name = a.citizen_third_name, citizen_third_name_arabic = a.citizen_third_name_arabic }).SingleOrDefault(); ;
            if (father != null)
            {
                aa.Add(father);
            }

            // mother id
            var motherid = db.Citizens.Find(Id).citizen_mother_id;
            var mother = db.Citizens.Where(a => a.citizen_id == motherid).Select(a => new IdNationalId { NId = a.citizen_national_id, Id = a.citizen_id  , citizen_first_name=a.citizen_first_name ,citizen_first_name_arabic = a.citizen_first_name_arabic , citizen_fourth_name= a.citizen_fourth_name, citizen_fourth_name_arabic=a.citizen_fourth_name_arabic,citizen_second_name=a.citizen_second_name,citizen_second_name_arabic=a.citizen_second_name_arabic, citizen_third_name=a.citizen_third_name,citizen_third_name_arabic=a.citizen_third_name_arabic}).SingleOrDefault(); ;
            if (mother != null)
            {
                aa.Add(mother);
            }

            // all childs 
            var child = db.Citizens.Where(a => (a.citizen_father_id == Id || a.citizen_mother_id == Id)).Select(a=> new IdNationalId { NId = a.citizen_national_id, Id = a.citizen_id, citizen_first_name = a.citizen_first_name, citizen_first_name_arabic = a.citizen_first_name_arabic, citizen_fourth_name = a.citizen_fourth_name, citizen_fourth_name_arabic = a.citizen_fourth_name_arabic, citizen_second_name = a.citizen_second_name, citizen_second_name_arabic = a.citizen_second_name_arabic, citizen_third_name = a.citizen_third_name, citizen_third_name_arabic = a.citizen_third_name_arabic }).ToList();

            foreach (var item in child)
            {
                //var childsData = db.Citizens.Where(a => a.deceased_citizenId == item.citizen_id).Select(a => new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id }).SingleOrDefault();
                //if (childsData != null)
                //{
                //    aa.Add(childsData);
                //}
                aa.Add(item);
            }
           
            return aa;
        }
        public IEnumerable<CustomCitizenDataBirthDocument> GetcitizenData(int citi)
        {

            var data = db.Citizens.Find(citi);

            var father = db.Citizens.Find(data.citizen_father_id);
            var mother = db.Citizens.Find(data.citizen_mother_id);

            var addresCiti = (
                from ct in db.Citizens
                from adc in db.AddressCitizens
                from stt in db.States
                from ci in db.Cities
                from re in db.Regions
                from dis in db.Districts
                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                )
                select new CustomCitizenDataBirthDocument
                {
                    citizen_national_id = ct.citizen_national_id,
                    citizen_first_name = ct.citizen_first_name,
                    citizen_second_name = ct.citizen_second_name,
                    citizen_third_name = ct.citizen_third_name,
                    citizen_fourth_name = ct.citizen_fourth_name,
                    citizen_first_name_arabic = ct.citizen_first_name_arabic,
                    citizen_second_name_arabic = ct.citizen_second_name_arabic,
                    citizen_third_name_arabic = ct.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = ct.citizen_fourth_name_arabic,
                    citizen_gender = ct.citizen_gender,
                    citizen_birthDate = ct.citizen_birthDate.ToString(),
                    citizen_relegion = ct.citizen_relegion,
                    state_arabic_name = stt.state_arabic_name,
                    city_arabic_name = ci.city_arabic_name,
                    state_name = stt.state_name,
                    city_name = ci.city_name
                }
                );

            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;


            return addresCiti;
        }
        public IEnumerable<MotherOrFather> GetFatherInformation(int x)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Citizens.Find(x);




            var father = db.Citizens.Where(a => a.citizen_id == data.citizen_father_id).Select(a => new MotherOrFather
            {

               citizen_first_name          =  a.citizen_first_name,
               citizen_fourth_name         = a.citizen_fourth_name,
               citizen_second_name         = a.citizen_second_name,
               citizen_third_name          = a.citizen_third_name,
               citizen_relegion            = a.citizen_relegion,
               citizen_first_name_arabic    = a.citizen_first_name_arabic,
                citizen_second_name_arabic  =a.citizen_fourth_name_arabic,
                citizen_third_name_arabic   =a.citizen_second_name_arabic,
                citizen_fourth_name_arabic  = a.citizen_third_name_arabic

            });

            return father;
        }
        public IEnumerable<MotherOrFather> GetMotherInformation(int y)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Citizens.Find(y);




            var mother = db.Citizens.Where(a => a.citizen_id == data.citizen_mother_id).Select(a => new MotherOrFather
            {

                citizen_first_name = a.citizen_first_name,
                citizen_fourth_name = a.citizen_fourth_name,
                citizen_second_name = a.citizen_second_name,
                citizen_third_name = a.citizen_third_name,
                citizen_relegion = a.citizen_relegion,
                citizen_first_name_arabic = a.citizen_first_name_arabic,
                citizen_second_name_arabic = a.citizen_fourth_name_arabic,
                citizen_third_name_arabic = a.citizen_second_name_arabic,
                citizen_fourth_name_arabic = a.citizen_third_name_arabic

            });

            return mother;
        }


    }
}
