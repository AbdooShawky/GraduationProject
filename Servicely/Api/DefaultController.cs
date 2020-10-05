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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DefaultController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        public IEnumerable<IdNationalId> GetFamilyById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<IdNationalId> aa = new List<IdNationalId>();
            // father id
            var father = db.Citizens.Find(Id).citizen_father_id;
            var fatherData = db.Deceaseds.Where(a => a.deceased_citizenId == father).Select(a => new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id }).SingleOrDefault();
            if (fatherData != null)
            {
                aa.Add(fatherData);
            }

            // mother id
            var mother = db.Citizens.Find(Id).citizen_mother_id;
            var motherData = db.Deceaseds.Where(a => a.deceased_citizenId == father).Select(a => new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id }).SingleOrDefault();
            if (motherData != null)
            {
                aa.Add(motherData);
            }

            // all childs 
            var child = db.Citizens.Where(a => (a.citizen_father_id == Id || a.citizen_mother_id == Id)).ToList();

            foreach (var item in child)
            {
                var childsData = db.Deceaseds.Where(a => a.deceased_citizenId == item.citizen_id).Select(a => new IdNationalId { NId = a.Citizen.citizen_national_id, Id = a.Citizen.citizen_id }).SingleOrDefault();
                if (childsData != null)
                {
                    aa.Add(childsData);
                }

            }

            return aa;
        }
    }

    
   
}
