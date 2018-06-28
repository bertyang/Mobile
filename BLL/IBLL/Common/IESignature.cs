using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.IBLL
{
    /// <summary>
    /// 电子签名接口
    /// 吴鹏飞
    /// </summary>
    public interface IESignature
    {
        /// <summary>
        /// 生成数字摘要
        /// </summary>
        /// <param name="source">源数据</param>
        /// <returns></returns>
        string GetStrDigest(string source);
        /// <summary>
        /// 生成数字签名
        /// </summary>
        /// <param name="strDigest">数字摘要</param>
        /// <returns></returns>
        string GetStrSignData(string strDigest);
        /// <summary>
        /// 返回签名的数字证书
        /// </summary>
        /// <returns></returns>
        string GetStrSignCert();
        /// <summary>
        /// 验证数字签名
        /// </summary>
        /// <param name="strDigest">数字摘要</param>
        /// <param name="strSign">数字签名</param>
        /// <param name="strCert">数字证书</param>
        /// <returns></returns>
        bool VerifySignData(string strDigest, string strSign, string strCert);
    }
}
