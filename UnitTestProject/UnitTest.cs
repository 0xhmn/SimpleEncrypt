using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleEncrypt;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void RSATest()
        {
            var message = "this is a test";
            RSAClass.GenerateKey();

            byte[] rsaEncrypted = RSAClass.Encrypt(Encoding.UTF8.GetBytes(message));
            byte[] rsaDecrypted = RSAClass.Decrypt(rsaEncrypted);

            Console.WriteLine("Original: " + message + "\n");
            Console.WriteLine("Encrypted: " + BitConverter.ToString(rsaEncrypted) + "\n");
            Console.WriteLine("Decrypted: " + Encoding.UTF8.GetString(rsaDecrypted));
        }

        [TestMethod]
        public void AESTest()
        {
            var message = "this is a test 2";
            RSAClass.GenerateKey();

            byte[] password = Encoding.UTF8.GetBytes("password");
            byte[] aesEncrypted = AESClass.AES_Encryption(Encoding.UTF8.GetBytes(message), password);
            byte[] aesDecypted = AESClass.AES_Decrypt(aesEncrypted, password);

            Console.WriteLine("Original: " + message + "\n");
            Console.WriteLine("Encrypted: " + BitConverter.ToString(aesEncrypted) + "\n");
            Console.WriteLine("Decrypted: " + Encoding.UTF8.GetString(aesDecypted));
        }
    }
}
