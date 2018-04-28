using Wss.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain
{
    public class Domain
    {
        public string GetData()
        {
            var user  = new User();
            var list = new Users();
            var name= user.GetDisPlayName("Name");
            //list.List.OrderBy

           // var ret= list.List.OrderByFormWss(u => u.Age);
            return name;
        } 
    }
}
