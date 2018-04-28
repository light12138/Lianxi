using Wss.DoMain.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jwt.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetToken()
        {
            try
            {
                var token = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret("wss")
                    .AddClaim("Name", "wzl")
                    .AddClaim("exp", DateTimeOffset.Now.AddHours(-1))
                    .Build();
                return Json(new { token });
            }catch(Exception e)
            {
                return Json(e.Message);
            }

        }


        public ActionResult CheckToken()
        {
            var token= Request.Headers["token"];
            try
            {
                var jwtBuilder = new JwtBuilder().WithAlgorithm(new HMACSHAAlgorithmFactory().Create(JwtHashAlgorithm.HS256)).WithVerifySignature(true).WithSecret("wss");
                var date = jwtBuilder.CheckToken(token);
                return Json(new { state = 1, date });
            }catch(Exception ex)
            {
                return Json(new { state = 2, date= ex.Message });
            }
        }



    }
}