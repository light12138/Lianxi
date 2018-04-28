using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Model
{
    public class ConfigRead : IConfigRead
    {
        public string Read()
        {
            return "这是ConfigRead在读取";
        }
    }
}
