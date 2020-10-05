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
    public class aaa
    {
        public string state_name { get; set; }
        public string state_arabic_name { get; set; }

        public string FacultyNameArabic { get; set; }
        public string FacultyName { get; set; }
        public int? Id { get; set; }
        public string GradeName { get; set; }
        public string GradeNameArabic { get; set; }
        public string Date { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_birthDate { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string SpecializationName { get; set; }
        public string SpecializationNameArabic { get; set; }
        public string UniversityName { get; set; }
        public string UniversityNameArabic { get; set; }
        public string UniversityLogo { get; set; }
        public string citizen_national_id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolNameArabic { get; set; }
        public string SchoolSection { get; set; }
        public string DirectorateName { get; set; }
        public string DirectorateNameArabic { get; set; }
        public string GradeName1 { get; set; }
        public string GradeNameArabic1 { get; set; }

    }
    public class stat
    {
        public string state_name { get; set; }
        public string state_arabic_name { get; set; }
    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UniversityCertificateController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        public aaa GetAllDataByStudentId(int Id)
        {
            var student = db.Students.Where(a=> a.CitizenId ==Id).SingleOrDefault();
            var faculty = db.Students.Find(student.Id).FacultyId;
            var cirtificate = db.FacultyCertificateM_M.Where(a => a.FacultyId == faculty).SingleOrDefault().CertificateId;
            var NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate = db.CertificateStudentM_M.Where(a => a.StudentId == student.Id && a.CertificateId == cirtificate).Select(a => new { a.Student.Citizen.citizen_first_name, a.Student.Citizen.citizen_second_name, a.Student.Citizen.citizen_third_name, a.Student.Citizen.citizen_fourth_name, a.Student.Citizen.citizen_first_name_arabic, a.Student.Citizen.citizen_second_name_arabic, a.Student.Citizen.citizen_third_name_arabic, a.Student.Citizen.citizen_fourth_name_arabic, a.Student.Citizen.citizen_birthDate, a.Student.Faculty.FacultyName, a.Student.Faculty.FacultyNameArabic, a.Student.University.UniversityName, a.Student.University.UniversityNameArabic, a.Grade.GradeName, a.Grade.GradeNameArabic, a.Student.SpecializationOfFaculty.SpecializationName, a.Student.SpecializationOfFaculty.SpecializationNameArabic, a.Date, a.Student.University.UniversityLogo } ).SingleOrDefault();
            var g = db.CertificateStudentM_M.Where(a => a.StudentId == student.Id && a.CertificateId == cirtificate).Select(a => new { a.Grade1.GradeName , a.Grade1.GradeNameArabic } ).SingleOrDefault();
            var address = db.AddressCitizens.Where(a => a.CA_CitizenId == student.CitizenId).Select(a => new { a.Address.District.Region.City.State.state_name, a.Address.District.Region.City.State.state_arabic_name }).FirstOrDefault();
            aaa aa = new aaa();
           
            aa.state_name = address.state_name;
            aa.state_arabic_name = address.state_arabic_name;
            aa.citizen_birthDate = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_birthDate.ToShortDateString();
            aa.SpecializationName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationName;
            aa.SpecializationNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationNameArabic;
            aa.UniversityName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityName;
            aa.UniversityNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityNameArabic;
            aa.citizen_first_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name;
            aa.citizen_first_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name_arabic;
            aa.citizen_second_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name;
            aa.citizen_second_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name_arabic;
            aa.citizen_third_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name;
            aa.citizen_third_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name_arabic;
            aa.citizen_fourth_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name;
            aa.citizen_fourth_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name_arabic;
            aa.FacultyName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyName;
            aa.FacultyNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyNameArabic;
            aa.Date = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.Date.Value.ToShortDateString();
            aa.GradeName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeName;
            aa.GradeNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeNameArabic;
            aa.UniversityLogo = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityLogo;
            aa.GradeName1 = g.GradeName;
            aa.GradeNameArabic1 = g.GradeNameArabic;

            return aa;
        }

        public stat GetAddressByStudentId(int CId)
        {
            var citizen = db.Students.Find(CId).CitizenId;
            db.Configuration.ProxyCreationEnabled = false;
            var address = db.AddressCitizens.Where(a => a.CA_CitizenId == citizen && a.Address.address_isCurrent == true).Select(a => new stat { state_name = a.Address.District.Region.City.State.state_name, state_arabic_name = a.Address.District.Region.City.State.state_arabic_name }).SingleOrDefault();
            //var data = db.AddressCitizens.Where(a => a.CA_CitizenId == citizen).FirstOrDefault().CA_AddressId;//Address.District.Region.City.State.state_name;
            //var state = db.Addresses.Where(a => a.address_id == data).Select(a => a.District.Region.City.State.state_name).SingleOrDefault();

            return address;
        }
    }
}
