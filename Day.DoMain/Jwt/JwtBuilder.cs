using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class JwtBuilder
    {

        private readonly JwtData _jwt = new JwtData();

        private IJwtEncoder _encoder;

        private IJwtDecoder _decoder;

        private IJwtValidator _validator;

        private IJsonSerializer _serialize = new JsonNetSerializer();

        private IBase64UrlEncoder _urlEncoder = new Base64UrlEncoder();

        private IJwtAlgorithm _algorithm;

        private string _secret;

        private bool _verify;

        #region 像 Jwt中 添加 头部 和权限信息

        public JwtBuilder AddHeader(HeaderName name, string value)
        {
            _jwt.Header.Add(name.GetHeaderName(), value);
            return this;
        }

        public JwtBuilder AddClaim(string name, object value)
        {
            _jwt.Payload.Add(name, value);
            return this;
        }
        public JwtBuilder AddClaim(string name, string value) => AddClaim(name, (object)value);


        public JwtBuilder AddClaim (ClaimName name, string value) => AddClaim(name.GetPublicClaimName(), value);


        public JwtBuilder AddClaim(ClaimName name, object value) => AddClaim(name.GetPublicClaimName(), value);

        #endregion

        /// <summary>
        /// 键入一个json的处理对象
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public JwtBuilder WithSerializer(IJsonSerializer serializer)
        {
            _serialize = serializer;
            return this;
        }


        /// <summary>
        /// 键入一个加密的对象
        /// </summary>
        /// <param name="encoder"></param>
        /// <returns></returns>
       public JwtBuilder WithEncoder(IJwtEncoder encoder)
        {
            _encoder = encoder;
            return this;
        }


        /// <summary>
        /// 键入一个解密的对象
        /// </summary>
        /// <param name="decoder"></param>
        /// <returns></returns>
        public  JwtBuilder WithDecoder(IJwtDecoder decoder)
        {
            _decoder = decoder;
            return this;
        }

        /// <summary>
        /// 键入一个签名验证的对象
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        public  JwtBuilder WithValidator(IJwtValidator validator)
        {
            _validator = validator;
            return this;
        }

        /// <summary>
        /// 键入一个base64的对象
        /// </summary>
        /// <param name="urlEncoder"></param>
        /// <returns></returns>
        public JwtBuilder WithUrlEncoder(IBase64UrlEncoder urlEncoder)
        {
            _urlEncoder = urlEncoder;
            return this;
        }

        /// <summary>
        /// 键入一个 生成算法签名的对象
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public JwtBuilder WithAlgorithm(IJwtAlgorithm algorithm)
        {
            _algorithm = algorithm;
            return this;
        }


        /// <summary>
        /// 键入一个 密钥
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        public JwtBuilder WithSecret(string secret)
        {
            _secret = secret;
            return this;
        }

        /// <summary>
        ///  判断  设置verify 的值   verify  是否要验证签名
        /// </summary>
        /// <returns></returns>
        public JwtBuilder MustVerifySignature() => WithVerifySignature(true);

        public JwtBuilder DoNotVerifySignature() => WithVerifySignature(false);
        public JwtBuilder WithVerifySignature(bool verify)
        {
            _verify = verify;
            return this;
        }

        public string Build()
        {
            if (_encoder == null)
            {
                TryCreateEncoder();
            }
            EnsureCanBuild();
            return _encoder.Encode(_jwt.Payload, _secret);

        }


        public string CheckToken(string token)
        {
            if (_decoder == null)
            {
                TryCreateDecoder();
            }
            EnsureCanBuild();
            return _decoder.Decode(token, _secret,_verify);
        }



        /// <summary>
        /// 将创建的编码的对象实例化
        /// </summary>
        private void TryCreateEncoder()
        {
            if (_algorithm == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtEncoder)}. Call {nameof(WithAlgorithm)}.");
            if (_serialize == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtEncoder)}. Call {nameof(WithSerializer)}");
            if (_urlEncoder == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtEncoder)}. Call {nameof(WithUrlEncoder)}.");

            _encoder = new JwtEncoder(_algorithm, _serialize, _urlEncoder);
        }


        private void TryCreateDecoder()
        {
            if (_algorithm == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtDecoder)}. Call {nameof(WithAlgorithm)}.");
            if (_serialize == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtDecoder)}. Call {nameof(WithSerializer)}");
            if (_urlEncoder == null)
                throw new InvalidOperationException($"Can't instantiate {nameof(JwtDecoder)}. Call {nameof(WithUrlEncoder)}.");
            _decoder = new JwtDecoder(_serialize, new JwtValidator(_serialize,new UtcDateTimeProvider()), _urlEncoder, new HMACSHAAlgorithmFactory());
        }




        private void EnsureCanBuild()
        {
            if (!CanBuild())
                throw new InvalidOperationException("Can't build a token. Check if you have call all of the followng methods:\r\n" +
                                                    $"-{nameof(WithAlgorithm)}\r\n" +
                                                    $"-{nameof(WithSerializer)}\r\n" +
                                                    $"-{nameof(WithUrlEncoder)}.");
        }

        /// <summary>
        /// 检查是否有为空的对象 
        /// </summary>
        /// <returns></returns>
        private bool CanBuild()
        {
            return _algorithm != null &&
                   _serialize != null &&
                   _urlEncoder != null &&
                   _jwt.Payload != null &&
                   _algorithm.IsAsymmetric || !String.IsNullOrEmpty(_secret);
        }




    }
}
