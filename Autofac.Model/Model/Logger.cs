using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Model
{
    public class Logger  : Interface.ILogger
    {
        public string Write()
        {
            return "这个是logger在写入";
        }
    }
}
