using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneBook.Classes
{
    public class Encryption
    {
        // Ideally the salt should be stored in secure environment like registry or environment variable of operating system
        private static readonly string _salt = "Sah@ra!blAck4";

        public static string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_salt, new byte[] 
                    { 
                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 
                    });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using(var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        public static string Decrypt(string chiperText)
        {
            chiperText = chiperText.Replace(" ", "+");
            byte[] chipherBytes = Convert.FromBase64String(chiperText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_salt, new byte[] 
                    {
                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(chipherBytes, 0, chipherBytes.Length);
                        cs.Close();
                    }
                    chiperText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return chiperText;
        }
    }
}
