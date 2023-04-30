﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject.Utils
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "");
                return hashedPassword;
            }
        }
    }
}
