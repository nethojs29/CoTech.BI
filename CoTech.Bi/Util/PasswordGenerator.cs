using System;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Util
{
    public static class PasswordGenerator {
        public static string CreateRandomPassword(this PasswordHasher<UserEntity> hasher, int passwordLength = 8) {
            return CreateRandomPassword(passwordLength);
        }

        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

    }
}