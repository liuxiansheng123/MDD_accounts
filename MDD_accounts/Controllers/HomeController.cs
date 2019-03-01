using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDD_BLL;
using MDD_Model;
using Newtonsoft.Json;

namespace MDD_accounts.Controllers
{
    public class HomeController : Controller
    {
        private MDD_accountsBll accountsBll = new MDD_accountsBll();
        private MDD_dictionariesbll dictionariesbll = new MDD_dictionariesbll();

        private JsonSerializer serializer = new JsonSerializer();
        private StringWriter sw = new StringWriter();

        /// <summary>
        /// 列表显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<MDD_accountsM> _Accounts = new List<MDD_accountsM>();
            _Accounts = accountsBll.GetModelList("");
            ViewData["data"] = _Accounts;
            return View();
        }

        public ActionResult Addaction()
        {
            return View();
        }

    }
}