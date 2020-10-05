using Servicely.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class UploadPhotos
    {
        [Required(ErrorMessage = "Uploading file is required")]
        [MyFileExtension(ErrorMessage = "Only images can be uploaded", AllowedExtensions = "jpg,jpeg,png,gif")]
        [MyFileSize(MaxSize = 10, ErrorMessage = "Maximum size is 10mb")]
        public HttpPostedFileBase f1 { get; set; }
        public int Photo_id { get; set; }
        public string Photo_Url { get; set; }
        public Nullable<int> Photo_citizen_id { get; set; }
    }
}