using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class JwtValidator:IJwtValidator
    {
        private readonly IJsonSerializer _jsonSerializer;

        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtValidator(IJsonSerializer jsonSerializer,IDateTimeProvider dateTimeProvider )
        {
            _jsonSerializer = jsonSerializer;
            _dateTimeProvider = dateTimeProvider;


        }

        public void Validate(string payloadJson,string decodedCrypto,string decodedSignature)
        {
            if (string.IsNullOrWhiteSpace(payloadJson))
                throw new ArgumentException(nameof(payloadJson));

            if (string.IsNullOrWhiteSpace(decodedCrypto))
                throw new ArgumentException(nameof(decodedCrypto));

            if (string.IsNullOrWhiteSpace(decodedSignature))
                throw new ArgumentException(nameof(decodedSignature));

            if (!CompareCryptoWithSignature(decodedCrypto, decodedSignature))
            {
                throw new SignatureVerificationException("Invalid signature")
                {
                    Expected = decodedCrypto,
                    Received = decodedSignature
                };
            }

            var payload = _jsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);
            var now = _dateTimeProvider.GetNow();
            var sercondsSinceEpoch = UnixEpoch.GetSecondsSince(now);

        }



        /// <summary>
        /// 验证实验的要求。
        /// </summary>
        /// <param name="payloadData"></param>
        /// <param name="secondsSinceEpoch"></param>
        private static void ValidateExpClaim(IDictionary<string,object>payloadData,double secondsSinceEpoch)
        {
            if (!payloadData.TryGetValue("exp", out var expObj))
                return;
            if (expObj == null)
            {
                throw new SignatureVerificationException("Claim 'exp' must be a  number");
            }
            double expValue;
            try
            {
                expValue = Convert.ToDouble(expObj);
            }
            catch
            {
                throw new SignatureVerificationException("Claim 'exp' must be a  number");
            }

            if (secondsSinceEpoch >= expValue)
            {
                throw new TokenExpiredException("Token has expired.")
                {
                    Expiration = UnixEpoch.Value.AddSeconds(expValue),
                    PayloadData = payloadData
                };
            }
        }



        private static void ValidateNbfClaim(IDictionary<string,object>payloadData,double secondsSinceEpoch)
        {
            if (!payloadData.TryGetValue("nbf", out var nbfObj))
                return;

            if (nbfObj == null)
                throw new SignatureVerificationException("Claim 'nbf' must be a number.");

            double nbfValue;
            try
            {
                nbfValue = Convert.ToDouble(nbfObj);
            }
            catch
            {
                throw new SignatureVerificationException("Claim 'nbf' must be a number.");
            }

            if (secondsSinceEpoch < nbfValue)
            {
                throw new SignatureVerificationException("Token is not yet valid.");
            }
        }



        private static bool CompareCryptoWithSignature(string decodedCrypto,string decodedSignature)
        {
            if (decodedCrypto.Length != decodedSignature.Length)
                return false;
            var decodedCryptoBytes = Getbytes(decodedCrypto);
            var decodedSignatureBytes = Getbytes(decodedSignature);
            byte result = 0;
            for (int i = 0; i < decodedCrypto.Length; i++)
            {
                result |= (byte)(decodedCryptoBytes[i] ^ decodedSignatureBytes[i]);
            }

            return result == 0;
        }



        public static byte[] Getbytes(string input) => Encoding.ASCII.GetBytes(input);

    }
}
