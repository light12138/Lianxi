using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public interface IJwtValidator
    {
        /// <summary>
        /// 验证Jwt的签名是否正确         
        /// </summary>
        /// <param name="payloadJson"></param>
        /// <param name="decodedCrypto"></param>
        /// <param name="decodedSignature"></param>
        /// 不正确 异常抛出
        /// SignatureVerificationException 验证不通过，  TokenExpiredException 令牌过期
        void Validate(string payloadJson, string decodedCrypto, string decodedSignature);



    }
}
