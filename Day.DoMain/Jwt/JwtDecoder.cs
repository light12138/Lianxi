using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class JwtDecoder : IJwtDecoder
    {

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IJwtValidator _jwtValidator;
        private readonly IBase64UrlEncoder _urlEncoder;
        private readonly IAlgorithmFactory _algFactory;

        public JwtDecoder(IJsonSerializer jsonSerializer, IJwtValidator jwtValidator, IBase64UrlEncoder urlEncoder)
            : this(jsonSerializer, jwtValidator, urlEncoder, new HMACSHAAlgorithmFactory())
        {
        }
        public JwtDecoder(IJsonSerializer jsonSerializer, IJwtValidator jwtValidator, IBase64UrlEncoder urlEncoder, IAlgorithmFactory algFactory)
        {
            _jsonSerializer = jsonSerializer;
            _jwtValidator = jwtValidator;
            _urlEncoder = urlEncoder;
            _algFactory = algFactory;
        }

        /// <summary>
        /// 将整个token传入 返回 payload的 解码信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Decode(string token)
        {
            var payload = new JwtParts(token).Payload;
            var decoded = _urlEncoder.Decode(payload);
            return GetString(decoded);
        }

        public string Decode(string token, string key, bool verify) => Decode(token, GetBytes(key), verify);

        public string Decode(string token, byte[] key, bool verify)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException(nameof(token));
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(key));
            }
            if (verify)
            {
                Validate(new JwtParts(token), key);
            }
            return Decode(token);
        }

        public IDictionary<string, object> DecodeToObj(string token) => DecodeToObject<Dictionary<string, object>>(token);

        public IDictionary<string, object> DecodeToObject(string token, string key, bool verify) => DecodeToObject(token, GetBytes(key), verify);

        public IDictionary<string, object> DecodeToObject(string token, byte[] key, bool verify) => DecodeToObject<Dictionary<string, object>>(token, key, verify);

        public T DecodeToObject<T>(string token)
        {
            var payload = Decode(token);
            return _jsonSerializer.Deserialize<T>(payload);
        }

        public T DecodeToObject<T>(string token, string key, bool verify) => DecodeToObject<T>(token, GetBytes(key), verify);

        public T DecodeToObject<T>(string token, byte[] key, bool verify)
        {
            var payload = Decode(token, key, verify);
            return _jsonSerializer.Deserialize<T>(payload);
        }
        public void Validate(string[] parts, byte[] key) => Validate(new JwtParts(parts), key);

        public void Validate(JwtParts jwt,byte[] key)
        {
            if (jwt == null)
                throw new ArgumentNullException(nameof(jwt));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (key.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(key));

            var crypto = _urlEncoder.Decode(jwt.Signature);
            var decodedCrypto = Convert.ToBase64String(crypto);
            var headerJson = GetString(_urlEncoder.Decode(jwt.Header));
            var headerData = _jsonSerializer.Deserialize<Dictionary<string, object>>(headerJson);

            var payload = jwt.Payload;
            var payloadJson = GetString(_urlEncoder.Decode(payload));
            var bytesToSign = GetBytes(string.Concat(jwt.Header, ".", payload));
            var algName = (string)headerData["alg"];
            var alg = _algFactory.Create(algName);

            var signatureData = alg.Sign(key, bytesToSign);
            var decodedSignature = Convert.ToBase64String(signatureData);

            _jwtValidator.Validate(payloadJson, decodedCrypto, decodedSignature);
        }



        private static byte[] GetBytes(string input) => Encoding.UTF8.GetBytes(input);
        private static string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }
}
