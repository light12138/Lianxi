using Autofac.Con.Domain;
using Autofac.Core;
using Autofac.Features.Metadata;
using Autofac.Model.Genericity;
using Autofac.Model.Interface;
using Autofac.Model.Model;
using Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace Autofac.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            //autofac
            var builder = new ContainerBuilder();
            #region 反射注入  通过RegisterType 反射去得到对象 在对类型实例化时 会去调用其构造函数，如果有多个构造感觉，会和其中吻合度最高的一个匹配上。
            //// 反射注册  通过RegisterType 反射去得到对象 在对类型实例化时 会去调用其构造函数，如果有多个构造感觉，会和其中吻合度最高的一个匹配上。
            //builder.RegisterType<MyComponent>();
            ////builder.RegisterType<MyComponent>().UsingConstructor(typeof(ILogger),typeof(IConfigRead)); // UsingConstructor() 指定一个构造器要传入的参数
            builder.RegisterType<TextLogger>().As<ILogger>();
            //var container = builder.Build();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var component = scope.Resolve<MyComponent>();
            //    Console.Write(component.Write());

            //}
            #endregion

            #region  lambda注入 builder.Register(c => new A(c.Resolve<B>()));  c代表 IComponentContext的这个内置对象  A B则为有依赖关系的两个类

            var logger = new TextLogger();
            builder.RegisterInstance(logger).AsSelf().ExternallyOwned();
            //配置组建  使实例永远不会被容器处理
            ////lambda注入 builder.Register(c => new A(c.Resolve<B>()));  c代表 IComponentContext的这个内置对象  A B则为有依赖关系的两个类
            //var builder = new ContainerBuilder();
            //builder.Register(com => new MyComponent(com.Resolve<ILogger>()));
            //builder.RegisterType<Logger>().As<ILogger>();
            ////builder.RegisterType<TextLogger>().As<ILogger>();
            ////builder.Register(com => new MyComponent {Date=DateTime.Now }); // 属性注入 当注入的属性为空时 不会抛出异常
            //var container = builder.Build();
            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var component = scope.Resolve<MyComponent>(new NamedParameter("accountId","wzl"));
            //    Console.Write(component.Write());
            //}


            //带参数注册   可以用在工厂模式中
            //var builder = new ContainerBuilder();
            //builder.Register<ParamComponent>(
            //  (c, p) =>
            //  {
            //      var name = p.Named<string>("accountId");
            //      if (name == "wss")
            //      {
            //          return new ParamComponent(111);
            //      }
            //      else
            //      {
            //          return new ParamComponent("222");
            //      }

            //  });
            //var container = builder.Build();
            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var component = scope.Resolve<ParamComponent>(new NamedParameter("accountId", "wzl"));
            //    Console.Write(component.ToString());
            //}
            #endregion

            #region  泛型注入 RegisterGeneric  autofac提供泛型注入 可以作用于仓储模式中 做数据迁移用
            ////泛型注入  autofac提供泛型注入 可以作用于仓储模式中 做数据迁移用
            //builder.RegisterGeneric(typeof(DapperRepository<>))
            //        .As(typeof(IRepository<>))
            //        .InstancePerLifetimeScope();
            //var container= builder.Build();
            //var obj= container.Resolve<IRepository<TextLogger>>();
            //Console.Write(obj.Write()); 
            #endregion

            #region 组件 和 服务
            //// 我们把一个组件公开他的服务， 让后最后通过这个服务去访问
            ////builder.RegisterType<TextLogger>();  // 这个则 公开的服务为他自己本身
            //builder.RegisterType<TextLogger>().As<ILogger>().AsSelf(); // 这个则是指定了一个 ILogger 这个服务(也可以指定多个)， 当我们调用的时候也只能通过这个ILogger 服务去调用  
            //// AsSelf() 表示将自己本身也当作服务
            //var contrainer = builder.Build();
            ////var logger= contrainer.Resolve<TextLogger>();  
            //var logger = contrainer.Resolve<ILogger>(); //调用时 如果这里的服务和注入时 指定的服务不相同的话 这里就会报错。

            ////默认注册  当我们以同一个服务为多个组件公开时，默认的 我们会取最后一次注入的组件为该服务的组件，当想要使某个组件起作用时， 就可以在其他组件注入时 加上PreserveExistingDefaults();
            //builder.RegisterType<TextLogger>().As<ILogger>();
            //builder.RegisterType<Logger>().As<ILogger>();
            //builder.RegisterType<SqlLogger>().As<ILogger>().PreserveExistingDefaults();
            //var contrainer = builder.Build();
            //var logger = contrainer.Resolve<ILogger>();
            //Console.Write(logger.ToString());


            #endregion


            #region 参数传递
            //参数传递
            //builder.RegisterType<MyComponent>().AsSelf().WithParameter(new TypedParameter(typeof(ILogger),new TextLogger()));
            //builder.RegisterType<Logger>().As<ILogger>();
            //var contrainer = builder.Build();
            //var component= contrainer.Resolve<MyComponent>();
            //component.Write(); 
            #endregion

            #region 类型扫描注入   
            // 类型扫描注入         
            //var dataAccess = Assembly.GetAssembly(typeof(ILogger));
            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();  //  在iis中 所有的程序集都会被打包 
            //builder.RegisterAssemblyTypes(dataAccess)
            //.Where(t => t.Name.EndsWith("Logger") || t.Name.StartsWith("Text"))  //筛选 类的名称必须以什么结尾
            ////.Except<SqlLogger>(et => et.As<ILogger>().SingleInstance())               // 筛选  排除哪个类
            //.As(t => t.GetInterfaces()[0]) //获取实现的所有接口
            //.AsImplementedInterfaces();   //从指定的类的接口取做为 组件的服务

            //var contrainer = builder.Build();
            //var looger = contrainer.Resolve<ILogger>(); 
            // Console.Write(looger.Write());
            #endregion

            #region 服务的生成
            ////服务的生成
            //builder.RegisterType<ParamComponent>();
            //var contrainer = builder.Build();

            ////在根容器中 创建一个子容器 在子容器中  该服务只能在子容器中有效
            ////在根容器中创建服务，生命周期和根容器的生命周期一样，容易造成内存泄漏
            //using (var scope = contrainer.BeginLifetimeScope())  
            //{
            //    var logger = scope.Resolve<ParamComponent>(new NamedParameter("a",1));
            //    Console.Write(logger.ToString());
            //} 
            #endregion

            #region 生命周期和控制范围
            //生命周期和控制范围
            //使用生命周期范围
            //using (var scope = contrainer.BeginLifetimeScope())  
            //我们可以从 contrainer容器中创建一个新的生命周期范围

            //将注册添加到生命周期范围
            //using(var scope = container.BeginLifetimeScope(
            //  builder =>
            //  {
            //    builder.RegisterType<Override>().As<IService>();
            //    builder.RegisterModule<MyModule>();
            //  }))

            //标记生命周期范围  

            //  1. singleton 生命周期內一直存在
            //builder.RegisterType<TextConfigRead>().As<IConfigRead>().SingleInstance();

            // 2.InstancePerLifetimeScope   在同一个生命周期中，  每一个依赖创建或者调用的实例是共享的 其子域也是可以共享的，但在不同的生命周期中 实例是唯一的  不共享
            // builder.RegisterType<TextConfigRead>().As<IConfigRead>().InstancePerLifetimeScope();

            //3.InstancePerMatchingLifetimeScope("name"); 在一个被标记为name的生命周期中，每一个依赖创建或者调用的实例是共享的 在打了标记的子生命周期中 父实例也是可以共享的， 如果没有标记或者标记出错 则会抛出一个错误

            //4.InstancePerRequest(name); 专门针对于 web form 和 web mvc 在一次http请求内是共享的 要有标记

            //5. a.InstancePerOwned<b>();  在一个生命周期中 每一个依赖创建或者调用的实例a ， b只能存在与a的实例中

            #endregion



            //模块化封装
            //
            //builder.RegisterType<TextLogger>().AsSelf().As<ILogger>().AutoActivate();  //只需要激活容器时 所创建的组件

            builder.RegisterType<TextLogger>().Named<ILogger>("Logger");
            builder.RegisterType<SqlLogger>().Named<ILogger>("Logger");
            //builder.RegisterAdapter<Meta<IConfigRead>, MyComponent>(cmd => new MyComponent(cmd.Value, (string)cmd.Metadata["Name"]));




            builder.RegisterModule(new AutoFacModel { ObeySpeedLimit = true });



            var contrainer = builder.Build();
            contrainer.TryResolve(out MyComponent trycom);
            var auto = contrainer.Resolve<AutoFacModel>();
            var com = contrainer.Resolve<MyComponent>();

            Console.WriteLine(com.Write());
            Console.ReadKey();
        }
    }
}
