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
    public class QRcodeController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        public string GetCitizenQRCode1(int cid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            string aa = "";
            var data = db.LoginCitizens.Where(a => a.Login_CitizenId == cid).SingleOrDefault();
            if (data != null)
                aa = data.Login_QRcode.ToString();
            return aa;
        }
    }
}
