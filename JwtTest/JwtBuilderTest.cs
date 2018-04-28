using Wss.DoMain.Jwt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JwtTest
{
    public class JwtBuilderTest
    {

        string secret = "wss";

        string _token = "eyJ0eXAiOiJKd3QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOiIwMy8xNS8yMDE4IDEwOjExOjU1In0.knakr_Ni4sQO2eSbCGFofM9Ue1Rpwe7DH0pt79E0FEg";


        [Fact]
        public void Builder_Encoder()
        {
            var testtime = DateTime.Now.AddHours(-1).ToString(CultureInfo.InvariantCulture);
            var token  = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
               .WithSecret(secret)
               .AddClaim(ClaimName.ExpirationTime, testtime)
               .Build();

            var date1 = DateTimeOffset.Now;
            var date2 = date1.ToUniversalTime();
            Assert.Equal(date1, date2 );
        }
    }
}
