using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Servicely
{
    public class Encrypt
    {
        public static string enc(string pass)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(pass);

                b = md5.ComputeHash(b);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte item in b)
                {
                    sb.Append(item.ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}