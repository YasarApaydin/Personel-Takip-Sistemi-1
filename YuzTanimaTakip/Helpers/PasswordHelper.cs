using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Text;
using YuzTanimaTakip.Models;
namespace YuzTanimaTakip.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var passwordHasher = new PasswordHasher<Kullanici>();
            return passwordHasher.VerifyHashedPassword(null!, storedHash, enteredPassword) == PasswordVerificationResult.Success;
        }
    }
}
