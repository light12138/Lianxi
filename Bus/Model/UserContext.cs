using Bus.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus
{
    public class UserContext:IdentityDbContext<IdentityUser>
    {

        public UserContext():base("name=DefaultConntext")
        {
           
        }


        public DbSet<Customer> Customers  { get; set; }
        
    }
}
