using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public interface IBase64UrlEncoder
    {

        /// <summary>
        /// 将byte[]转化成 base64 的string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Encode(byte[] input);


        /// <summary>
        ///  Decode the base64 string to a byte array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        byte[] Decode(string input);
    }
}
