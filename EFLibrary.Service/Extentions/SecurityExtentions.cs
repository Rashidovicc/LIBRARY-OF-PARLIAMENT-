using System;
using System.Security.Cryptography;
using System.Text;

namespace EFLibrary.Service.Extentions
{
    public static class SecurityExtentions
    {
        public static string GetHash(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}