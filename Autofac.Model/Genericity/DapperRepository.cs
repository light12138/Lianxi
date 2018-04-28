using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Genericity
{
    public class DapperRepository<T> : IRepository<T> where T:class
    {
        public string Write()
        {
            return "dapper处理";
        }
    }
}
