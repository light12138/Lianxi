using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.Model.Interface;
using Autofac.Model.Model;

namespace Autofac.Con.Domain
{
   public class AutoFacModel: Module
    {
        public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MyComponent(c.Resolve<ILogger>()));

            if (ObeySpeedLimit)
            {
                builder.RegisterType<TextLogger>().As<ILogger>();
            }
            else
            {
                builder.RegisterType<SqlLogger>().As<ILogger>();
            }
        }

        
    }
}
