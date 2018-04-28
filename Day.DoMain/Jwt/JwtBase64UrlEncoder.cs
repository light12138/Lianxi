using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public sealed class Base64UrlEncoder  : IBase64UrlEncoder
    {

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"  />
        /// <exception cref="ArgumentOutOfRangeException" />
        public byte[] Decode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException(nameof(input));

            var output = input;
            output = output.Replace('-', '+');
            output = output.Replace('_', '/');
            switch (output.Length % 4)
            {
                case 0:break;
                case 2:
                    output += "==";break;
                case 3:
                    output += "=";break;
                default:
                    throw new FormatException("非法的base64 字符");
            }
            var converted = Convert.FromBase64String(output);
            return converted;
        }

        public string Encode(byte[] input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0];
            output = output.Replace('+', '-');
            output = output.Replace('/','_');
            return output;
        }

        
    }
}
