using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    public interface IEncrypt
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="originalValue"></param>
        /// <returns></returns>
        string Encrypt(int WorkerId,string originalValue);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        string Decrypt(string encryptedValue);

    }
}
