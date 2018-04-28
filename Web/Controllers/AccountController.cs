using Autofac.Model.Interface;
using Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ModelBuilder;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController() { }

        public AccountController (ILogger logger,BLLAccount bLLAccount,BLLHome bLLHome)
        {
            _logger = logger;
            _bll_account = bLLAccount;
            _bll_home = bLLHome;
        }
        private ILogger _logger { get; set; }

        private BLLAccount _bll_account  { get; set; }

        private BLLHome _bll_home { get; set; }
        private Guid guid { get; set; }
        // GET: Account
        public ActionResult Index(IConfigRead read)
        {
           // var aa = book.BindModel();

            guid = Guid.NewGuid();
            var text = _logger.Write();
            return Content(text + read.Read());

        }

        public ActionResult Wirte()
        {
            var text_home = _bll_home.Read();
            var text_account = _bll_account.Read();
            return Content(text_home + text_account);
        }


    }
}