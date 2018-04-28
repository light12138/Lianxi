using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class JwtEncoder : IJwtEncoder
    {
        private readonly IJwtAlgorithm _algorithm;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IBase64UrlEncoder _urlEncoder;


        public JwtEncoder(IJwtAlgorithm algorithm, IJsonSerializer jsonSerializer, IBase64UrlEncoder urlEncoder)
        {
            _algorithm = algorithm;
            _jsonSerializer = jsonSerializer;
            _urlEncoder = urlEncoder;
        }

        public string Encode(object payload, string key) => Encode(null, payload, key != null ? GetBytes(key) : null);


        public string Encode(object payload, byte[] key) => Encode(null, payload, key);



        public string Encode(IDictionary<string, object> extraHeaders, object payload, string key) => Encode(extraHeaders, payload, GetBytes(key));



        public string Encode(IDictionary<string,object>extraHeaders,object payload,byte[]
            key)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));
            var segments = new List<string>(3);
            var header = extraHeaders!= null ? new Dictionary<string, object>(extraHeaders) : new Dictionary<string, object>();
            header.Add("typ", "Jwt");
            header.Add("alg", _algorithm.Name);
            var headerBytes = GetBytes(_jsonSerializer.Serialize(header));
            var payloadBytes = GetBytes(_jsonSerializer.Serialize(payload));
            segments.Add(_urlEncoder.Encode(headerBytes));
            segments.Add(_urlEncoder.Encode(payloadBytes));
            var stringToSign = string.Join(".", segments.ToArray());
            var bytesToSign = GetBytes(stringToSign);
            var signature = _algorithm.Sign(key, bytesToSign);
            segments.Add(_urlEncoder.Encode(signature));
            return string.Join(".", segments.ToArray());
        }
       

       


        private static byte[] GetBytes(string input) => Encoding.UTF8.GetBytes(input);
    }
}
