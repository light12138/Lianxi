using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{

    /// <summary>
    /// 当jwt的token长度不是由三段组成时， 抛出的异常
    /// </summary>
     public class InvalidTokenPartsException:ArgumentOutOfRangeException
    {

        public  InvalidTokenPartsException (string paramName):base(paramName,"Token 不是由三段落组成的")
        {

        }
    }
}
