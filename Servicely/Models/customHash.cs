using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Servicely
{
    public class customHash : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Encrypt.enc(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(providedPassword))
            {
                return PasswordVerificationResult.Success;

            }
            else
                return PasswordVerificationResult.Failed;
        }
    }
}