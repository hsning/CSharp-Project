using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Application
{
    public class LoginHashing
    {
        public LoginHashing()
        {

        }
        public bool Hash(string storedPassword,string enteredPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0,salt , 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            int ok = 1;
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    ok = 0;
            if (ok == 1)
                return true;
            else
                return false;
        }

        //public bool ShowPassword(string storedPassword)
        //{
        //    byte[] hashBytes = Convert.FromBase64String(storedPassword);
        //    byte[] salt = new byte[16];
        //    Array.Copy(hashBytes, 0, salt, 0, 16);
        //}
    }
}