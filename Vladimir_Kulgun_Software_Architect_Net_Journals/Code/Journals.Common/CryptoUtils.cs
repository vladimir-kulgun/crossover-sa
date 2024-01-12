using System.IO;
using System.Security.Cryptography;

namespace Journals.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptoUtils
    {
        private static readonly byte[] rgbSalt = {0x43, 0x87, 0x23, 0x72};

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] input, string password)
        {
            var pdb = new PasswordDeriveBytes(password, rgbSalt); 
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] input, string password)
        {
            var pdb = new PasswordDeriveBytes(password, rgbSalt);
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms,
              aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}
