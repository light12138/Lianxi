using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public interface IJwtEncoder
    {

        /// <summary>
        /// 给定一个 payload 和key    编码
        /// /// </summary>
        /// <param name="payload">信息</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        string Encode(object payload, string key);

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Encode(object payload, byte[] key);


        /// <summary>
        /// 创建一个JWT，给定一组任意额外的头、有效负载、签名键和使用的算法。
        /// </summary>
        /// <param name="extraHeaders"></param>
        /// <param name="payload"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string Encode(IDictionary<string, object> extraHeaders, object payload, string key);


        string Encode(IDictionary<string, object> extraHeaders, object payload, byte[] key);
    }
}
