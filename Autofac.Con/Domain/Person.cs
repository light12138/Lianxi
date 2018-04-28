using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Con
{

    public class Person : IPerson
    {

        public string Name = "哈哈";
        public string Get()
        {
            return "这是一个Person"+Name;
        }
    }

    public class User : IPerson
    {
        public string Get()
        {
            return "这是一个User";
        }
    }

    public class Stu : IPerson
    {
        public string Get()
        {
            return "这是一个Stu";
        }
    }


}
