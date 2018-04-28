using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{

    /// <summary>
    /// 表示生成JWT签名的算法。
    /// </summary>
    public interface IJwtAlgorithm
    {

        byte[] Sign(byte[] key, byte[] bytesToSign);



        string Name { get; }

        /// <summary>
        /// 是否对称
        /// </summary>
        bool IsAsymmetric { get; }
    }
}
