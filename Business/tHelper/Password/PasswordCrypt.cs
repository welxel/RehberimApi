using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.tHelper.Bases
{
    public class PasswordCrypt : lPasswordCrypt
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        public bool Check(string text, string compare)
        {
            text = Crypt(text);
            return text == compare;
        }

        public string Crypt(string text)
        {
            StringBuilder sb;
            byte[] array = Encoding.Unicode.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                array = md5.ComputeHash(array);
            }
            sb = new StringBuilder();
            foreach (byte by in array)
            {
                sb.Append(by.ToString("x2").ToUpper());
            }
            return sb.ToString();
        }
    }
}
