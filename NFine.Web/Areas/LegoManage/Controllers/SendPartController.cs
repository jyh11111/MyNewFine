using NFine.Application.LegoManage;
using NFine.Code;
using NFine.Domain.Entity.LegoManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.LegoManage.Controllers
{
    public class SendPartController : ControllerBase
    {
        //
        // GET: /LegoManage/SendPart/
        private SendTransApp sendApp = new SendTransApp();
         
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = sendApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        //得到未被接收过的记录且接收部门等于自已部门的
        public ActionResult GetGridJson2(Pagination pagination)
        {
            var data = new
            {
                rows = sendApp.GetList2(pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SendTransEntity entity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue)) //新增则先检查一下柜子
            {
                if (!sendApp.CheckSubmit(entity)) //失败
                    return Error("此位置没有此实物，或者数量小于要发送的数量");
            }
            sendApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = sendApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            
                sendApp.DeleteFrom(keyValue);
                return Success("删除成功。");
          
        }
        [HttpPost]
        //[HandlerAuthorize]
        [HandlerAjaxOnly]       
        public ActionResult Received(string keyValue)
        {
            sendApp.changeRecived(keyValue,true);
            return Success("接收成功");

        }

        public ActionResult SelectSend()
        {
            return View();
        }
    }
}
