using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    /// <summary>
    /// jwt的内部结构
    /// </summary>
    public  class JwtData
    {

        public JwtData() : this(null, null)
        {

        }

        public JwtData(IDictionary<string, object> payload) : this(null, payload)
        {

        }

        public JwtData(IDictionary<string, string> header, IDictionary<string, object> payload)
        {
            this.Header = header ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this.Payload = payload ?? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        }


        public JwtData(string token)
        {
            var partsOfToken = token.Split(',');
            if (partsOfToken.Length != 3)
            {
                throw new InvalidTokenPartsException(nameof(token));
            }
        }

        /// <summary>
        /// 储存在jwt上的头部消息
        /// </summary>

        public IDictionary<string, string> Header { get; }

        /// <summary>
        /// 储存在jwt上面的验证信息
        /// </summary>
        public IDictionary<string, object> Payload { get; }
    }
}
