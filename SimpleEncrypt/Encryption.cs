using System.IO;
using System.Text;

namespace SimpleEncrypt
{
    public class Encryption
    {
        public static void EncryptFile(string file, string password)
        {
            byte[] fileByte = File.ReadAllBytes(file);
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            byte[] aesEncrypted = AESClass.AES_Encryption(fileByte, passwordByte);

            // DO NOT DELETE/MOVE THE ORIGINAL FILE
            File.WriteAllBytes(file + ".encrypted", aesEncrypted);
        }
    }
}
