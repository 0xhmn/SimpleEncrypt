using System;
using System.IO;
using System.Text;

namespace SimpleEncrypt
{
    public class Decryption
    {
        public static bool DecryptFile(string file, string password)
        {
            byte[] fileByte = File.ReadAllBytes(file);
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            byte[] aesDecrypted = AESClass.AES_Decrypt(fileByte, passwordByte);
            if (aesDecrypted == null)
            {
                return false;
            }

            string addedExtension = System.IO.Path.GetExtension(file);
            string originalName = file.Substring(0, file.Length - addedExtension.Length);

            string originalExtension = System.IO.Path.GetExtension(originalName);
            string newName = originalName + "++DECRYPTED++" + originalExtension;

            File.WriteAllBytes(newName, aesDecrypted);
            return true;
        }
    }
}
