using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Servicely.Models;
using Servicely.Controllers;

namespace Servicely.ATMApi
{

    public class customPhase
    {

        public int Id { get; set; }
        public string PhaseNameArabic { get; set; }
        public string PhaseName { get; set; }

    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SchoolCertificateController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        public IEnumerable<customPhase> GetPhase()
        {
            var data = db.PhasesOfSchooles.Where(a => a.Is_Deleted != true).Select(a=> new customPhase { PhaseName =a.PhaseName, PhaseNameArabic=a.PhaseNameArabic,Id = a.Id}).ToList();

            return data;
        }

        //public IEnumerable< CustomCitizen> GetAllStudentByPhase(int Id)
        //{
        //    if (Id == 1)
        //    {
        //        var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS == true).Select(a => new CustomCitizen
        //        {
                  
        //           citizen_first_name= a.Citizen.citizen_first_name,
        //           citizen_second_name= a.Citizen.citizen_second_name,
        //           citizen_third_name= a.Citizen.citizen_third_name,
        //          citizen_fourth_name=  a.Citizen.citizen_fourth_name,
        //           citizen_national_id= a.Citizen.citizen_national_id,
        //           citizen_first_name_arabic= a.Citizen.citizen_first_name_arabic,
        //           citizen_second_name_arabic= a.Citizen.citizen_second_name_arabic,
        //           citizen_third_name_arabic= a.Citizen.citizen_third_name_arabic,
        //           citizen_fourth_name_arabic =a.Citizen.citizen_fourth_name_arabic,
        //        }).ToList();
        //        return data;
        //    }
        //    else if (Id == 2)
        //    {
        //        var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true).Select(a => new CustomCitizen
        //        {
        //            citizen_first_name = a.Citizen.citizen_first_name,
        //            citizen_second_name = a.Citizen.citizen_second_name,
        //            citizen_third_name = a.Citizen.citizen_third_name,
        //            citizen_fourth_name = a.Citizen.citizen_fourth_name,
        //            citizen_national_id = a.Citizen.citizen_national_id,
        //            citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
        //            citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
        //            citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
        //            citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



        //        }).ToList();
        //        return data;
        //    }
        //    else
        //    {

        //        var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true).Select(a => new CustomCitizen
        //        {
        //            citizen_first_name = a.Citizen.citizen_first_name,
        //            citizen_second_name = a.Citizen.citizen_second_name,
        //            citizen_third_name = a.Citizen.citizen_third_name,
        //            citizen_fourth_name = a.Citizen.citizen_fourth_name,
        //            citizen_national_id = a.Citizen.citizen_national_id,
        //            citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
        //            citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
        //            citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
        //            citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



        //        }).ToList();
        //        return data;

        //    }

        //}


        public CustomCitizen GetAllInfoOfStudentByPaseAndSTudentId(int Id, int ph)
        {
            int d = db.Students.Where(a => a.Is_Deleted != true && a.CitizenId == Id).SingleOrDefault().Id;
            if (ph == 1)
            {
                var data = db.Students.Where(a => a.Id == d && a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS == true).Select(a => new CustomCitizen
                {

                    citizen_first_name = a.Citizen.citizen_first_name,
                    citizen_second_name = a.Citizen.citizen_second_name,
                    citizen_third_name = a.Citizen.citizen_third_name,
                    citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,




                }).ToList().SingleOrDefault();
                return data;
            }
            else if (ph == 2)
            {
                var data = db.Students.Where(a => a.Id == d && a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true).Select(a => new CustomCitizen
                {
                    citizen_first_name = a.Citizen.citizen_first_name,
                    citizen_second_name = a.Citizen.citizen_second_name,
                    citizen_third_name = a.Citizen.citizen_third_name,
                    citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



                }).ToList().SingleOrDefault();
                return data;
            }
            else
            {

                var data = db.Students.Where(a => a.Id == d && a.Is_Deleted != true && a.IsGraduatedP == true).Select(a => new CustomCitizen
                {
                    citizen_first_name = a.Citizen.citizen_first_name,
                    citizen_second_name = a.Citizen.citizen_second_name,
                    citizen_third_name = a.Citizen.citizen_third_name,
                    citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



                }).ToList().SingleOrDefault();
                return data;

            }



        }
        public aaa GetGradesOfStudentByIdAndPhase(int Id1, int ph1)
        {
            db.Configuration.LazyLoadingEnabled = false;
            int d = db.Students.Where(a => a.Is_Deleted != true && a.CitizenId == Id1).SingleOrDefault().Id;

            if (ph1 == 1 || ph1 == 3 || ph1 == 2)
            {
                var data = db.CertificateStudentM_M.Where(a => a.Is_Deleted != true && a.CertificateId == ph1 && a.StudentId == d).Select(a => new aaa
                {
                   SchoolSection= a.Student.SchoolSection,
                   GradeName= a.Grade.GradeName,
                   GradeNameArabic= a.Grade.GradeNameArabic,
                   SchoolName= a.Student.School.SchoolName,
                   SchoolNameArabic= a.Student.School.SchoolNameArabic,
                   DirectorateName= a.Student.School.EducationDirectorate.DirectorateName,
                   DirectorateNameArabic= a.Student.School.EducationDirectorate.DirectorateNameArabic,
                   state_arabic_name= a.Student.School.EducationDirectorate.State.state_arabic_name,
                   state_name= a.Student.School.EducationDirectorate.State.state_name

                }).SingleOrDefault();
                return data;
            }

            return null;

        }
        public string GetCitizenQRCode1(int cid)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var dd = db.Students.Find(cid);
            string aa = "";
            var data = db.LoginCitizens.Where(a => a.Login_CitizenId == dd.CitizenId).SingleOrDefault();
            if (data != null)
                aa = data.Login_QRcode.ToString();
            return aa;
        }
    }
}
