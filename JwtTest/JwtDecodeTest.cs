using Wss.DoMain.Jwt;
using Wss.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JwtTest
{
    public  class JwtDecodeTest
    {
        string token = "eyJ0eXAiOiJKd3QiLCJhbGciOiJIUzI1NiJ9.eyJOYW1lIjoid3NzIiwiU2V4Ijoi55S3IiwiQWdlIjoxOX0.1TZ_E73jqc59NCdhNssHq8sIXhVjtTTFn1aGuyIm-40";
        string secret = "wss";

        string token_head = "eyJhdXRob3IiOiJ3c3MiLCJ0eXAiOiJKd3QiLCJhbGciOiJIUzI1NiJ9.eyJOYW1lIjoid3NzIiwiU2V4Ijoi55S3IiwiQWdlIjoxOX0.0Aa4MLAB3QKriD0qCV1CyvHMXK6Yjf24_Lz--yf6FEI";

        [Fact]
        public void Decode_Toket_To_Json()
        {
            var serialize = new JsonNetSerializer();
            var decoder = new JwtDecoder(serialize, new JwtValidator(serialize,new UtcDateTimeProvider()), new Base64UrlEncoder());
            var expected = serialize.Serialize(TestData.user);
            var actual = "";
            try
            {
                actual = decoder.Decode(token, "light", verify: false);
            }catch(SignatureVerificationException ex)
            {
                var aa = ex;
            }
            Assert.Equal(actual, expected);
        }

        [Fact]
        public  void Decode_Token_To_Obj()
        {
            var decoder = new JwtDecoder(new JsonNetSerializer(), null, new Base64UrlEncoder());
            Func<User> func = () => decoder.DecodeToObject<User>(token, secret, false);
            var user = func();
            Assert.NotNull(user);
        }

        [Fact]
        public void Decode_To_Encode()
        {
            var serialize = new JsonNetSerializer();
            
            var validTor = new JwtValidator(serialize,new UtcDateTimeProvider());
            var urlEncoder = new Base64UrlEncoder();
            var encoder = new JwtEncoder(new HMACSHA256Algorithm(), serialize, urlEncoder);
            var decoder  = new JwtDecoder(serialize,validTor,urlEncoder,new HMACSHAAlgorithmFactory());
            var token = encoder.Encode(new { exp = "WSS" }, "wss");
            var ret = decoder.Decode(token, "wss", true);

            Assert.NotEmpty(ret);
        }

        [Fact]
        public void Decode_To_Encode_Expired()
        {
            var use = new User() { Name="123"};
            var serialize = new JsonNetSerializer();
            var dateTimeProvider = new UtcDateTimeProvider();
            var validTor = new JwtValidator(serialize, dateTimeProvider);
            var urlEncoder = new Base64UrlEncoder();
            var endocer = new JwtEncoder(new HMACSHA256Algorithm(), serialize, urlEncoder);
            var decoder = new JwtDecoder(serialize, validTor, urlEncoder, new HMACSHAAlgorithmFactory());

            var now = dateTimeProvider.GetNow();
            var exp = UnixEpoch.GetSecondsSince(now.AddHours(-1));          
            var token = endocer.Encode(new { exp }, "wss");
            var actual = decoder.Decode(token, "wss", true);
            var name = nameof(User.Name);

            Assert.NotEmpty(name);
        }


    }
}
