using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.Con.Domain
{
    public class ParamComponent
    {
        public ParamComponent(int a)
        {
            A = a;
        }

        public ParamComponent(string a,string b)
        {
            A = a;
            B = b;
        }

        public ParamComponent()
        {

        }

        public object A { get; set; }
        public object B { get; set; }

        public override string ToString()
        {
            return A.ToString();
        }
    }
}
