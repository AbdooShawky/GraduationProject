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
    public class PhotosController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        public HttpResponseMessage GetPhoto(int Id)
        {
            var respons = Request.CreateResponse(HttpStatusCode.OK);

            var data = db.Photos.Where(a=> a.Photo_isDeleted!= true && a.Photo_citizen_id == Id && a.Photo_isCurrent == true).SingleOrDefault();
           if(data != null)
            {
                string path = "~/CitizenPhotos/" + data.Photo_Url;

                path = System.Web.Hosting.HostingEnvironment.MapPath(path);
                var extension = System.IO.Path.GetExtension(path);

                var content = System.IO.File.ReadAllBytes(path);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(content);
                respons.Content = new StreamContent(ms);
                respons.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + extension);

                return respons;
            }

            return respons;
           

        }
        public HttpResponseMessage GetPhoto1(int Id1)
        {
            var respons = Request.CreateResponse(HttpStatusCode.OK);
            var Id = db.Students.Find(Id1);
            var data = db.Photos.Where(a => a.Photo_citizen_id == Id.CitizenId && a.Photo_isCurrent == true).SingleOrDefault();
            if (data != null)
            {
                string path = "~/CitizenPhotos/" + data.Photo_Url;

                path = System.Web.Hosting.HostingEnvironment.MapPath(path);
                var extension = System.IO.Path.GetExtension(path);

                var content = System.IO.File.ReadAllBytes(path);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(content);
                respons.Content = new StreamContent(ms);
                respons.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + extension);

                return respons;
            }

            return respons;


        }
        public HttpResponseMessage GetUniversityLogo(int std)
        {
            var respons = Request.CreateResponse(HttpStatusCode.OK);
            var Id = db.Students.Where(a=> a.CitizenId == std).SingleOrDefault().UniversityId;
            var universityLogo = db.Universities.Find(Id).UniversityLogo;
           // var data = db.Photos.Where(a => a.Photo_citizen_id == Id.CitizenId && a.Photo_isCurrent == true).SingleOrDefault();
            if (universityLogo != null)
            {
                string path = "~/UniversityLogo/" + universityLogo;

                path = System.Web.Hosting.HostingEnvironment.MapPath(path);
                var extension = System.IO.Path.GetExtension(path);

                var content = System.IO.File.ReadAllBytes(path);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(content);
                respons.Content = new StreamContent(ms);
                respons.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + extension);

                return respons;
            }

            return respons;


        }
    }
}
