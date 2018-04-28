using Autofac;
using Autofac.Features.OwnedInstances;
using Autofac.Model.Interface;
using Autofac.Model.Model;
using Bus;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Filter;
using Web.Models;

namespace Web.Controllers
{



    public class HomeController : Controller
    {
        //static ContainerBuilder builder = new ContainerBuilder();
        static IContainer contrainer = null;
        public ActionResult Index()
        {
            //throw new Exception("这个是一个错误");  //508e9fed-d6ce-4673-9789-62f9879e5912

            ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<TextConfigRead>().As<IConfigRead>().SingleInstance(); // 单例模式
            //builder.RegisterType<TextConfigRead>().AsSelf().InstancePerDependency();   //默认的  每一次都会创建一个新的实例
            //contrainer = builder.Build();

            // builder.RegisterType<TextConfigRead>().As<IConfigRead>().InstancePerLifetimeScope(); // 在生命周期中 同一个依赖调用火创建的实例是共享的

            //builder.RegisterType<TextConfigRead>().As<IConfigRead>().InstancePerMatchingLifetimeScope("wss", "wzl"); //在标记的生命周期内是共享的

            //builder.RegisterType<TextConfigRead>().As<IConfigRead>().InstancePerRequest(); //在生命周期中 在一次http请求内共享

            builder.RegisterType<TextConfigRead>().ExternallyOwned();
            
            contrainer = builder.Build();

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            //每一次调用的服务是同一个
            string text = "";
            try
            {
                var read1 = contrainer.Resolve<TextConfigRead>();
                text = read1.Read();

                using (var scope = contrainer.BeginLifetimeScope())
                {
                    var read = contrainer.Resolve<TextConfigRead>();
                    text = read.Read();
                }
                return Content(text);
            }

            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public async Task<ActionResult> Register(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.UserName };
            var userManager = new UserManager<IdentityUser, string>(new UserStore<IdentityUser>(new UserContext()));
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Content("ok");
            }
            return Content("no");
        }



    }
}