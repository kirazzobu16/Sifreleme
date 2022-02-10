using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;



namespace Şifreleme
{
      public class CryptAndDecrypt
    {
        public string Ceaser(string text, int key)
        {
            StringBuilder builder = new StringBuilder();
           
            foreach (char item in text)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(item) + key));
            }
           

          
            return builder.ToString();
        }//Listbox1' deki harfleri sezar şifreleme yöntemiyle şifreler.
        public string CeaserCozme(string text, int key)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char item in text)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(item) - key));
            }
            return builder.ToString();
        } //Sezar şifrelemesini çözer.
        public string md5(string text)
        {
            MD5 md5Encrypting = new MD5CryptoServiceProvider();
            byte[] bytes = md5Encrypting.ComputeHash(Encoding.UTF8.GetBytes(text.ToCharArray()));
            StringBuilder builder = new StringBuilder();
            foreach (var item in bytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }//Listbox1'deki verileri md5 şifreleme yöntemiyle şifreler.

        public string Des(string message, string password)
        {
            
            byte[] messagebytes = ASCIIEncoding.ASCII.GetBytes(message);
            byte[] passwordbytes = ASCIIEncoding.ASCII.GetBytes(password);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateEncryptor(passwordbytes, passwordbytes);
            CryptoStreamMode mode = CryptoStreamMode.Write;

            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
            cryptoStream.Write(messagebytes, 0, messagebytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] encryptedMessageBytes = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
            String encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);

            return encryptedMessage;
        } //Des şifrelemesi yöntemiyle şifreler.
        public string DesCozme(string encryptedMessage, string password)
        {
            byte[] encryptedMessageBytes = Convert.FromBase64String(encryptedMessage);
            byte[] passwordbytes = ASCIIEncoding.ASCII.GetBytes(password);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateDecryptor(passwordbytes, passwordbytes);
            CryptoStreamMode mode = CryptoStreamMode.Write;
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
            cryptoStream.Write(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] decryptedMessageBytes = new byte[memStream.Position];
            memStream.Position = 0;
            memStream.Read(decryptedMessageBytes, 0, decryptedMessageBytes.Length);

            string message = ASCIIEncoding.ASCII.GetString(decryptedMessageBytes);
            return message;
        } //DES şifrelemesini çözer.

        private const string AES_IV = @"eThWmZq4t7w!z%C*";
        private static string AesAnahtarlama = @"mZq4t7w9z$C&F)J+";
        

        public string Aes(string text)
        {

            AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
            provider.BlockSize = 128;
            provider.KeySize = 128;
            provider.IV = Encoding.UTF8.GetBytes(AES_IV);
            provider.Key = Encoding.UTF8.GetBytes(AesAnahtarlama);
            provider.Mode = CipherMode.CBC;
            provider.Padding = PaddingMode.PKCS7;

            byte[] source = Encoding.Unicode.GetBytes(text);
            using (ICryptoTransform encrpt = provider.CreateEncryptor())
            {
                byte[] target = encrpt.TransformFinalBlock(source, 0, source.Length);
                return Convert.ToBase64String(target);
            }
        } //Aes şifrelemesi yöntemiyle şifreler.

        public string AesCozme(string encryptedMessage)
        {
            AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
            provider.BlockSize = 128;
            provider.KeySize = 128;
            provider.IV = Encoding.UTF8.GetBytes(AES_IV);
            provider.Key = Encoding.UTF8.GetBytes(AesAnahtarlama);
            provider.Mode = CipherMode.CBC;
            provider.Padding = PaddingMode.PKCS7;
            byte[] source = System.Convert.FromBase64String(encryptedMessage);
            using (ICryptoTransform decrypt = provider.CreateDecryptor())
            {
                byte[] target = decrypt.TransformFinalBlock(source, 0, source.Length);
                return Encoding.Unicode.GetString(target);
            }



        }//Aes şifrelemesini çözer.
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        public enum KeySizes
        {
            SIZE_512=512,
            SIZE_1024=1024,
            SIZE_2048=2048,
            SIZE_952=952,
            SIZE_1369=1369
        }

        public static void generateKeys()
        {
            using(var rsa= new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }
         public static byte[] RSA(byte[] input)
        {
            
            byte[] encrypt;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypt = rsa.Encrypt(input, true);
            }
            return encrypt;
        }
        public static byte[] RSACozme(byte[] input)
        {
            byte[] decrypt;
            using(var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypt = rsa.Decrypt(input, true);
            }
            return decrypt;
        }


    }
}
