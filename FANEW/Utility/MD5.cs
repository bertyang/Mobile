using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.Utility
{
    public class MD5 : IEncrypt
    {
        public string Encrypt(int workerId,string s)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(workerId+s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();  

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }

        public string Decrypt(string s)
        {
            return "a";
        }
    }
}
