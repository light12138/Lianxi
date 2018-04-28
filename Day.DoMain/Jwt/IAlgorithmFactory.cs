using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public  interface IAlgorithmFactory
    {

        /// <summary>
        /// 使用提供的算法名创建一个算法工厂。
        /// </summary>
        /// <param name="algorithmName"></param>
        /// <returns></returns>
        IJwtAlgorithm Create(string algorithmName);



        IJwtAlgorithm Create(JwtHashAlgorithm algorithm);
    }
}
