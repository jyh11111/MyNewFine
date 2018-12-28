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
    public class PartController : ControllerBase
    {
        //
        // GET: /LegoManage/Part/

        private LegoPartApp partApp = new LegoPartApp();
        private SendTransApp sendApp = new SendTransApp();
        private ReceiveTransApp recvApp = new ReceiveTransApp();
         
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = partApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(LegoPartEntity entity, string keyValue)
        {
            partApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = partApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {

            if ((sendApp.FindEntity(t => t.PartId.ToLower() == keyValue.ToLower()) != null) ||
                (recvApp.FindEntity(t => t.PartId.ToLower() == keyValue.ToLower()) != null))
            { return Error("不可以删除此物品，此物品已经交易过!"); }                
            
                partApp.DeleteFrom(keyValue);
            return Success("删除成功。");
            
           
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson()
        {
            var data = partApp.GetList();
            return Content(data.ToJson());
        }

       
        public ActionResult SelectOnePart()
        {
            return View();
        }
       
    }
}
