using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Servicely.CustomValidation
{
    public class MyFileSize:ValidationAttribute , IClientValidatable
    {
        public int MaxSize { get; set; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "filesize";
            rule.ValidationParameters.Add("maxsize",MaxSize);
            return new ModelClientValidationRule[] { rule};
        }

        public override bool IsValid(object value)
        {
            try
                {

                if (value == null)
                {
                    return true;
                }
                else
                {
                    HttpPostedFileBase file = value as HttpPostedFileBase;
                    return file.ContentLength <= MaxSize * 1048576;
                }
            }
            catch(Exception e)
            {
                return false ;
            }
            }
    }
}