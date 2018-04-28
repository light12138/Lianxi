using Autofac.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Model.Model
{
    public class TextLogger : ILogger
    {

        public TextLogger()
        {
            Console.WriteLine("TextLogger已经被创建");
        }
        public string Write()
        {
            return "这是一个TextLogger在写入";
        }
    }
}
