using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Servicely.CustomValidation
{
    public class MyFileExtension : ValidationAttribute
    {
        public string  AllowedExtensions { get; set; }
        public override bool IsValid(object value)
        {
            HttpPostedFileBase myfile = value as HttpPostedFileBase;
            string ext = Path.GetExtension(myfile.FileName); //abc.txt
            ext = ext.TrimStart('.');

            return AllowedExtensions.Contains(ext);
        }



    }
}