using System;
using System.Security.Cryptography;
using System.Text;

namespace AiXiu.Common
{
    /// <summary>
    /// 哈希散列加密类
    /// </summary>
    public class SHAEncryption
    {
        /// <summary>
        /// 散列加密（不可逆），加密失败时返回源字符串
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <returns>返回40个字符</returns>
        public string SHA1Encrypt(string input)
        {
            // 字符串为空
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            // 尝试加密
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            string ciphertext;
            try
            {
                byte[] cipherByte;
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    cipherByte = sha1.ComputeHash(byteArray);
                }
                ciphertext = Convert.ToBase64String(cipherByte);
            }
            catch (Exception ex)
            {
                LogHelper.Error("数据加密失败", ex);
                return input;
            }
            // 返回
            return ciphertext;
        }
    }
}