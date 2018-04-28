using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public interface IJwtDecoder
    {
        #region json DecodeTojson

        /// <summary>
        /// 给定一个 jwt 解码他 并且返回一个json的负载均衡
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string Decode(string token);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key">签名</param>
        /// <param name="verify">是否验证签名  默认是true</param>
        /// <returns></returns>
        string Decode(string token, string key, bool verify);


        string Decode(string token, byte[] key, bool verify);

        #endregion

        #region IDictionary<string, object>  DecodeToObj

        IDictionary<string, object> DecodeToObj(string token);

        IDictionary<string, object> DecodeToObject(string token, string key, bool verify);

        IDictionary<string, object> DecodeToObject(string token, byte[] key, bool verify);

        #endregion


        #region T DecodeToT

        T DecodeToObject<T>(string token);

        T DecodeToObject<T>(string token, string key, bool verify);

        T DecodeToObject<T>(string token, byte[] key, bool verify); 
        #endregion
    }
}
