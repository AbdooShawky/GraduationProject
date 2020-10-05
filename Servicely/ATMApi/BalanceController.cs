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
    public class BalanceController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        public string GetWithdrow(int Id, int balance)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var old = db.CitizenBalances.Where(a => a.CitizenBalance_citizen_id == Id).SingleOrDefault();
            old.CitizenBalance_balance -= balance;
            db.SaveChanges();
            return "Successful process";
        }

        public string GetCitizenBalance(int Id)
        {
            var data = db.CitizenBalances.Where(a => a.CitizenBalance_isDeleted != true && a.CitizenBalance_citizen_id == Id).SingleOrDefault();
            if (data != null)
            {
                decimal? balance = data.CitizenBalance_balance;
                if (balance >= 40)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "not";
            }
        }

        public string GetBalance (int Cid)
        {
            var data = db.CitizenBalances.Where(a => a.CitizenBalance_citizen_id == Cid).SingleOrDefault();
            return data.CitizenBalance_balance.ToString();
        }
    }
}
