using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HappiNESs
{
    /// <summary>
    /// A helper to encrypt data and passwords
    /// </summary>
    public static class EncryptionHelpers
    {
        #region Encryption Methods

        /// <summary>
        /// Encrypts a string with SHA256 encryption
        /// </summary>
        /// <param name="String">The string to be encrypted</param>
        /// <returns></returns>
        public static string SHA256(string String)
        {
            var sha256 = SHA256Managed.Create();
            var bytes = Encoding.UTF8.GetBytes(String);
            var hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        /// <summary>
        /// Encrypt a string with the <see cref="Rijndael"></see> method
        /// </summary>
        /// <param name="String">The string to be encrypted</param>
        /// <returns></returns>
        public static string RijndaelEncrypt(string String)
        {
            if (string.IsNullOrEmpty(String))
                throw new ArgumentNullException("A string a ser encriptografada não pode ser nula.");

            string outStr = null;
            RijndaelManaged aesAlg = null;

            try
            {
                // Generate the key from the shared secret and the salt
                var key = GenerateKey();

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    // Prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    //Write all data to the stream.
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                        swEncrypt.Write(String);
                            

                    // Regenerate the string
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt a string with the <see cref="Rijndael"/> method
        /// </summary>
        /// <param name="cipherText">The encrypted string</param>
        /// <returns></returns>
        public static string RijndaelDecrypt(string CryptedString)
        {
            if (string.IsNullOrEmpty(CryptedString))
                throw new ArgumentNullException("cipherText");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            
            try
            {
                // Generate the key from the shared secret and the salt
                var key = GenerateKey();

                // Create the streams used for decryption.                
                var bytes = Convert.FromBase64String(CryptedString);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);

                    // Create a decrytor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the hash from a SHA256 encryption and returns the encrypted string
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// Create the main <see cref="Rijndael"></see> encryption key
        /// </summary>
        /// <returns></returns>
        public static Rfc2898DeriveBytes GenerateKey()
        {
            var Password = @"BioFoco";
            var SharedKey = @"Encoding";

            // Generate Salt
            var UE = new UnicodeEncoding();
            var _salt = UE.GetBytes(Password);

            // Return the key
            return new Rfc2898DeriveBytes(SharedKey, _salt);
        }

        #endregion

        /// <summary>
        /// Converts a <see cref="Stream"/> to a <see cref="byte[]"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
