using System.Security.Cryptography;

namespace SimpleEncrypt
{
    public class RSAClass
    {
        private static RSAParameters PublicKey { get; set; }
        private static RSAParameters PrivateKey { get; set; }

        public static void GenerateKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                PublicKey = rsa.ExportParameters(false);
                PrivateKey = rsa.ExportParameters(true);
            }
        }

        public static byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(PublicKey);
                encrypted = rsa.Encrypt(input, true);
            }

            return encrypted;
        }

        public static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(PrivateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
        }
    }
}
