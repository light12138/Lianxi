using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class HMACSHAAlgorithmFactory : IAlgorithmFactory
    {

        
        public IJwtAlgorithm Create(string algorithmName)
        {
            return Create((JwtHashAlgorithm)Enum.Parse(typeof(JwtHashAlgorithm), algorithmName));
        }

        public IJwtAlgorithm Create(JwtHashAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case JwtHashAlgorithm.HS256:
                    return new HMACSHA256Algorithm();
                case JwtHashAlgorithm.HS384:
                    return new HMACSHA384Algorithm();
                case JwtHashAlgorithm.HS512:
                    return new HMACSHA512Algorithm();
                case JwtHashAlgorithm.RS256:
                    throw new NotSupportedException($"For algorithm {nameof(JwtHashAlgorithm.RS256)} please create custom factory by implementing {nameof(IAlgorithmFactory)}");
                default:
                    throw new NotSupportedException($"Algorithm {algorithm} is not supported.");
            }
        }
    }
}
