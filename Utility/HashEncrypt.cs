using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.IO;

using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.Utility
{
    public class HashEncrypt : IEncrypt
    {
        private bool isReturnNum;
        private bool isCaseSensitive;
        public HashEncrypt() { }
        /// <summary>
        /// IsCaseSensitive：是否区分大小写。
        /// IsReturnNum:是否返回为加密后字符的Byte代码
        /// </summary>
        /// <param name="IsCaseSensitive">是否区分大小写</param>
        /// <param name="IsReturnNum">是否返回为加密后字符的Byte代码</param>
        public HashEncrypt(bool IsCaseSensitive, bool IsReturnNum)
        {
            this.isReturnNum = IsReturnNum;
            this.isCaseSensitive = IsCaseSensitive;
        }

        #region MD5
        /// <summary>
        /// MD5加密（Add in xueshulin）
        /// </summary>
        /// <param name="pToEncrypt">原码</param>
        /// <param name="sKey">密钥(最大长度8)</param>
        /// <returns></returns>
        public string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            sKey += "12345678";
            sKey = sKey.Substring(0, 8);
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        ///  MD5解密（Add in xueshulin）
        /// </summary>
        /// <param name="pToDecrypt">MD5码</param>
        /// <param name="sKey">密钥(最大长度8)</param>
        /// <returns></returns>
        public string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            sKey += "12345678";
            sKey = sKey.Substring(0, 8);

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region DES
        /// <summary>
        /// 使用DES加密（Add in xueshulin）
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public string DESEncrypt(string originalValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(originalValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct,
            CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());

        }
        /// <summary>
        /// 使用DES解密（Add in xueshulin）
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <param name="IV">m初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //将key和IV处理成8个字符
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();

            byt = Convert.FromBase64String(encryptedValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct,
            CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());

        }

        /// <summary>
        /// 使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns></returns>
        public string Encrypt(int workerId,string originalValue)
        {
            return DESEncrypt(originalValue, "", "");
        }

        /// <summary>
        /// 使用DES解密
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedValue)
        {
            return DESDecrypt(encryptedValue, "", "");
        }

        /// <summary>
        /// 使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns></returns>
        public string DESEncrypt(string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }
        /// <summary>
        /// 使用DES解密（Add in xueshulin）
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns></returns>
        public string DESDecrypt(string encryptedValue, string key)
        {
            return DESDecrypt(encryptedValue, key, key);
        }

        #endregion

        //private string GetStringValue(byte[] Byte)
        //{
        //    string tmpString = "";

        //    if (this.isReturnNum == false)
        //    {
        //        ASCIIEncoding Asc = new ASCIIEncoding();
        //        tmpString = Asc.GetString(Byte);
        //    }
        //    else
        //    {
        //        int iCounter;
        //        for
        //        (iCounter = 0; iCounter < Byte.Length; iCounter++)
        //        {
        //            tmpString = tmpString +
        //            Byte[iCounter].ToString();
        //        }

        //    }
        //    return tmpString;
        //}
        //private byte[] GetKeyByteArray(string strKey)
        //{
        //    ASCIIEncoding Asc = new ASCIIEncoding();
        //    int tmpStrLen = strKey.Length;
        //    byte[] tmpByte = new byte[tmpStrLen - 1];
        //    tmpByte = Asc.GetBytes(strKey);
        //    return tmpByte;
        //}
    }
}
