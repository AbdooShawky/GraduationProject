using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Servicely.CustomValidation;
namespace Servicely.Models
{
    public class Upload
    {
        [Required(ErrorMessage ="Uploading file is required")]
       // [MyFileExtension (ErrorMessage = "Only images and pdf files can be uploaded",AllowedExtensions ="jpg,jpeg,png,gif,pdf")]
        //[MyFileSize( MaxSize =10 , ErrorMessage ="Maximum size is 10mb")]
        public HttpPostedFileBase file { get; set; }
        public int document_documentType_id  { get; set; }
    }
}