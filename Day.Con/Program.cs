using Wss.DoMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            var ret = new Domain().GetData();
            Console.WriteLine(ret);
            Console.ReadKey();
        }
    }
}
