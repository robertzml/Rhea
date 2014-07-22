using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Rhea.Common
{
    /// <summary>
    /// 散列算法类
    /// </summary>
    public class Hasher
    {
        #region Method
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="source">明文</param>
        /// <returns>密文</returns>
        public static string SHA1Encrypt(string source)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] result = sha.ComputeHash(Encoding.Default.GetBytes(source));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">明文</param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string source)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion //Method
    }
}
