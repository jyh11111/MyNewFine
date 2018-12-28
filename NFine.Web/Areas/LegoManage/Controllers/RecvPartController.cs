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
    public class RecvPartController : ControllerBase
    {
        //
        // GET: /LegoManage/RecvPart/

        private ReceiveTransApp recvApp = new ReceiveTransApp();
        private SendTransApp sendApp = new SendTransApp();
    
        
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = recvApp.GetList(pagination, keyword)  ,
                
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ReceiveTransEntity entity, string keyValue)
        {
            if ( !string.IsNullOrWhiteSpace(entity.SendTransId))
            {
                sendApp.changeRecived(entity.SendTransId, true);
            }
            recvApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = recvApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {   //1.先清除SendTrans表中 Recived标识
            var entity = recvApp.GetForm(keyValue);
            if (entity != null && !string.IsNullOrWhiteSpace(entity.SendTransId))
            {                 
                sendApp.changeRecived(entity.SendTransId, false);
            }
            //2.再删除
            recvApp.DeleteFrom(keyValue);
            return Success("删除成功。");

        }


    }
}
