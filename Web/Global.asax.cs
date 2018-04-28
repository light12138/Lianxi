using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Model.Interface;
using Autofac.Model.Model;
using Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Controllers;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoFacInject();

        }
        private void AutoFacInject()
        {
            var builder = new ContainerBuilder();

            //将mvc控制器注入进去  程序集扫描
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            builder.RegisterType<TextConfigRead>().As<IConfigRead>().InstancePerRequest();
            builder.RegisterType<TextLogger>().As<ILogger>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IBLL).Assembly)
                .Where(c => c.Name.StartsWith("BLL"));


            //builder.RegisterType<HomeController>().InstancePerRequest();


            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // 注册web的抽象类 如同 httpbasecontext  
            builder.RegisterModule<AutofacWebTypesModule>();  //模块注册  可以将  HttpContextBase  HttpRequestBase  HttpResponseBase 等对象注入

            //在视图页面中实现属性注入
            builder.RegisterSource(new ViewRegistrationSource());

            //启用属性注入动作过滤器
            builder.RegisterFilterProvider();       //启用 将属性注册在过滤器中去

            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InjectActionInvoker();  //直接在 action方法里面启用注册


            var contrainer = builder.Build();
            //将依赖关系解析器设置为autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(contrainer));

            

        }
    }
}
