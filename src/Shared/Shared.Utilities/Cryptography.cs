using System.Security.Cryptography;
using System.Text;

namespace Utilities.Cryptography
{
    public class Cryptography
    {
        private static int IV_LENGTH = 12; // 12 bytes is recommended IV size
        private static int AUTH_TAG_LENGTH = 16; // 16 bytes is max tag length that can be used in the GCM mode
        static string encryptionKey = "E546C8DF278CD5931069B522E695D4F2";
        //static string additionalAuthenticatedData = System.Configuration.ConfigurationManager.AppSettings["AdditionalAuthenticatedData"];

        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "osslack_13579";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        private const string passPhrase = "OSSlack2024";
        public static string Encrypt(string plainText)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            Aes symmetricKey = Aes.Create();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return ReplaceCharstoEncrypt(Convert.ToBase64String(cipherTextBytes));
        }

        /// <summary>
        /// Encrypts a string using the authenticated AES algorithm (using IV and Additional Authenticated Data)
        /// </summary>
        /// <param name="plainText">The string to encrypt</param>
        /// <returns>Base64 version of the encrypted string in the format [IV]-[TAG]-[DATA]</returns>
        public static string EncryptString(string text)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(encryptionKey);

                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(text);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            return ReplaceCharstoEncrypt(Convert.ToBase64String(result));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred in EncryptString_AES method", ex);
            }
        }

        /// <summary>
        /// Decrypts a string using the authenticated AES algorithm (using IV and Additional Authenticated Data)
        /// </summary>
        /// <param name="encryptedText">The base64 version of the string to decrypt in the format [IV]-[TAG]-[DATA]</param>
        /// <returns></returns>
        public static string DecryptString(string cipherText)
        {
            try
            {
                cipherText = ReplaceCharstoDecrypt(cipherText);
                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var key = Encoding.UTF8.GetBytes(encryptionKey);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Exception occurred in DecryptString_AES method", ex);
            }
        }

        /// <summary>
        /// Generates a random 12 byte IV
        /// </summary>
        /// <returns>Returns the random nonce</returns>
        private static byte[] generateIV()
        {
            byte[] nonce = new byte[IV_LENGTH];
            RandomNumberGenerator.Create().GetBytes(nonce);
            return nonce;
        }

        /// <summary>
        /// Gets the Authentication Tag from the provided encrypted data
        /// </summary>
        /// <param name="encDataArray">The encrypted data array which has the format [IV]-[TAG]-[DATA]</param>
        /// <returns></returns>
        private static byte[] GetAuthTag(byte[] encDataArray)
        {
            byte[] tag = new byte[AUTH_TAG_LENGTH];
            Array.Copy(encDataArray, IV_LENGTH, tag, 0, AUTH_TAG_LENGTH);
            return tag;
        }


        /// <summary>
        /// Gets the IV from the provided encrypted data
        /// </summary>
        /// <param name="encDataArray">The encrypted data array which has the format [IV]-[TAG]-[DATA]</param>
        /// <returns></returns>
        private static byte[] GetIV(byte[] encDataArray)
        {
            byte[] IV = new byte[IV_LENGTH];
            Array.Copy(encDataArray, 0, IV, 0, IV_LENGTH);
            return IV;
        }

        /// <summary>
        /// Gets the cipher text from the provided encrypted data after removing the IV and Auth Tag
        /// </summary>
        /// <param name="encDataArray"></param>
        /// <returns></returns>
        private static byte[] GetCipherText(byte[] encDataArray)
        {
            byte[] enc = new byte[encDataArray.Length - AUTH_TAG_LENGTH - IV_LENGTH];
            Array.Copy(encDataArray, IV_LENGTH + AUTH_TAG_LENGTH, enc, 0, encDataArray.Length - IV_LENGTH - AUTH_TAG_LENGTH);
            return enc;
        }

        /// <summary>
        /// Returns the 256 bit hash for the input string
        /// </summary>
        /// <param name="inputText">The string for which the hash needs to be created</param>
        /// <returns></returns>
        private static byte[] Get256BitHash(string inputText)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputText));
        }

        /// <summary>
        /// Replace message chars into special keywords
        /// </summary>
        /// <param name="encryptMessage">encryted message</param>
        /// <returns>encrypt message</returns>
        private static string ReplaceCharstoDecrypt(string encryptMessage)
        {
            encryptMessage = encryptMessage.Replace("__p_", "&");
            encryptMessage = encryptMessage.Replace("__s_", "=");
            encryptMessage = encryptMessage.Replace("__q_", "?");
            encryptMessage = encryptMessage.Replace("__i_", "/");
            encryptMessage = encryptMessage.Replace("__h_", "\"");
            encryptMessage = encryptMessage.Replace("__x_", "+");
            return encryptMessage;
        }

        /// <summary>
        /// Replace message special keywords into chars
        /// </summary>
        /// <param name="encryptMessage">encrypted message</param>
        /// <returns>encrypt message</returns>
        private static string ReplaceCharstoEncrypt(string encryptMessage)
        {
            encryptMessage = encryptMessage.Replace("&", "__p_");
            encryptMessage = encryptMessage.Replace("=", "__s_");
            encryptMessage = encryptMessage.Replace("?", "__q_");
            encryptMessage = encryptMessage.Replace("/", "__i_");
            encryptMessage = encryptMessage.Replace("\"", "__h_");
            encryptMessage = encryptMessage.Replace("+", "__x_");
            return encryptMessage;
        }
    }
}
