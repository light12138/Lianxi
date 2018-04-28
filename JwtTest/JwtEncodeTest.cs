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

   
    public  class JwtEncodeTest
    {

        string token = "eyJ0eXAiOiJKd3QiLCJhbGciOiJIUzI1NiJ9.eyJOYW1lIjoid3NzIiwiU2V4Ijoi55S3IiwiQWdlIjoxOX0.1TZ_E73jqc59NCdhNssHq8sIXhVjtTTFn1aGuyIm-40";
        string secret = "wss";

        string token_head = "eyJhdXRob3IiOiJ3c3MiLCJ0eXAiOiJKd3QiLCJhbGciOiJIUzI1NiJ9.eyJOYW1lIjoid3NzIiwiU2V4Ijoi55S3IiwiQWdlIjoxOX0.0Aa4MLAB3QKriD0qCV1CyvHMXK6Yjf24_Lz--yf6FEI";

        [Fact]
        public void Encode_GetToken()
        {   
            var encoder = new JwtEncoder(new HMACSHAAlgorithmFactory().Create(JwtHashAlgorithm.HS256), new JsonNetSerializer(), new Base64UrlEncoder());
            var actual = encoder.Encode(TestData.user, "wzl");
            Assert.Equal(actual,token);
        }
        [Fact]
        public void Encode_GetToken_WithHeaders()
        {
            var encoder = new JwtEncoder(new HMACSHAAlgorithmFactory().Create(JwtHashAlgorithm.HS256), new JsonNetSerializer(), new Base64UrlEncoder());
            var extraHeaders  = new Dictionary<string, object> { { "author", "wss" } };
            var actual = encoder.Encode(extraHeaders, TestData.user, secret);

            Assert.NotEmpty(actual);
        }

    }
}
