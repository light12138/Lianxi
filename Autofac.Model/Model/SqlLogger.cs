using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Model
{
    public class SqlLogger : ILogger
    {
        public string Write()
        {
            return "这个一个sql在写入";
        }
    }
}
