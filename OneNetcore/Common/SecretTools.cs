using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class SecretTools
    {

        #region 获取密钥 + GetKey()
        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns>密钥</returns>
        public static string GetKey()
        {
            return "!@#$aIMeILIfe2014SysTem%^*";
        }
        #endregion
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="text">需要加密的文字</param>
        /// <returns>加密后的文字</returns>
        public static string Md5Encrypt(string text)
        {
            return EncryptSecurity.Md5Encrypt(text);
        }

        #region 加密 + Encrypt(string text)
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">需要加密的文字</param>
        /// <returns>加密后的文字</returns>
        public static string Encrypt(string text)
        {
            return EncryptSecurity.Encrypt(text, GetKey());
        }
        #endregion

        #region 解密 + Decrypt(string text)
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">需要解密的文字</param>
        /// <returns>解密后的文字</returns>
        public static string Decrypt(string text)
        {
            return EncryptSecurity.Decrypt(text, GetKey());
        }
        #endregion
    }
}
