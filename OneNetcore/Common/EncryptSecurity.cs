using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class EncryptSecurity
    {
        private static byte[] Keys = new byte[] { 0x41, 0x72, 0x65, 0x79, 0x6f, 0x75, 0x6d, 0x79,
                                                  0x53,  110, 0x6f, 0x77, 0x6d, 0x61,  110, 0x3f };
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string Text)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Text));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">待解密的文本</param>
        /// <param name="cipherkey">密钥</param>
        /// <returns>返回与此实例等效的解密文本</returns>
        public static string Decrypt(string cipherText, string cipherkey)
        {
            try
            {
                cipherkey = CutLeft(cipherkey, 32);
                cipherkey = cipherkey.PadRight(32, ' ');
                ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(cipherkey), IV = Keys }.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(cipherText);
                byte[] bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">待解密的字节</param>
        /// <param name="cipherkey">密钥</param>
        /// <returns>返回与此实例等效的解密字节</returns>
        public static byte[] DecryptBuffer(byte[] cipherText, string cipherkey)
        {
            try
            {
                cipherkey = CutLeft(cipherkey, 32);
                cipherkey = cipherkey.PadRight(32, ' ');
                RijndaelManaged managed = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(cipherkey),
                    IV = Keys
                };
                return managed.CreateDecryptor().TransformFinalBlock(cipherText, 0, cipherText.Length);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">待加密的文本</param>
        /// <param name="cipherkey">密钥</param>
        /// <returns>返回与此实例等效的加密文本</returns>
        public static string Encrypt(string plainText, string cipherkey)
        {
            cipherkey = CutLeft(cipherkey, 32);
            cipherkey = cipherkey.PadRight(32, ' ');
            ICryptoTransform transform = new RijndaelManaged { Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 32)), IV = Keys }.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">待加密的字节</param>
        /// <param name="cipherkey">密钥</param>
        /// <returns>返回与此实例等效的加密字节</returns>
        public static byte[] EncryptBuffer(byte[] plainText, string cipherkey)
        {
            cipherkey = CutLeft(cipherkey, 0x20);
            cipherkey = cipherkey.PadRight(0x20, ' ');
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 0x20)),
                IV = Keys
            };
            return managed.CreateEncryptor().TransformFinalBlock(plainText, 0, plainText.Length);
        }

        /// <summary>
        /// 从给定字符串(originalVal)左侧开始截取指定长度(cutLength)个字符,[使用字节宽度]
        /// </summary>
        /// <param name="originalVal"></param>
        /// <param name="cutLength"></param>
        /// <returns></returns>
        private static string CutLeft(string originalVal, int cutLength)
        {
            if (string.IsNullOrEmpty(originalVal))
            {
                return string.Empty;
            }

            if (cutLength < 1)
            {
                return originalVal;
            }

            byte[] bytes = Encoding.Default.GetBytes(originalVal);
            if (bytes.Length <= cutLength)
            {
                return originalVal;
            }

            int length = cutLength;
            int[] numArray = new int[cutLength];
            byte[] destinationArray = null;
            int num2 = 0;
            for (int i = 0; i < cutLength; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num2++;
                    if (num2 == 3)
                    {
                        num2 = 1;
                    }
                }
                else
                {
                    num2 = 0;
                }
                numArray[i] = num2;
            }

            if ((bytes[cutLength - 1] > 0x7f) && (numArray[cutLength - 1] == 1))
            {
                length = cutLength + 1;
            }

            destinationArray = new byte[length];
            Array.Copy(bytes, destinationArray, length);

            return Encoding.Default.GetString(destinationArray);
        }
    }
}
