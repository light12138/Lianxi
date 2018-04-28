using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class HMACSHA256Algorithm : IJwtAlgorithm
    {
        public string Name => JwtHashAlgorithm.HS256.ToString();

        public bool IsAsymmetric { get; } = false;

        public byte[] Sign(byte[] key, byte[] bytesToSign)
        {
            using (var sha=new HMACSHA256(key))
            {
                return sha.ComputeHash(bytesToSign);
            }
        }
    }
}
