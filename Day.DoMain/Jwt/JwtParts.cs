using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public  class JwtParts
    {

        public string[] Parts { get;  }
        public JwtParts (string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(nameof(token));
            }

            var parts = token.Split('.');
            if (parts.Length != 3)
                throw new InvalidTokenPartsException(nameof(token));
            this.Parts = parts;            
        }

        public JwtParts(string[] parts)
        {
            if (parts == null)
                throw new ArgumentNullException(nameof(parts));
            if (parts.Length != 3)
                throw new InvalidTokenPartsException(nameof(parts));

            this.Parts = parts;
        }

        public string Header => this.Parts[(int)JwtPartsIndex.Header];

        public string Payload => this.Parts[(int)JwtPartsIndex.Payload];

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature => this.Parts[(int)JwtPartsIndex.Signature];
    }
}
