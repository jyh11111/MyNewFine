/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        private UserLogOnApp userLogOnApp = new UserLogOnApp();
        [HttpGet]
        public ActionResult Index()
        {
            if (OperatorProvider.Provider.GetCurrent() == null)
                return Redirect("/Login/Index");
            return View();
        }
        [HttpGet]
        public ActionResult Default()
        {
            
               
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]       
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "重置密码成功。" }.ToJson());
        }
    }
}
