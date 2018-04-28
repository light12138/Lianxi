using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
    public interface IDateTimeProvider
    {
        DateTimeOffset GetNow();
    }
}
